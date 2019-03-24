using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class ImageWindow : BaseWindow
    {

        #region Constructors

        public ImageWindow()
        {
            this.NativePtr = NativeMethods.image_window_new();
        }

        public ImageWindow(Array2DBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            this.NativePtr = NativeMethods.image_window_new_array2d1(image.ImageType.ToNativeArray2DType(), image.NativePtr);
        }

        public ImageWindow(Array2DBase image, string title)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            image.ThrowIfDisposed(nameof(image));

            var str = Dlib.Encoding.GetBytes(title);
            this.NativePtr = NativeMethods.image_window_new_array2d2(image.ImageType.ToNativeArray2DType(), image.NativePtr, str);
        }

        public ImageWindow(MatrixBase matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed(nameof(matrix));

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            this.NativePtr = NativeMethods.image_window_new_matrix1(type, matrix.NativePtr);
        }

        public ImageWindow(MatrixBase matrix, string title)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            matrix.ThrowIfDisposed(nameof(matrix));

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var str = Dlib.Encoding.GetBytes(title);
            this.NativePtr = NativeMethods.image_window_new_matrix2(type, matrix.NativePtr, str);
        }

        public ImageWindow(MatrixOp matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed(nameof(matrix));

            if(matrix.TemplateRows == -1 && matrix.TemplateColumns == -1)
                this.NativePtr = NativeMethods.image_window_new_matrix_op1(matrix.ElementType, matrix.Array2DType, matrix.NativePtr);
            else
                this.NativePtr = NativeMethods.image_window_new_matrix_op3(matrix.ElementType, matrix.MatrixElementType, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns);
        }

        public ImageWindow(MatrixOp matrix, string title)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            matrix.ThrowIfDisposed(nameof(matrix));

            var str = Dlib.Encoding.GetBytes(title);
            if (matrix.TemplateRows == -1 && matrix.TemplateColumns == -1)
                this.NativePtr = NativeMethods.image_window_new_matrix_op2(matrix.ElementType, matrix.Array2DType, matrix.NativePtr, str);
            else
                this.NativePtr = NativeMethods.image_window_new_matrix_op4(matrix.ElementType, matrix.MatrixElementType, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, str);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        #region AddOverlay(Rectangle rect, pixel_type color)

        public void AddOverlay(Rectangle rect)
        {
            this.AddOverlay(rect, new RgbPixel
            {
                Red = 255
            });
        }

        public void AddOverlay(Rectangle rect, byte color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
        }

        public void AddOverlay(Rectangle rect, ushort color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
        }

        public void AddOverlay(Rectangle rect, float color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Float, ref color);
        }

        public void AddOverlay(Rectangle rect, double color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Double, ref color);
        }

        public void AddOverlay(Rectangle rect, RgbPixel color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
        }

        public void AddOverlay(Rectangle rect, RgbAlphaPixel color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
        }

        public void AddOverlay(Rectangle rect, HsiPixel color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
        }

        #endregion

        #region AddOverlay(IEnumerable<Rectangle> rects, pixel_type color)

        public void AddOverlay(IEnumerable<Rectangle> rects)
        {
            this.AddOverlay(rects, new RgbPixel
            {
                Red = 255
            });
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, byte color)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, ushort color)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, float color)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Float, ref color);
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, double color)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Double, ref color);
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, RgbPixel color)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, RgbAlphaPixel color)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, HsiPixel color)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
        }

        #endregion

        #region AddOverlay(DRectangle rect, pixel_type color)

        public void AddOverlay(DRectangle rect)
        {
            this.AddOverlay(rect, new RgbPixel
            {
                Red = 255
            });
        }

        public void AddOverlay(DRectangle rect, byte color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
        }

        public void AddOverlay(DRectangle rect, ushort color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
        }

        public void AddOverlay(DRectangle rect, float color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Float, ref color);
        }

        public void AddOverlay(DRectangle rect, double color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Double, ref color);
        }

        public void AddOverlay(DRectangle rect, RgbPixel color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
        }

        public void AddOverlay(DRectangle rect, RgbAlphaPixel color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
        }

        public void AddOverlay(DRectangle rect, HsiPixel color)
        {
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
        }

        #endregion

        #region AddOverlay(OverlayLine line)

        public void AddOverlay(OverlayLine line)
        {
            this.ThrowIfDisposed();

            if (line == null)
                throw new ArgumentNullException(nameof(line));

            line.ThrowIfDisposed();

            NativeMethods.image_window_add_overlay4(this.NativePtr, line.NativePtr);
        }

        #endregion

        #region AddOverlay(IEnumerable<OverlayLine> lines)

        public void AddOverlay(IEnumerable<OverlayLine> lines)
        {
            this.ThrowIfDisposed();

            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            using (var vector = new StdVector<OverlayLine>(lines))
                NativeMethods.image_window_add_overlay5(this.NativePtr, vector.NativePtr);
        }

        #endregion

        public void ClearOverlay()
        {
            this.ThrowIfDisposed();
            NativeMethods.image_window_clear_overlay(this.NativePtr);
        }

        public bool GetNextDoubleClick(out Point p)
        {
            this.ThrowIfDisposed();

            var ret = NativeMethods.image_window_get_next_double_click(this.NativePtr, out var ptr);
            p = new Point(ptr);
            return ret;
        }

        public bool GetNextDoubleClick(out Point p, out uint mouseButton)
        {
            this.ThrowIfDisposed();

            var ret = NativeMethods.image_window_get_next_double_click2(this.NativePtr, out var ptr, out mouseButton);
            p = new Point(ptr);
            return ret;
        }

        public bool IsClosed()
        {
            this.ThrowIfDisposed();

            return NativeMethods.image_window_is_closed(this.NativePtr);
        }

        public void SetImage(Array2DBase image)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            var ret = NativeMethods.image_window_set_image_array2d(this.NativePtr, image.ImageType.ToNativeArray2DType(), image.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{image.ImageType} is not supported.");
            }
        }

        public void SetImage(MatrixBase matrix)
        {
            this.ThrowIfDisposed();

            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var ret = NativeMethods.image_window_set_image_matrix(this.NativePtr, matrix.MatrixElementType.ToNativeMatrixElementType(), matrix.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");
            }
        }

        public void SetImage(MatrixOp matrix)
        {
            this.ThrowIfDisposed();

            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            NativeMethods.ErrorType ret;
            switch (matrix.ElementType)
            {
                case NativeMethods.ElementType.OpHeatmap:
                case NativeMethods.ElementType.OpJet:
                case NativeMethods.ElementType.OpArray2DToMat:
                case NativeMethods.ElementType.OpTrans:
                case NativeMethods.ElementType.OpStdVectToMat:
                    ret = NativeMethods.image_window_set_image_matrix_op_array2d(this.NativePtr, matrix.ElementType, matrix.Array2DType, matrix.NativePtr);
                    break;
                case NativeMethods.ElementType.OpJoinRows:
                    ret = NativeMethods.image_window_set_image_matrix_op_matrix(this.NativePtr, matrix.ElementType, matrix.MatrixElementType, matrix.NativePtr);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (ret)
            {
                case NativeMethods.ErrorType.InputElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.ElementType} is not supported.");
            }
        }

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.image_window_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
