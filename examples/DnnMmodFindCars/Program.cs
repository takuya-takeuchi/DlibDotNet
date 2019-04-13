/*
 * This sample program is ported by C# from examples\dnn_mmod_find_cars_ex.cpp.
*/

using System;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnMmodFindCars
{

    internal class Program
    {

        private static void Main()
        {
            try
            {
                // You can get this file from http://dlib.net/files/mmod_rear_end_vehicle_detector.dat.bz2
                // This network was produced by the dnn_mmod_train_find_cars_ex.cpp example program.
                // As you can see, the file also includes a separately trained shape_predictor.  To see
                // a generic example of how to train those refer to train_shape_predictor_ex.cpp.
                using (var deserialize = new ProxyDeserialize("mmod_rear_end_vehicle_detector.dat"))
                using (var net = LossMmod.Deserialize(deserialize, 1))
                using (var sp = ShapePredictor.Deserialize(deserialize))
                using (var img = Dlib.LoadImageAsMatrix<RgbPixel>("mmod_cars_test_image.jpg"))
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

                        win.AddOverlay(rect, new RgbPixel(255, 0, 0));
                    }



                    Console.WriteLine("Hit enter to view the intermediate processing steps");
                    Console.ReadKey();


                    // Now let's look at how the detector works.  The high level processing steps look like:
                    //   1. Create an image pyramid and pack the pyramid into one big image.  We call this
                    //      image the "tiled pyramid".
                    //   2. Run the tiled pyramid image through the CNN.  The CNN outputs a new image where
                    //      bright pixels in the output image indicate the presence of cars.  
                    //   3. Find pixels in the CNN's output image with a value > 0.  Those locations are your
                    //      preliminary car detections.  
                    //   4. Perform non-maximum suppression on the preliminary detections to produce the
                    //      final output.
                    //
                    // We will be plotting the images from steps 1 and 2 so you can visualize what's
                    // happening.  For the CNN's output image, we will use the jet colormap so that "bright"
                    // outputs, i.e. pixels with big values, appear in red and "dim" outputs appear as a
                    // cold blue color.  To do this we pick a range of CNN output values for the color
                    // mapping.  The specific values don't matter.  They are just selected to give a nice
                    // looking output image.
                    const float lower = -2.5f;
                    const float upper = 0.0f;
                    Console.WriteLine($"jet color mapping range:  lower={lower}  upper={upper}");



                    // Create a tiled pyramid image and display it on the screen.
                    // Get the type of pyramid the CNN used
                    //using pyramid_type = std::remove_reference < decltype(input_layer(net)) >::type::pyramid_type;
                    // And tell create_tiled_pyramid to create the pyramid using that pyramid type.
                    using (var inputLayer = new InputRgbImagePyramid<PyramidDown>(6))
                    {
                        net.TryGetInputLayer(inputLayer);

                        var padding = inputLayer.GetPyramidPadding();
                        var outerPadding = inputLayer.GetPyramidOuterPadding();
                        Dlib.CreateTiledPyramid<RgbPixel, PyramidDown>(img,
                                                                       padding,
                                                                       outerPadding,
                                                                       6,
                                                                       out var tiledImg,
                                                                       out var rects);

                        using (var winpyr = new ImageWindow(tiledImg, "Tiled pyramid"))
                        {
                            // This CNN detector represents a sliding window detector with 3 sliding windows.  Each
                            // of the 3 windows has a different aspect ratio, allowing it to find vehicles which
                            // are either tall and skinny, squarish, or short and wide.  The aspect ratio of a
                            // detection is determined by which channel in the output image triggers the detection.
                            // Here we are just going to max pool the channels together to get one final image for
                            // our display.  In this image, a pixel will be bright if any of the sliding window
                            // detectors thinks there is a car at that location.
                            using (var subnet = net.GetSubnet())
                            {
                                var output = subnet.Output;
                                Console.WriteLine($"Number of channels in final tensor image: {output.K}");
                                var networkOutput = Dlib.ImagePlane(output);
                                for (var k = 1; k < output.K; k++)
                                    using (var tmpNetworkOutput = Dlib.ImagePlane(output, 0, k))
                                    {
                                        var maxPointWise = Dlib.MaxPointWise(networkOutput, tmpNetworkOutput);
                                        networkOutput.Dispose();
                                        networkOutput = maxPointWise;
                                    }

                                // We will also upsample the CNN's output image.  The CNN we defined has an 8x
                                // downsampling layer at the beginning. In the code below we are going to overlay this
                                // CNN output image on top of the raw input image.  To make that look nice it helps to
                                // upsample the CNN output image back to the same resolution as the input image, which
                                // we do here.
                                var networkOutputScale = img.Columns / (double)networkOutput.Columns;
                                Dlib.ResizeImage(networkOutput, networkOutputScale);


                                // Display the network's output as a color image.
                                using (var jet = Dlib.Jet(networkOutput, upper, lower))
                                using (var winOutput = new ImageWindow(jet, "Output tensor from the network"))
                                {


                                    // Also, overlay network_output on top of the tiled image pyramid and display it.
                                    for (var r = 0; r < tiledImg.Rows; ++r)
                                        for (var c = 0; c < tiledImg.Columns; ++c)
                                        {
                                            var tmp = new DPoint(c, r);
                                            tmp = Dlib.InputTensorToOutputTensor(net, tmp);
                                            var dp = networkOutputScale * tmp;
                                            tmp = new DPoint((int)dp.X, (int)dp.Y);
                                            if (Dlib.GetRect(networkOutput).Contains((int)tmp.X, (int)tmp.Y))
                                            {
                                                var val = networkOutput[(int)tmp.Y, (int)tmp.X];

                                                // alpha blend the network output pixel with the RGB image to make our
                                                // overlay.
                                                var p = new RgbAlphaPixel();
                                                Dlib.AssignPixel(ref p, Dlib.ColormapJet(val, lower, upper));
                                                p.Alpha = 120;

                                                var rgb = new RgbPixel();
                                                Dlib.AssignPixel(ref rgb, p);
                                                tiledImg[r, c] = rgb;
                                            }
                                        }

                                    // If you look at this image you can see that the vehicles have bright red blobs on
                                    // them.  That's the CNN saying "there is a car here!".  You will also notice there is
                                    // a certain scale at which it finds cars.  They have to be not too big or too small,
                                    // which is why we have an image pyramid.  The pyramid allows us to find cars of all
                                    // scales.
                                    using (var winPyrOverlay = new ImageWindow(tiledImg, "Detection scores on image pyramid"))
                                    {




                                        // Finally, we can collapse the pyramid back into the original image.  The CNN doesn't
                                        // actually do this step, since it's enough to threshold the tiled pyramid image to get
                                        // the detections.  However, it makes a nice visualization and clearly indicates that
                                        // the detector is firing for all the cars.
                                        using (var collapsed = new Matrix<float>(img.Rows, img.Columns))
                                        using (var inputTensor = new ResizableTensor())
                                        {
                                            inputLayer.ToTensor(img, 1, inputTensor);
                                            for (var r = 0; r < collapsed.Rows; ++r)
                                                for (var c = 0; c < collapsed.Columns; ++c)
                                                {
                                                    // Loop over a bunch of scale values and look up what part of network_output
                                                    // corresponds to the point(c,r) in the original image, then take the max
                                                    // detection score over all the scales and save it at pixel point(c,r).
                                                    var maxScore = -1e30f;
                                                    for (double scale = 1; scale > 0.2; scale *= 5.0 / 6.0)
                                                    {
                                                        // Map from input image coordinates to tiled pyramid coordinates.
                                                        var tensorSpace = inputLayer.ImageSpaceToTensorSpace(inputTensor, scale, new DRectangle(new DPoint(c, r)));
                                                        var tmp = tensorSpace.Center;

                                                        // Now map from pyramid coordinates to network_output coordinates.
                                                        var dp = networkOutputScale * Dlib.InputTensorToOutputTensor(net, tmp);
                                                        tmp = new DPoint((int)dp.X, (int)dp.Y);

                                                        if (Dlib.GetRect(networkOutput).Contains((int)tmp.X, (int)tmp.Y))
                                                        {
                                                            var val = networkOutput[(int)tmp.Y, (int)tmp.X];
                                                            if (val > maxScore)
                                                                maxScore = val;
                                                        }
                                                    }

                                                    collapsed[r, c] = maxScore;

                                                    // Also blend the scores into the original input image so we can view it as
                                                    // an overlay on the cars.
                                                    var p = new RgbAlphaPixel();
                                                    Dlib.AssignPixel(ref p, Dlib.ColormapJet(maxScore, lower, upper));
                                                    p.Alpha = 120;

                                                    var rgb = new RgbPixel();
                                                    Dlib.AssignPixel(ref rgb, p);
                                                    img[r, c] = rgb;
                                                }

                                            using (var jet2 = Dlib.Jet(collapsed, upper, lower))
                                            using (var winCollapsed = new ImageWindow(jet2, "Collapsed output tensor from the network"))
                                            using (var winImgAndSal = new ImageWindow(img, "Collapsed detection scores on raw image"))
                                            {
                                                Console.WriteLine("Hit enter to end program");
                                                Console.ReadKey();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
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