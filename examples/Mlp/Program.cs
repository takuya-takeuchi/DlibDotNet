/*
 * This sample program is ported by C# from examples\mlp.cpp.
*/

using System;
using DlibDotNet;

using SampleType = DlibDotNet.Matrix<double>;

namespace Mlp
{

    internal class Program
    {

        private static void Main()
        {
            // The mlp takes column vectors as input and gives column vectors as output.  The dlib::matrix
            // object is used to represent the column vectors. So the first thing we do here is declare 
            // a convenient typedef for the matrix object we will be using.

            // This typedef declares a matrix with 2 rows and 1 column.  It will be the
            // object that contains each of our 2 dimensional samples.   (Note that if you wanted 
            // more than 2 features in this vector you can simply change the 2 to something else)
            //typedef matrix<double, 2, 1 > sample_type;

            // make an instance of a sample matrix so we can use it below
            using (var sample = new SampleType())
            {
                // Create a multi-layer perceptron network.   This network has 2 nodes on the input layer 
                // (which means it takes column vectors of length 2 as input) and 5 nodes in the first 
                // hidden layer.  Note that the other 4 variables in the mlp's constructor are left at
                // their default values.
                using (var net = new MultilayerPerceptron<Kernel1>(2, 5))
                {
                    // Now let's put some data into our sample and train on it.  We do this
                    // by looping over 41*41 points and labeling them according to their
                    // distance from the origin.
                    for (var i = 0; i < 1000; ++i)
                    {
                        for (var r = -20; r <= 20; ++r)
                        {
                            for (var c = -20; c <= 20; ++c)
                            {
                                sample(0) = r;
                                sample(1) = c;

                                // if this point is less than 10 from the origin
                                if (Math.Sqrt((double)r * r + c * c) <= 10)
                                    net.Train(sample, 1);
                                else
                                    net.Train(sample, 0);
                            }
                        }
                    }

                    // Now we have trained our mlp.  Let's see how well it did.  
                    // Note that if you run this program multiple times you will get different results. This
                    // is because the mlp network is randomly initialized.

                    // each of these statements prints out the output of the network given a particular sample.

                    sample(0) = 3.123;
                    sample(1) = 4;
                    Console.WriteLine($"This sample should be close to 1 and it is classified as a {net(sample)}");

                    sample(0) = 13.123;
                    sample(1) = 9.3545;
                    Console.WriteLine($"This sample should be close to 0 and it is classified as a {net(sample)}");

                    sample(0) = 13.123;
                    sample(1) = 0;
                    Console.WriteLine($"This sample should be close to 0 and it is classified as a {net(sample)}");
                }
            }
        }

    }

}
