/*
 * This sample program is ported by C# from examples\dnn_inception_ex.cpp.
*/

using System;
using System.IO;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnInception
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            try
            {
                // This example is going to run on the MNIST dataset.
                if (args.Length != 1)
                {
                    Console.WriteLine("This example needs the MNIST dataset to run!");
                    Console.WriteLine("You can get MNIST from http://yann.lecun.com/exdb/mnist/");
                    Console.WriteLine("Download the 4 files that comprise the dataset, decompress them, and");
                    Console.WriteLine("put them in a folder.  Then give that folder as input to this program.");
                    return;
                }

                Dlib.LoadMNISTDataset(args[0],
                                      out var trainingImages,
                                      out var trainingLabels,
                                      out var testingImages,
                                      out var testingLabels);


                // Make an instance of our inception network.
                using (var net = new LossMulticlassLog())
                {
                    Console.WriteLine($"The net has {net.NumLayers} layers in it.");
                    Console.WriteLine(net);

                    Console.WriteLine("Traning NN...");
                    using (var trainer = new DnnTrainer<LossMulticlassLog>(net))
                    {
                        trainer.SetLearningRate(0.01);
                        trainer.SetMinLearningRate(0.00001);
                        trainer.SetMinBatchSize(128);
                        trainer.BeVerbose();
                        trainer.SetSynchronizationFile("inception_sync", 20);
                        // Train the network.  This might take a few minutes...
                        LossMulticlassLog.Train(trainer, trainingImages, trainingLabels);

                        // At this point our net object should have learned how to classify MNIST images.  But
                        // before we try it out let's save it to disk.  Note that, since the trainer has been
                        // running images through the network, net will have a bunch of state in it related to
                        // the last batch of images it processed (e.g. outputs from each layer).  Since we
                        // don't care about saving that kind of stuff to disk we can tell the network to forget
                        // about that kind of transient data so that our file will be smaller.  We do this by
                        // "cleaning" the network before saving it.
                        net.Clean();
                        LossMulticlassLog.Serialize(net, "mnist_network_inception.dat");
                        // Now if we later wanted to recall the network from disk we can simply say:
                        // deserialize("mnist_network_inception.dat") >> net;


                        // Now let's run the training images through the network.  This statement runs all the
                        // images through it and asks the loss layer to convert the network's raw output into
                        // labels.  In our case, these labels are the numbers between 0 and 9.
                        using (var predictedLabels = net.Operator(trainingImages))
                        {
                            var numRight = 0;
                            var numWrong = 0;
                            // And then let's see if it classified them correctly.
                            for (var i = 0; i < trainingImages.Length; ++i)
                            {
                                if (predictedLabels[i] == trainingLabels[i])
                                    ++numRight;
                                else
                                    ++numWrong;
                            }

                            Console.WriteLine($"training num_right: {numRight}");
                            Console.WriteLine($"training num_wrong: {numWrong}");
                            Console.WriteLine($"training accuracy:  {numRight / (double)(numRight + numWrong)}");

                            // Let's also see if the network can correctly classify the testing images.
                            // Since MNIST is an easy dataset, we should see 99% accuracy.
                            using (var predictedLabels2 = net.Operator(testingImages))
                            {
                                numRight = 0;
                                numWrong = 0;
                                for (var i = 0; i < testingImages.Length; ++i)
                                {
                                    if (predictedLabels2[i] == testingLabels[i])
                                        ++numRight;
                                    else
                                        ++numWrong;
                                }

                                Console.WriteLine($"testing num_right: {numRight}");
                                Console.WriteLine($"testing num_wrong: {numWrong}");
                                Console.WriteLine($"testing accuracy:  {numRight / (double)(numRight + numWrong)}");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }

}