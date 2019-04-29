/*
 * This sample program is ported by C# from examples\krls_ex.cpp.
*/

using System;
using DlibDotNet;

namespace Krls
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

            using (var rbk = new RadialBasisKernel<double, Matrix<double>>(0.1, 1, 1))
            using (var test = new Krls<double, RadialBasisKernel<double, Matrix<double>>>(rbk, 0.001))
            {
                // now we train our object on a few samples of the sinc function.
                using (var m = Matrix<double>.CreateTemplateParameterizeMatrix(1, 1))
                {
                    for (double x = -10; x <= 4; x += 1)
                    {
                        m[0] = x;
                        test.Train(m, Sinc(x));
                    }

                    // now we output the value of the sinc function for a few test points as well as the 
                    // value predicted by krls object.
                    m[0] = 2.5;
                    Console.WriteLine($"{Sinc(m[0])}   {test.Operator(m)}");
                    m[0] = 0.1;
                    Console.WriteLine($"{Sinc(m[0])}   {test.Operator(m)}");
                    m[0] = -4;
                    Console.WriteLine($"{Sinc(m[0])}   {test.Operator(m)}");
                    m[0] = 5.0;
                    Console.WriteLine($"{Sinc(m[0])}   {test.Operator(m)}");

                    // The output is as follows:
                    // 0.239389   0.239362
                    // 0.998334   0.998333
                    // -0.189201   -0.189201
                    // -0.191785   -0.197267


                    // The first column is the true value of t          he sinc function and the second
                    // column is the output from the krls estimate.  





                    // Another thing that is worth knowing is that just about everything in dlib is serializable.
                    // So for example, you can save the test object to disk and recall it later like so:
                    Krls<double, RadialBasisKernel<double, Matrix<double>>>.Serialize(test, "saved_krls_object.dat");

                    // Now let's open that file back up and load the krls object it contains.
                    using (var rbk2 = new RadialBasisKernel<double, Matrix<double>>(0.1, 1, 1))
                    {
                        var test2 = new Krls<double, RadialBasisKernel<double, Matrix<double>>>(rbk2, 0.001);
                        Krls<double, RadialBasisKernel<double, Matrix<double>>>.Deserialize("saved_krls_object.dat", ref test2);

                        // If you don't want to save the whole krls object (it might be a bit large) 
                        // you can save just the decision function it has learned so far.  You can get 
                        // the decision function out of it by calling test.get_decision_function() and
                        // then you can serialize that object instead.  E.g.
                        var funct = test2.GetDecisionFunction();
                        DecisionFunction<double, RadialBasisKernel<double, Matrix<double>>>.Serialize(funct, "saved_krls_function.dat");
                    }
                }
            }
        }

        #region Helpers

        // Here is the sinc function we will be trying to learn with the krls
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