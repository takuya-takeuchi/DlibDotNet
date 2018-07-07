using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void LoadMNISTDataset(string folderPath,
                                            out Matrix<byte>[] trainingImages,
                                            out uint[] trainingLabels,
                                            out Matrix<byte>[] testingImages,
                                            out uint[] testingLabels)
        {
            if (folderPath == null)
                throw new ArgumentNullException(nameof(folderPath));
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException();

            trainingImages = null;
            trainingLabels = null;
            testingImages = null;
            testingLabels = null;

            var str = Encoding.UTF8.GetBytes(folderPath);

            Native.load_mnist_dataset(str, 
                                      out var retTrainingImages,
                                      out var retTrainingLabels, 
                                      out var retTestingImages,
                                      out var retTestingLabels);

            using (var tmp = new StdVector<Matrix<byte>>(retTrainingImages))
                trainingImages = tmp.ToArray();
            using (var tmp = new StdVector<uint>(retTrainingLabels))
                trainingLabels = tmp.ToArray();
            using (var tmp = new StdVector<Matrix<byte>>(retTestingImages))
                testingImages = tmp.ToArray();
            using (var tmp = new StdVector<uint>(retTestingLabels))
                testingLabels = tmp.ToArray();
        }
        
        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void load_mnist_dataset(byte[] folderPath, 
                                                         out IntPtr training_images,
                                                         out IntPtr training_labels,
                                                         out IntPtr testing_images,
                                                         out IntPtr testing_labels);

        }

    }

}