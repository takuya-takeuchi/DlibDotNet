using System;
using System.IO;
using System.Runtime.InteropServices;
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
                Native.load_image_dataset_metadata(dataset.NativePtr, str);

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

                Native.save_image_dataset_metadata(dataset.NativePtr, str);
            }

            #endregion

            internal sealed class Native
            {

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void load_image_dataset_metadata(IntPtr dataset, byte[] filename);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void save_image_dataset_metadata(IntPtr dataset, byte[] filename);

            }

        }

    }

}