/*
 * This sample program is ported by C# from examples\dnn_introduction_ex.cpp.
*/

using System;
using System.Collections.Generic;
using DlibDotNet;
using DlibDotNet.Dnn;

namespace DnnIntroduction
{

    internal class Program
    {

        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("This example needs the MNIST dataset to run!");
                Console.WriteLine("You can get MNIST from http://yann.lecun.com/exdb/mnist/");
                Console.WriteLine("Download the 4 files that comprise the dataset, decompress them, and");
                Console.WriteLine("put them in a folder.  Then give that folder as input to this program.");
                return 1;
            }

            try
            {
                // MNIST is broken into two parts, a training set of 60000 images and a test set of
                // 10000 images.  Each image is labeled so that we know what hand written digit is
                // depicted.  These next statements load the dataset into memory.
                IList<Matrix<byte>> trainingImages;
                IList<uint> trainingLabels;
                IList<Matrix<byte>> testingImages;
                IList<uint> testingLabels;
                Dlib.LoadMNISTDataset(args[0], out trainingImages, out trainingLabels, out testingImages, out testingLabels);


                // Now let's define the LeNet.  Broadly speaking, there are 3 parts to a network
                // definition.  The loss layer, a bunch of computational layers, and then an input
                // layer.  You can see these components in the network definition below.  
                // 
                // The input layer here says the network expects to be given matrix<unsigned char>
                // objects as input.  In general, you can use any dlib image or matrix type here, or
                // even define your own types by creating custom input layers.
                //
                // Then the middle layers define the computation the network will do to transform the
                // input into whatever we want.  Here we run the image through multiple convolutions,
                // ReLU units, max pooling operations, and then finally a fully connected layer that
                // converts the whole thing into just 10 numbers.  
                // 
                // Finally, the loss layer defines the relationship between the network outputs, our 10
                // numbers, and the labels in our dataset.  Since we selected loss_multiclass_log it
                // means we want to do multiclass classification with our network.   Moreover, the
                // number of network outputs (i.e. 10) is the number of possible labels.  Whichever
                // network output is largest is the predicted label.  So for example, if the first
                // network output is largest then the predicted digit is 0, if the last network output
                // is largest then the predicted digit is 9.

                // This net_type defines the entire network architecture.  For example, the block
                // relu<fc<84,SUBNET>> means we take the output from the subnetwork, pass it through a
                // fully connected layer with 84 outputs, then apply ReLU.  Similarly, a block of
                // max_pool<2,2,2,2,relu<con<16,5,5,1,1,SUBNET>>> means we apply 16 convolutions with a
                // 5x5 filter size and 1x1 stride to the output of a subnetwork, then apply ReLU, then
                // perform max pooling with a 2x2 window and 2x2 stride.  



                // So with that out of the way, we can make a network instance.
                using (var net = new LossMulticlassLog(3))
                {
                    // And then train it using the MNIST data.  The code below uses mini-batch stochastic
                    // gradient descent with an initial learning rate of 0.01 to accomplish this.
                    using (var trainer = new DnnTrainer<LossMulticlassLog>(net))
                    {
                        trainer.SetLearningRate(0.01);
                        trainer.SetMinLearningRate(0.00001);
                        trainer.SetMiniBatchSize(128);
                        trainer.BeVerbose();
                        // Since DNN training can take a long time, we can ask the trainer to save its state to
                        // a file named "mnist_sync" every 20 seconds.  This way, if we kill this program and
                        // start it again it will begin where it left off rather than restarting the training
                        // from scratch.  This is because, when the program restarts, this call to
                        // set_synchronization_file() will automatically reload the settings from mnist_sync if
                        // the file exists.
                        trainer.SetSynchronizationFile("mnist_sync", 20);
                        // Finally, this line begins training.  By default, it runs SGD with our specified
                        // learning rate until the loss stops decreasing.  Then it reduces the learning rate by
                        // a factor of 10 and continues running until the loss stops decreasing again.  It will
                        // keep doing this until the learning rate has dropped below the min learning rate
                        // defined above or the maximum number of epochs as been executed (defaulted to 10000).
                        LossMulticlassLog.Train(trainer, trainingImages, trainingLabels);

                        // At this point our net object should have learned how to classify MNIST images.  But
                        // before we try it out let's save it to disk.  Note that, since the trainer has been
                        // running images through the network, net will have a bunch of state in it related to
                        // the last batch of images it processed (e.g. outputs from each layer).  Since we
                        // don't care about saving that kind of stuff to disk we can tell the network to forget
                        // about that kind of transient data so that our file will be smaller.  We do this by
                        // "cleaning" the network before saving it.
                        net.Clean();
                        LossMulticlassLog.Serialize(net, "mnist_network.dat");
                        // Now if we later wanted to recall the network from disk we can simply say:
                        // deserialize("mnist_network.dat") >> net;


                        // Now let's run the training images through the network.  This statement runs all the
                        // images through it and asks the loss layer to convert the network's raw output into
                        // labels.  In our case, these labels are the numbers between 0 and 9.
                        using (var predictedLabels = net.Operator(trainingImages))
                        {
                            var numRight = 0;
                            var numWrong = 0;
                            // And then let's see if it classified them correctly.
                            for (var i = 0; i < trainingImages.Count; ++i)
                            {
                                if (predictedLabels[i] == trainingLabels[i])
                                    ++numRight;
                                else
                                    ++numWrong;
                            }

                            Console.WriteLine($"training num_right: {numRight}");
                            Console.WriteLine($"training num_wrong: {numWrong}");
                            Console.WriteLine($"training accuracy:  {numRight / (double)(numRight + numWrong)}");

                            // Let's also see if the network can correctly classify the testing images.  Since
                            // MNIST is an easy dataset, we should see at least 99% accuracy.
                            using (var predictedLabels2 = net.Operator(testingImages))
                            {
                                numRight = 0;
                                numWrong = 0;
                                for (var i = 0; i < testingImages.Count; ++i)
                                {
                                    if (predictedLabels2[i] == testingLabels[i])
                                        ++numRight;
                                    else
                                        ++numWrong;
                                }

                                Console.WriteLine($"testing num_right: {numRight}");
                                Console.WriteLine($"testing num_wrong: {numWrong}");
                                Console.WriteLine($"testing accuracy:  {numRight / (double)(numRight + numWrong)}");


                                // Finally, you can also save network parameters to XML files if you want to do
                                // something with the network in another tool.  For example, you could use dlib's
                                // tools/convert_dlib_nets_to_caffe to convert the network to a caffe model.
                                Dlib.NetToXml(net, "lenet.xml");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

    }

}