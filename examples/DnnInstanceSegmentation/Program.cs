/*
 * This sample program is ported by C# from examples\dnn_instance_segmentation_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;

namespace DnnInstanceSegmentation
{

    internal class Program
    {

        #region Fields

        private const int SegDim = 227;

        private const string InstanceSegmentationNetFilename = "instance_segmentation_voc2012net.dnn";

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("You call this program like this: ");
                Console.WriteLine("./dnn_instance_segmentation_train_ex /path/to/images");
                Console.WriteLine();
                Console.WriteLine($"You will also need a trained '{InstanceSegmentationNetFilename}' file.");
                Console.WriteLine("You can either train it yourself (see example program");
                Console.WriteLine("dnn_instance_segmentation_train_ex), or download a");
                Console.WriteLine($"copy from here: http://dlib.net/files/{InstanceSegmentationNetFilename}");
                return;
            }

            try
            {
                // Read the file containing the trained network from the working directory.
                using (var deserialize = new ProxyDeserialize(InstanceSegmentationNetFilename))
                using (var detNet = LossMmod.Deserialize(deserialize, 4))
                {
                    var segNetsByClass = new Dictionary<string, LossMulticlassLogPerPixel>();
                    segNetsByClass.Deserialize(deserialize, 4);

                    // Show inference results in a window.
                    using (var win = new ImageWindow())
                    {
                        // Find supported image files.
                        var files = Directory.GetFiles(args[0])
                                             .Where(s => s.EndsWith(".jpeg") || s.EndsWith(".jpg") || s.EndsWith(".png")).ToArray();

                        using (var rnd = new Rand())
                        {
                            Console.WriteLine($"Found {files.Length} images, processing...");
                            foreach (var file in files.Select(s => new FileInfo(s)))
                            {
                                // Load the input image.
                                using (var inputImage = Dlib.LoadImageAsMatrix<RgbPixel>(file.FullName))
                                {
                                    // Create predictions for each pixel. At this point, the type of each prediction
                                    // is an index (a value between 0 and 20). Note that the net may return an image
                                    // that is not exactly the same size as the input.
                                    using (var output = detNet.Operator(inputImage))
                                    {
                                        var instances = output.First().ToList();
                                        instances.Sort((lhs, rhs) => (int)lhs.Rect.Area - (int)rhs.Rect.Area);

                                        using (var rgbLabelImage = new Matrix<RgbPixel>())
                                        {
                                            rgbLabelImage.SetSize(inputImage.Rows, inputImage.Columns);
                                            rgbLabelImage.Assign(Enumerable.Range(0, rgbLabelImage.Size).Select(i => new RgbPixel(0, 0, 0)).ToArray());

                                            var foundSomething = false;
                                            foreach (var instance in instances)
                                            {
                                                if (!foundSomething)
                                                {
                                                    Console.Write("Found ");
                                                    foundSomething = true;
                                                }
                                                else
                                                {
                                                    Console.Write(", ");
                                                }

                                                Console.Write(instance.Label);

                                                var croppingRect = GetCroppingRect(instance.Rect);
                                                using (var dims = new ChipDims(SegDim, SegDim))
                                                using (var chipDetails = new ChipDetails(croppingRect, dims))
                                                using (var inputChip = Dlib.ExtractImageChip<RgbPixel>(inputImage, chipDetails, InterpolationTypes.Bilinear))
                                                {
                                                    if (!segNetsByClass.TryGetValue(instance.Label, out var i))
                                                    {
                                                        // per-class segmentation net not found, so we must be using the same net for all classes
                                                        // (see bool separate_seg_net_for_each_class in dnn_instance_segmentation_train_ex.cpp)
                                                        if (segNetsByClass.Count == 1)
                                                            throw new ApplicationException();
                                                        if (string.IsNullOrEmpty(segNetsByClass.First().Key))
                                                            throw new ApplicationException();
                                                    }

                                                    var segNet = i != null
                                                               ? i // use the segmentation net trained for this class
                                                               : segNetsByClass.First().Value; // use the same segmentation net for all classes

                                                    using (var mask = segNet.Operator(inputChip))
                                                    {
                                                        var randomColor = new RgbPixel(
                                                            rnd.GetRandom8BitNumber(),
                                                            rnd.GetRandom8BitNumber(),
                                                            rnd.GetRandom8BitNumber()
                                                        );

                                                        using (var resizedMask = new Matrix<ushort>((int)chipDetails.Rect.Height, (int)chipDetails.Rect.Width))
                                                        {
                                                            Dlib.ResizeImage(mask.First(), resizedMask);

                                                            for (int r = 0, nr = resizedMask.Rows; r < nr; ++r)
                                                                for (int c = 0, nc = resizedMask.Columns; c < nc; ++c)
                                                                    if (resizedMask[r, c] != 0)
                                                                    {
                                                                        var y = (int)(chipDetails.Rect.Top + r);
                                                                        var x = (int)(chipDetails.Rect.Left + c);
                                                                        if (y >= 0 && y < rgbLabelImage.Rows && x >= 0 && x < rgbLabelImage.Columns)
                                                                            rgbLabelImage[y, x] = randomColor;
                                                                    }
                                                        }

                                                        var voc2012Class = PascalVOC2012.FindVoc2012Class(instance.Label);
                                                        Dlib.DrawRectangle(rgbLabelImage, instance.Rect, voc2012Class.RgbLabel, 1u);
                                                    }
                                                }
                                            }

                                            instances.DisposeElement();

                                            using (var tmp = Dlib.JoinRows(inputImage, rgbLabelImage))
                                            {
                                                // Show the input image on the left, and the predicted RGB labels on the right.
                                                win.SetImage(tmp);

                                                if (instances.Any())
                                                {
                                                    Console.Write($" in {file.Name} - hit enter to process the next image");
                                                    Console.ReadKey();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (var kvp in segNetsByClass) 
                        kvp.Value.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #region Helpers

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

        #endregion

        #endregion

    }

}