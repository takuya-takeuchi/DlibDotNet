using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref uint color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref sbyte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref short color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref int color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref HsiPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_image(Array2DType out_type,
                                                    IntPtr out_img,
                                                    Array2DType in_type,
                                                    IntPtr in_img);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assign_image_matrix(MatrixElementType out_type,
                                                           IntPtr out_img,
                                                           MatrixElementType in_type,
                                                           IntPtr in_img);

    }

}