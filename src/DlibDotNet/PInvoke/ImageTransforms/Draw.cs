using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region draw_line

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref HsiPixel color);

        #endregion

        #region draw_rectangle

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref byte color, uint thickness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref ushort color, uint thickness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref int color, uint thickness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref float color, uint thickness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref double color, uint thickness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref RgbPixel color, uint thickness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref RgbAlphaPixel color, uint thickness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref HsiPixel color, uint thickness);

        #endregion

        #region tile_images

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType tile_images(Array2DType inType, IntPtr array, out IntPtr ret_image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType tile_images_matrix(MatrixElementType in_type, IntPtr images, out IntPtr ret_image);

        #endregion

    }

}