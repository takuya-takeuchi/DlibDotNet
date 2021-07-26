using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class PerspectiveWindow : BaseWindow
    {

        #region Constructors

        public PerspectiveWindow()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.NativePtr = NativeMethods.perspective_window_new();
#else
            throw new NotSupportedException();
#endif
        }

        public PerspectiveWindow(IEnumerable<Vector<double>> points)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                this.NativePtr = NativeMethods.perspective_window_new2(vector.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public PerspectiveWindow(IEnumerable<Vector<double>> points, string title)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            var str = Dlib.Encoding.GetBytes(title);
            using (var vector = new StdVector<Vector<double>>(points))
                this.NativePtr = NativeMethods.perspective_window_new3(vector.NativePtr, str, str.Length);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        #region AddOverlay(Vector<double>, Vector<double>, pixelcolor)

        public void AddOverlay(Vector<double> p1, Vector<double> p2, byte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, ushort color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, short color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Int16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, int color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Int32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, float color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Float, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, double color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Double, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, HsiPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Vector<double> p1, Vector<double> p2, LabPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.LabPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        public void AddOverlay(IEnumerable<Vector<double>> points)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay2(this.NativePtr, vector.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #region AddOverlay(IEnumerable<Vector<double>>, pixelcolor)

        public void AddOverlay(IEnumerable<Vector<double>> points, byte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, ushort color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, short color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Int16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, int color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Int32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, float color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Float, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, double color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Double, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, HsiPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, LabPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.LabPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        public void AddOverlay(IEnumerable<OverlayDot> overlay)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (overlay == null)
                throw new ArgumentNullException(nameof(overlay));

            overlay.ThrowIfDisposed();

            using (var vector = new StdVector<OverlayDot>(overlay))
                NativeMethods.perspective_window_add_overlay4(this.NativePtr, vector.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
#if !DLIB_NO_GUI_SUPPORT
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.perspective_window_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}
