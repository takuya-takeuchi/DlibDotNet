/*
 * This sample program is ported by C# from examples\svm_pegasos_ex.cpp.
*/

using System;
using System.Collections.Generic;
using DlibDotNet;
using SampleType = DlibDotNet.Matrix<double>;

namespace SvmPegasos
{

    internal class Program
    {

        #region Methods

        private static int Main()
        {
            // The svm functions use column vectors to contain a lot of the data on which they 
            // operate. So the first thing we do here is declare a convenient typedef.  

            // This typedef declares a matrix with 2 rows and 1 column.  It will be the
            // object that contains each of our 2 dimensional samples.   (Note that if you wanted 
            // more than 2 features in this vector you can simply change the 2 to something else.
            // Or if you don't know how many features you want until runtime then you can put a 0
            // here and use the matrix.set_size() member function)
            //typedef matrix<double, 2, 1 > sample_type;

            // This is a typedef for the type of kernel we are going to use in this example.
            // In this case I have selected the radial basis kernel that can operate on our
            // 2D sample_type objects
            //typedef radial_basis_kernel<sample_type> kernel_type;


            // Here we create an instance of the pegasos svm trainer object we will be using.
            using (var trainer = new SvmPegasos<double, RadialBasisKernel<double, Matrix<double>>>())
            using (var kernel = new RadialBasisKernel<double, Matrix<double>>(0.005, 0, 0))
            {
                // Here we setup the parameters to this object.  See the dlib documentation for a 
                // description of what these parameters are.
                trainer.SetLambda(0.00001);
                trainer.Kernel = kernel;

                // Set the maximum number of support vectors we want the trainer object to use
                // in representing the decision function it is going to learn.  In general, 
                // supplying a bigger number here will only ever give you a more accurate
                // answer.  However, giving a smaller number will make the algorithm run
                // faster and decision rules that involve fewer support vectors also take
                // less time to evaluate.  
                trainer.MaxNumSupportVector = 10;

                var samples = new List<SampleType>();
                var labels = new List<double>();

                // make an instance of a sample matrix so we can use it below
                var center = new SampleType();
                center.SetSize(2, 1);
                center.Assign(new[] { 20d, 20d });

                // Now let's go into a loop and randomly generate 1000 samples.
                Dlib.SRand((uint)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                for (var i = 0; i < 10000; ++i)
                {
                    // Make a random sample vector.
                    using (var r = Dlib.RandM(2, 1))
                    {
                        var sample = r * 40 - center;

                        // Now if that random vector is less than 10 units from the origin then it is in 
                        // the +1 class.
                        if (Dlib.Length(sample) <= 10)
                        {
                            // let the svm_pegasos learn about this sample
                            trainer.Train(sample, +1);

                            // save this sample so we can use it with the batch training examples below
                            samples.Add(sample);
                            labels.Add(+1);
                        }
                        else
                        {
                            // let the svm_pegasos learn about this sample
                            trainer.Train(sample, -1);

                            // save this sample so we can use it with the batch training examples below
                            samples.Add(sample);
                            labels.Add(-1);
                        }
                    }
                }

                // Now we have trained our SVM.  Let's see how well it did.  
                // Each of these statements prints out the output of the SVM given a particular sample.  
                // The SVM outputs a number > 0 if a sample is predicted to be in the +1 class and < 0 
                // if a sample is predicted to be in the -1 class.

                // Now let's try this decision_function on some samples we haven't seen before.
                using (var sample = new SampleType())
                {
                    sample.SetSize(2, 1);

                    sample[0] = 3.123;
                    sample[1] = 4;
                    Console.WriteLine($"This is a +1 example, its SVM output is: {trainer.Operator(sample)}");

                    sample[0] = 13.123;
                    sample[1] = 9.3545;
                    Console.WriteLine($"This is a -1 example, its SVM output is: {trainer.Operator(sample)}");

                    sample[0] = 13.123;
                    sample[1] = 0;
                    Console.WriteLine($"This is a -1 example, its SVM output is: {trainer.Operator(sample)}");
                }




                // The previous part of this example program showed you how to perform online training
                // with the pegasos algorithm.  But it is often the case that you have a dataset and you 
                // just want to perform batch learning on that dataset and get the resulting decision
                // function.  To support this the dlib library provides functions for converting an online
                // training object like svm_pegasos into a batch training object.  

                // First let's clear out anything in the trainer object.
                trainer.Clear();

                // Now to begin with, you might want to compute the cross validation score of a trainer object
                // on your data.  To do this you should use the batch_cached() function to convert the svm_pegasos object
                // into a batch training object.  Note that the second argument to batch_cached() is the minimum 
                // learning rate the trainer object must report for the batch_cached() function to consider training
                // complete.  So smaller values of this parameter cause training to take longer but may result
                // in a more accurate solution. 
                // Here we perform 4-fold cross validation and print the results
                using (var batchTrainer = Dlib.BatchCached<double,
                                                           RadialBasisKernel<double, Matrix<double>>,
                                                           SvmPegasos<double, RadialBasisKernel<double, Matrix<double>>>>(trainer, 0.1))
                using (var ret = Dlib.CrossValidateTrainer(batchTrainer, samples, labels, 4))
                    Console.Write($"cross validation: {ret}");

                // Here is an example of creating a decision function.  Note that we have used the verbose_batch_cached()
                // function instead of batch_cached() as above.  They do the same things except verbose_batch_cached() will
                // print status messages to standard output while training is under way.
                using (var verboseBatchCached = Dlib.VerboseBatchCached<double,
                                                                        RadialBasisKernel<double, Matrix<double>>,
                                                                        SvmPegasos<double, RadialBasisKernel<double, Matrix<double>>>>(trainer, 0.1))
                using (var df = verboseBatchCached.Train(samples, labels))
                using (var sample = new SampleType())
                {
                    sample.SetSize(2, 1);

                    sample[0] = 3.123;
                    sample[1] = 4;
                    Console.WriteLine($"This is a +1 example, its SVM output is: {df.Operator(sample)}");

                    sample[0] = 13.123;
                    sample[1] = 9.3545;
                    Console.WriteLine($"This is a -1 example, its SVM output is: {df.Operator(sample)}");

                    sample[0] = 13.123;
                    sample[1] = 0;
                    Console.WriteLine($"This is a -1 example, its SVM output is: {df.Operator(sample)}");
                }
            }

            return 0;
        }

        #endregion

    }

}