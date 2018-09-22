/*
 * This sample program is ported by C# from examples\fhog_ex.cpp.
*/

using System;
using DlibDotNet;

namespace FHog
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            try
            {
                // Make sure the user entered an argument to this program.  It should be the
                // filename for an image.
                if (args.Length != 1)
                {
                    Console.WriteLine("error, you have to enter a BMP file as an argument to this program.");
                    return;
                }

                // Here we declare an image object that can store color rgb_pixels.  

                // Now load the image file into our image.  If something is wrong then
                // load_image() will throw an exception.  Also, if you linked with libpng
                // and libjpeg then load_image() can load PNG and JPEG files in addition
                // to BMP files.
                using (var img = Dlib.LoadImage<RgbPixel>(args[0]))
                {
                    // Now convert the image into a FHOG feature image.  The output, hog, is a 2D array
                    // of 31 dimensional vectors.
                    using (var hog = Dlib.ExtractFHogFeatures<float>(img))
                    {
                        Console.WriteLine($"hog image has {hog.Rows} rows and {hog.Columns} columns.");

                        // Let's see what the image and FHOG features look like.
                        using (var win = new ImageWindow(img))
                        using (var drawhog = Dlib.DrawFHog(hog))
                        using (var winhog = new ImageWindow(drawhog))
                        {
                            // Another thing you might want to do is map between the pixels in img and the
                            // cells in the hog image.  dlib provides the image_to_fhog() and fhog_to_image()
                            // routines for this.  Their use is demonstrated in the following loop which
                            // responds to the user clicking on pixels in the image img.
                            Point p;  // A 2D point, used to represent pixel locations.
                            while (win.GetNextDoubleClick(out p))
                            {
                                var hp = Dlib.ImgaeToFHog(p);
                                Console.WriteLine($"The point {p} in the input image corresponds to {hp} in hog space.");
                                var row = hog[hp.Y];
                                var column = row[hp.X];
                                var t = Dlib.Trans(column);
                               // Console.WriteLine($"FHOG features at this point: {t}");
                            }

                            // Finally, sometimes you want to get a planar representation of the HOG features
                            // rather than the explicit vector (i.e. interlaced) representation used above.  
                            var planar_hog = Dlib.ExtracFHogFeaturesArray<float>(img);
                            // Now we have an array of 31 float valued image planes, each representing one of
                            // the dimensions of the HOG feature vector.  
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"exception thrown: {e}");
            }
        }

    }

}
