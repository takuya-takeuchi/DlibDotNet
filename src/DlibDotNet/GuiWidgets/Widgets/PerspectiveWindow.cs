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
            this.NativePtr = NativeMethods.perspective_window_new();
        }

        public PerspectiveWindow(IEnumerable<Vector<double>> points)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                this.NativePtr = NativeMethods.perspective_window_new2(vector.NativePtr);
        }

        public PerspectiveWindow(IEnumerable<Vector<double>> points, string title)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            var str = Dlib.Encoding.GetBytes(title);
            using (var vector = new StdVector<Vector<double>>(points))
                this.NativePtr = NativeMethods.perspective_window_new3(vector.NativePtr, str, str.Length);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Int16, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Int32, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Float, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.Double, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
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

            NativeMethods.perspective_window_add_overlay(this.NativePtr, p1.NativePtr, p2.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
        }

        #endregion

        public void AddOverlay(IEnumerable<Vector<double>> points)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay2(this.NativePtr, vector.NativePtr);
        }

        #region AddOverlay(IEnumerable<Vector<double>>, pixelcolor)

        public void AddOverlay(IEnumerable<Vector<double>> points, byte color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, ushort color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, short color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Int16, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, int color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Int32, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, float color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Float, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, double color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Double, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, RgbPixel color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, RgbAlphaPixel color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
        }

        public void AddOverlay(IEnumerable<Vector<double>> points, HsiPixel color)
        {
            this.ThrowIfDisposed();

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            points.ThrowIfDisposed();

            using (var vector = new StdVector<Vector<double>>(points))
                NativeMethods.perspective_window_add_overlay3(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
        }

        #endregion

        public void AddOverlay(IEnumerable<OverlayDot> overlay)
        {
            this.ThrowIfDisposed();

            if (overlay == null)
                throw new ArgumentNullException(nameof(overlay));

            overlay.ThrowIfDisposed();

            using (var vector = new StdVector<OverlayDot>(overlay))
                NativeMethods.perspective_window_add_overlay4(this.NativePtr, vector.NativePtr);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.perspective_window_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
