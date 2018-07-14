using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class PerspectiveWindow
    {

        public sealed class OverlayDot : DlibObject
        {

            #region Constructors

            public OverlayDot(Vector<double> point) :
                this(Native.perspective_window_overlay_dot_new(point.NativePtr))
            {
            }

            public OverlayDot(Vector<double> point, byte pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.UInt8, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, ushort pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.UInt16, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, short pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.Int16, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, int pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.Int32, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, float pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.Float, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, double pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.Double, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, RgbPixel pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.RgbPixel, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, RgbAlphaPixel pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.RgbAlphaPixel, ref pixel))
            {
            }

            public OverlayDot(Vector<double> point, HsiPixel pixel) :
                this(Native.perspective_window_overlay_dot_new2(point.NativePtr, Dlib.Native.Array2DType.HsiPixel, ref pixel))
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

            private RgbPixel _Color;

            public RgbPixel Color
            {
                get
                {
                    var color = new RgbPixel();
                    Native.perspective_window_overlay_dot_color(this.NativePtr, ref color);
                    return color;
                }
            }

            public Vector<double> Point
            {
                get
                {
                    Native.perspective_window_overlay_dot_p(this.NativePtr, out var point);
                    return new Vector<double>(point);
                }
            }

            #endregion

            #region Methods

            #region Overrides

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                Native.perspective_window_overlay_dot_delete(this.NativePtr);
            }

            #endregion

            #endregion

            internal sealed class Native
            {

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new(IntPtr v);
                
                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref byte color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref ushort color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref short color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref int color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref float color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref double color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref RgbPixel color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref RgbAlphaPixel color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr perspective_window_overlay_dot_new2(IntPtr v, Dlib.Native.Array2DType type, ref HsiPixel color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                [return: MarshalAs(UnmanagedType.U1)]
                public static extern bool perspective_window_overlay_dot_p(IntPtr dot, out IntPtr vector);
                
                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                [return: MarshalAs(UnmanagedType.U1)]
                public static extern bool perspective_window_overlay_dot_color(IntPtr dot, ref RgbPixel color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void perspective_window_overlay_dot_delete(IntPtr dot);

            }

        }

    }

}
