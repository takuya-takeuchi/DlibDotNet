using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref BgrPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_all_pixels(Array2DType out_type, IntPtr out_img, Array2DType in_type, ref LabPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_image(Array2DType out_type,
                                                    IntPtr out_img,
                                                    Array2DType in_type,
                                                    IntPtr in_img);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assign_image_matrix(MatrixElementType out_type,
                                                           IntPtr out_img,
                                                           MatrixElementType in_type,
                                                           IntPtr in_img);

    }

}