/*
 * This sample program is ported by C# from examples\face_landmark_detection.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;

namespace FaceLandmarkDetection
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Give some image files as arguments to this program.");
                Console.WriteLine("Call this program like this:");
                Console.WriteLine("./face_landmark_detection_ex shape_predictor_68_face_landmarks.dat faces/*.jpg");
                Console.WriteLine("You can get the shape_predictor_68_face_landmarks.dat file from:");
                Console.WriteLine("http://dlib.net/files/shape_predictor_68_face_landmarks.dat.bz2");
                return;
            }

            using (var win = new ImageWindow())
            using (var winFaces = new ImageWindow())
            using (var detector = FrontalFaceDetector.GetFrontalFaceDetector())
            using (var sp = new ShapePredictor(args[0]))
                foreach (var file in args.ToList().GetRange(1, args.Length - 1))
                {
                    Console.WriteLine($"processing image {file}");

                    using (var img = Dlib.LoadImage<RgbPixel>(file))
                    {
                        Dlib.PyramidUp(img);

                        var dets = detector.Detect(img);
                        Console.WriteLine($"Number of faces detected: {dets.Length}");

                        var shapes = new List<FullObjectDetection>();
                        foreach (var rect in dets)
                        {
                            var shape = sp.Detect(img, rect);
                            Console.WriteLine($"number of parts: {shape.Parts}");
                            if (shape.Parts > 2)
                            {
                                Console.WriteLine($"pixel position of first part:  {shape.GetPart(0)}");
                                Console.WriteLine($"pixel position of second part: {shape.GetPart(1)}");
                                shapes.Add(shape);
                            }
                        }

                        win.ClearOverlay();
                        win.SetImage(img);

                        if (shapes.Any())
                        {
                            var lines = Dlib.RenderFaceDetections(shapes);
                            win.AddOverlay(lines);

                            foreach (var l in lines)
                                l.Dispose();

                            var chipLocations = Dlib.GetFaceChipDetails(shapes);
                            using (var faceChips = Dlib.ExtractImageChips<RgbPixel>(img, chipLocations))
                            using (var tileImage = Dlib.TileImages(faceChips))
                                winFaces.SetImage(tileImage);

                            foreach (var c in chipLocations)
                                c.Dispose();
                        }

                        Console.WriteLine("hit enter to process next frame");
                        Console.ReadKey();

                        foreach (var s in shapes)
                            s.Dispose();
                    }
                }
        }

    }

}