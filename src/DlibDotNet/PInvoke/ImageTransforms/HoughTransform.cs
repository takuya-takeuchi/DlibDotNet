#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr hough_transform_new(uint size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool hough_transform_nc(IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool hough_transform_nr(IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool hough_transform_size(IntPtr obj, out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType hough_transform_get_best_hough_point(IntPtr obj, IntPtr p, Array2DType type, IntPtr img, out IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType hough_transform_operator(IntPtr obj,
                                                                Array2DType in_type,
                                                                IntPtr in_img,
                                                                Array2DType out_type,
                                                                IntPtr out_img,
                                                                IntPtr rectangle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr hough_transform_get_line(IntPtr obj, IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool hough_transform_get_rect(IntPtr obj, out IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void hough_transform_delete(IntPtr obj);

    }

}
#endif
