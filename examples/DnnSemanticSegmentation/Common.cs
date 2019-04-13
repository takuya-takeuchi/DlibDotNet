using System;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnSemanticSegmentation
{

    public sealed class Common
    {

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

        public static Voc2012Class FindVoc2012Class(Func<Voc2012Class, bool> predicate)
        {
            var i = Common.Classes.FirstOrDefault(predicate);
            if (i != null)
                return i;

            throw new ApplicationException("Unable to find a matching VOC2012 class");
        }

    }

}