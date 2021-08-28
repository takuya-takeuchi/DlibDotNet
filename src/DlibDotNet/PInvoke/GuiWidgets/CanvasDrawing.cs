#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if !DLIB_NO_GUI_SUPPORT
        #region draw_line_canvas

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas(IntPtr canvas, IntPtr p1, IntPtr p2, IntPtr area, Array2DType pixelType, ref LabPixel color);

        #endregion

        #region draw_line_canvas_infinity

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_line_canvas_infinity(IntPtr canvas, IntPtr p1, IntPtr p2, Array2DType pixelType, ref LabPixel color);

        #endregion

        #region draw_rectangle_canvas

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas(IntPtr canvas, IntPtr rect, IntPtr area, Array2DType pixelType, ref LabPixel color);

        #endregion

        #region draw_rectangle_canvas_infinity

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_rectangle_canvas_infinity(IntPtr canvas, IntPtr rect, Array2DType pixelType, ref LabPixel color);

        #endregion
#endif

    }

}
#endif
