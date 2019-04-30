/*
 * This sample program is ported by C# from examples\dnn_mmod_train_find_cars_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;
using DlibDotNet.ImageTransforms;

namespace DnnMmodTrainFindCars
{

    internal class Program
    {
        
        #region Methods

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    Console.WriteLine("Give the path to a folder containing training.xml and testing.xml files.");
                    Console.WriteLine("This example program is specifically designed to run on the dlib vehicle ");
                    Console.WriteLine("detection dataset, which is available at this URL: ");
                    Console.WriteLine("   http://dlib.net/files/data/dlib_rear_end_vehicles_v1.tar");
                    Console.WriteLine();
                    Console.WriteLine("So download that dataset, extract it somewhere, and then run this program");
                    Console.WriteLine("with the dlib_rear_end_vehicles folder as an argument.  E.g. if you extract");
                    Console.WriteLine("the dataset to the current folder then you should run this example program");
                    Console.WriteLine("by typing: ");
                    Console.WriteLine("   ./dnn_mmod_train_find_cars_ex dlib_rear_end_vehicles");
                    Console.WriteLine();
                    Console.WriteLine("It takes about a day to finish if run on a high end GPU like a 1080ti.");
                    Console.WriteLine();
                    return;
                }

                var dataDirectory = args[0];

                IList<Matrix<RgbPixel>> imagesTrain;
                IList<Matrix<RgbPixel>> imagesTest;
                IList<IList<MModRect>> boxesTrain;
                IList<IList<MModRect>> boxesTest;
                Dlib.LoadImageDataset(dataDirectory + "/training.xml", out imagesTrain, out boxesTrain);
                Dlib.LoadImageDataset(dataDirectory + "/testing.xml", out imagesTest, out boxesTest);

                // When I was creating the dlib vehicle detection dataset I had to label all the cars
                // in each image.  MMOD requires all cars to be labeled, since any unlabeled part of an
                // image is implicitly assumed to be not a car, and the algorithm will use it as
                // negative training data.  So every car must be labeled, either with a normal
                // rectangle or an "ignore" rectangle that tells MMOD to simply ignore it (i.e. neither
                // treat it as a thing to detect nor as negative training data).  
                // 
                // In our present case, many images contain very tiny cars in the distance, ones that
                // are essentially just dark smudges.  It's not reasonable to expect the CNN
                // architecture we defined to detect such vehicles.  However, I erred on the side of
                // having more complete annotations when creating the dataset.  So when I labeled these
                // images I labeled many of these really difficult cases as vehicles to detect.   
                //
                // So the first thing we are going to do is clean up our dataset a little bit.  In
                // particular, we are going to mark boxes smaller than 35*35 pixels as ignore since
                // only really small and blurry cars appear at those sizes.  We will also mark boxes
                // that are heavily overlapped by another box as ignore.  We do this because we want to
                // allow for stronger non-maximum suppression logic in the learned detector, since that
                // will help make it easier to learn a good detector. 
                // 
                // To explain this non-max suppression idea further it's important to understand how
                // the detector works.  Essentially, sliding window detectors scan all image locations
                // and ask "is there a car here?".  If there really is a car in a specific location in
                // an image then usually many slightly different sliding window locations will produce
                // high detection scores, indicating that there is a car at those locations.  If we
                // just stopped there then each car would produce multiple detections.  But that isn't
                // what we want.  We want each car to produce just one detection.  So it's common for
                // detectors to include "non-maximum suppression" logic which simply takes the
                // strongest detection and then deletes all detections "close to" the strongest.  This
                // is a simple post-processing step that can eliminate duplicate detections.  However,
                // we have to define what "close to" means.  We can do this by looking at your training
                // data and checking how close the closest target boxes are to each other, and then
                // picking a "close to" measure that doesn't suppress those target boxes but is
                // otherwise as tight as possible.  This is exactly what the mmod_options object does
                // by default.
                //
                // Importantly, this means that if your training dataset contains an image with two
                // target boxes that really overlap a whole lot, then the non-maximum suppression
                // "close to" measure will be configured to allow detections to really overlap a whole
                // lot.  On the other hand, if your dataset didn't contain any overlapped boxes at all,
                // then the non-max suppression logic would be configured to filter out any boxes that
                // overlapped at all, and thus would be performing a much stronger non-max suppression.  
                //
                // Why does this matter?  Well, remember that we want to avoid duplicate detections.
                // If non-max suppression just kills everything in a really wide area around a car then
                // the CNN doesn't really need to learn anything about avoiding duplicate detections.
                // However, if non-max suppression only suppresses a tiny area around each detection
                // then the CNN will need to learn to output small detection scores for those areas of
                // the image not suppressed.  The smaller the non-max suppression region the more the
                // CNN has to learn and the more difficult the learning problem will become.  This is
                // why we remove highly overlapped objects from the training dataset.  That is, we do
                // it so the non-max suppression logic will be able to be reasonably effective.  Here
                // we are ensuring that any boxes that are entirely contained by another are
                // suppressed.  We also ensure that boxes with an intersection over union of 0.5 or
                // greater are suppressed.  This will improve the resulting detector since it will be
                // able to use more aggressive non-max suppression settings.

                var numOverlappedIgnoredTest = 0;
                foreach (var v in boxesTest)
                    using (var overlap = new TestBoxOverlap(0.50, 0.95))
                        numOverlappedIgnoredTest += IgnoreOverlappedBoxes(v, overlap);

                var numOverlappedIgnored = 0;
                var numAdditionalIgnored = 0;

                foreach (var v in boxesTrain)
                {
                    using (var overlap = new TestBoxOverlap(0.50, 0.95))
                        numOverlappedIgnored += IgnoreOverlappedBoxes(v, overlap);
                    foreach (var bb in v)
                    {
                        if (bb.Rect.Width < 35 && bb.Rect.Height < 35)
                        {
                            if (!bb.Ignore)
                            {
                                bb.Ignore = true;
                                ++numAdditionalIgnored;
                            }
                        }

                        // The dlib vehicle detection dataset doesn't contain any detections with
                        // really extreme aspect ratios.  However, some datasets do, often because of
                        // bad labeling.  So it's a good idea to check for that and either eliminate
                        // those boxes or set them to ignore.  Although, this depends on your
                        // application.  
                        // 
                        // For instance, if your dataset has boxes with an aspect ratio
                        // of 10 then you should think about what that means for the network
                        // architecture.  Does the receptive field even cover the entirety of the box
                        // in those cases?  Do you care about these boxes?  Are they labeling errors?
                        // I find that many people will download some dataset from the internet and
                        // just take it as given.  They run it through some training algorithm and take
                        // the dataset as unchallengeable truth.  But many datasets are full of
                        // labeling errors.  There are also a lot of datasets that aren't full of
                        // errors, but are annotated in a sloppy and inconsistent way.  Fixing those
                        // errors and inconsistencies can often greatly improve models trained from
                        // such data.  It's almost always worth the time to try and improve your
                        // training dataset.   
                        //
                        // In any case, my point is that there are other types of dataset cleaning you
                        // could put here.  What exactly you need depends on your application.  But you
                        // should carefully consider it and not take your dataset as a given.  The work
                        // of creating a good detector is largely about creating a high quality
                        // training dataset. 
                    }
                }

                // When modifying a dataset like this, it's a really good idea to print a log of how
                // many boxes you ignored.  It's easy to accidentally ignore a huge block of data, so
                // you should always look and see that things are doing what you expect.
                Console.WriteLine($"num_overlapped_ignored: {numOverlappedIgnored}");
                Console.WriteLine($"num_additional_ignored: {numAdditionalIgnored}");
                Console.WriteLine($"num_overlapped_ignored_test: {numOverlappedIgnoredTest}");


                Console.WriteLine($"num training images: {imagesTrain.Count()}");
                Console.WriteLine($"num testing images: {imagesTest.Count()}");


                // Our vehicle detection dataset has basically 3 different types of boxes.  Square
                // boxes, tall and skinny boxes (e.g. semi trucks), and short and wide boxes (e.g.
                // sedans).  Here we are telling the MMOD algorithm that a vehicle is recognizable as
                // long as the longest box side is at least 70 pixels long and the shortest box side is
                // at least 30 pixels long.  mmod_options will use these parameters to decide how large
                // each of the sliding windows needs to be so as to be able to detect all the vehicles.
                // Since our dataset has basically these 3 different aspect ratios, it will decide to
                // use 3 different sliding windows.  This means the final con layer in the network will
                // have 3 filters, one for each of these aspect ratios. 
                //
                // Another thing to consider when setting the sliding window size is the "stride" of
                // your network.  The network we defined above downsamples the image by a factor of 8x
                // in the first few layers.  So when the sliding windows are scanning the image, they
                // are stepping over it with a stride of 8 pixels.  If you set the sliding window size
                // too small then the stride will become an issue.  For instance, if you set the
                // sliding window size to 4 pixels, then it means a 4x4 window will be moved by 8
                // pixels at a time when scanning. This is obviously a problem since 75% of the image
                // won't even be visited by the sliding window.  So you need to set the window size to
                // be big enough relative to the stride of your network.  In our case, the windows are
                // at least 30 pixels in length, so being moved by 8 pixel steps is fine. 
                using (var options = new MModOptions(boxesTrain, 70, 30))
                {
                    // This setting is very important and dataset specific.  The vehicle detection dataset
                    // contains boxes that are marked as "ignore", as we discussed above.  Some of them are
                    // ignored because we set ignore to true in the above code.  However, the xml files
                    // also contained a lot of ignore boxes.  Some of them are large boxes that encompass
                    // large parts of an image and the intention is to have everything inside those boxes
                    // be ignored.  Therefore, we need to tell the MMOD algorithm to do that, which we do
                    // by setting options.overlaps_ignore appropriately.  
                    // 
                    // But first, we need to understand exactly what this option does.  The MMOD loss
                    // is essentially counting the number of false alarms + missed detections produced by
                    // the detector for each image.  During training, the code is running the detector on
                    // each image in a mini-batch and looking at its output and counting the number of
                    // mistakes.  The optimizer tries to find parameters settings that minimize the number
                    // of detector mistakes.
                    // 
                    // This overlaps_ignore option allows you to tell the loss that some outputs from the
                    // detector should be totally ignored, as if they never happened.  In particular, if a
                    // detection overlaps a box in the training data with ignore==true then that detection
                    // is ignored.  This overlap is determined by calling
                    // options.overlaps_ignore(the_detection, the_ignored_training_box).  If it returns
                    // true then that detection is ignored.
                    // 
                    // You should read the documentation for test_box_overlap, the class type for
                    // overlaps_ignore for full details.  However, the gist is that the default behavior is
                    // to only consider boxes as overlapping if their intersection over union is > 0.5.
                    // However, the dlib vehicle detection dataset contains large boxes that are meant to
                    // mask out large areas of an image.  So intersection over union isn't an appropriate
                    // way to measure "overlaps with box" in this case.  We want any box that is contained
                    // inside one of these big regions to be ignored, even if the detection box is really
                    // small.  So we set overlaps_ignore to behave that way with this line.
                    options.OverlapsIgnore = new TestBoxOverlap(0.5, 0.95);

                    using (var net = new LossMmod(options, 3))
                    {
                        // The final layer of the network must be a con layer that contains 
                        // options.detector_windows.size() filters.  This is because these final filters are
                        // what perform the final "sliding window" detection in the network.  For the dlib
                        // vehicle dataset, there will be 3 sliding window detectors, so we will be setting
                        // num_filters to 3 here.
                        var detectorWindows = options.DetectorWindows.ToArray();
                        using (var subnet = net.GetSubnet())
                        using (var details = subnet.GetLayerDetails())
                        {
                            details.SetNumFilters(detectorWindows.Length);

                            using (var trainer = new DnnTrainer<LossMmod>(net))
                            {
                                trainer.SetLearningRate(0.1);
                                trainer.BeVerbose();


                                // While training, we are going to use early stopping.  That is, we will be checking
                                // how good the detector is performing on our test data and when it stops getting
                                // better on the test data we will drop the learning rate.  We will keep doing that
                                // until the learning rate is less than 1e-4.   These two settings tell the trainer to
                                // do that.  Essentially, we are setting the first argument to infinity, and only the
                                // test iterations without progress threshold will matter.  In particular, it says that
                                // once we observe 1000 testing mini-batches where the test loss clearly isn't
                                // decreasing we will lower the learning rate.
                                trainer.SetIterationsWithoutProgressThreshold(50000);
                                trainer.SetTestIterationsWithoutProgressThreshold(1000);

                                const string syncFilename = "mmod_cars_sync";
                                trainer.SetSynchronizationFile(syncFilename, 5 * 60);



                                IEnumerable<Matrix<RgbPixel>> mini_batch_samples;
                                IEnumerable<IEnumerable<MModRect>> mini_batch_labels;
                                using (var cropper = new RandomCropper())
                                {
                                    cropper.SetSeed(0);
                                    cropper.SetChipDims(350, 350);
                                    // Usually you want to give the cropper whatever min sizes you passed to the
                                    // mmod_options constructor, or very slightly smaller sizes, which is what we do here.
                                    cropper.SetMinObjectSize(69, 28);
                                    cropper.MaxRotationDegrees = 2;

                                    using (var rnd = new Rand())
                                    {
                                        // Log the training parameters to the console
                                        Console.WriteLine($"{trainer}{cropper}");

                                        var cnt = 1;
                                        // Run the trainer until the learning rate gets small.  
                                        while (trainer.GetLearningRate() >= 1e-4)
                                        {
                                            // Every 30 mini-batches we do a testing mini-batch.  
                                            if (cnt % 30 != 0 || !imagesTest.Any())
                                            {
                                                cropper.Operator(87, imagesTrain, boxesTrain, out mini_batch_samples, out mini_batch_labels);
                                                // We can also randomly jitter the colors and that often helps a detector
                                                // generalize better to new images.
                                                foreach (var img in mini_batch_samples)
                                                    Dlib.DisturbColors(img, rnd);

                                                // It's a good idea to, at least once, put code here that displays the images
                                                // and boxes the random cropper is generating.  You should look at them and
                                                // think about if the output makes sense for your problem.  Most of the time
                                                // it will be fine, but sometimes you will realize that the pattern of cropping
                                                // isn't really appropriate for your problem and you will need to make some
                                                // change to how the mini-batches are being generated.  Maybe you will tweak
                                                // some of the cropper's settings, or write your own entirely separate code to
                                                // create mini-batches.  But either way, if you don't look you will never know.
                                                // An easy way to do this is to create a dlib::image_window to display the
                                                // images and boxes.

                                                LossMmod.TrainOneStep(trainer, mini_batch_samples, mini_batch_labels);

                                                mini_batch_samples.DisposeElement();
                                                mini_batch_labels.DisposeElement();
                                            }
                                            else
                                            {
                                                cropper.Operator(87, imagesTest, boxesTest, out mini_batch_samples, out mini_batch_labels);
                                                // We can also randomly jitter the colors and that often helps a detector
                                                // generalize better to new images.
                                                foreach (var img in mini_batch_samples)
                                                    Dlib.DisturbColors(img, rnd);

                                                LossMmod.TestOneStep(trainer, mini_batch_samples, mini_batch_labels);

                                                mini_batch_samples.DisposeElement();
                                                mini_batch_labels.DisposeElement();
                                            }
                                            ++cnt;
                                        }
                                        // wait for training threads to stop
                                        trainer.GetNet();
                                        Console.WriteLine("done training");

                                        // Save the network to disk
                                        net.Clean();
                                        LossMmod.Serialize(net, "mmod_rear_end_vehicle_detector.dat");


                                        // It's a really good idea to print the training parameters.  This is because you will
                                        // invariably be running multiple rounds of training and should be logging the output
                                        // to a file.  This print statement will include many of the training parameters in
                                        // your log.
                                        Console.WriteLine($"{trainer}{cropper}");

                                        Console.WriteLine($"\nsync_filename: {syncFilename}");
                                        Console.WriteLine($"num training images: {imagesTrain.Count()}");
                                        using (var _ = new TestBoxOverlap())
                                        using (var matrix = Dlib.TestObjectDetectionFunction(net, imagesTrain, boxesTrain, _, 0, options.OverlapsIgnore))
                                            Console.WriteLine($"training results: {matrix}");
                                        // Upsampling the data will allow the detector to find smaller cars.  Recall that 
                                        // we configured it to use a sliding window nominally 70 pixels in size.  So upsampling
                                        // here will let it find things nominally 35 pixels in size.  Although we include a
                                        // limit of 1800*1800 here which means "don't upsample an image if it's already larger
                                        // than 1800*1800".  We do this so we don't run out of RAM, which is a concern because
                                        // some of the images in the dlib vehicle dataset are really high resolution.
                                        Dlib.UpsampleImageDataset(2, imagesTrain, boxesTrain, 1800 * 1800);
                                        using (var _ = new TestBoxOverlap())
                                        using (var matrix = Dlib.TestObjectDetectionFunction(net, imagesTrain, boxesTrain, _, 0, options.OverlapsIgnore))
                                            Console.WriteLine($"training upsampled results: {matrix}");


                                        Console.WriteLine("num testing images: {images_test.Count()}");
                                        using (var _ = new TestBoxOverlap())
                                        using (var matrix = Dlib.TestObjectDetectionFunction(net, imagesTest, boxesTest, _, 0, options.OverlapsIgnore))
                                            Console.WriteLine($"testing results: {matrix}");
                                        Dlib.UpsampleImageDataset(2, imagesTest, boxesTest, 1800 * 1800);
                                        using (var _ = new TestBoxOverlap())
                                        using (var matrix = Dlib.TestObjectDetectionFunction(net, imagesTest, boxesTest, _, 0, options.OverlapsIgnore))
                                            Console.WriteLine($"testing upsampled results: {matrix}");

                                        /*
                                            This program takes many hours to execute on a high end GPU.  It took about a day to
                                            train on a NVIDIA 1080ti.  The resulting model file is available at
                                                http://dlib.net/files/mmod_rear_end_vehicle_detector.dat.bz2
                                            It should be noted that this file on dlib.net has a dlib::shape_predictor appended
                                            onto the end of it (see dnn_mmod_find_cars_ex.cpp for an example of its use).  This
                                            explains why the model file on dlib.net is larger than the
                                            mmod_rear_end_vehicle_detector.dat output by this program.

                                            You can see some videos of this vehicle detector running on YouTube:
                                                https://www.youtube.com/watch?v=4B3bzmxMAZU
                                                https://www.youtube.com/watch?v=bP2SUo5vSlc

                                            Also, the training and testing accuracies were:
                                                num training images: 2217
                                                training results: 0.990738 0.736431 0.736073 
                                                training upsampled results: 0.986837 0.937694 0.936912 
                                                num testing images: 135
                                                testing results: 0.988827 0.471372 0.470806 
                                                testing upsampled results: 0.987879 0.651132 0.650399 
                                        */

                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #region Helpers

        private static int IgnoreOverlappedBoxes(IList<MModRect> boxes, TestBoxOverlap overlaps)
        {
            var numIgnored = 0;
            for (var i = 0; i < boxes.Count; ++i)
            {
                if (boxes[i].Ignore)
                    continue;

                for (var j = i + 1; j < boxes.Count; ++j)
                {
                    if (boxes[j].Ignore)
                        continue;

                    if (overlaps.Operator(boxes[i], boxes[j]))
                    {
                        ++numIgnored;
                        if (boxes[i].Rect.Area < boxes[j].Rect.Area)
                            boxes[i].Ignore = true;
                        else
                            boxes[j].Ignore = true;
                    }
                }
            }

            return numIgnored;
        }

        #endregion

        #endregion

    }
}