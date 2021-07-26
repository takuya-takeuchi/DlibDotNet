using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region DrawLine(Array2DBase canvas, Point p1, Point p2, pixelType color)

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2)
        {
#if !DLIB_NO_GUI_SUPPORT
           DrawLine(canvas, p1, p2, new RgbPixel());
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, byte color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                             p1.NativePtr,
                                                             p2.NativePtr,
                                                             NativeMethods.Array2DType.UInt8,
                                                             ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, ushort color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                             p1.NativePtr,
                                                             p2.NativePtr,
                                                             NativeMethods.Array2DType.UInt16,
                                                             ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, float color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                             p1.NativePtr,
                                                             p2.NativePtr,
                                                             NativeMethods.Array2DType.Float,
                                                             ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, double color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                             p1.NativePtr,
                                                             p2.NativePtr,
                                                             NativeMethods.Array2DType.Double,
                                                             ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                             p1.NativePtr,
                                                             p2.NativePtr,
                                                             NativeMethods.Array2DType.RgbPixel,
                                                             ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                             p1.NativePtr,
                                                             p2.NativePtr,
                                                             NativeMethods.Array2DType.RgbAlphaPixel,
                                                             ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, HsiPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                             p1.NativePtr,
                                                             p2.NativePtr,
                                                             NativeMethods.Array2DType.HsiPixel,
                                                             ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, pixelType color)

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area)
        {
#if !DLIB_NO_GUI_SUPPORT
           DrawLine(canvas, p1, p2, area, new RgbPixel());
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, byte color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));
           if (area == null)
               throw new ArgumentNullException(nameof(area));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           area.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                    p1.NativePtr,
                                                    p2.NativePtr,
                                                    area.NativePtr,
                                                    NativeMethods.Array2DType.UInt8,
                                                    ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, ushort color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));
           if (area == null)
               throw new ArgumentNullException(nameof(area));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           area.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                    p1.NativePtr,
                                                    p2.NativePtr,
                                                    area.NativePtr,
                                                    NativeMethods.Array2DType.UInt16,
                                                    ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, float color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));
           if (area == null)
               throw new ArgumentNullException(nameof(area));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           area.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                    p1.NativePtr,
                                                    p2.NativePtr,
                                                    area.NativePtr,
                                                    NativeMethods.Array2DType.Float,
                                                    ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, double color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));
           if (area == null)
               throw new ArgumentNullException(nameof(area));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           area.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                    p1.NativePtr,
                                                    p2.NativePtr,
                                                    area.NativePtr,
                                                    NativeMethods.Array2DType.Double,
                                                    ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));
           if (area == null)
               throw new ArgumentNullException(nameof(area));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           area.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                    p1.NativePtr,
                                                    p2.NativePtr,
                                                    area.NativePtr,
                                                    NativeMethods.Array2DType.RgbPixel,
                                                    ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));
           if (area == null)
               throw new ArgumentNullException(nameof(area));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           area.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                    p1.NativePtr,
                                                    p2.NativePtr,
                                                    area.NativePtr,
                                                    NativeMethods.Array2DType.RgbAlphaPixel,
                                                    ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        public static void DrawLine(Array2DBase canvas, Point p1, Point p2, Rectangle area, HsiPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
           if (canvas == null)
               throw new ArgumentNullException(nameof(canvas));
           if (p1 == null)
               throw new ArgumentNullException(nameof(p1));
           if (p2 == null)
               throw new ArgumentNullException(nameof(p2));
           if (area == null)
               throw new ArgumentNullException(nameof(area));

           canvas.ThrowIfDisposed();
           p1.ThrowIfDisposed();
           p2.ThrowIfDisposed();
           area.ThrowIfDisposed();

           var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                    p1.NativePtr,
                                                    p2.NativePtr,
                                                    area.NativePtr,
                                                    NativeMethods.Array2DType.HsiPixel,
                                                    ref color);
           switch (ret)
           {
               case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                   throw new ArgumentException($"{color} is not supported.");
           }
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

    }

}