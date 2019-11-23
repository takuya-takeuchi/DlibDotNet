/*
 * This sample program is ported by C# from examples\dnn_semantic_segmentation_ex.cpp.
*/

using System;
using System.IO;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnSemanticSegmentationOld
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("You call this program like this: ");
                Console.WriteLine("./dnn_semantic_segmentation_train_ex /path/to/images");
                Console.WriteLine();
                Console.WriteLine("You will also need a trained 'semantic_segmentation_voc2012net.dnn' file.");
                Console.WriteLine("You can either train it yourself (see example program");
                Console.WriteLine("dnn_semantic_segmentation_train_ex), or download a");
                Console.WriteLine("copy from here: http://dlib.net/files/semantic_segmentation_voc2012net.dnn");
                return;
            }

            try
            {
                // Read the file containing the trained network from the working directory.
                using (var net = LossMulticlassLogPerPixel.Deserialize("semantic_segmentation_voc2012net.dnn"))
                {
                    // Show inference results in a window.
                    using (var win = new ImageWindow())
                    {
                        // Find supported image files.
                        var files = Directory.GetFiles(args[0])
                                             .Where(s => s.EndsWith(".jpeg") || s.EndsWith(".jpg") || s.EndsWith(".png")).ToArray();
                        Console.WriteLine($"Found {files.Length} images, processing...");
                        foreach (var file in files)
                        {
                            // Load the input image.
                            using (var inputImage = Dlib.LoadImageAsMatrix<RgbPixel>(file))
                            {
                                // Create predictions for each pixel. At this point, the type of each prediction
                                // is an index (a value between 0 and 20). Note that the net may return an image
                                // that is not exactly the same size as the input.
                                using (var output = net.Operator(inputImage))
                                using (var temp = output.First())
                                {
                                    // Crop the returned image to be exactly the same size as the input.
                                    var rect = Rectangle.CenteredRect((int)(temp.Columns / 2d), (int)(temp.Rows / 2d), (uint)inputImage.Columns, (uint)inputImage.Rows);
                                    using (var dims = new ChipDims((uint)inputImage.Rows, (uint)inputImage.Columns))
                                    using (var chipDetails = new ChipDetails(rect, dims))
                                    using (var indexLabelImage = Dlib.ExtractImageChip<ushort>(temp, chipDetails, InterpolationTypes.NearestNeighbor))
                                    {
                                        // Convert the indexes to RGB values.
                                        using (var rgbLabelImage = IndexLabelImageToRgbLabelImage(indexLabelImage))
                                        {
                                            // Show the input image on the left, and the predicted RGB labels on the right.
                                            using (var joinedRow = Dlib.JoinRows(inputImage, rgbLabelImage))
                                            {
                                                win.SetImage(joinedRow);

                                                // Find the most prominent class label from amongst the per-pixel predictions.
                                                var classLabel = GetMostProminentNonBackgroundClassLabel(indexLabelImage);

                                                Console.WriteLine($"{file} : {classLabel} - hit enter to process the next image");
                                                Console.ReadKey();
                                            }
                                        }
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

        // Convert an image containing indexes in the range [0, 20] to a corresponding
        // image containing RGB class labels.
        private static Matrix<RgbPixel> IndexLabelImageToRgbLabelImage(Matrix<ushort> indexLabelImage)
        {
            var nr = indexLabelImage.Rows;
            var nc = indexLabelImage.Columns;

            var rgbLabelImage = new Matrix<RgbPixel>(nr, nc);
            for (var r = 0; r < nr; ++r)
                for (var c = 0; c < nc; ++c)
                    rgbLabelImage[r, c] = PascalVOC2012.IndexLabelToRgbLabel(indexLabelImage[r, c]);

            return rgbLabelImage;
        }

        // Find the most prominent class label from amongst the per-pixel predictions.
        private static string GetMostProminentNonBackgroundClassLabel(Matrix<ushort> indexLabelImage)
        {
            var nr = indexLabelImage.Rows;
            var nc = indexLabelImage.Columns;

            var counters = new uint[PascalVOC2012.ClassCount];
            for (var r = 0; r < nr; ++r)
                for (var c = 0; c < nc; ++c)
                {
                    var label = indexLabelImage[r, c];
                    ++counters[label];
                }

            var maxValue = counters.ToList().GetRange(1, counters.Length - 1).Max();
            var mostProminentIndexLabel = counters.ToList().FindIndex(1, counters.Length - 1, u => u == maxValue);

            return PascalVOC2012.FindVoc2012Class((ushort)mostProminentIndexLabel).ClassLabel;
        }

    }

}