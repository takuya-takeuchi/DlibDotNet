/*
 * This sample program is ported by C# from examples\krls_filter_ex.cpp.
*/

using System;
using DlibDotNet;

namespace KrlsFilter
{

    internal class Program
    {

        #region Methods

        private static void Main()
        {
            // Here we declare that our samples will be 1 dimensional column vectors.  The reason for
            // using a matrix here is that in general you can use N dimensional vectors as inputs to the
            // krls object.  But here we only have 1 dimension to make the example simple.


            // Now we are making a typedef for the kind of kernel we want to use.  I picked the
            // radial basis kernel because it only has one parameter and generally gives good
            // results without much fiddling.


            // Here we declare an instance of the krls object.  The first argument to the constructor
            // is the kernel we wish to use.  The second is a parameter that determines the numerical 
            // accuracy with which the object will perform part of the regression algorithm.  Generally
            // smaller values give better results but cause the algorithm to run slower (because it tries
            // to use more "dictionary vectors" to represent the function it is learning.  
            // You just have to play with it to decide what balance of speed and accuracy is right 
            // for your problem.  Here we have set it to 0.001.
            //
            // The last argument is the maximum number of dictionary vectors the algorithm is allowed
            // to use.  The default value for this field is 1,000,000 which is large enough that you 
            // won't ever hit it in practice.  However, here we have set it to the much smaller value
            // of 7.  This means that once the krls object accumulates 7 dictionary vectors it will 
            // start discarding old ones in favor of new ones as it goes through the training process.  
            // In other words, the algorithm "forgets" about old training data and focuses on recent
            // training samples. So the bigger the maximum dictionary size the longer its memory will 
            // be.  But in this example program we are doing filtering so we only care about the most 
            // recent data.  So using a small value is appropriate here since it will result in much
            // faster filtering and won't introduce much error.

            using(var rbk = new RadialBasisKernel<double, Matrix<double>>(0.05, 1, 1))
            using (var test = new Krls<double, RadialBasisKernel<double, Matrix<double>>>(rbk, 0.001, 7))
            {
                using (var rnd = new Rand())
                {
                    // Now let's loop over a big range of values from the sinc() function.  Each time
                    // adding some random noise to the data we send to the krls object for training.
                    using (var m = Matrix<double>.CreateTemplateParameterizeMatrix(1, 1))
                    {
                        double mseNoise = 0;
                        double mse = 0;
                        double count = 0;
                        for (double x = -20; x <= 20; x += 0.01)
                        {
                            m[0] = x;
                            // get a random number between -0.5 and 0.5
                            double noise = rnd.GetRandomDouble() - 0.5;

                            // train on this new sample
                            test.Train(m, Sinc(x) + noise);

                            // once we have seen a bit of data start measuring the mean squared prediction error.
                            // Also measure the mean squared error due to the noise.
                            if (x > -19)
                            {
                                ++count;
                                mse += Math.Pow(Sinc(x) - test.Operator(m), 2);
                                mseNoise += Math.Pow(noise, 2);
                            }
                        }

                        mse /= count;
                        mseNoise /= count;

                        // Output the ratio of the error from the noise and the mean squared prediction error.  
                        Console.WriteLine($"prediction error:                   {mse}");
                        Console.WriteLine($"noise:                              {mseNoise}");
                        Console.WriteLine($"ratio of noise to prediction error: {mseNoise / mse}");

                        // When the program runs it should print the following:
                        //    prediction error:                   0.00735201
                        //    noise:                              0.0821628
                        //    ratio of noise to prediction error: 11.1756

                        // And we see that the noise has been significantly reduced by filtering the points 
                        // through the krls object.
                    }
                }
            }
        }

        #region Helpers

        // Here is the function we will be trying to learn with the krls
        // object.
        private static double Sinc(double x)
        {
            if (Math.Abs(x) < double.Epsilon)
                return 1;

            return Math.Sin(x) / x + x;
        }

        #endregion

        #endregion

    }

}