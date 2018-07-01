/*
 * This sample program is ported by C# from examples\dnn_mmod_face_detection_ex.cpp.
*/

using System;
using DlibDotNet;

namespace DnnMmodFaceDetection
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Call this program like this:");
                Console.WriteLine("./dnn_mmod_face_detection_ex mmod_human_face_detector.dat faces/*.jpg");
                Console.WriteLine("You can get the mmod_human_face_detector.dat file from:");
                Console.WriteLine("http://dlib.net/files/mmod_human_face_detector.dat.bz2");
                return;
            }

            using (var net = DlibDotNet.Dnn.LossMmod.Deserialize(args[0]))
            {
                //image_window win;
                using (var win = new ImageWindow())
                    for (var index = 1; index < args.Length; index++)
                        using (var tmp = Dlib.LoadImage<RgbPixel>(args[index]))
                        using (var img = new Matrix<RgbPixel>(tmp))
                        {
                            // Upsampling the image will allow us to detect smaller faces but will cause the
                            // program to use more RAM and run longer.
                            while (img.Size < 1800 * 1800)
                                Dlib.PyramidUp(img);

                            // Note that you can process a bunch of images in a std::vector at once and it runs
                            // much faster, since this will form mini-batches of images and therefore get
                            // better parallelism out of your GPU hardware.  However, all the images must be
                            // the same size.  To avoid this requirement on images being the same size we
                            // process them individually in this example.
                            using (var dets = net.Operator(img))
                                foreach (var det in dets)
                                {
                                    win.ClearOverlay();
                                    win.SetImage(img);
                                    foreach (var d in det)
                                        win.AddOverlay(d);
                                }

                            Console.WriteLine("Hit enter to process the next image.");
                            Console.ReadKey();
                        }
            }
        }

    }

}