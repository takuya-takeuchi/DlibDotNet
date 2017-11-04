/*
 * This sample program is ported by C# from examples\random_cropper_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;
using DlibDotNet.ImageProcessing;

namespace RandomCropper
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Give an image dataset XML file to run this program.");
                    Console.WriteLine("For example, if you are running from the examples folder then run this program by typing");
                    Console.WriteLine("   ./RandomCropper faces/training.xml");
                    return;
                }

                // First lets load a dataset
                IEnumerable<Matrix<RgbPixel>> images;
                IEnumerable<IEnumerable<MModRect>> boxes;
                Dlib.LoadImageDataset(args[0], out images, out boxes);

                // Here we make our random_cropper.  It has a number of options. 
                var cropper = new DlibDotNet.ImageTransforms.RandomCropper();
                // We can tell it how big we want the cropped images to be.
                cropper.ChipDims = new ChipDims(400, 400);
                // Also, when doing cropping, it will map the object annotations from the
                // dataset to the cropped image as well as perform random scale jittering.
                // You can tell it how much scale jittering you would like by saying "please
                // make the objects in the crops have a min and max size of such and such".
                // You do that by calling these two functions.  Here we are saying we want the
                // objects in our crops to be between 0.2*400 and 0.8*400 pixels in height.
                cropper.MinObjectSize = 0.2;
                cropper.MaxObjectSize = 0.8;
                // The cropper can also randomly mirror and rotate crops, which we ask it to
                // perform as well.
                cropper.RandomlyFlip = true;
                cropper.MaxRotationDegrees = 50;
                // This fraction of crops are from random parts of images, rather than being centered
                // on some object.
                cropper.BackgroundCropsFraction = 0.2;

                // Now ask the cropper to generate a bunch of crops.  The output is stored in
                // crops and crop_boxes.
                IEnumerable<Matrix<RgbPixel>> crops;
                IEnumerable<IEnumerable<MModRect>> cropBoxes;
                // Make 1000 crops.
                cropper.Operator(1000, images, boxes, out crops, out cropBoxes);

                // Finally, lets look at the results
                var cropList = crops?.ToArray() ?? new Matrix<RgbPixel>[0];
                var cropBoxesList = cropBoxes?.ToArray() ?? new IEnumerable<MModRect>[0];
                using (var win = new ImageWindow())
                    for (var i = 0; i < cropList.Count(); ++i)
                    {
                        win.ClearOverlay();
                        win.SetImage(cropList[i]);
                        foreach (var b in cropBoxesList[i])
                        {
                            // Note that mmod_rect has an ignore field.  If an object was labeled
                            // ignore in boxes then it will still be labeled as ignore in
                            // crop_boxes.  Moreover, objects that are not well contained within
                            // the crop are also set to ignore.
                            using (var rect = b.Rect)
                                if (b.Ignore)
                                    win.AddOverlay(rect, new RgbPixel { Red = 255, Blue = 255 }); // draw ignored boxes as orange 
                                else
                                    win.AddOverlay(rect, new RgbPixel { Red = 255 });   // draw other boxes as red
                        }

                        Console.WriteLine("Hit enter to view the next random crop.");
                        Console.ReadKey();
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }

}
