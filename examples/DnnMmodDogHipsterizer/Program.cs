/*
 * This sample program is ported by C# from examples\dnn_mmod_dog_hipsterizer.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnMmodDogHipsterizer
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Call this program like this:");
                    Console.WriteLine("./dnn_mmod_dog_hipsterizer mmod_dog_hipsterizer.dat faces/dogs.jpg");
                    Console.WriteLine("You can get the mmod_dog_hipsterizer.dat file from:");
                    Console.WriteLine("http://dlib.net/files/mmod_dog_hipsterizer.dat.bz2");
                    return;
                }

                // load the models as well as glasses and mustache.
                using (var deserialize = new ProxyDeserialize(args[0]))
                using (var net = LossMmod.Deserialize(deserialize))
                using (var sp = ShapePredictor.Deserialize(deserialize))
                using (var glasses = Matrix<RgbAlphaPixel>.Deserialize(deserialize))
                using (var mustache = Matrix<RgbAlphaPixel>.Deserialize(deserialize))
                {
                    Dlib.PyramidUp(glasses);
                    Dlib.PyramidUp(mustache);

                    using (var win1 = new ImageWindow(glasses))
                    using (var win2 = new ImageWindow(mustache))
                    using (var winWireframe = new ImageWindow())
                    using (var winHipster = new ImageWindow())
                    {
                        // Now process each image, find dogs, and hipsterize them by drawing glasses and a
                        // mustache on each dog :)
                        for (var i = 1; i < args.Length; ++i)
                        {
                            using (var tmp = Dlib.LoadImage<RgbPixel>(args[i]))
                            using (var img = new Matrix<RgbPixel>(tmp))
                            {

                                // Upsampling the image will allow us to find smaller dog faces but will use more
                                // computational resources.
                                //pyramid_up(img);
                                var dets = net.Operator(img).First();
                                winWireframe.ClearOverlay();
                                winWireframe.SetImage(img);

                                // We will also draw a wireframe on each dog's face so you can see where the
                                // shape_predictor is identifying face landmarks.
                                var lines = new List<ImageWindow.OverlayLine>();
                                foreach (var d in dets)
                                {
                                    // get the landmarks for this dog's face
                                    var shape = sp.Detect(img, d.Rect);

                                    var color = new RgbPixel(0, 255, 0);
                                    var top = shape.GetPart(0);
                                    var leftEar = shape.GetPart(1);
                                    var leftEye = shape.GetPart(2);
                                    var nose = shape.GetPart(3);
                                    var rightEar = shape.GetPart(4);
                                    var rightEye = shape.GetPart(5);

                                    // The locations of the left and right ends of the mustache.
                                    var leftMustache = 1.3 * (leftEye - rightEye) / 2 + nose;
                                    var rightMustache = 1.3 * (rightEye - leftEye) / 2 + nose;

                                    // Draw the glasses onto the image.
                                    var from = new[] { 2 * new Point(176, 36), 2 * new Point(59, 35) };
                                    var to = new[] { leftEye, rightEye };
                                    using (var transform = Dlib.FindSimilarityTransform(from, to))
                                        for (uint r = 0, nr = (uint)glasses.Rows; r < nr; ++r)
                                            for (uint c = 0, nc = (uint)glasses.Columns; c < nc; ++c)
                                            {
                                                var p = (Point)transform.Operator(new DPoint(c, r));
                                                if (Dlib.GetRect(img).Contains(p))
                                                {
                                                    var rgb = img[p.Y, p.X];
                                                    Dlib.AssignPixel(ref rgb, glasses[(int)r, (int)c]);
                                                    img[p.Y, p.X] = rgb;
                                                }
                                            }

                                    // Draw the mustache onto the image right under the dog's nose.
                                    var mustacheRect = Dlib.GetRect(mustache);
                                    from = new[] { mustacheRect.TopLeft, mustacheRect.TopRight };
                                    to = new[] { rightMustache, leftMustache };
                                    using (var transform = Dlib.FindSimilarityTransform(from, to))
                                        for (uint r = 0, nr = (uint)mustache.Rows; r < nr; ++r)
                                        for (uint c = 0, nc = (uint)mustache.Columns; c < nc; ++c)
                                            {
                                                var p = (Point)transform.Operator(new DPoint(c, r));
                                                if (Dlib.GetRect(img).Contains(p))
                                                {
                                                    var rgb = img[p.Y, p.X];
                                                    Dlib.AssignPixel(ref rgb, mustache[(int)r, (int)c]);
                                                    img[p.Y, p.X] = rgb;
                                                }
                                            }

                                    // Record the lines needed for the face wire frame.
                                    lines.Add(new ImageWindow.OverlayLine(leftEye, nose, color));
                                    lines.Add(new ImageWindow.OverlayLine(nose, rightEye, color));
                                    lines.Add(new ImageWindow.OverlayLine(rightEye, leftEye, color));
                                    lines.Add(new ImageWindow.OverlayLine(rightEye, rightEar, color));
                                    lines.Add(new ImageWindow.OverlayLine(rightEar, top, color));
                                    lines.Add(new ImageWindow.OverlayLine(top, leftEar, color));
                                    lines.Add(new ImageWindow.OverlayLine(leftEar, leftEye, color));

                                    winWireframe.AddOverlay(lines);
                                    winHipster.SetImage(img);
                                }

                                Console.WriteLine("Hit enter to process the next image.");
                                Console.ReadKey();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}