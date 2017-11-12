/*
 * This sample program is ported by C# from examples\surf_ex.cpp.
*/

using System;
using System.Linq;
using DlibDotNet;

namespace Surf
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            try
            {
                // make sure the user entered an argument to this program
                if (args.Length != 1)
                {
                    Console.WriteLine("error, you have to enter a BMP file as an argument to this program");
                    return;
                }

                // Here we declare an image object that can store rgb_pixels.  Note that in dlib
                // there is no explicit image object, just a 2D array and various pixel types.  
                Array2D<RgbPixel> img;

                // Now load the image file into our image.  If something is wrong then load_image()
                // will throw an exception.  Also, if you linked with libpng and libjpeg then
                // load_image() can load PNG and JPEG files in addition to BMP files. 
                using (img = Dlib.LoadImage<RgbPixel>(args[0]))
                {
                    // Get SURF points from the image.  Note that get_surf_points() has some optional
                    // arguments that allow you to control the number of points you get back.  Here we
                    // simply take the default.
                    var sp = Dlib.GetSurfPoints(img).ToArray();
                    Console.WriteLine($"number of SURF points found: {sp.Length}");

                    if (sp.Length > 0)
                    {
                        // A surf_point object contains a lot of information describing each point.
                        // The most important fields are shown below:
                        Console.WriteLine($"center of first SURF point: {sp[0].P.Center}");
                        Console.WriteLine($"pyramid scale:     {sp[0].P.Scale}");
                        Console.WriteLine($"SURF descriptor: \n{sp[0].Des}");
                    }

                    // Create a window to display the input image and the SURF points.  (Note that
                    // you can zoom into the window by holding CTRL and scrolling the mouse wheel)
                    using (var myWindow = new ImageWindow(img))
                    {
                        Dlib.DrawSurfPoints(myWindow, sp);

                        // wait until the user closes the window before we let the program 
                        // terminate.
                        myWindow.WaitUntilClosed();
                    }

                    foreach (var surfPoint in sp)
                        surfPoint?.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"exception thrown: {e}");
            }
        }

    }

}
