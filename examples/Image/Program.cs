/*
 * This sample program is ported by C# from examples\image_ex.cpp.
*/

using System;
using DlibDotNet;

namespace Image
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(", you have to enter a BMP file as an argument to this program.");
                return;
            }

            // Here we declare an image object that can store rgb_pixels.  Note that in 
            // dlib there is no explicit image object, just a 2D array and
            // various pixel types.

            // Now load the image file into our image.  If something is wrong then
            // load_image() will throw an exception.  Also, if you linked with libpng
            // and libjpeg then load_image() can load PNG and JPEG files in addition
            // to BMP files.
            using (var img = Dlib.LoadImage<RgbPixel>(args[0]))
            {
                // Now let's use some image functions.  First let's blur the image a little.
                using (var blurredImg = new Array2D<byte>())
                {
                    Dlib.GaussianBlur(img, blurredImg);

                    // Now find the horizontal and vertical gradient images.
                    using (var horzGradient = new Array2D<short>())
                    using (var vertGradient = new Array2D<short>())
                    using (var edgeImage = new Array2D<byte>())
                    {
                        Dlib.SobelEdgeDetector(blurredImg, horzGradient, vertGradient);

                        // now we do the non-maximum edge suppression step so that our edges are nice and thin
                        Dlib.SuppressNonMaximumEdges(horzGradient, vertGradient, edgeImage);

                        // Now we would like to see what our images look like.  So let's use a 
                        // window to display them on the screen.  (Note that you can zoom into 
                        // the window by holding CTRL and scrolling the mouse wheel)
                        using (var myWindow = new ImageWindow(edgeImage, "Normal Edge Image"))
                        {
                            // We can also easily display the edge_image as a heatmap or using the jet color
                            // scheme like so.
                            using (var heatmap = Dlib.Heatmap(edgeImage))
                            using (var jet = Dlib.Jet(edgeImage))
                            using (var winHot = new ImageWindow(heatmap))
                            using (var winJet = new ImageWindow(jet))
                            {
                                // also make a window to display the original image
                                using (var myWindow2 = new ImageWindow(img, "Original Image"))
                                {
                                    // Sometimes you want to get input from the user about which pixels are important
                                    // for some task.  You can do this easily by trapping user clicks as shown below.
                                    // This loop executes every time the user double clicks on some image pixel and it
                                    // will terminate once the user closes the window.
                                    while (myWindow.GetNextDoubleClick(out var p))
                                    {
                                        Console.WriteLine($"User double clicked on pixel:         {p}");
                                        // ToDo
                                        //Console.WriteLine($"edge pixel value at this location is: {(int)edgeImage[p.y()][p.x()]}");
                                    }

                                    winHot.WaitUntilClosed();
                                    myWindow2.WaitUntilClosed();

                                    // Finally, note that you can access the elements of an image using the normal [row][column]
                                    // operator like so:
                                    // ToDo
                                    //Console.WriteLine(horzGradient[0][3]);
                                    Console.WriteLine($"number of rows in image:    {horzGradient.Rows}");
                                    Console.WriteLine($"number of columns in image: {horzGradient.Columns}");
                                }
                            }
                        }
                    }
                }
            }
        }

    }

}