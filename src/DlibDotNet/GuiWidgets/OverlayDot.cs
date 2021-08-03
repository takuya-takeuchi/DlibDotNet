using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class PerspectiveWindow
    {

        public sealed class OverlayDot : DlibObject
        {

            #region Constructors

            public OverlayDot(Vector<double> point)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new(point.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, byte pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.UInt8, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, ushort pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.UInt16, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, short pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Int16, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, int pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Int32, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, float pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Float, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, double pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Double, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, RgbPixel pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.RgbPixel, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, RgbAlphaPixel pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, HsiPixel pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.HsiPixel, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            public OverlayDot(Vector<double> point, LabPixel pixel)
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.LabPixel, ref pixel);
#else
                throw new NotSupportedException();
#endif
            }

            internal OverlayDot(IntPtr ptr)
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

            public RgbPixel Color
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    var color = new RgbPixel();
                    NativeMethods.perspective_window_overlay_dot_color(this.NativePtr, ref color);
                    return color;
#else
                    throw new NotSupportedException();
#endif
                }
            }

            public Vector<double> Point
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    NativeMethods.perspective_window_overlay_dot_p(this.NativePtr, out var point);
                    return new Vector<double>(point);
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

                NativeMethods.perspective_window_overlay_dot_delete(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }

            #endregion

            #endregion

        }

    }

}
