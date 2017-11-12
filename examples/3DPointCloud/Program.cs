/*
 * This sample program is ported by C# from examples\3d_point_cloud_ex.cpp.
*/

using System;
using System.Collections.Generic;
using DlibDotNet;

namespace _3DPointCloud
{

    internal class Program
    {

        private static void Main()
        {
            // Let's make a point cloud that looks like a 3D spiral.
            var points = new List<PerspectiveWindow.OverlayDot>();
            using (var rnd = new Rand())
                for (double i = 0; i < 20; i += 0.001)
                {
                    // Get a point on a spiral
                    using (var val = new Vector<double>(Math.Sin(i), Math.Cos(i), i / 4))
                    {
                        // Now add some random noise to it
                        var x = rnd.GetRandomGaussian();
                        var y = rnd.GetRandomGaussian();
                        var z = rnd.GetRandomGaussian();
                        using (var temp = new Vector<double>(x, y, z))
                        using (var val1 = val + (temp / 20))
                        {
                            // Pick a color based on how far we are along the spiral
                            var color = Dlib.ColormapJet(i, 0, 20);

                            // And add the point to the list of points we will display
                            points.Add(new PerspectiveWindow.OverlayDot(val1, color));
                        }
                    }
                }

            // Now finally display the point cloud.
            using (var win = new PerspectiveWindow())
            {
                win.Title = "perspective_window 3D point cloud";
                win.AddOverlay(points);
                win.WaitUntilClosed();
            }
        }

    }

}