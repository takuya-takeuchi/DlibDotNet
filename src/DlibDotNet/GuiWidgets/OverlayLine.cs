#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class ImageWindow
    {

        public sealed class OverlayLine : DlibObject
        {

            #region Fields

            private RgbAlphaPixel _Color;

            #endregion

            #region Constructors

            public OverlayLine()
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.image_window_overlay_line_new();
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayLine(Point p1, Point p2, RgbPixel pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                using(var native1 = p1.ToNative())
                using (var native2 = p2.ToNative())
                    this.NativePtr = NativeMethods.image_window_overlay_line_new_rgb(native1.NativePtr, native2.NativePtr, pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayLine(Point p1, Point p2, BgrPixel pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                using (var native1 = p1.ToNative())
                using (var native2 = p2.ToNative())
                    this.NativePtr = NativeMethods.image_window_overlay_line_new_bgr(native1.NativePtr, native2.NativePtr, pixel);
#else
                throw new NotSupportedException();
#endif
            }

            //public OverlayLine(Point p1, Point p2, RgbAlphaPixel color)
            //{
            //    if (p1 == null)
            //        throw new ArgumentNullException(nameof(p1));
            //    if (p2 == null)
            //        throw new ArgumentNullException(nameof(p2));

            //    p1.ThrowIfDisposed();
            //    p2.ThrowIfDisposed();
            //}

            internal OverlayLine(IntPtr ptr)
            {
#if !DLIB_NO_GUI_SUPPORT
                if (ptr == IntPtr.Zero)
                    throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

                this.NativePtr = ptr;
#else
                throw new NotSupportedException();
#endif
            }

            #endregion

            #region Properties

            public RgbAlphaPixel Color
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    NativeMethods.image_window_overlay_line_color(this.NativePtr, ref this._Color);
                    return this._Color;
#else
                    throw new NotSupportedException();
#endif
                }
            }

            public Point Point1
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    NativeMethods.image_window_overlay_line_p1(this.NativePtr, out var point);
                    return new Point(point);
#else
                    throw new NotSupportedException();
#endif
                }
            }

            public Point Point2
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    NativeMethods.image_window_overlay_line_p2(this.NativePtr, out var point);
                    return new Point(point);
#else
                    throw new NotSupportedException();
#endif
                }
            }

            #endregion

            #region Methods

            #region Overrides

            /// <summary>
            /// Releases all unmanaged resources.
            /// </summary>
            protected override void DisposeUnmanaged()
            {
#if !DLIB_NO_GUI_SUPPORT
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.image_window_overlay_line_delete(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }

            #endregion

            #endregion

        }

    }

}

#endif
