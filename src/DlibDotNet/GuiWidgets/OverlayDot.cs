using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class PerspectiveWindow
    {

        public sealed class OverlayDot : DlibObject
        {

            #region Constructors

            public OverlayDot(Vector<double> point) :
                this(NativeMethods.perspective_window_overlay_dot_new(point.NativePtr))
            {
            }

            public OverlayDot(Vector<double> point, byte pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.UInt8, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, ushort pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.UInt16, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, short pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Int16, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, int pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Int32, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, float pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Float, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, double pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.Double, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, RgbPixel pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.RgbPixel, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, RgbAlphaPixel pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, HsiPixel pixel) :
                this(NativeMethods.perspective_window_overlay_dot_new2(point.NativePtr, NativeMethods.Array2DType.HsiPixel, ref pixel))
            {
            }

            internal OverlayDot(IntPtr ptr)
            {
                if (ptr == IntPtr.Zero)
                    throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

                this.NativePtr = ptr;
            }

            #endregion

            #region Properties

            public RgbPixel Color
            {
                get
                {
                    var color = new RgbPixel();
                    NativeMethods.perspective_window_overlay_dot_color(this.NativePtr, ref color);
                    return color;
                }
            }

            public Vector<double> Point
            {
                get
                {
                    NativeMethods.perspective_window_overlay_dot_p(this.NativePtr, out var point);
                    return new Vector<double>(point);
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
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.perspective_window_overlay_dot_delete(this.NativePtr);
            }

            #endregion

            #endregion

        }

    }

}
