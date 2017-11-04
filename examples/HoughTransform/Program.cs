/*
 * This sample program is ported by C# from examples\hough_transforom_ex.cpp.
*/

using System;
using DlibDotNet;

namespace HoughTransform
{

    internal class Program
    {

        private static void Main()
        {
            using (var img = new Array2D<byte>(400, 400))
            using (var ht = new DlibDotNet.HoughTransform(300))
            using (var win = new ImageWindow())
            using (var win2 = new ImageWindow())
            {
                var angle1 = 0d;
                var angle2 = 0d;

                while (true)
                {
                    angle1 += Math.PI / 130;
                    angle2 += Math.PI / 400;

                    using (var rect = img.Rect)
                    using (var cent = rect.Center)
                    using (var point1 = new Point(90, 0))
                    using (var tmp1 = cent + point1)
                    using (var arc = Point.Rotate(cent, tmp1, angle1 * 180 / Math.PI))
                    using (var point2 = new Point(500, 0))
                    using (var tmp2 = arc + point2)
                    using (var tmp3 = arc - point2)
                    using (var l = Point.Rotate(arc, tmp2, angle2 * 180 / Math.PI))
                    using (var r = Point.Rotate(arc, tmp3, angle2 * 180 / Math.PI))
                    {
                        Dlib.AssignAllPpixels(img, 0);
                        Dlib.DrawLine(img, l, r, 255);

                        using (var offset = new Point(50, 50))
                        using (var himg = new Array2D<int>())
                        using (var hrect = Dlib.GetRect(ht))
                        using (var box = Rectangle.Translate(hrect, offset))
                        {
                            // Now let's compute the hough transform for a subwindow in the image.  In
                            // particular, we run it on the 300x300 subwindow with an upper left corner at the
                            // pixel point(50,50).  The output is stored in himg.
                            ht.Operator(img, box, himg);

                            // Now that we have the transformed image, the Hough image pixel with the largest
                            // value should indicate where the line is.  So we find the coordinates of the
                            // largest pixel:
                            using (var mat = Dlib.Mat(himg))
                            using (var p = Dlib.MaxPoint(mat))
                            {
                                // And then ask the ht object for the line segment in the original image that
                                // corresponds to this point in Hough transform space.
                                using (var line = ht.GetLine(p))
                                {
                                    // Finally, let's display all these things on the screen.  We copy the original
                                    // input image into a color image and then draw the detected line on top in red.
                                    using (var temp = new Array2D<RgbPixel>())
                                    {
                                        Dlib.AssignImage(img, temp);

                                        using (var p1 = line.First + offset)
                                        using (var p2 = line.Second + offset)
                                        {
                                            Dlib.DrawLine(temp, p1, p2, new RgbPixel
                                            {
                                                Red = 255
                                            });
                                            win.ClearOverlay();
                                            win.SetImage(temp);

                                            // Also show the subwindow we ran the Hough transform on as a green box.  You will
                                            // see that the detected line is exactly contained within this box and also
                                            // overlaps the original line.
                                            win.AddOverlay(box, new RgbPixel
                                            {
                                                Green = 255
                                            });

                                            using (var jet = Dlib.Jet(himg))
                                                win2.SetImage(jet);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }

}
