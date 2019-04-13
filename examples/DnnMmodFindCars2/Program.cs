/*
 * This sample program is ported by C# from examples\dnn_mmod_find_cars2_ex.cpp.
*/

using System;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnMmodFindCars2
{

    internal class Program
    {

        private static void Main()
        {
            try
            {
                // You can get this file from http://dlib.net/files/mmod_front_and_rear_end_vehicle_detector.dat.bz2
                // This network was produced by the dnn_mmod_train_find_cars_ex.cpp example program.
                // As you can see, the file also includes a separately trained shape_predictor.  To see
                // a generic example of how to train those refer to train_shape_predictor_ex.cpp.
                using (var deserialize = new ProxyDeserialize("mmod_front_and_rear_end_vehicle_detector.dat"))
                using (var net = LossMmod.Deserialize(deserialize, 1))
                using (var sp = ShapePredictor.Deserialize(deserialize))
                using (var img = Dlib.LoadImageAsMatrix<RgbPixel>("mmod_cars_test_image2.jpg"))
                using (var win = new ImageWindow())
                {
                    win.SetImage(img);

                    // Run the detector on the image and show us the output.
                    var dets = net.Operator(img).First();
                    foreach (var d in dets)
                    {
                        // We use a shape_predictor to refine the exact shape and location of the detection
                        // box.  This shape_predictor is trained to simply output the 4 corner points of
                        // the box.  So all we do is make a rectangle that tightly contains those 4 points
                        // and that rectangle is our refined detection position.
                        var fd = sp.Detect(img, d);
                        var rect = Rectangle.Empty;
                        for (var j = 0u; j < fd.Parts; ++j)
                            rect += fd.GetPart(j);

                        if (d.Label == "rear")
                            win.AddOverlay(rect, new RgbPixel(255, 0, 0), d.Label);
                        else
                            win.AddOverlay(rect, new RgbPixel(255, 255, 0), d.Label);
                    }

                    Console.WriteLine("Hit enter to end program");
                    Console.ReadKey();
                }
            }
            catch (ImageLoadException ile)
            {
                Console.WriteLine(ile.Message);
                Console.WriteLine("The test image is located in the examples folder.  So you should run this program from a sub folder so that the relative path is correct.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }

}