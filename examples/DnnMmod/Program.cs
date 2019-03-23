/*
 * This sample program is ported by C# from examples\dnn_mmod_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;
using DlibDotNet.ImageTransforms;

namespace DnnMmod
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            try
            {
                // In this example we are going to train a face detector based on the
                // small faces dataset in the examples/faces directory.  So the first
                // thing we do is load that dataset.  This means you need to supply the
                // path to this faces folder as a command line argument so we will know
                // where it is.
                if (args.Length != 1)
                {
                    Console.WriteLine("Give the path to the examples/faces directory as the argument to this");
                    Console.WriteLine("program.  For example, if you are in the examples folder then execute ");
                    Console.WriteLine("this program by running: ");
                    Console.WriteLine("   ./dnn_mmod_ex faces");
                    return;
                }

                var facesDirectory = args[0];

                // The faces directory contains a training dataset and a separate
                // testing dataset.  The training data consists of 4 images, each
                // annotated with rectangles that bound each human face.  The idea is 
                // to use this training data to learn to identify human faces in new
                // images.  
                // 
                // Once you have trained an object detector it is always important to
                // test it on data it wasn't trained on.  Therefore, we will also load
                // a separate testing set of 5 images.  Once we have a face detector
                // created from the training data we will see how well it works by
                // running it on the testing images. 
                // 
                // So here we create the variables that will hold our dataset.
                // images_train will hold the 4 training images and face_boxes_train
                // holds the locations of the faces in the training images.  So for
                // example, the image images_train[0] has the faces given by the
                // rectangles in face_boxes_train[0].
                IEnumerable<Matrix<RgbPixel>> imagesTrain;
                IEnumerable<Matrix<RgbPixel>> imagesTest;
                IEnumerable<IEnumerable<MModRect>> faceBoxesTrain;
                IEnumerable<IEnumerable<MModRect>> faceBoxesTest;

                // Now we load the data.  These XML files list the images in each dataset
                // and also contain the positions of the face boxes.  Obviously you can use
                // any kind of input format you like so long as you store the data into
                // images_train and face_boxes_train.  But for convenience dlib comes with
                // tools for creating and loading XML image datasets.  Here you see how to
                // load the data.  To create the XML files you can use the imglab tool which
                // can be found in the tools/imglab folder.  It is a simple graphical tool
                // for labeling objects in images with boxes.  To see how to use it read the
                // tools/imglab/README.txt file.
                Dlib.LoadImageDataset(facesDirectory + "/training.xml", out imagesTrain, out faceBoxesTrain);
                Dlib.LoadImageDataset(facesDirectory + "/testing.xml", out imagesTest, out faceBoxesTest);

                Console.WriteLine($"num training images: {imagesTrain.Count()}");
                Console.WriteLine($"num testing images:  {imagesTest.Count()}");


                // The MMOD algorithm has some options you can set to control its behavior.  However,
                // you can also call the constructor with your training annotations and a "target
                // object size" and it will automatically configure itself in a reasonable way for your
                // problem.  Here we are saying that faces are still recognizably faces when they are
                // 40x40 pixels in size.  You should generally pick the smallest size where this is
                // true.  Based on this information the mmod_options constructor will automatically
                // pick a good sliding window width and height.  It will also automatically set the
                // non-max-suppression parameters to something reasonable.  For further details see the
                // mmod_options documentation.
                using (var options = new MModOptions(faceBoxesTrain, 40, 40))
                {
                    // The detector will automatically decide to use multiple sliding windows if needed.
                    // For the face data, only one is needed however.
                    var detectorWindows = options.DetectorWindows.ToArray();
                    Console.WriteLine($"num detector windows: {detectorWindows.Length}");
                    foreach (var w in detectorWindows)
                        Console.WriteLine($"detector window width by height: {w.Width} x {w.Height}");

                    Console.WriteLine($"overlap NMS IOU thresh:             {options.OverlapsNms.GetIouThresh()}");
                    Console.WriteLine($"overlap NMS percent covered thresh: {options.OverlapsNms.GetPercentCoveredThresh()}");

                    // Now we are ready to create our network and trainer.
                    using (var net = new LossMmod(options, 2))
                    {
                        // The MMOD loss requires that the number of filters in the final network layer equal
                        // options.detector_windows.size().  So we set that here as well.
                        using (var subnet = net.GetSubnet())
                        using (var details = subnet.GetLayerDetails())
                        {
                            details.SetNumFilters(detectorWindows.Length);
                            using (var trainer = new DnnTrainer<LossMmod>(net))
                            {
                                trainer.SetLearningRate(0.1);
                                trainer.BeVerbose();
                                trainer.SetSynchronizationFile("mmod_sync", 5 * 60);
                                trainer.SetIterationsWithoutProgressThreshold(300);

                                // Now let's train the network.  We are going to use mini-batches of 150
                                // images.   The images are random crops from our training set (see
                                // random_cropper_ex.cpp for a discussion of the random_cropper). 
                                IEnumerable<Matrix<RgbPixel>> miniBatchSamples;
                                //IEnumerable<IEnumerable<RgbPixel>> mini_batch_labels;
                                IEnumerable<IEnumerable<MModRect>> miniBatchLabels;

                                using (var cropper = new RandomCropper())
                                using (var chipDims = new ChipDims(200, 200))
                                {
                                    cropper.ChipDims = chipDims;
                                    // Usually you want to give the cropper whatever min sizes you passed to the
                                    // mmod_options constructor, which is what we do here.
                                    cropper.SetMinObjectSize(40, 40);

                                    using (var rnd = new Rand())
                                    {
                                        // Run the trainer until the learning rate gets small.  This will probably take several
                                        // hours.
                                        while (trainer.GetLearningRate() >= 1e-4)
                                        {
                                            cropper.Operator(150, imagesTrain, faceBoxesTrain, out miniBatchSamples, out miniBatchLabels);
                                            // We can also randomly jitter the colors and that often helps a detector
                                            // generalize better to new images.
                                            foreach (var img in miniBatchSamples)
                                                Dlib.DisturbColors(img, rnd);

                                            LossMmod.TrainOneStep(trainer, miniBatchSamples, miniBatchLabels);

                                            miniBatchSamples.DisposeElement();
                                            miniBatchLabels.DisposeElement();
                                        }
                                        // wait for training threads to stop
                                        trainer.GetNet();
                                        Console.WriteLine("done training");

                                        // Save the network to disk
                                        net.Clean();
                                        LossMmod.Serialize(net, "mmod_network.dat");


                                        // Now that we have a face detector we can test it.  The first statement tests it
                                        // on the training data.  It will print the precision, recall, and then average precision.
                                        // This statement should indicate that the network works perfectly on the
                                        // training data.
                                        using (var matrix = Dlib.TestObjectDetectionFunction(net, imagesTrain, faceBoxesTrain))
                                            Console.WriteLine($"training results: {matrix}");
                                        // However, to get an idea if it really worked without overfitting we need to run
                                        // it on images it wasn't trained on.  The next line does this.   Happily,
                                        // this statement indicates that the detector finds most of the faces in the
                                        // testing data.
                                        using (var matrix = Dlib.TestObjectDetectionFunction(net, imagesTest, faceBoxesTest))
                                            Console.WriteLine($"testing results:  {matrix}");


                                        // If you are running many experiments, it's also useful to log the settings used
                                        // during the training experiment.  This statement will print the settings we used to
                                        // the screen.
                                        Console.WriteLine($"{trainer}{cropper}");

                                        // Now lets run the detector on the testing images and look at the outputs.
                                        using (var win = new ImageWindow())
                                            foreach (var img in imagesTest)
                                            {
                                                Dlib.PyramidUp(img);
                                                var dets = net.Operator(img);
                                                win.ClearOverlay();
                                                win.SetImage(img);
                                                foreach (var d in dets[0])
                                                    win.AddOverlay(d);

                                                Console.ReadKey();

                                                foreach (var det in dets)
                                                    foreach (var d in det)
                                                        d.Dispose();
                                            }

                                        // Now that you finished this example, you should read dnn_mmod_train_find_cars_ex.cpp,
                                        // which is a more advanced example.  It discusses many issues surrounding properly
                                        // setting the MMOD parameters and creating a good training dataset.
                                    }
                                }
                            }
                        }
                    }

                    detectorWindows.DisposeElement();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}