#if !LITE
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                                  p1Native.NativePtr,
                                                                  p2Native.NativePtr,
                                                                  NativeMethods.Array2DType.UInt8,
                                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                                  p1Native.NativePtr,
                                                                  p2Native.NativePtr,
                                                                  NativeMethods.Array2DType.UInt16,
                                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                                  p1Native.NativePtr,
                                                                  p2Native.NativePtr,
                                                                  NativeMethods.Array2DType.Float,
                                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                                  p1Native.NativePtr,
                                                                  p2Native.NativePtr,
                                                                  NativeMethods.Array2DType.Double,
                                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                                  p1Native.NativePtr,
                                                                  p2Native.NativePtr,
                                                                  NativeMethods.Array2DType.RgbPixel,
                                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                                  p1Native.NativePtr,
                                                                  p2Native.NativePtr,
                                                                  NativeMethods.Array2DType.RgbAlphaPixel,
                                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas_infinity(canvas.NativePtr,
                                                                  p1Native.NativePtr,
                                                                  p2Native.NativePtr,
                                                                  NativeMethods.Array2DType.HsiPixel,
                                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            using (var areaNative = area.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                         p1Native.NativePtr,
                                                         p2Native.NativePtr,
                                                         areaNative.NativePtr,
                                                         NativeMethods.Array2DType.UInt8,
                                                         ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            using (var areaNative = area.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                         p1Native.NativePtr,
                                                         p2Native.NativePtr,
                                                         areaNative.NativePtr,
                                                         NativeMethods.Array2DType.UInt16,
                                                         ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            using (var areaNative = area.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                         p1Native.NativePtr,
                                                         p2Native.NativePtr,
                                                         areaNative.NativePtr,
                                                         NativeMethods.Array2DType.Float,
                                                         ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            using (var areaNative = area.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                         p1Native.NativePtr,
                                                         p2Native.NativePtr,
                                                         areaNative.NativePtr,
                                                         NativeMethods.Array2DType.Double,
                                                         ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            using (var areaNative = area.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                         p1Native.NativePtr,
                                                         p2Native.NativePtr,
                                                         areaNative.NativePtr,
                                                         NativeMethods.Array2DType.RgbPixel,
                                                         ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            using (var areaNative = area.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                         p1Native.NativePtr,
                                                         p2Native.NativePtr,
                                                         areaNative.NativePtr,
                                                         NativeMethods.Array2DType.RgbAlphaPixel,
                                                         ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            canvas.ThrowIfDisposed();

            using (var p1Native = p1.ToNative())
            using (var p2Native = p2.ToNative())
            using (var areaNative = area.ToNative())
            {
                var ret = NativeMethods.draw_line_canvas(canvas.NativePtr,
                                                         p1Native.NativePtr,
                                                         p2Native.NativePtr,
                                                         areaNative.NativePtr,
                                                         NativeMethods.Array2DType.HsiPixel,
                                                         ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
#else
        throw new NotSupportedException();
#endif
        }

        #endregion

    }

}
#endif
