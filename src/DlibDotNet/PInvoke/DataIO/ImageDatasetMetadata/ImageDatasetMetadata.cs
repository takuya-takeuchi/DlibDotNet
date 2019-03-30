using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{
    
    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_image_dataset_metadata(IntPtr dataset, byte[] filename);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_image_dataset_metadata(IntPtr dataset, byte[] filename);

    }

}