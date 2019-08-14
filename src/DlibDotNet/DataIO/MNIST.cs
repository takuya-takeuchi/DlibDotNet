using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void LoadMNISTDataset(string folderPath,
                                            out IList<Matrix<byte>> trainingImages,
                                            out IList<uint> trainingLabels,
                                            out IList<Matrix<byte>> testingImages,
                                            out IList<uint> testingLabels)
        {
            if (folderPath == null)
                throw new ArgumentNullException(nameof(folderPath));
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException();

            trainingImages = null;
            trainingLabels = null;
            testingImages = null;
            testingLabels = null;

            var str = Encoding.GetBytes(folderPath);
            var strLength = str.Length;
            Array.Resize(ref str, strLength + 1);
            str[strLength] = (byte)'\0';

            NativeMethods.load_mnist_dataset(str, 
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

    }

}