#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{
    
    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType load_image_dataset_metadata(IntPtr dataset, byte[] filename, int filenameLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType save_image_dataset_metadata(IntPtr dataset, byte[] filename, int filenameLength);

    }

}
#endif
