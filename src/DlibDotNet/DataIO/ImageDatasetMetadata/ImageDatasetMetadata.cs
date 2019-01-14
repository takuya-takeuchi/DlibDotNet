using System;
using System.IO;
using System.Text;
using DlibDotNet.ImageDatasetMetadata;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        public static class ImageDatasetMetadata
        {

            #region Methods

            public static Dataset LoadImageDatasetMetadata(string filename)
            {
                if (filename == null)
                    throw new ArgumentNullException(nameof(filename));

                if (!File.Exists(filename))
                    throw new FileNotFoundException($"{filename} is not found", filename);

                var str = Encoding.UTF8.GetBytes(filename);

                var dataset = new Dataset();
                NativeMethods.load_image_dataset_metadata(dataset.NativePtr, str);

                return dataset;
            }

            public static void SaveImageDatasetMetadata(Dataset dataset, string filename)
            {
                if (dataset == null)
                    throw new ArgumentNullException(nameof(dataset));
                if (filename == null)
                    throw new ArgumentNullException(nameof(filename));

                dataset.ThrowIfDisposed();

                var str = Encoding.UTF8.GetBytes(filename);

                NativeMethods.save_image_dataset_metadata(dataset.NativePtr, str);
            }

            #endregion

        }

    }

}