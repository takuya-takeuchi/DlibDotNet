/*
 * This sample program is ported by C# from examples\dnn_instance_segmentation_train_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DlibDotNet;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;
using DlibDotNet.ImageTransforms;

namespace DnnInstanceSegmentationTrain
{

    internal class Program
    {

        #region Fields

        private const int SegDim = 227;

        private const string InstanceSegmentationNetFilename = "instance_segmentation_voc2012net.dnn";

        #endregion

        #region Constructors

        static Program()
        {
            ContainerBridgeRepository.Add(new DetTrainingSampleContainerBridge());
            ContainerBridgeRepository.Add(new SegTrainingSampleContainerBridge());
        }

        #endregion

        #region Methods

        private static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("To run this program you need a copy of the PASCAL VOC2012 dataset.");
                Console.WriteLine();
                Console.WriteLine("You call this program like this: ");
                Console.WriteLine("./dnn_instance_segmentation_train_ex /path/to/VOC2012 [det-minibatch-size] [seg-minibatch-size] [class-1] [class-2] [class-3] ...");
                return 1;
            }

            try
            {
                Console.WriteLine("\nSCANNING PASCAL VOC2012 DATASET");
                Console.WriteLine();

                var listing = PascalVOC2012.GetPascalVoc2012TrainListing(args[0]).ToArray();
                Console.WriteLine($"images in entire dataset: {listing.Length}");
                if (listing.Length == 0)
                {
                    Console.WriteLine("Didn't find the VOC2012 dataset. ");
                    return 1;
                }

                // mini-batches smaller than the default can be used with GPUs having less memory
                var argc = args.Length;
                var detMiniBatchSize = argc >= 2 ? int.Parse(args[1]) : 35;
                var segMiniBatchSize = argc >= 3 ? int.Parse(args[2]) : 100;
                Console.WriteLine($"det mini-batch size: {detMiniBatchSize}");
                Console.WriteLine($"seg mini-batch size: {segMiniBatchSize}");

                var desiredClassLabels = new List<string>();
                for (var arg = 3; arg < argc; ++arg)
                    desiredClassLabels.Add(args[arg]);

                if (!desiredClassLabels.Any())
                {
                    desiredClassLabels.Add("bicycle");
                    desiredClassLabels.Add("car");
                    desiredClassLabels.Add("cat");
                }

                Console.Write("desired classlabels:");
                foreach (var desiredClassLabel in desiredClassLabels)
                    Console.Write($" {desiredClassLabel}");
                Console.WriteLine();

                // extract the MMOD rects
                Console.Write("\nExtracting all truth instances...");
                var truthInstances = LoadAllTruthInstances(listing);
                Console.WriteLine(" Done!");
                Console.WriteLine();


                if (listing.Length != truthInstances.Count)
                    throw new ApplicationException();

                var originalTruthImages = new List<TruthImage>();
                for (int i = 0, end = listing.Length; i < end; ++i)
                {
                    originalTruthImages.Add(new TruthImage
                    {
                        Info = listing[i],
                        TruthInstances = truthInstances[i]
                    });
                }


                var truthImagesFilteredByClass = FilterBasedOnClassLabel(originalTruthImages, desiredClassLabels);

                Console.WriteLine($"images in dataset filtered by class: {truthImagesFilteredByClass.Count}");

                IgnoreSomeTruthBoxes(truthImagesFilteredByClass);
                var truthImages = FilterImagesWithNoTruth(truthImagesFilteredByClass);

                Console.WriteLine($"images in dataset after ignoring some truth boxes: {truthImages.Count}");

                // First train an object detector network (loss_mmod).
                Console.WriteLine("\nTraining detector network:");
                var detNet = TrainDetectionNetwork(truthImages, (uint)detMiniBatchSize);

                // Then train mask predictors (segmentation).
                var segNetsByClass = new Dictionary<string, LossMulticlassLogPerPixel>();

                // This flag controls if a separate mask predictor is trained for each class.
                // Note that it would also be possible to train a separate mask predictor for
                // class groups, each containing somehow similar classes -- for example, one
                // mask predictor for cars and buses, another for cats and dogs, and so on.
                const bool separateSegNetForEachClass = true;


                if (separateSegNetForEachClass)
                {
                    foreach (var classLabel in desiredClassLabels)
                    {
                        // Consider only the truth images belonging to this class.
                        var classImages = FilterBasedOnClassLabel(truthImages, new[] { classLabel });

                        Console.WriteLine($"\nTraining segmentation network for class {classLabel}:");
                        segNetsByClass[classLabel] = TrainSegmentationNetwork(classImages, (uint)segMiniBatchSize, classLabel);
                    }
                }
                else
                {
                    Console.WriteLine("Training a single segmentation network:");
                    segNetsByClass[""] = TrainSegmentationNetwork(truthImages, (uint)segMiniBatchSize, "");
                }

                Console.WriteLine("Saving networks");
                using (var proxy = new ProxySerialize(InstanceSegmentationNetFilename))
                {
                    LossMmod.Serialize(proxy, detNet);
                    segNetsByClass.Serialize(proxy, 4);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }

        #region Helpers

        private static IEnumerable<MModRect> ExtractMmodRects(IReadOnlyCollection<TruthInstance> truthInstances)
        {
            var mmodRects = new List<MModRect>(truthInstances.Count);
            mmodRects.AddRange(truthInstances.Select(truth => truth.MmodRect));

            return mmodRects;
        }

        private static IEnumerable<IEnumerable<MModRect>> ExtractMmodRectVectors(IReadOnlyCollection<TruthImage> truthImages)
        {
            var mmodRects = new List<IEnumerable<MModRect>>(truthImages.Count);
            mmodRects.AddRange(truthImages.Select(truthImage => ExtractMmodRects(truthImage.TruthInstances)));

            return mmodRects;
        }

        private static List<TruthImage> FilterBasedOnClassLabel(IEnumerable<TruthImage> truthImages, IReadOnlyCollection<string> desiredClassLabels)
        {
            var result = new List<TruthImage>();

            var representsDesiredClass = new Func<TruthInstance, bool>(truthInstance =>
            {
                return desiredClassLabels.Any(s => s == truthInstance.MmodRect.Label);
            });

            foreach (var input in truthImages)
            {
                var hasDesiredClass = input.TruthInstances.Any(instance => representsDesiredClass(instance));
                if (hasDesiredClass)
                {
                    // NB: This keeps only MMOD rects belonging to any of the desired classes.
                    //     A reasonable alternative could be to keep all rects, but mark those
                    //     belonging in other classes to be ignored during training.
                    var temp = new List<TruthInstance>(input.TruthInstances.Where(representsDesiredClass));
                    result.Add(new TruthImage
                    {
                        Info = input.Info,
                        TruthInstances = temp
                    });
                }
            }

            return result;
        }

        // Filter images that have no (non-ignored) truth
        private static List<TruthImage> FilterImagesWithNoTruth(IEnumerable<TruthImage> truthImages)
        {
            var result = new List<TruthImage>();
            foreach (var truthImage in truthImages)
            {
                var ignored = new Func<TruthInstance, bool>(truth => truth.MmodRect.Ignore);
                var truthInstances = truthImage.TruthInstances;
                if (!truthInstances.All(ignored))
                    result.Add(truthImage);
            }

            return result;
        }

        private static Rectangle GetCroppingRect(Rectangle rectangle)
        {
            if (rectangle.IsEmpty)
                throw new ArgumentException();

            var centerPoint = Dlib.Center(rectangle);
            var maxDim = Math.Max(rectangle.Width, rectangle.Height);
            var d = (int)(Math.Round(maxDim / 2.0 * 1.5)); // add +50%

            return new Rectangle(
                centerPoint.X - d,
                centerPoint.Y - d,
                centerPoint.X + d,
                centerPoint.Y + d
            );
        }

        private static int IgnoreOverlappedBoxes(IReadOnlyList<TruthInstance> truthInstances, TestBoxOverlap overlaps)
        /*!
        ensures
            - Whenever two rectangles in boxes overlap, according to overlaps(), we set the
              smallest box to ignore.
            - returns the number of newly ignored boxes.
        !*/
        {
            var numIgnored = 0;
            for (int i = 0, end = truthInstances.Count; i < end; ++i)
            {
                var boxI = truthInstances[i].MmodRect;
                if (boxI.Ignore)
                    continue;

                for (var j = i + 1; j < end; ++j)
                {
                    var boxJ = truthInstances[j].MmodRect;
                    if (boxJ.Ignore)
                        continue;
                    if (overlaps.Operator(boxI, boxJ))
                    {
                        ++numIgnored;
                        if (boxI.Rect.Area < boxJ.Rect.Area)
                            boxI.Ignore = true;
                        else
                            boxJ.Ignore = true;
                    }
                }
            }

            return numIgnored;
        }

        // Ignore truth boxes that overlap too much, are too small, or have a large aspect ratio.
        private static void IgnoreSomeTruthBoxes(IEnumerable<TruthImage> truthImages)
        {
            using (var testBoxOverlap = new TestBoxOverlap(0.90, 0.95))
                foreach (var i in truthImages)
                {
                    var truthInstances = i.TruthInstances;

                    IgnoreOverlappedBoxes(truthInstances, testBoxOverlap);

                    foreach (var truth in truthInstances)
                    {
                        if (truth.MmodRect.Ignore)
                            continue;

                        var rect = truth.MmodRect.Rect;

                        const uint minWidth = 35;
                        const uint minHeight = 35;
                        if (rect.Width < minWidth && rect.Height < minHeight)
                        {
                            truth.MmodRect.Ignore = true;
                            continue;
                        }

                        const double maxAspectRatioWidthToHeight = 3.0;
                        const double maxAspectRatioHeightToWidth = 1.5;
                        var aspectRatioWidthToHeight = rect.Width / (double)rect.Height;
                        var aspectRatioHeightToWidth = 1.0 / aspectRatioWidthToHeight;
                        var isAspectRatioTooLarge
                            = aspectRatioWidthToHeight > maxAspectRatioWidthToHeight || aspectRatioHeightToWidth > maxAspectRatioHeightToWidth;

                        if (isAspectRatioTooLarge)
                            truth.MmodRect.Ignore = true;
                    }
                }
        }

        private static bool IsInstancePixel(RgbPixel rgbLabel)
        {
            if (rgbLabel == new RgbPixel(0, 0, 0))
                return false; // Background
            if (rgbLabel == new RgbPixel(224, 224, 192))
                return false; // The cream-colored `void' label is used in border regions and to mask difficult objects

            return true;
        }

        private static Matrix<ushort> KeepOnlyCurrentInstance(Matrix<RgbPixel> rgbLabelImage, RgbPixel rgbLabel)
        {
            var nr = rgbLabelImage.Rows;
            var nc = rgbLabelImage.Columns;

            var result = new Matrix<ushort>(nr, nc);
            for (var r = 0; r < nr; ++r)
                for (var c = 0; c < nc; ++c)
                {
                    var index = rgbLabelImage[r, c];
                    if (index == rgbLabel)
                        result[r, c] = 1;
                    else if (index == new RgbPixel(224, 224, 192))
                        result[r, c] = LossMulticlassLogPerPixel.LabelToIgnore;
                    else
                        result[r, c] = 0;
                }

            return result;
        }

        private static List<TruthInstance> LoadTruthInstances(ImageInfo info)
        {
            var instanceLabelImage = Dlib.LoadImageAsMatrix<RgbPixel>(info.InstanceLabelFilename);
            var classLabelImage = Dlib.LoadImageAsMatrix<RgbPixel>(info.ClassLabelFilename);

            return RgbLabelImagesToTruthInstances(instanceLabelImage, classLabelImage);
        }

        private static List<List<TruthInstance>> LoadAllTruthInstances(ICollection<ImageInfo> listing)
        {
            var truthInstances = new List<List<TruthInstance>>(listing.Count);
            truthInstances.AddRange(listing.Select(LoadTruthInstances));

            return truthInstances;
        }

        private static List<TruthInstance> RgbLabelImagesToTruthInstances(Matrix<RgbPixel> instanceLabelImage, Matrix<RgbPixel> classLabelImage)
        {
            var resultMap = new Dictionary<RgbPixel, MModRect>();

            if (instanceLabelImage.Rows != classLabelImage.Rows)
                throw new ApplicationException();
            if (instanceLabelImage.Columns != classLabelImage.Columns)
                throw new ApplicationException();

            var nr = instanceLabelImage.Rows;
            var nc = instanceLabelImage.Columns;

            for (var r = 0; r < nr; ++r)
                for (var c = 0; c < nc; ++c)
                {
                    var rgbInstanceLabel = instanceLabelImage[r, c];

                    if (!IsInstancePixel(rgbInstanceLabel))
                        continue;

                    var rgbClassLabel = classLabelImage[r, c];
                    var voc2012Class = PascalVOC2012.FindVoc2012Class(rgbClassLabel);

                    if (!resultMap.TryGetValue(rgbInstanceLabel, out var i))
                    {
                        // Encountered a new instance
                        resultMap[rgbInstanceLabel] = new Rectangle(c, r, c, r);
                        resultMap[rgbInstanceLabel].Label = voc2012Class.ClassLabel;
                    }
                    else
                    {
                        // Not the first occurrence - update the rect
                        var rect = i.Rect;

                        if (c < rect.Left)
                            rect.Left = c;
                        else if (c > rect.Right)
                            rect.Right = c;

                        if (r > rect.Bottom)
                            rect.Bottom = r;

                        resultMap[rgbInstanceLabel].Rect = rect;

                        if (i.Label != voc2012Class.ClassLabel)
                            throw new ApplicationException();

                    }
                }

            var flatResult = new List<TruthInstance>(resultMap.Count);
            foreach (var i in resultMap)
            {
                flatResult.Add(new TruthInstance
                {
                    RgbLabel = i.Key,
                    MmodRect = i.Value
                });
            }

            return flatResult;
        }

        private static LossMulticlassLogPerPixel TrainSegmentationNetwork(IReadOnlyList<TruthImage> truthImages,
                                                                          uint segMiniBatchSize,
                                                                          string classLabel)
        {
            const double initialLearningRate = 0.1;
            const double weightDecay = 0.0001;
            const double momentum = 0.9;

            var synchronizationFileName = $"pascal_voc2012_seg_trainer_state_file{(classLabel.Length == 0 ? "" : $"_{classLabel}")}.dat";

            var segNet = new LossMulticlassLogPerPixel(4);
            using (var sgd = new Sgd((float)weightDecay, (float)momentum))
            using (var segTrainer = new DnnTrainer<LossMulticlassLogPerPixel>(segNet, sgd))
            {
                segTrainer.BeVerbose();
                segTrainer.SetLearningRate(initialLearningRate);
                segTrainer.SetSynchronizationFile(synchronizationFileName, 60 * 10);
                segTrainer.SetIterationsWithoutProgressThreshold(2000);
                Dlib.SetAllBnRunningStatsWindowSizes(segNet, 1000);


                // Output training parameters.
                Console.WriteLine(segTrainer);

                var samples = new List<Matrix<RgbPixel>>();
                var labels = new List<Matrix<ushort>>();

                // Start a bunch of threads that read images from disk and pull out random crops.  It's
                // important to be sure to feed the GPU fast enough to keep it busy.  Using multiple
                // thread for this kind of data preparation helps us do that.  Each thread puts the
                // crops into the data queue.
                using (var data = new Pipe<SegTrainingSample>(200))
                {
                    var function = new Action<object>(seed =>
                    {
                        using (var rnd = new Rand((ulong)seed))
                        {
                            while (data.IsEnabled)
                            {
                                // Pick a random input image.
                                var randomIndex = rnd.GetRandom32BitNumber() % truthImages.Count;
                                var truthImage = truthImages[(int)randomIndex];
                                var imageTruths = truthImage.TruthInstances;

                                if (imageTruths.Any())
                                {
                                    var info = truthImage.Info;

                                    // Load the input image.
                                    var inputImage = Dlib.LoadImageAsMatrix<RgbPixel>(info.ImageFilename);

                                    // Load the ground-truth (RGB) instance labels.
                                    var rgbLabelImage = Dlib.LoadImageAsMatrix<RgbPixel>(info.InstanceLabelFilename);

                                    // Pick a random training instance.
                                    var truthInstance = imageTruths[(int)(rnd.GetRandom32BitNumber() % imageTruths.Count)];
                                    var truthRect = truthInstance.MmodRect.Rect;
                                    var croppingRect = GetCroppingRect(truthRect);

                                    // Pick a random crop around the instance.
                                    var maxXTranslateAmount = (long)(truthRect.Width / 10.0);
                                    var maxYTranslateAmount = (long)(truthRect.Height / 10.0);

                                    var randomTranslate = new Point(
                                        (int)rnd.GetIntegerInRange(-maxXTranslateAmount, maxXTranslateAmount + 1),
                                        (int)rnd.GetIntegerInRange(-maxYTranslateAmount, maxYTranslateAmount + 1)
                                    );

                                    var randomRect = new Rectangle(
                                        croppingRect.Left + randomTranslate.X,
                                        croppingRect.Top + randomTranslate.Y,
                                        croppingRect.Right + randomTranslate.X,
                                        croppingRect.Bottom + randomTranslate.Y
                                    );

                                    // Crop the input image.
                                    using (var chipDims = new ChipDims(SegDim, SegDim))
                                    using (var chipDetails = new ChipDetails(randomRect, chipDims))
                                    {
                                        var tempInputImage = Dlib.ExtractImageChip<RgbPixel>(inputImage, chipDetails, InterpolationTypes.Bilinear);
                                        var temp = new SegTrainingSample
                                        {
                                            InputImage = tempInputImage
                                        };
                                        Dlib.DisturbColors(temp.InputImage, rnd);

                                        // Crop the labels correspondingly. However, note that here bilinear
                                        // interpolation would make absolutely no sense - you wouldn't say that
                                        // a bicycle is half-way between an aeroplane and a bird, would you?
                                        var rgbLabelChip = Dlib.ExtractImageChip<RgbPixel>(rgbLabelImage, chipDetails, InterpolationTypes.NearestNeighbor);

                                        // Clear pixels not related to the current instance.
                                        temp.LabelImage = KeepOnlyCurrentInstance(rgbLabelChip, truthInstance.RgbLabel);

                                        // Push the result to be used by the trainer.
                                        data.Enqueue(temp);
                                    }
                                }
                                else
                                {
                                    // TODO: use background samples as well
                                }
                            }
                        }
                    });

                    var threads = Enumerable.Range(1, 4).Select(i =>
                    {
                        var dataLoader = new Thread(new ParameterizedThreadStart(function))
                        {
                            Name = $"dataLoader{i}"
                        };
                        dataLoader.Start((ulong)i);
                        return dataLoader;
                    }).ToArray();

                    var stopDataLoaders = new Action(() =>
                    {
                        data.Disable();
                        foreach (var thread in threads)
                            thread.Join();
                    });

                    try
                    {
                        // The main training loop.  Keep making mini-batches and giving them to the trainer.
                        // We will run until the learning rate has dropped by a factor of 1e-4.
                        while (segTrainer.GetLearningRate() >= 1e-4)
                        {
                            samples.DisposeElement();
                            labels.DisposeElement();
                            samples.Clear();
                            labels.Clear();

                            samples.Capacity = (int)segMiniBatchSize;
                            labels.Capacity = (int)segMiniBatchSize;

                            // make a mini-batch
                            while (samples.Count < segMiniBatchSize)
                            {
                                data.Dequeue(out var temp);

                                samples.Add(temp.InputImage);
                                labels.Add(temp.LabelImage);

                                temp.Dispose();
                            }

                            LossMulticlassLogPerPixel.TrainOneStep(segTrainer, samples, labels);
                        }
                    }
                    catch (Exception)
                    {
                        stopDataLoaders();
                        throw;
                    }

                    // Training done, tell threads to stop and make sure to wait for them to finish before
                    // moving on.
                    stopDataLoaders();

                    // also wait for threaded processing to stop in the trainer.
                    segTrainer.GetNet();

                    segNet.Clean();

                    return segNet;
                }
            }
        }

        private static LossMmod TrainDetectionNetwork(IReadOnlyList<TruthImage> truthImages, uint detMiniBatchSize)
        {
            const double initialLearningRate = 0.1;
            const double weightDecay = 0.0001;
            const double momentum = 0.9;
            const double minDetectorWindowOverlapIou = 0.65;

            const int targetSize = 70;
            const int minTargetSize = 30;

            var boxes = ExtractMmodRectVectors(truthImages);
            using (var options = new MModOptions(boxes,
                                                 targetSize,
                                                 minTargetSize,
                                                 minDetectorWindowOverlapIou))
            using (var testBoxOverlap = new TestBoxOverlap(0.5, 0.9))
            {
                options.OverlapsIgnore = testBoxOverlap;

                var detNet = new LossMmod(options, 4);
                detNet.GetSubnet().GetLayerDetails().SetNumFilters(options.DetectorWindows.Count());

                using (var data = new Pipe<DetTrainingSample>(200))
                {
                    var function = new Action<object>(seed =>
                    {
                        using (var rnd = new Rand((ulong)seed))
                        using (var cropper = new RandomCropper())
                        {
                            cropper.SetSeed(0);
                            cropper.SetChipDims(350, 350);

                            // Usually you want to give the cropper whatever min sizes you passed to the
                            // mmod_options constructor, or very slightly smaller sizes, which is what we do here.
                            cropper.SetMinObjectSize(targetSize - 2, minTargetSize - 2);
                            cropper.MaxRotationDegrees = 2;

                            while (data.IsEnabled)
                            {
                                // Pick a random input image.
                                var randomIndex = rnd.GetRandom32BitNumber() % truthImages.Count();
                                var truthImage = truthImages[(int)randomIndex];

                                // Load the input image.
                                var inputImage = Dlib.LoadImageAsMatrix<RgbPixel>(truthImage.Info.ImageFilename);

                                // Get a random crop of the input.
                                var mmodRects = ExtractMmodRects(truthImage.TruthInstances);
                                cropper.Operator(inputImage, mmodRects, out var tmpInputImage, out var tmpMmodRects);

                                var temp = new DetTrainingSample
                                {
                                    InputImage = tmpInputImage,
                                    MmodRects = new StdVector<MModRect>(tmpMmodRects)
                                };

                                Dlib.DisturbColors(temp.InputImage, rnd);

                                // Push the result to be used by the trainer.
                                data.Enqueue(temp);
                            }
                        }

                    });

                    var threads = Enumerable.Range(1, 4).Select(i =>
                    {
                        var dataLoader = new Thread(new ParameterizedThreadStart(function))
                        {
                            Name = $"dataLoader{i}"
                        };
                        dataLoader.Start((ulong)i);
                        return dataLoader;
                    }).ToArray();

                    var stopDataLoaders = new Action(() =>
                    {
                        data.Disable();
                        foreach (var thread in threads)
                            thread.Join();
                    });

                    using (var sgd = new Sgd((float)weightDecay, (float)momentum))
                    using (var detTrainer = new DnnTrainer<LossMmod>(detNet, sgd))
                    {
                        try
                        {
                            detTrainer.BeVerbose();
                            detTrainer.SetLearningRate(initialLearningRate);
                            detTrainer.SetSynchronizationFile("pascal_voc2012_det_trainer_state_file.dat", 60 * 10);
                            detTrainer.SetIterationsWithoutProgressThreshold(5000);

                            // Output training parameters.
                            Console.WriteLine(detTrainer);

                            var samples = new List<Matrix<RgbPixel>>();
                            var labels = new List<List<MModRect>>();

                            // The main training loop.  Keep making mini-batches and giving them to the trainer.
                            // We will run until the learning rate becomes small enough.
                            while (detTrainer.GetLearningRate() >= 1e-4)
                            {
                                samples.DisposeElement();
                                labels.DisposeElement();
                                samples.Clear();
                                labels.Clear();

                                samples.Capacity = (int)detMiniBatchSize;
                                labels.Capacity = (int)detMiniBatchSize;

                                // make a mini-batch
                                while (samples.Count < detMiniBatchSize)
                                {
                                    data.Dequeue(out var temp);

                                    samples.Add(temp.InputImage);
                                    labels.Add(new List<MModRect>(temp.MmodRects.ToArray()));
                                }

                                LossMmod.TrainOneStep(detTrainer, samples, labels);
                            }
                        }
                        catch (Exception)
                        {
                            stopDataLoaders();
                            throw;
                        }

                        // Training done, tell threads to stop and make sure to wait for them to finish before
                        // moving on.
                        stopDataLoaders();

                        // also wait for threaded processing to stop in the trainer.
                        detTrainer.GetNet();

                        detNet.Clean();

                        return detNet;
                    }
                }
            }
        }

        #endregion

        #endregion

    }

}