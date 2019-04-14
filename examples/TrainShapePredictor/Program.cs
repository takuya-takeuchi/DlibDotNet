/*
 * This sample program is ported by C# from examples\train_shape_predictor_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.IO;
using DlibDotNet;

namespace TrainShapePredictor
{

    internal class Program
    {

        #region Methods

        private static void Main(string[] args)
        {
            try
            {
                // In this example we are going to train a shape_predictor based on the
                // small faces dataset in the examples/faces directory.  So the first
                // thing we do is load that dataset.  This means you need to supply the
                // path to this faces folder as a command line argument so we will know
                // where it is.
                if (args.Length != 1)
                {
                    Console.WriteLine("Give the path to the examples/faces directory as the argument to this");
                    Console.WriteLine("program.  For example, if you are in the examples folder then execute ");
                    Console.WriteLine("this program by running: ");
                    Console.WriteLine("   ./train_shape_predictor_ex faces");
                    Console.WriteLine();
                    return;
                }
                var facesDirectory = args[0];
                // The faces directory contains a training dataset and a separate
                // testing dataset.  The training data consists of 4 images, each
                // annotated with rectangles that bound each human face along with 68
                // face landmarks on each face.  The idea is to use this training data
                // to learn to identify the position of landmarks on human faces in new
                // images. 
                // 
                // Once you have trained a shape_predictor it is always important to
                // test it on data it wasn't trained on.  Therefore, we will also load
                // a separate testing set of 5 images.  Once we have a shape_predictor 
                // created from the training data we will see how well it works by
                // running it on the testing images. 
                // 
                // So here we create the variables that will hold our dataset.
                // images_train will hold the 4 training images and faces_train holds
                // the locations and poses of each face in the training images.  So for
                // example, the image images_train[0] has the faces given by the
                // full_object_detections in faces_train[0].
                Array<Array2D<byte>> imagesTrain;
                Array<Array2D<byte>> imagesTest;
                IList<IList<FullObjectDetection>> faces_train, faces_test;

                // Now we load the data.  These XML files list the images in each
                // dataset and also contain the positions of the face boxes and
                // landmarks (called parts in the XML file).  Obviously you can use any
                // kind of input format you like so long as you store the data into
                // images_train and faces_train.  But for convenience dlib comes with
                // tools for creating and loading XML image dataset files.  Here you see
                // how to load the data.  To create the XML files you can use the imglab
                // tool which can be found in the tools/imglab folder.  It is a simple
                // graphical tool for labeling objects in images.  To see how to use it
                // read the tools/imglab/README.txt file.
                Dlib.LoadImageDataset(Path.Combine(facesDirectory, "training_with_face_landmarks.xml"), out imagesTrain, out faces_train);
                Dlib.LoadImageDataset(Path.Combine(facesDirectory, "testing_with_face_landmarks.xml"), out imagesTest, out faces_test);

                // Now make the object responsible for training the model.
                using (var trainer = new ShapePredictorTrainer())
                {
                    // This algorithm has a bunch of parameters you can mess with.  The
                    // documentation for the shape_predictor_trainer explains all of them.
                    // You should also read Kazemi's paper which explains all the parameters
                    // in great detail.  However, here I'm just setting three of them
                    // differently than their default values.  I'm doing this because we
                    // have a very small dataset.  In particular, setting the oversampling
                    // to a high amount (300) effectively boosts the training set size, so
                    // that helps this example.
                    trainer.OverSamplingAmount = 300;
                    // I'm also reducing the capacity of the model by explicitly increasing
                    // the regularization (making nu smaller) and by using trees with
                    // smaller depths.  
                    trainer.Nu = 0.05d;
                    trainer.TreeDepth = 2;

                    // some parts of training process can be parallelized.
                    // Trainer will use this count of threads when possible
                    trainer.NumThreads = 2;

                    // Tell the trainer to print status messages to the console so we can
                    // see how long the training will take.
                    trainer.BeVerbose();

                    // Now finally generate the shape model
                    using (var sp = trainer.Train(imagesTrain, faces_train))
                    {



                        // Now that we have a model we can test it.  This function measures the
                        // average distance between a face landmark output by the
                        // shape_predictor and where it should be according to the truth data.
                        // Note that there is an optional 4th argument that lets us rescale the
                        // distances.  Here we are causing the output to scale each face's
                        // distances by the interocular distance, as is customary when
                        // evaluating face landmarking systems.
                        Console.WriteLine($"mean training error: {Dlib.TestShapePredictor(sp, imagesTrain, faces_train, GetInterocularDistances(faces_train))}");

                        // The real test is to see how well it does on data it wasn't trained
                        // on.  We trained it on a very small dataset so the accuracy is not
                        // extremely high, but it's still doing quite good.  Moreover, if you
                        // train it on one of the large face landmarking datasets you will
                        // obtain state-of-the-art results, as shown in the Kazemi paper.
                        Console.WriteLine($"mean testing error:  {Dlib.TestShapePredictor(sp, imagesTest, faces_test, GetInterocularDistances(faces_test))}");

                        // Finally, we save the model to disk so we can use it later.
                        ShapePredictor.Serialize(sp, "sp.dat");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nexception thrown!");
                Console.WriteLine(e.Message);
            }
        }

        #region Helpers

        private static IList<IList<double>> GetInterocularDistances(IList<IList<FullObjectDetection>> objects)
        {
            var temp = new List<IList<double>>(objects.Count);
            for (var i = 0; i < objects.Count; ++i)
            {
                temp.Add(new List<double>());
                for (var j = 0; j < objects[i].Count; ++j)
                    temp[i].Add(InterocularDistance(objects[i][j]));
            }

            return temp;
        }

        private static double InterocularDistance(FullObjectDetection det)
        {
            var l = new DPoint();
            var r = new DPoint();
            double cnt = 0;
            // Find the center of the left eye by averaging the points around 
            // the eye.
            for (var i = 36u; i <= 41; ++i)
            {
                l += det.GetPart(i);
                ++cnt;
            }
            l /= cnt;

            // Find the center of the right eye by averaging the points around 
            // the eye.
            cnt = 0;
            for (var i = 42u; i <= 47; ++i)
            {
                r += det.GetPart(i);
                ++cnt;
            }
            r /= cnt;

            // Now return the distance between the centers of the eyes
            return Dlib.Length(l - r);
        }

        #endregion

        #endregion

    }

}