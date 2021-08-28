#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void load_mnist_dataset(byte[] folderPath,
                                                     int folderPathLength,
                                                     out IntPtr training_images,
                                                     out IntPtr training_labels,
                                                     out IntPtr testing_images,
                                                     out IntPtr testing_labels);

    }

}
#endif
