using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region draw_line_canvas

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref HsiPixel color);

        #endregion

        #region draw_line_canvas_infinity

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref HsiPixel color);

        #endregion

        #region draw_rectangle_canvas

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref int color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref HsiPixel color);

        #endregion

        #region draw_rectangle_canvas_infinity

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref int color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref HsiPixel color);

        #endregion

    }

}