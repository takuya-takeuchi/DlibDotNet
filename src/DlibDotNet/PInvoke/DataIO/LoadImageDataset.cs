#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType load_image_dataset_array_full_object_detection(Array2DType type,
                                                                                      IntPtr array_array2d,
                                                                                      IntPtr boxes,
                                                                                      byte[] path,
                                                                                      int pathLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType load_image_dataset_mmod_rect(MatrixElementType type, IntPtr images, IntPtr boxes, byte[] path, int pathLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType load_image_dataset_rectangle(MatrixElementType type, IntPtr images, IntPtr boxes, byte[] path, int pathLength);

    }

}

#endif
