/*
 * This sample program is ported by C# from examples\dnn_semantic_segmentation_train_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using DlibDotNet;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;
using DnnSemanticSegmentation;

namespace DnnSemanticSegmentationTrain
{

    internal class Program
    {

        #region Fields

        private const string SemanticSegmentationNetFilename = "semantic_segmentation_voc2012net_v2.dnn";

        #endregion

        #region Constructors

        static Program()
        {
            ContainerBridgeRepository.Add(new TrainingSampleContainerBridge());
        }

        #endregion

        #region Methods

        private static int Main(string[] args)
        {
            try
            {
                if (args.Length != 1 && args.Length != 2)
                {
                    Console.WriteLine("To run this program you need a copy of the PASCAL VOC2012 dataset.");
                    Console.WriteLine();
                    Console.WriteLine("You call this program like this: ");
                    Console.WriteLine("./dnn_semantic_segmentation_train_ex /path/to/VOC2012 [minibatch-size]");
                    return 1;
                }

                // a mini-batch smaller than the default can be used with GPUs having less memory
                var minibatchSize = args.Length == 2 ? uint.Parse(args[1]) : 23u;
                Console.WriteLine($"mini-batch size: {minibatchSize}");

                Console.WriteLine("\nSCANNING PASCAL VOC2012 DATASET\n");

                var listing = PascalVOC2012.GetPascalVoc2012TrainListing(args[0]).ToArray();
                Console.WriteLine($"images in dataset: {listing.Length}");
                if (listing.Length == 0)
                {
                    Console.WriteLine("Didn't find the VOC2012 dataset.");
                    return 1;
                }

                const double initialLearningRate = 0.1;
                const double weightDecay = 0.0001;
                const double momentum = 0.9;

                using (var bnet = new LossMulticlassLogPerPixel(2))
                using (var sgd = new Sgd((float)weightDecay, (float)momentum))
                using (var trainer = new DnnTrainer<LossMulticlassLogPerPixel>(bnet, sgd))
                {
                    trainer.BeVerbose();
                    trainer.SetLearningRate(initialLearningRate);
                    trainer.SetSynchronizationFile("pascal_voc2012_trainer_state_file.dat", 10 * 60);
                    // This threshold is probably excessively large.
                    trainer.SetIterationsWithoutProgressThreshold(5000);
                    // Since the progress threshold is so large might as well set the batch normalization
                    // stats window to something big too.
                    Dlib.SetAllBnRunningStatsWindowSizes(bnet, 1000);

                    // Output training parameters.
                    Console.WriteLine();
                    Console.WriteLine(trainer);

                    var samples = new List<Matrix<RgbPixel>>();
                    var labels = new List<Matrix<ushort>>();

                    //// Start a bunch of threads that read images from disk and pull out random crops.  It's
                    //// important to be sure to feed the GPU fast enough to keep it busy.  Using multiple
                    //// thread for this kind of data preparation helps us do that.  Each thread puts the
                    //// crops into the data queue.
                    using (var data = new Pipe<TrainingSample>(200))
                    {
                        var function = new Action<object>(seed =>
                        {
                            using (var rnd = new Rand((ulong)seed))
                            {
                                while (data.IsEnabled)
                                {
                                    // Pick a random input image.
                                    var imageInfo = listing[rnd.GetRandom32BitNumber() % listing.Length];

                                    // Load the input image.
                                    using (var inputImage = Dlib.LoadImageAsMatrix<RgbPixel>(imageInfo.ImageFilename))
                                    {
                                        // Load the ground-truth (RGB) labels.
                                        using (var rgbLabelImage = Dlib.LoadImageAsMatrix<RgbPixel>(imageInfo.LabelFilename))
                                        {
                                            // Convert the indexes to RGB values.
                                            using (var indexLabelImage = new Matrix<ushort>())
                                            {
                                                PascalVOC2012.RgbLabelImageToIndexLabelImage(rgbLabelImage, indexLabelImage);

                                                // Randomly pick a part of the image.
                                                var temp = new TrainingSample();
                                                RandomlyCropImage(inputImage, indexLabelImage, temp, rnd);

                                                // Push the result to be used by the trainer.
                                                data.Enqueue(temp);
                                            }
                                        }
                                    }
                                }
                            }
                        });

                        var threads = Enumerable.Range(1, 1).Select(i =>
                        {
                            var dataLoader = new Thread(new ParameterizedThreadStart(function))
                            {
                                Name = $"dataLoader{i}"
                            };
                            dataLoader.Start((ulong)i);
                            return dataLoader;
                        }).ToArray();

                        // The main training loop.  Keep making mini-batches and giving them to the trainer.
                        // We will run until the learning rate has dropped by a factor of 1e-4.
                        while (trainer.GetLearningRate() >= 1e-4)
                        {
                            samples.DisposeElement();
                            labels.DisposeElement();
                            samples.Clear();
                            labels.Clear();

                            // make a mini-batch
                            while (samples.Count < minibatchSize)
                            {
                                data.Dequeue(out var temp);

                                samples.Add(temp.InputImage);
                                labels.Add(temp.LabelImage);

                                temp.Dispose();
                            }

                            LossMulticlassLogPerPixel.TrainOneStep(trainer, samples, labels);
                        }

                        // Training done, tell threads to stop and make sure to wait for them to finish before
                        // moving on.
                        data.Disable();
                        foreach (var thread in threads)
                            thread.Join();

                        // also wait for threaded processing to stop in the trainer.
                        trainer.GetNet();

                        bnet.Clean();
                        Console.WriteLine("saving network");
                        LossMulticlassLogPerPixel.Serialize(bnet, SemanticSegmentationNetFilename);
                    }

                    // Make a copy of the network to use it for inference.
                    using (var anet = bnet.CloneAs(3))
                    {
                        Console.WriteLine("Testing the network...");

                        // Find the accuracy of the newly trained network on both the training and the validation sets.
                        Console.WriteLine($"train accuracy  :  {CalculateAccuracy(anet, PascalVOC2012.GetPascalVoc2012TrainListing(args[0]))}");
                        Console.WriteLine($"val accuracy    :  {CalculateAccuracy(anet, PascalVOC2012.GetPascalVoc2012ValListing(args[0]))}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }

            return 0;
        }

        #region Helpers

        // Calculate the per-pixel accuracy on a dataset whose file names are supplied as a parameter.
        private static double CalculateAccuracy(LossMulticlassLogPerPixel anet, IEnumerable<ImageInfo> dataset)
        {
            var numRight = 0;
            var numWrong = 0;

            foreach (var imageInfo in dataset)
            {
                // Load the input image.
                using (var inputImage = Dlib.LoadImageAsMatrix<RgbPixel>(imageInfo.ImageFilename))
                {
                    // Load the ground-truth (RGB) labels.;
                    using (var rgbLabelImage = Dlib.LoadImageAsMatrix<RgbPixel>(imageInfo.LabelFilename))
                    {
                        // Create predictions for each pixel. At this point, the type of each prediction
                        // is an index (a value between 0 and 20). Note that the net may return an image
                        // that is not exactly the same size as the input.
                        using (var output = anet.Operator(inputImage))
                        using (var temp = output.First())
                        {
                            // Convert the indexes to RGB values.
                            using (var indexLabelImage = new Matrix<ushort>())
                            {
                                PascalVOC2012.RgbLabelImageToIndexLabelImage(rgbLabelImage, indexLabelImage);

                                // Crop the net output to be exactly the same size as the input.
                                using (var chipDims = new ChipDims((uint)inputImage.Rows, (uint)inputImage.Columns))
                                using (var chipDetails = new ChipDetails(Dlib.CenteredRect(temp.Columns / 2, temp.Rows / 2,
                                                                         (uint)inputImage.Columns,
                                                                         (uint)inputImage.Rows),
                                                                         chipDims))
                                {
                                    using (var netOutput = Dlib.ExtractImageChip<ushort>(temp, chipDetails, InterpolationTypes.NearestNeighbor))
                                    {
                                        var nr = indexLabelImage.Rows;
                                        var nc = indexLabelImage.Columns;

                                        // Compare the predicted values to the ground-truth values.
                                        for (var r = 0; r < nr; ++r)
                                            for (var c = 0; c < nc; ++c)
                                            {
                                                var truth = indexLabelImage[r, c];
                                                if (truth != LossMulticlassLogPerPixel.LabelToIgnore)
                                                {
                                                    var prediction = netOutput[r, c];
                                                    if (prediction == truth)
                                                        ++numRight;
                                                    else
                                                        ++numWrong;
                                                }
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Return the accuracy estimate.
            return numRight / (double)(numRight + numWrong);
        }
        
        private static Rectangle MakeRandomCroppingRect(Matrix<RgbPixel> img, Rand rnd)
        {
            // figure out what rectangle we want to crop from the image
            const double mins = 0.466666666;
            const double maxs = 0.875;

            var scale = mins + rnd.GetRandomDouble() * (maxs - mins);
            var size = (uint)(scale * Math.Min(img.Rows, img.Columns));
            var rect = new Rectangle(size, size);
            // randomly shift the box around
            var offset = new Point((int)(rnd.GetRandom32BitNumber() % (img.Columns - rect.Width)),
                                   (int)(rnd.GetRandom32BitNumber() % (img.Rows - rect.Height)));

            return Dlib.MoveRect(rect, offset);
        }

        private static void RandomlyCropImage(Matrix<RgbPixel> inputImage, Matrix<ushort> labelImage, TrainingSample crop, Rand rnd)
        {
            var rect = MakeRandomCroppingRect(inputImage, rnd);
            using (var chipDims = new ChipDims(227, 227))
            using (var chipDetails = new ChipDetails(rect, chipDims))
            {
                // Crop the input image.
                crop.InputImage = Dlib.ExtractImageChip<RgbPixel>(inputImage, chipDetails, InterpolationTypes.Bilinear);

                // Crop the labels correspondingly. However, note that here bilinear
                // interpolation would make absolutely no sense - you wouldn't say that
                // a bicycle is half-way between an aeroplane and a bird, would you?
                crop.LabelImage = Dlib.ExtractImageChip<ushort>(labelImage, chipDetails, InterpolationTypes.NearestNeighbor);

                // Also randomly flip the input image and the labels.
                if (rnd.GetRandomDouble() > 0.5)
                {
                    var tmpInput = Dlib.FlipLR(crop.InputImage);
                    var tmpLabel = Dlib.FlipLR(crop.LabelImage);
                    crop.InputImage?.Dispose();
                    crop.LabelImage?.Dispose();
                    crop.InputImage = tmpInput;
                    crop.LabelImage = tmpLabel;
                }

                // And then randomly adjust the colors.
                Dlib.ApplyRandomColorOffset(crop.InputImage, rnd);
            }
        }

        #endregion

        #endregion

    }

}