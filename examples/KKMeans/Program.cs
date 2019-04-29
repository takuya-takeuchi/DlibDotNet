/*
 * This sample program is ported by C# from examples\kkmeans_ex.cpp.
*/

using System;
using System.Collections.Generic;
using DlibDotNet;
using DlibDotNet.Extensions;

namespace KKMeans
{

    internal class Program
    {

        #region Methods

        private static void Main()
        {
            // Here we declare that our samples will be 2 dimensional column vectors.  
            // (Note that if you don't know the dimensionality of your vectors at compile time
            // you can change the 2 to a 0 and then set the size at runtime)

            // Now we are making a typedef for the kind of kernel we want to use.  I picked the
            // radial basis kernel because it only has one parameter and generally gives good
            // results without much fiddling.
            using (var rbk = new RadialBasisKernel<double, Matrix<double>>(0.1d, 2, 1))
            {
                // Here we declare an instance of the kcentroid object.  It is the object used to 
                // represent each of the centers used for clustering.  The kcentroid has 3 parameters 
                // you need to set.  The first argument to the constructor is the kernel we wish to 
                // use.  The second is a parameter that determines the numerical accuracy with which 
                // the object will perform part of the learning algorithm.  Generally, smaller values 
                // give better results but cause the algorithm to attempt to use more dictionary vectors 
                // (and thus run slower and use more memory).  The third argument, however, is the 
                // maximum number of dictionary vectors a kcentroid is allowed to use.  So you can use
                // it to control the runtime complexity.
                using (var kc = new KCentroid<double, RadialBasisKernel<double, Matrix<double>>>(rbk, 0.01, 8))
                {
                    // Now we make an instance of the kkmeans object and tell it to use kcentroid objects
                    // that are configured with the parameters from the kc object we defined above.

                    using (var test = new KKMeans<double, RadialBasisKernel<double, Matrix<double>>>(kc))
                    {
                        var samples = new List<Matrix<double>>();

                        using (var m = Matrix<double>.CreateTemplateParameterizeMatrix(2, 1))
                        using (var rnd = new Rand())
                        {
                            // we will make 50 points from each class
                            const int num = 50;

                            // make some samples near the origin
                            var radius = 0.5d;
                            for (var i = 0; i < num; ++i)
                            {
                                double sign = 1;
                                if (rnd.GetRandomDouble() < 0.5)
                                    sign = -1;
                                m[0] = 2 * radius * rnd.GetRandomDouble() - radius;
                                m[1] = sign * Math.Sqrt(radius * radius - m[0] * m[0]);

                                // add this sample to our set of samples we will run k-means
                                samples.Add(m.Clone());
                            }

                            // make some samples in a circle around the origin but far away
                            radius = 10.0;
                            for (var i = 0; i < num; ++i)
                            {
                                double sign = 1;
                                if (rnd.GetRandomDouble() < 0.5)
                                    sign = -1;
                                m[0] = 2 * radius * rnd.GetRandomDouble() - radius;
                                m[1] = sign * Math.Sqrt(radius * radius - m[0] * m[0]);

                                // add this sample to our set of samples we will run k-means
                                samples.Add(m.Clone());
                            }

                            // make some samples in a circle around the point (25,25) 
                            radius = 4.0;
                            for (var i = 0; i < num; ++i)
                            {
                                double sign = 1;
                                if (rnd.GetRandomDouble() < 0.5)
                                    sign = -1;
                                m[0] = 2 * radius * rnd.GetRandomDouble() - radius;
                                m[1] = sign * Math.Sqrt(radius * radius - m[0] * m[0]);

                                // translate this point away from the origin
                                m[0] += 25;
                                m[1] += 25;

                                // add this sample to our set of samples we will run k-means
                                samples.Add(m.Clone());
                            }

                            // tell the kkmeans object we made that we want to run k-means with k set to 3. 
                            // (i.e. we want 3 clusters)
                            test.NumberOfCenters = 3;

                            // You need to pick some initial centers for the k-means algorithm.  So here
                            // we will use the dlib::pick_initial_centers() function which tries to find
                            // n points that are far apart (basically).
                            var initialCenters = Dlib.PickInitialCenters(3, samples, test.Kernel);

                            // now run the k-means algorithm on our set of samples.  
                            test.Train(samples, initialCenters);

                            // now loop over all our samples and print out their predicted class.  In this example
                            // all points are correctly identified.
                            for (var i = 0; i < samples.Count / 3; ++i)
                            {
                                Console.Write($"{test.Operator(samples[i])} ");
                                Console.Write($"{test.Operator(samples[i + num])} ");
                                Console.WriteLine($"{test.Operator(samples[i + 2 * num])}");
                            }

                            // Now print out how many dictionary vectors each center used.  Note that 
                            // the maximum number of 8 was reached.  If you went back to the kcentroid 
                            // constructor and changed the 8 to some bigger number you would see that these
                            // numbers would go up.  However, 8 is all we need to correctly cluster this dataset.
                            Console.WriteLine($"num dictionary vectors for center 0: {test.GetKCentroid(0).DictionarySize}");
                            Console.WriteLine($"num dictionary vectors for center 1: {test.GetKCentroid(1).DictionarySize}");
                            Console.WriteLine($"num dictionary vectors for center 2: {test.GetKCentroid(2).DictionarySize}");


                            // Finally, we can also solve the same kind of non-linear clustering problem with
                            // spectral_cluster().  The output is a vector that indicates which cluster each sample
                            // belongs to.  Just like with kkmeans, it assigns each point to the correct cluster.
                            using (var tmp = new RadialBasisKernel<double, Matrix<double>>(0.1, 2, 1))
                            {
                                var assignments = Dlib.SpectralCluster(tmp, samples, 3);
                                using (var mat = Dlib.Mat(assignments))
                                    Console.WriteLine($"{mat}");
                            }
                        }

                        samples.DisposeElement();
                    }
                }
            }
        }

        #endregion

    }

}