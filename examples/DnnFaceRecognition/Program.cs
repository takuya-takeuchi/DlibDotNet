/*
 * This sample program is ported by C# from examples\dnn_face_recognition_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;

namespace DnnFaceRecognition
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Run this example by invoking it like this: ");
                Console.WriteLine("   ./DnnFaceRecognition faces/bald_guys.jpg");
                Console.WriteLine("You will also need to get the face landmarking model file as well as ");
                Console.WriteLine("the face recognition model file.  Download and then decompress these files from: ");
                Console.WriteLine("http://dlib.net/files/shape_predictor_5_face_landmarks.dat.bz2");
                Console.WriteLine("http://dlib.net/files/dlib_face_recognition_resnet_model_v1.dat.bz2");
                return;
            }

            // The first thing we are going to do is load all our models.  First, since we need to
            // find faces in the image we will need a face detector:
            using (var detector = FrontalFaceDetector.GetFrontalFaceDetector())
            // We will also use a face landmarking model to align faces to a standard pose:  (see face_landmark_detection_ex.cpp for an introduction)
            using (var sp = new ShapePredictor("shape_predictor_5_face_landmarks.dat"))
            // And finally we load the DNN responsible for face recognition.
            using (var net = DlibDotNet.Dnn.LossMetric.Deserialize("dlib_face_recognition_resnet_model_v1.dat"))

            using (var img = Dlib.LoadImage<RgbPixel>(args[0]))
            using (var mat = new Matrix<RgbPixel>(img))

            // Display the raw image on the screen
            using (var win = new ImageWindow(img))
            {
                // Run the face detector on the image of our action heroes, and for each face extract a
                // copy that has been normalized to 150x150 pixels in size and appropriately rotated
                // and centered.
                var faces = new List<Matrix<RgbPixel>>();
                foreach (var face in detector.Detect(img))
                {
                    var shape = sp.Detect(img, face);
                    var faceChipDetail = Dlib.GetFaceChipDetails(shape, 150, 0.25);
                    var faceChip = Dlib.ExtractImageChip<RgbPixel>(mat, faceChipDetail);

                    //faces.Add(move(face_chip));
                    faces.Add(faceChip);

                    // Also put some boxes on the faces so we can see that the detector is finding
                    // them.
                    win.AddOverlay(face);
                }

                if (!faces.Any())
                {
                    Console.WriteLine("No faces found in image!");
                    return;
                }

                // This call asks the DNN to convert each face image in faces into a 128D vector.
                // In this 128D vector space, images from the same person will be close to each other
                // but vectors from different people will be far apart.  So we can use these vectors to
                // identify if a pair of images are from the same person or from different people.  
                var faceDescriptors = net.Operator(faces);

                // In particular, one simple thing we can do is face clustering.  This next bit of code
                // creates a graph of connected faces and then uses the Chinese whispers graph clustering
                // algorithm to identify how many people there are and which faces belong to whom.
                var edges = new List<SamplePair>();
                for (uint i = 0; i < faceDescriptors.Length; ++i)
                {
                    for (var j = i; j < faceDescriptors.Length; ++j)
                    {
                        // Faces are connected in the graph if they are close enough.  Here we check if
                        // the distance between two face descriptors is less than 0.6, which is the
                        // decision threshold the network was trained to use.  Although you can
                        // certainly use any other threshold you find useful.
                        var diff = faceDescriptors[i] - faceDescriptors[j];
                        if (Dlib.Length(diff) < 0.6)
                            edges.Add(new SamplePair(i, j));
                    }
                }

                Dlib.ChineseWhispers(edges, 100, out var numClusters, out var labels);

                // This will correctly indicate that there are 4 people in the image.
                Console.WriteLine($"number of people found in the image: {numClusters}");

                // Now let's display the face clustering results on the screen.  You will see that it
                // correctly grouped all the faces. 
                var winClusters = new List<ImageWindow>();
                for (var i = 0; i < numClusters; i++)
                    winClusters.Add(new ImageWindow());
                var tileImages = new List<Matrix<RgbPixel>>();
                for (var clusterId = 0ul; clusterId < numClusters; ++clusterId)
                {
                    var temp = new List<Matrix<RgbPixel>>();
                    for (var j = 0; j < labels.Length; ++j)
                    {
                        if (clusterId == labels[j])
                            temp.Add(faces[j]);
                    }

                    winClusters[(int)clusterId].Title = $"face cluster {clusterId}";
                    var tileImage = Dlib.TileImages(temp);
                    tileImages.Add(tileImage);
                    winClusters[(int)clusterId].SetImage(tileImage);
                }

                // Finally, let's print one of the face descriptors to the screen.
                using (var trans = Dlib.Trans(faceDescriptors[0]))
                {
                    Console.WriteLine($"face descriptor for one face: {trans}");

                    // It should also be noted that face recognition accuracy can be improved if jittering
                    // is used when creating face descriptors.  In particular, to get 99.38% on the LFW
                    // benchmark you need to use the jitter_image() routine to compute the descriptors,
                    // like so:
                    var jitterImages = JitterImage(faces[0]).ToArray();
                    var ret = net.Operator(jitterImages);
                    using (var m = Dlib.Mat(ret))
                    using (var faceDescriptor = Dlib.Mean<float>(m))
                    using (var t = Dlib.Trans(faceDescriptor))
                    {
                        Console.WriteLine($"jittered face descriptor for one face: {t}");

                        // If you use the model without jittering, as we did when clustering the bald guys, it
                        // gets an accuracy of 99.13% on the LFW benchmark.  So jittering makes the whole
                        // procedure a little more accurate but makes face descriptor calculation slower.

                        Console.WriteLine("hit enter to terminate");
                        Console.ReadKey();

                        foreach (var jitterImage in jitterImages)
                            jitterImage.Dispose();

                        foreach (var tileImage in tileImages)
                            tileImage.Dispose();

                        foreach (var edge in edges)
                            edge.Dispose();

                        foreach (var descriptor in faceDescriptors)
                            descriptor.Dispose();

                        foreach (var face in faces)
                            face.Dispose();
                    }
                }
            }
        }

        private static IEnumerable<Matrix<RgbPixel>> JitterImage(Matrix<RgbPixel> img)
        {
            // All this function does is make 100 copies of img, all slightly jittered by being
            // zoomed, rotated, and translated a little bit differently. They are also randomly
            // mirrored left to right.
            var rnd = new Rand();

            var crops = new List<Matrix<RgbPixel>>(); 
            for (var i = 0; i< 100; ++i)
                crops.Add(Dlib.JitterImage(img, rnd));

            return crops;
        }

}

}