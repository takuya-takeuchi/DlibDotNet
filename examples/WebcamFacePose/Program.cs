/*
 * This sample program is ported by C# from examples\webcam_face_pose_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet;
using OpenCvSharp;

namespace WebcamFacePose
{

    internal class Program
    {

        private static void Main()
        {
            try
            {
                var cap = new VideoCapture(0);
                //var cap = new VideoCapture("20090124_WeeklyAddress.ogv.360p.webm");
                if (!cap.IsOpened())
                {
                    Console.WriteLine("Unable to connect to camera");
                    return;
                }

                using (var win = new ImageWindow())
                {
                    // Load face detection and pose estimation models.
                    using (var detector = Dlib.GetFrontalFaceDetector())
                    using (var poseModel = ShapePredictor.Deserialize("shape_predictor_68_face_landmarks.dat"))
                    {
                        // Grab and process frames until the main window is closed by the user.
                        while (!win.IsClosed())
                        {
                            // Grab a frame
                            var temp = new Mat();
                            if (!cap.Read(temp))
                            {
                                break;
                            }

                            // Turn OpenCV's Mat into something dlib can deal with.  Note that this just
                            // wraps the Mat object, it doesn't copy anything.  So cimg is only valid as
                            // long as temp is valid.  Also don't do anything to temp that would cause it
                            // to reallocate the memory which stores the image as that will make cimg
                            // contain dangling pointers.  This basically means you shouldn't modify temp
                            // while using cimg.
                            var array = new byte[temp.Width * temp.Height * temp.ElemSize()];
                            Marshal.Copy(temp.Data, array, 0 , array.Length);
                            using (var cimg = Dlib.LoadImageData<RgbPixel>(array, (uint)temp.Height, (uint)temp.Width, (uint)(temp.Width * temp.ElemSize())))
                            {
                                // Detect faces 
                                var faces = detector.Operator(cimg);
                                // Find the pose of each face.
                                var shapes = new List<FullObjectDetection>();
                                for (var i = 0; i < faces.Length; ++i)
                                {
                                    var det = poseModel.Detect(cimg, faces[i]);
                                    shapes.Add(det);
                                }

                                // Display it all on the screen
                                win.ClearOverlay();
                                win.SetImage(cimg);
                                var lines = Dlib.RenderFaceDetections(shapes);
                                win.AddOverlay(lines);

                                foreach (var line in lines)
                                    line.Dispose();
                            }
                        }
                    }
                }
            }
            //catch (serialization_error&e)
            //{
            //    cout << "You need dlib's default face landmarking model file to run this example." << endl;
            //    cout << "You can get it from the following URL: " << endl;
            //    cout << "   http://dlib.net/files/shape_predictor_68_face_landmarks.dat.bz2" << endl;
            //    cout << endl << e.what() << endl;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

}