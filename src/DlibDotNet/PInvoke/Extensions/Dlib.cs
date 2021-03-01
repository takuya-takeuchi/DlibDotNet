using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region Array2D

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void extensions_convert_array_to_managed_image(Array2DType src_type, IntPtr src, IntPtr dst, bool rgb_reverse, uint rows, uint columns, uint steps, uint channels);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void extensions_convert_managed_image_to_array(IntPtr src, Array2DType dst_type, IntPtr dst, bool rgb_reverse, uint rows, uint columns, uint steps, uint channels);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void extensions_convert_managed_image_to_array_by_palette(IntPtr src, Array2DType dst_type, IntPtr dst, RgbPixel[] palette, uint rows, uint columns, uint steps, uint channels);

        #endregion

        #region Matrix

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void extensions_convert_matrix_to_managed_image(MatrixElementType src_type, IntPtr src, IntPtr dst, bool rgb_reverse, uint rows, uint columns, uint steps, uint channels);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void extensions_convert_managed_image_to_matrix(IntPtr src, MatrixElementType dst_type, IntPtr dst, bool rgb_reverse, uint rows, uint columns, uint steps, uint channels);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void extensions_convert_managed_image_to_matrix_by_palette(IntPtr src, MatrixElementType dst_type, IntPtr dst, RgbPixel[] palette, uint rows, uint columns, uint steps, uint channels);

        #endregion

    }

}