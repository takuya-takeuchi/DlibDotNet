/*
 * This sample program is ported by C# from examples\dnn_semantic_segmentation_ex.cpp.
*/

using System;
using System.IO;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnSemanticSegmentation
{

    internal class Program
    {

        public class Voc2012Class
        {

            #region Constructors

            public Voc2012Class(ushort index, RgbPixel rgbLabel, string classLabel)
            {
                this.Index = index;
                this.RgbLabel = rgbLabel;
                this.ClassLabel = classLabel;
            }

            #endregion

            #region Properties

            // The index of the class. In the PASCAL VOC 2012 dataset, indexes from 0 to 20 are valid.
            public ushort Index
            {
                get;
            }

            // The corresponding RGB representation of the class.
            public RgbPixel RgbLabel
            {
                get;
            }

            // The label of the class in plain text.
            public string ClassLabel
            {
                get;
            }

            #endregion

        }

        private const int ClassCount = 21; // background + 20 classes

        private static readonly Voc2012Class[] classes =
        {
            new Voc2012Class(0, new RgbPixel{ Red = 0, Green = 0, Blue = 0 }, ""), // background

            // The cream-colored `void' label is used in border regions and to mask difficult objects
            // (see http://host.robots.ox.ac.uk/pascal/VOC/voc2012/htmldoc/devkit_doc.html)
            new Voc2012Class(LossMulticlassLogPerPixel.LabelToIgnore, new RgbPixel{ Red =224, Green = 224, Blue = 192 }, "border"),

            new Voc2012Class(1,  new RgbPixel{ Red = 128, Green =   0, Blue =   0 }, "aeroplane"),
            new Voc2012Class(2,  new RgbPixel{ Red =   0, Green = 128, Blue =   0 }, "bicycle"),
            new Voc2012Class(3,  new RgbPixel{ Red = 128, Green = 128, Blue =   0 }, "bird"),
            new Voc2012Class(4,  new RgbPixel{ Red =   0, Green =   0, Blue = 128 }, "boat"),
            new Voc2012Class(5,  new RgbPixel{ Red = 128, Green =   0, Blue = 128 }, "bottle"),
            new Voc2012Class(6,  new RgbPixel{ Red =   0, Green = 128, Blue = 128 }, "bus"),
            new Voc2012Class(7,  new RgbPixel{ Red = 128, Green = 128, Blue = 128 }, "car"),
            new Voc2012Class(8,  new RgbPixel{ Red =  64, Green =   0, Blue =   0 }, "cat"),
            new Voc2012Class(9,  new RgbPixel{ Red = 192, Green =   0, Blue =   0 }, "chair"),
            new Voc2012Class(10, new RgbPixel{ Red =  64, Green = 128, Blue =   0 }, "cow"),
            new Voc2012Class(11, new RgbPixel{ Red = 192, Green = 128, Blue =   0 }, "diningtable"),
            new Voc2012Class(12, new RgbPixel{ Red =  64, Green =   0, Blue = 128 }, "dog"),
            new Voc2012Class(13, new RgbPixel{ Red = 192, Green =   0, Blue = 128 }, "horse"),
            new Voc2012Class(14, new RgbPixel{ Red =  64, Green = 128, Blue = 128 }, "motorbike"),
            new Voc2012Class(15, new RgbPixel{ Red = 192, Green = 128, Blue = 128 }, "person"),
            new Voc2012Class(16, new RgbPixel{ Red =   0, Green =  64, Blue =   0 }, "pottedplant"),
            new Voc2012Class(17, new RgbPixel{ Red = 128, Green =  64, Blue =   0 }, "sheep"),
            new Voc2012Class(18, new RgbPixel{ Red =   0, Green = 192, Blue =   0 }, "sofa"),
            new Voc2012Class(19, new RgbPixel{ Red = 128, Green = 192, Blue =   0 }, "train"),
            new Voc2012Class(20, new RgbPixel{ Red =   0, Green =  64, Blue = 128 }, "tvmonitor"),
        };

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

        // Given an index in the range [0, 20], find the corresponding PASCAL VOC2012 class
        // (e.g., 'dog').
        private static Voc2012Class find_voc2012_class(ushort indexLabel)
        {
            var i = classes.FirstOrDefault(@class => @class.Index == indexLabel);
            if (i != null)
                return i;

            throw new ApplicationException("Unable to find a matching VOC2012 class");
        }

        // Convert an index in the range [0, 20] to a corresponding RGB class label.
        private static RgbPixel IndexLabelToRgbLabel(ushort indexLabel)
        {
            return find_voc2012_class(indexLabel).RgbLabel;
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
                    rgbLabelImage[r, c] = IndexLabelToRgbLabel(indexLabelImage[r, c]);

            return rgbLabelImage;
        }

        // Find the most prominent class label from amongst the per-pixel predictions.
        private static string GetMostProminentNonBackgroundClassLabel(Matrix<ushort> indexLabelImage)
        {
            var nr = indexLabelImage.Rows;
            var nc = indexLabelImage.Columns;

            var counters = new uint[ClassCount];
            for (var r = 0; r < nr; ++r)
                for (var c = 0; c < nc; ++c)
                {
                    var label = indexLabelImage[r, c];
                    ++counters[label];
                }

            var maxValue = counters.ToList().GetRange(1, counters.Length - 1).Max();
            var mostProminentIndexLabel = counters.ToList().FindIndex(1, counters.Length - 1, u => u == maxValue);

            return find_voc2012_class((ushort)mostProminentIndexLabel).ClassLabel;
        }

    }

}