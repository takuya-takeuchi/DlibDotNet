/*
 * This sample program is ported by C# from examples\kcentroid_ex.cpp.
*/

using System;
using DlibDotNet;

namespace KCentroid
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
                // Here we declare an instance of the kcentroid object.  The kcentroid has 3 parameters 
                // you need to set.  The first argument to the constructor is the kernel we wish to 
                // use.  The second is a parameter that determines the numerical accuracy with which 
                // the object will perform the centroid estimation.  Generally, smaller values 
                // give better results but cause the algorithm to attempt to use more dictionary vectors 
                // (and thus run slower and use more memory).  The third argument, however, is the 
                // maximum number of dictionary vectors a kcentroid is allowed to use.  So you can use
                // it to control the runtime complexity.
                using (var test = new KCentroid<double, RadialBasisKernel<double, Matrix<double>>>(rbk, 0.01, 15))
                {
                    // now we train our object on a few samples of the sinc function.
                    using (var m = Matrix<double>.CreateTemplateParameterizeMatrix(2, 1))
                    {
                        for (double x = -15; x <= 8; x += 1)
                        {
                            m[0] = x;
                            m[1] = Sinc(x);
                            test.Train(m);
                        }

                        using (var rs = new RunningStats<double>())
                        {
                            // Now let's output the distance from the centroid to some points that are from the sinc function.
                            // These numbers should all be similar.  We will also calculate the statistics of these numbers
                            // by accumulating them into the running_stats object called rs.  This will let us easily
                            // find the mean and standard deviation of the distances for use below.
                            Console.WriteLine("Points that are on the sinc function:");
                            m[0] = -1.5; m[1] = Sinc(m[0]); Console.WriteLine($"   {test.Operator(m)}"); rs.Add(test.Operator(m));
                            m[0] = -1.5; m[1] = Sinc(m[0]); Console.WriteLine($"   {test.Operator(m)}"); rs.Add(test.Operator(m));
                            m[0] = -0;   m[1] = Sinc(m[0]); Console.WriteLine($"   {test.Operator(m)}"); rs.Add(test.Operator(m));
                            m[0] = -0.5; m[1] = Sinc(m[0]); Console.WriteLine($"   {test.Operator(m)}"); rs.Add(test.Operator(m));
                            m[0] = -4.1; m[1] = Sinc(m[0]); Console.WriteLine($"   {test.Operator(m)}"); rs.Add(test.Operator(m));
                            m[0] = -1.5; m[1] = Sinc(m[0]); Console.WriteLine($"   {test.Operator(m)}"); rs.Add(test.Operator(m));
                            m[0] = -0.5; m[1] = Sinc(m[0]); Console.WriteLine($"   {test.Operator(m)}"); rs.Add(test.Operator(m));

                            Console.WriteLine();
                            // Let's output the distance from the centroid to some points that are NOT from the sinc function.
                            // These numbers should all be significantly bigger than previous set of numbers.  We will also
                            // use the rs.scale() function to find out how many standard deviations they are away from the 
                            // mean of the test points from the sinc function.  So in this case our criterion for "significantly bigger"
                            // is > 3 or 4 standard deviations away from the above points that actually are on the sinc function.
                            Console.WriteLine("Points that are NOT on the sinc function:");
                            m[0] = -1.5; m[1] = Sinc(m[0]) + 4;   Console.WriteLine($"   {test.Operator(m)} is {rs.Scale(test.Operator(m))} standard deviations from sinc.");
                            m[0] = -1.5; m[1] = Sinc(m[0]) + 3;   Console.WriteLine($"   {test.Operator(m)} is {rs.Scale(test.Operator(m))} standard deviations from sinc.");
                            m[0] = -0;   m[1] = -Sinc(m[0]);      Console.WriteLine($"   {test.Operator(m)} is {rs.Scale(test.Operator(m))} standard deviations from sinc.");
                            m[0] = -0.5; m[1] = -Sinc(m[0]);      Console.WriteLine($"   {test.Operator(m)} is {rs.Scale(test.Operator(m))} standard deviations from sinc.");
                            m[0] = -4.1; m[1] = Sinc(m[0]) + 2;   Console.WriteLine($"   {test.Operator(m)} is {rs.Scale(test.Operator(m))} standard deviations from sinc.");
                            m[0] = -1.5; m[1] = Sinc(m[0]) + 0.9; Console.WriteLine($"   {test.Operator(m)} is {rs.Scale(test.Operator(m))} standard deviations from sinc.");
                            m[0] = -0.5; m[1] = Sinc(m[0]) + 1;   Console.WriteLine($"   {test.Operator(m)} is {rs.Scale(test.Operator(m))} standard deviations from sinc.");

                            // And finally print out the mean and standard deviation of points that are actually from sinc().  
                            Console.WriteLine($"\nmean: {rs.Mean}");
                            Console.WriteLine($"standard deviation: {rs.StdDev}");

                            // The output is as follows:
                            /*
                                Points that are on the sinc function:
                                    0.869913
                                    0.869913
                                    0.873408
                                    0.872807
                                    0.870432
                                    0.869913
                                    0.872807

                                Points that are NOT on the sinc function:
                                    1.06366 is 119.65 standard deviations from sinc.
                                    1.02212 is 93.8106 standard deviations from sinc.
                                    0.921382 is 31.1458 standard deviations from sinc.
                                    0.918439 is 29.3147 standard deviations from sinc.
                                    0.931428 is 37.3949 standard deviations from sinc.
                                    0.898018 is 16.6121 standard deviations from sinc.
                                    0.914425 is 26.8183 standard deviations from sinc.

                                    mean: 0.871313
                                    standard deviation: 0.00160756
                            */

                            // So we can see that in this example the kcentroid object correctly indicates that 
                            // the non-sinc points are definitely not points from the sinc function.
                        }
                    }
                }
            }
        }

        #region Helpers

        // Here is the sinc function we will be trying to learn with the kcentroid 
        // object.
        private static double Sinc(double x)
        {
            if (Math.Abs(x) < double.Epsilon)
                return 1;

            return Math.Sin(x) / x;
        }

        #endregion

        #endregion

    }

}