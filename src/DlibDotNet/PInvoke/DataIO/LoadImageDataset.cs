using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_image_dataset_mmod_rect(MatrixElementType type, IntPtr images, IntPtr boxes, byte[] path);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_image_dataset_rectangle(MatrixElementType type, IntPtr images, IntPtr boxes, byte[] path);

    }

}
