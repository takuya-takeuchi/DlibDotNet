/*
 * This sample program is ported by C# from examples\svm_ex.cpp.
*/

using System;
using System.Collections.Generic;
using DlibDotNet;
using SampleType = DlibDotNet.Matrix<double>;

namespace SvmC
{

    internal class Program
    {

        #region Methods

        private static int Main()
        {
            // The svm functions use column vectors to contain a lot of the data on which they
            // operate. So the first thing we do here is declare a convenient typedef.  

            // This typedef declares a matrix with 2 rows and 1 column.  It will be the object that
            // contains each of our 2 dimensional samples.   (Note that if you wanted more than 2
            // features in this vector you can simply change the 2 to something else.  Or if you
            // don't know how many features you want until runtime then you can put a 0 here and
            // use the matrix.set_size() member function)
            //typedef matrix<double, 2, 1 > sample_type;

            // This is a typedef for the type of kernel we are going to use in this example.  In
            // this case I have selected the radial basis kernel that can operate on our 2D
            // sample_type objects
            //typedef radial_basis_kernel<sample_type> kernel_type;


            // Now we make objects to contain our samples and their respective labels.
            var samples = new List<SampleType>();
            var labels = new List<double>();

            // Now let's put some data into our samples and labels objects.  We do this by looping
            // over a bunch of points and labeling them according to their distance from the
            // origin.
            for (var r = -20; r <= 20; ++r)
            {
                for (var c = -20; c <= 20; ++c)
                {
                    var samp = new SampleType();
                    samp.SetSize(2, 1);
                    samp[0] = r;
                    samp[1] = c;
                    samples.Add(samp);

                    // if this point is less than 10 from the origin
                    if (Math.Sqrt((double)r * r + c * c) <= 10)
                        labels.Add(+1);
                    else
                        labels.Add(-1);
                }
            }


            // Here we normalize all the samples by subtracting their mean and dividing by their
            // standard deviation.  This is generally a good idea since it often heads off
            // numerical stability problems and also prevents one large feature from smothering
            // others.  Doing this doesn't matter much in this example so I'm just doing this here
            // so you can see an easy way to accomplish this with the library.
            using (var normalizer = new VectorNormalizer<SampleType>())
            {
                // let the normalizer learn the mean and standard deviation of the samples
                normalizer.Train(samples);
                // now normalize each sample
                for (var i = 0; i < samples.Count; ++i)
                {
                    var ret = normalizer.Operator(samples[i]);
                    samples[i].Dispose();
                    samples[i] = ret;
                }
                

                // Now that we have some data we want to train on it.  However, there are two
                // parameters to the training.  These are the nu and gamma parameters.  Our choice for
                // these parameters will influence how good the resulting decision function is.  To
                // test how good a particular choice of these parameters is we can use the
                // cross_validate_trainer() function to perform n-fold cross validation on our training
                // data.  However, there is a problem with the way we have sampled our distribution
                // above.  The problem is that there is a definite ordering to the samples.  That is,
                // the first half of the samples look like they are from a different distribution than
                // the second half.  This would screw up the cross validation process but we can fix it
                // by randomizing the order of the samples with the following function call.
                Dlib.RandomizeSamples(samples, labels);
                
                // The nu parameter has a maximum value that is dependent on the ratio of the +1 to -1
                // labels in the training data.  This function finds that value.
                double maxNu = Dlib.MaximumNu(labels);

                // here we make an instance of the svm_nu_trainer object that uses our kernel type.
                using (var trainer = new SvmNuTrainer<double, RadialBasisKernel<double, Matrix<double>>>())
                {
                    // Now we loop over some different nu and gamma values to see how good they are.  Note
                    // that this is a very simple way to try out a few possible parameter choices.  You
                    // should look at the model_selection_ex.cpp program for examples of more sophisticated
                    // strategies for determining good parameter choices.
                    Console.WriteLine("doing cross validation");
                    for (var gamma = 0.00001; gamma <= 1; gamma *= 5)
                    {
                        for (var nu = 0.00001; nu < maxNu; nu *= 5)
                        {
                            // tell the trainer the parameters we want to use
                            using (var kernel = new RadialBasisKernel<double, Matrix<double>>(gamma, 0, 0))
                            {
                                trainer.Kernel = kernel;
                                trainer.Nu = nu;

                                Console.Write($"gamma: {gamma}    nu: {nu}");
                                // Print out the cross validation accuracy for 3-fold cross validation using
                                // the current gamma and nu.  cross_validate_trainer() returns a row vector.
                                // The first element of the vector is the fraction of +1 training examples
                                // correctly classified and the second number is the fraction of -1 training
                                // examples correctly classified.
                                using (var ret = Dlib.CrossValidateTrainer(trainer, samples, labels, 3))
                                    Console.Write($"     cross validation accuracy: {ret}");
                            }
                        }
                    }


                    // From looking at the output of the above loop it turns out that a good value for nu
                    // and gamma for this problem is 0.15625 for both.  So that is what we will use.

                    // Now we train on the full set of data and obtain the resulting decision function.  We
                    // use the value of 0.15625 for nu and gamma.  The decision function will return values
                    // >= 0 for samples it predicts are in the +1 class and numbers < 0 for samples it
                    // predicts to be in the -1 class.
                    using (var kernel = new RadialBasisKernel<double, Matrix<double>>(0.15625, 0, 0))
                    {
                        trainer.Kernel = kernel;
                        trainer.Nu = 0.15625;

                        // Here we are making an instance of the normalized_function object.  This object
                        // provides a convenient way to store the vector normalization information along with
                        // the decision function we are going to learn.  
                        var learnedFunction = new NormalizedFunction<double, DecisionFunction<double, RadialBasisKernel<double, Matrix<double>>>>();
                        learnedFunction.Normalizer = normalizer; // save normalization information
                        using (var function = trainer.Train(samples, labels))
                        {
                            learnedFunction.Function = function; // perform the actual SVM training and save the results

                            // print out the number of support vectors in the resulting decision function
                            Console.WriteLine();

                            // ToDo: must support nested matrix
                            //Console.WriteLine($"number of support vectors in our learned_function is {learnedFunction.Function.basis_vectors.size()}");
                        }

                        // Now let's try this decision_function on some samples we haven't seen before.
                        using (var sample = new SampleType())
                        {
                            sample.SetSize(2, 1);

                            sample[0] = 3.123;
                            sample[1] = 2;
                            Console.WriteLine($"This is a +1 class example, the classifier output is {learnedFunction.Operator(sample)}");

                            sample[0] = 3.123;
                            sample[1] = 9.3545;
                            Console.WriteLine($"This is a +1 class example, the classifier output is {learnedFunction.Operator(sample)}");

                            sample[0] = 13.123;
                            sample[1] = 9.3545;
                            Console.WriteLine($"This is a -1 class example, the classifier output is {learnedFunction.Operator(sample)}");

                            sample[0] = 13.123;
                            sample[1] = 0;
                            Console.WriteLine($"This is a -1 class example, the classifier output is {learnedFunction.Operator(sample)}");
                        }


                        // We can also train a decision function that reports a well conditioned probability
                        // instead of just a number > 0 for the +1 class and < 0 for the -1 class.  An example
                        // of doing that follows:
                        var learnedProbabilisticFunction = new NormalizedFunction<double, ProbabilisticDecisionFunction<double, RadialBasisKernel<double, Matrix<double>>>>();
                        learnedProbabilisticFunction.Normalizer = normalizer;
                        using (var function = Dlib.TrainProbabilisticDecisionFunction<double,
                                                                                      RadialBasisKernel<double, Matrix<double>>,
                                                                                      SvmNuTrainer<double, RadialBasisKernel<double, Matrix<double>>>>(trainer, samples, labels, 3))
                        {
                            learnedProbabilisticFunction.Function = function;

                            // Now we have a function that returns the probability that a given sample is of the +1 class.  

                            // print out the number of support vectors in the resulting decision function.  
                            // (it should be the same as in the one above)
                            Console.WriteLine();

                            // ToDo: must support nested matrix
                            //Console.WriteLine($"number of support vectors in our learned_pfunct  is {learnedProbabilisticFunction.Function.DecisionFunct.BasisVectors.size()}");
                        }


                        using (var sample = new SampleType())
                        {
                            sample.SetSize(2, 1);

                            sample[0] = 3.123;
                            sample[1] = 2;
                            Console.WriteLine($"This +1 class example should have high probability.  Its probability is: {learnedProbabilisticFunction.Operator(sample)}");

                            sample[0] = 3.123;
                            sample[1] = 9.3545;
                            Console.WriteLine($"This +1 class example should have high probability.  Its probability is: {learnedProbabilisticFunction.Operator(sample)}");

                            sample[0] = 13.123;
                            sample[1] = 9.3545;
                            Console.WriteLine($"This -1 class example should have low probability.  Its probability is: {learnedProbabilisticFunction.Operator(sample)}");

                            sample[0] = 13.123;
                            sample[1] = 0;
                            Console.WriteLine($"This -1 class example should have low probability.  Its probability is: {learnedProbabilisticFunction.Operator(sample)}");
                        }



                        // Another thing that is worth knowing is that just about everything in dlib is
                        // serializable.  So for example, you can save the learned_pfunct object to disk and
                        // recall it later like so:
                        NormalizedFunction<double, ProbabilisticDecisionFunction<double, RadialBasisKernel<double, Matrix<double>>>>.Serialize("saved_function.dat", learnedProbabilisticFunction);

                        // Now let's open that file back up and load the function object it contains.
                        learnedProbabilisticFunction.Dispose();
                        learnedProbabilisticFunction = NormalizedFunction<double, ProbabilisticDecisionFunction<double, RadialBasisKernel<double, Matrix<double>>>>.Deserialize("saved_function.dat");

                        // Note that there is also an example program that comes with dlib called
                        // the file_to_code_ex.cpp example.  It is a simple program that takes a
                        // file and outputs a piece of C++ code that is able to fully reproduce the
                        // file's contents in the form of a std::string object.  So you can use that
                        // along with the std::istringstream to save learned decision functions
                        // inside your actual C++ code files if you want.




                        // Note that there is also an example program that comes with dlib called the
                        // file_to_code_ex.cpp example.  It is a simple program that takes a file and outputs a
                        // piece of C++ code that is able to fully reproduce the file's contents in the form of
                        // a std::string object.  So you can use that along with the std::istringstream to save
                        // learned decision functions inside your actual C++ code files if you want.  




                        // Lastly, note that the decision functions we trained above involved well over 200
                        // basis vectors.  Support vector machines in general tend to find decision functions
                        // that involve a lot of basis vectors.  This is significant because the more basis
                        // vectors in a decision function, the longer it takes to classify new examples.  So
                        // dlib provides the ability to find an approximation to the normal output of a trainer
                        // using fewer basis vectors.  

                        // Here we determine the cross validation accuracy when we approximate the output using
                        // only 10 basis vectors.  To do this we use the reduced2() function.  It takes a
                        // trainer object and the number of basis vectors to use and returns a new trainer
                        // object that applies the necessary post processing during the creation of decision
                        // function objects.
                        using (var reduced = Dlib.Reduced2<double,
                                                           RadialBasisKernel<double, Matrix<double>>,
                                                           SvmNuTrainer<double, RadialBasisKernel<double, Matrix<double>>>>(trainer, 10))
                        {
                            Console.WriteLine();
                            using (var ret = Dlib.CrossValidateTrainer(reduced, samples, labels, 3))
                                Console.Write($"cross validation accuracy with only 10 support vectors: {ret}");
                        }

                        // Let's print out the original cross validation score too for comparison.
                        using (var ret2 = Dlib.CrossValidateTrainer(trainer, samples, labels, 3))
                            Console.Write($"cross validation accuracy with all the original support vectors: {ret2}");

                        // When you run this program you should see that, for this problem, you can reduce the
                        // number of basis vectors down to 10 without hurting the cross validation accuracy. 


                        // To get the reduced decision function out we would just do this:
                        using (var reduced = Dlib.Reduced2<double,
                                                           RadialBasisKernel<double, Matrix<double>>,
                                                           SvmNuTrainer<double, RadialBasisKernel<double, Matrix<double>>>>(trainer, 10))
                        using (var function = reduced.Train(samples, labels))
                            learnedFunction.Function = function;
                        // And similarly for the probabilistic_decision_function: 
                        using (var reduced = Dlib.Reduced2<double,
                                                           RadialBasisKernel<double, Matrix<double>>,
                                                           SvmNuTrainer<double, RadialBasisKernel<double, Matrix<double>>>>(trainer, 10))
                        using (var function = Dlib.TrainProbabilisticDecisionFunction(reduced, samples, labels, 3))
                            learnedProbabilisticFunction.Function = function;

                        learnedFunction.Dispose();
                        learnedProbabilisticFunction.Dispose();
                    }
                }
            }


            return 0;
        }

        #endregion

    }

}