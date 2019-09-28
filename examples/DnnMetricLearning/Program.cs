/*
 * This sample program is ported by C# from examples\dnn_metric_learning_ex.cpp.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnMetricLearning
{

    internal class Program
    {

        #region Methods

        private static void Main()
        {
            try
            {
                // The API for doing metric learning is very similar to the API for
                // multi-class classification.  In fact, the inputs are the same, a bunch of
                // labeled objects.  So here we create our dataset.  We make up some simple
                // vectors and label them with the integers 1,2,3,4.  The specific values of
                // the integer labels don't matter.
                var samples = new List<Matrix<double>>();
                var labels = new List<uint>();

                // class 1 training vectors
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 1, 0, 0, 0, 0, 0, 0, 0 })); labels.Add(1);
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 0, 1, 0, 0, 0, 0, 0, 0 })); labels.Add(1);

                // class 2 training vectors
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 0, 0, 1, 0, 0, 0, 0, 0 })); labels.Add(2);
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 0, 0, 0, 1, 0, 0, 0, 0 })); labels.Add(2);

                // class 3 training vectors
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 0, 0, 0, 0, 1, 0, 0, 0 })); labels.Add(3);
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 0, 0, 0, 0, 0, 1, 0, 0 })); labels.Add(3);

                // class 4 training vectors
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 0, 0, 0, 0, 0, 0, 1, 0 })); labels.Add(4);
                samples.Add(new Matrix<double>(new MatrixTemplateSizeParameter(0, 1), new double[] { 0, 0, 0, 0, 0, 0, 0, 1 })); labels.Add(4);


                // Make a network that simply learns a linear mapping from 8D vectors to 2D
                // vectors.
                using (var net = new LossMetric(1))
                using (var trainer = new DnnTrainer<LossMetric>(net))
                {
                    trainer.SetLearningRate(0.1);

                    // It should be emphasized out that it's really important that each mini-batch contain
                    // multiple instances of each class of object.  This is because the metric learning
                    // algorithm needs to consider pairs of objects that should be close as well as pairs
                    // of objects that should be far apart during each training step.  Here we just keep
                    // training on the same small batch so this constraint is trivially satisfied.
                    while (trainer.GetLearningRate() >= 1e-4)
                        LossMetric.TrainOneStep(trainer, samples, labels);

                    // Wait for training threads to stop
                    trainer.GetNet().Dispose();
                    Console.WriteLine("done training");


                    // Run all the samples through the network to get their 2D vector embeddings.
                    var embedded = net.Operator(samples);

                    // Print the embedding for each sample to the screen.  If you look at the
                    // outputs carefully you should notice that they are grouped together in 2D
                    // space according to their label.
                    for (var i = 0; i < embedded.Count(); ++i)
                        using (var trans = Dlib.Trans(embedded[i]))
                            Console.Write($"label: {labels[i]}\t{trans}");

                    // Now, check if the embedding puts things with the same labels near each other and
                    // things with different labels far apart.
                    var numRight = 0;
                    var numWrong = 0;
                    for (var i = 0; i < embedded.Count(); ++i)
                        for (var j = i + 1; j < embedded.Count(); ++j)
                        {
                            if (labels[i] == labels[j])
                            {
                                // The loss_metric layer will cause things with the same label to be less
                                // than net.loss_details().get_distance_threshold() distance from each
                                // other.  So we can use that distance value as our testing threshold for
                                // "being near to each other".
                                if (Dlib.Length(embedded[i] - embedded[j]) < net.GetLossDetails().GetDistanceThreshold())
                                    ++numRight;
                                else
                                    ++numWrong;
                            }
                            else
                            {
                                if (Dlib.Length(embedded[i] - embedded[j]) >= net.GetLossDetails().GetDistanceThreshold())
                                    ++numRight;
                                else
                                    ++numWrong;
                            }
                        }

                    Console.WriteLine($"num_right: {numRight}");
                    Console.WriteLine($"num_wrong: {numWrong}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

    }

}