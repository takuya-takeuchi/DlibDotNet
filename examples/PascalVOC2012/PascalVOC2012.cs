using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet.Dnn;

namespace DlibDotNet
{

    public static class PascalVOC2012
    {

        #region Fields

        public const int ClassCount = 21; // background + 20 classes
        
        public static readonly Voc2012Class[] Classes =
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
            new Voc2012Class(20, new RgbPixel{ Red =   0, Green =  64, Blue = 128 }, "tvmonitor")
        };

        #endregion

        #region Methods

        // Read the list of image files belong to the "train" set of the PASCAL VOC2012 data.
        public static IEnumerable<ImageInfo> GetPascalVoc2012TrainListing(string voc2012Folder)
        {
            return GetPascalVoc2012Listing(voc2012Folder, "train");
        }

        // Read the list of image files belong to the "val" set of the PASCAL VOC2012 data.
        public static IEnumerable<ImageInfo> GetPascalVoc2012ValListing(string voc2012Folder)
        {
            return GetPascalVoc2012Listing(voc2012Folder, "val");
        }

        // Read the list of image files belonging to either the "train", "trainval", or "val" set
        // of the PASCAL VOC2012 data.
        public static IEnumerable<ImageInfo> GetPascalVoc2012Listing(string voc2012Folder,
            string file = "train" // "train", "trainval", or "val"
        )
        {
            var tst = Path.Combine(voc2012Folder, "ImageSets", "Segmentation", $"{file}.txt");
            var results = new List<ImageInfo>();

            using (var fs = new FileStream(tst, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    var basename = sr.ReadLine();
                    if (string.IsNullOrEmpty(basename))
                        continue;

                    var imageInfo = new ImageInfo
                    {
                        ImageFilename = Path.Combine(voc2012Folder, "JPEGImages", $"{basename}.jpg"),
                        ClassLabelFilename = Path.Combine(voc2012Folder, "SegmentationClass", $"{basename}.png"),
                        InstanceLabelFilename = Path.Combine(voc2012Folder, "SegmentationObject", $"{basename}.png")
                    };

                    results.Add(imageInfo);
                }
            }

            return results;
        }

        // Given an index in the range [0, 20], find the corresponding PASCAL VOC2012 class
        // (e.g., 'dog').
        public static Voc2012Class FindVoc2012Class(ushort indexLabel)
        {
            return FindVoc2012Class(@class => @class.Index == indexLabel);
        }

        public static Voc2012Class FindVoc2012Class(string instanceLabel)
        {
            return FindVoc2012Class(@class => @class.ClassLabel == instanceLabel);
        }

        // The PASCAL VOC2012 dataset contains 20 ground-truth classes + background.  Each class
        // is represented using an RGB color value.  We associate each class also to an index in the
        // range [0, 20], used internally by the network.  To convert the ground-truth data to
        // something that the network can efficiently digest, we need to be able to map the RGB
        // values to the corresponding indexes.
        // Given an RGB representation, find the corresponding PASCAL VOC2012 class
        // (e.g., 'dog').
        public static Voc2012Class FindVoc2012Class(RgbPixel rgbLabel)
        {
            return FindVoc2012Class(@class => rgbLabel == @class.RgbLabel);
        }

        // Convert an RGB class label to an index in the range [0, 20].
        public static ushort RgbLabelToIndexLabel(RgbPixel rgbLabel)
        {
            return PascalVOC2012.FindVoc2012Class(rgbLabel).Index;
        }

        // Convert an index in the range [0, 20] to a corresponding RGB class label.
        public static RgbPixel IndexLabelToRgbLabel(ushort indexLabel)
        {
            return FindVoc2012Class(indexLabel).RgbLabel;
        }

        // Convert an image containing RGB class labels to a corresponding
        // image containing indexes in the range [0, 20].
        public static void RgbLabelImageToIndexLabelImage(Matrix<RgbPixel> rgbLabelImage, Matrix<ushort> indexLabelImage)
        {
            var nr = rgbLabelImage.Rows;
            var nc = rgbLabelImage.Columns;

            indexLabelImage.SetSize(nr, nc);

            for (var r = 0; r < nr; ++r)
            for (var c = 0; c < nc; ++c)
                indexLabelImage[r, c] = RgbLabelToIndexLabel(rgbLabelImage[r, c]);
        }

        #region Helpers

        public static Voc2012Class FindVoc2012Class(Func<Voc2012Class, bool> predicate)
        {
            var i = Classes.FirstOrDefault(predicate);
            if (i != null)
                return i;

            throw new ApplicationException("Unable to find a matching VOC2012 class");
        }

        #endregion

        #endregion

    }

}