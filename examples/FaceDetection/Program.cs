/*
 * This sample program is ported by C# from examples\face_detection_ex.cpp.
*/

using System;
using DlibDotNet;

namespace FaceDetection
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Give some image files as arguments to this program.");
                return;
            }

            using (var win = new ImageWindow())
            using (var detector = Dlib.GetFrontalFaceDetector())
                foreach (var file in args)
                    using (var img = Dlib.LoadImage<byte>(file))
                    {
                        Dlib.PyramidUp(img);

                        var dets = detector.Detect(img);
                        Console.WriteLine($"Number of faces detected: {dets.Length}");

                        win.ClearOverlay();
                        win.SetImage(img);
                        win.AddOverlay(dets, new RgbPixel { Red = 255 });

                        Console.WriteLine("hit enter to process next frame");
                        Console.ReadKey();
                    }
        }

    }

}