using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class ImageWindow
    {

        public sealed class OverlayLine : DlibObject
        {

            #region Events
            #endregion

            #region Fields

            private RgbAlphaPixel _Color;

            #endregion

            #region Constructors

            public OverlayLine()
                : this(Native.image_window_overlay_line_new())
            {
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
                if (ptr == IntPtr.Zero)
                    throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

                this.NativePtr = ptr;
            }

            #endregion

            #region Properties

            public RgbAlphaPixel Color
            {
                get
                {
                    Native.image_window_overlay_line_color(this.NativePtr, ref this._Color);
                    return this._Color;
                }
            }

            public Point Point1
            {
                get
                {
                    Native.image_window_overlay_line_p1(this.NativePtr, out var point);
                    return new Point(point);
                }
            }

            public Point Point2
            {
                get
                {
                    Native.image_window_overlay_line_p2(this.NativePtr, out var point);
                    return new Point(point);
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

                Native.image_window_overlay_line_delete(this.NativePtr);
            }

            #endregion

            #endregion

            internal sealed class Native
            {

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr image_window_overlay_line_new();

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                [return: MarshalAs(UnmanagedType.U1)]
                public static extern bool image_window_overlay_line_p1(IntPtr line, out IntPtr point);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                [return: MarshalAs(UnmanagedType.U1)]
                public static extern bool image_window_overlay_line_p2(IntPtr line, out IntPtr point);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                [return: MarshalAs(UnmanagedType.U1)]
                public static extern bool image_window_overlay_line_color(IntPtr line, ref RgbAlphaPixel color);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void image_window_overlay_line_delete(IntPtr line);

            }

        }

    }

}
