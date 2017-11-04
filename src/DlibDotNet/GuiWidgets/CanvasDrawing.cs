using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        //#region DrawLine(Array2DBase canvas, Point p1, Point p2, pixelType color)

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2)
        //{
        //    DrawLine(canvas, p1, p2, new RgbPixel());
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, byte color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas_infinity(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        Native.Array2DType.UInt8,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, ushort color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas_infinity(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        Native.Array2DType.UInt16,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, float color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas_infinity(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        Native.Array2DType.Float,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, double color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas_infinity(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        Native.Array2DType.Double,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, RgbPixel color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas_infinity(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        Native.Array2DType.RgbPixel,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, RgbAlphaPixel color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas_infinity(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        Native.Array2DType.RgbAlphaPixel,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, HsiPixel color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    var ret = Native.draw_line_canvas_infinity(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        Native.Array2DType.HsiPixel,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //#endregion

        //#region DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, pixelType color)

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area)
        //{
        //    DrawLine(canvas, p1, p2, area, new RgbPixel());
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, byte color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));
        //    if (area == null)
        //        throw new ArgumentNullException(nameof(area));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    area.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        area.NativePtr,
        //        Native.Array2DType.UInt8,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, ushort color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));
        //    if (area == null)
        //        throw new ArgumentNullException(nameof(area));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    area.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        area.NativePtr,
        //        Native.Array2DType.UInt16,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, float color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));
        //    if (area == null)
        //        throw new ArgumentNullException(nameof(area));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    area.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        area.NativePtr,
        //        Native.Array2DType.Float,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, double color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));
        //    if (area == null)
        //        throw new ArgumentNullException(nameof(area));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    area.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        area.NativePtr,
        //        Native.Array2DType.Double,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, RgbPixel color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));
        //    if (area == null)
        //        throw new ArgumentNullException(nameof(area));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    area.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        area.NativePtr,
        //        Native.Array2DType.RgbPixel,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, RgbAlphaPixel color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));
        //    if (area == null)
        //        throw new ArgumentNullException(nameof(area));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    area.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        area.NativePtr,
        //        Native.Array2DType.RgbAlphaPixel,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, HsiPixel color)
        //{
        //    if (canvas == null)
        //        throw new ArgumentNullException(nameof(canvas));
        //    if (p1 == null)
        //        throw new ArgumentNullException(nameof(p1));
        //    if (p2 == null)
        //        throw new ArgumentNullException(nameof(p2));
        //    if (area == null)
        //        throw new ArgumentNullException(nameof(area));

        //    canvas.ThrowIfDisposed();
        //    p1.ThrowIfDisposed();
        //    p2.ThrowIfDisposed();
        //    area.ThrowIfDisposed();

        //    var ret = Native.draw_line_canvas(
        //        canvas.NativePtr,
        //        p1.NativePtr,
        //        p2.NativePtr,
        //        area.NativePtr,
        //        Native.Array2DType.HsiPixel,
        //        ref color);
        //    switch (ret)
        //    {
        //        case Native.ErrorType.ArrayTypeNotSupport:
        //            throw new ArgumentException($"{color} is not supported.");
        //    }
        //}

        //#endregion

        internal sealed partial class Native
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

}