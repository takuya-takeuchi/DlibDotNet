using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class PerspectiveWindow : BaseWindow
    {

        #region Constructors

        public PerspectiveWindow()
        {
            this.NativePtr = Native.perspective_window_new();
        }

        public PerspectiveWindow(IEnumerable<Vector<double>> points)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                this.NativePtr = Native.perspective_window_new2(vector.NativePtr);
        }

        public PerspectiveWindow(IEnumerable<Vector<double>> points, string title)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            var str = Encoding.UTF8.GetBytes(title);
            using (var vector = new VectorOfVectorDouble(points))
                this.NativePtr = Native.perspective_window_new3(vector.NativePtr, str);
        }

        #endregion

        #region Methods

        #region AddOverlay(Vector<double>, Vector<double>, pixelcolor)

        public void AddOverlay(Vector<double> p1, Vector<double> p2, byte color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.UInt8, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, ushort color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.UInt16, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, short color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.Int16, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, int color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.Int32, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, float color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.Float, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, double color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.Double, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, RgbPixel color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.RgbPixel, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, RgbAlphaPixel color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.RgbAlphaPixel, ref color);
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, HsiPixel color)
        {
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            Native.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, Dlib.Native.Array2DType.HsiPixel, ref color);
        }

        #endregion

        public void AddOverlay(IEnumerable<Vector<double>> points)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay2(this.NativePtr, vector.NativePtr);
        }

        #region AddOverlay(IEnumerable<Vector<double>>, pixelcolor)

        public void AddOverlay(IEnumerable<Vector<double>> points, byte color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.UInt8, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, ushort color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.UInt16, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, short color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.Int16, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, int color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.Int32, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, float color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.Float, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, double color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.Double, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, RgbPixel color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.RgbPixel, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, RgbAlphaPixel color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.RgbAlphaPixel, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, HsiPixel color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new VectorOfVectorDouble(points))
                Native.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, Dlib.Native.Array2DType.HsiPixel, ref color);
        }

        #endregion

        public void AddOverlay(IEnumerable<OverlayDot> overlay)
        {
            this.ThrowIfDisposed();

            if (overlay == null)
                throw new ArgumentNullException(nameof(overlay));

            overlay.ThrowIfDisposed();

            using (var vector = new VectorOfPerspectiveWindowOverlayDot(overlay))
                Native.perspective_window_add_overlay4(this.NativePtr, vector.NativePtr);
        }

        #region Overrids

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.perspective_window_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr perspective_window_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr perspective_window_new2(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr perspective_window_new3(IntPtr vector, byte[] title);

            #region add_overlay(vector<double>, vector<double>, pixel_color)

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref byte color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref ushort color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref short color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref int color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref float color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref double color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref RgbPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref RgbAlphaPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay(IntPtr window, IntPtr p1, IntPtr p2, Dlib.Native.Array2DType type, ref HsiPixel color);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay2(IntPtr window, IntPtr vector);

            #region add_overlay3(vector<double>, vector<double>, pixel_color)

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref byte color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref ushort color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref short color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref int color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref float color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref double color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref RgbPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref RgbAlphaPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay3(IntPtr window, IntPtr vector, Dlib.Native.Array2DType type, ref HsiPixel color);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType perspective_window_add_overlay4(IntPtr window, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void perspective_window_delete(IntPtr ptr);

        }

    }

}
