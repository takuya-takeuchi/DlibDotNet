#if !LITE
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
#if !DLIB_NO_GUI_SUPPORT
            this.NativePtr = NativeMethods.image_window_new();
#else
            throw new NotSupportedException();
#endif
        }

        public ImageWindow(Array2DBase image)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed(nameof(image));

            this.NativePtr = NativeMethods.image_window_new_array2d1(image.ImageType.ToNativeArray2DType(), image.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public ImageWindow(Array2DBase image, string title)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            image.ThrowIfDisposed(nameof(image));

            var str = Dlib.Encoding.GetBytes(title);
            this.NativePtr = NativeMethods.image_window_new_array2d2(image.ImageType.ToNativeArray2DType(), image.NativePtr, str, str.Length);
#else
            throw new NotSupportedException();
#endif
        }

        public ImageWindow(MatrixBase matrix)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed(nameof(matrix));

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            this.NativePtr = NativeMethods.image_window_new_matrix1(type, matrix.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public ImageWindow(MatrixBase matrix, string title)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            matrix.ThrowIfDisposed(nameof(matrix));

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var str = Dlib.Encoding.GetBytes(title);
            this.NativePtr = NativeMethods.image_window_new_matrix2(type, matrix.NativePtr, str, str.Length);
#else
            throw new NotSupportedException();
#endif
        }

        public ImageWindow(MatrixOp matrix)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed(nameof(matrix));

            IntPtr ret;
            NativeMethods.ErrorType err;
            var tr = matrix.TemplateRows;
            var tc = matrix.TemplateColumns;
            var et = matrix.ElementType;
            var ptr = matrix.NativePtr;
            if (tr == -1 && tc == -1)
                err = NativeMethods.image_window_new_matrix_op1(et, matrix.Array2DType, ptr, out ret);
            else
                err = NativeMethods.image_window_new_matrix_op3(et, matrix.MatrixElementType, ptr, tr, tc, out ret);

            switch (err)
            {
                case NativeMethods.ErrorType.MatrixOpTypeNotSupport:
                    throw new ArgumentException($"{matrix.ElementType} is not supported.");
            }

            this.NativePtr = ret;
#else
            throw new NotSupportedException();
#endif
        }

        public ImageWindow(MatrixOp matrix, string title)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            matrix.ThrowIfDisposed(nameof(matrix));

            var str = Dlib.Encoding.GetBytes(title);

            IntPtr ret;
            NativeMethods.ErrorType err;
            var tr = matrix.TemplateRows;
            var tc = matrix.TemplateColumns;
            var et = matrix.ElementType;
            var ptr = matrix.NativePtr;
            if (tr == -1 && matrix.TemplateColumns == -1)
                err = NativeMethods.image_window_new_matrix_op2(et, matrix.Array2DType, ptr, str, str.Length, out ret);
            else
                err = NativeMethods.image_window_new_matrix_op4(et, matrix.MatrixElementType, ptr, tr, tc, str, str.Length, out ret);

            switch (err)
            {
                case NativeMethods.ErrorType.MatrixOpTypeNotSupport:
                    throw new ArgumentException($"{matrix.ElementType} is not supported.");
            }

            this.NativePtr = ret;
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        #region AddOverlay(Rectangle rect, pixel_type color)

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.AddOverlay(rect, new RgbPixel
            {
                Red = 255
            });
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="byte"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, byte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="ushort"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, ushort color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="uint"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, uint color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="sbyte"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, sbyte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="short"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, short color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="int"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, int color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="float"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, float color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Float, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="double"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, double color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Double, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="RgbPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="RgbAlphaPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="HsiPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, HsiPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="LabPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(Rectangle rect, LabPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.LabPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region AddOverlay(IEnumerable<Rectangle> rects, pixel_type color)

        public void AddOverlay(IEnumerable<Rectangle> rects)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.AddOverlay(rects, new RgbPixel
            {
                Red = 255
            });
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, byte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, ushort color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, uint color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.UInt32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, sbyte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Int8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, short color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Int16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, int color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Int32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, float color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Float, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, double color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.Double, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, HsiPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<Rectangle> rects, LabPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<Rectangle>(rects))
                NativeMethods.image_window_add_overlay2(this.NativePtr, vector.NativePtr, NativeMethods.Array2DType.LabPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region AddOverlay(DRectangle rect, pixel_type color)

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.AddOverlay(rect, new RgbPixel
            {
                Red = 255
            });
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="byte"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, byte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="ushort"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, ushort color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="uint"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, uint color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="sbyte"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, sbyte color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int8, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="short"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, short color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int16, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="int"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, int color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int32, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="float"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, float color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Float, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="double"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, double color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Double, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="RgbPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, RgbPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="RgbAlphaPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="HsiPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, HsiPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        /// <summary>
        /// Adds the given overlay rectangle into this object such that it will be displayed. 
        /// </summary>
        /// <param name="rect">A <see cref="DRectangle"/> structure that represents the rectangle to be displayed.</param>
        /// <param name="color">A <see cref="LabPixel"/> value that represents a color.</param>
        /// <exception cref="ObjectDisposedException"><see cref="ImageWindow"/> is disposed.</exception>
        public void AddOverlay(DRectangle rect, LabPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
                NativeMethods.image_window_add_overlay3(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.LabPixel, ref color);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region AddOverlay(Rectangle rect, pixel_type color, string str)

        public void AddOverlay(Rectangle rect, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.AddOverlay(rect, new RgbPixel
            {
                Red = 255
            },
            str);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, sbyte color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int8, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, short color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int16, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, int color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Int32, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, byte color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt8, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, ushort color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt16, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, uint color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.UInt32, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, float color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Float, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, double color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.Double, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, RgbPixel color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbPixel, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, RgbAlphaPixel color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.RgbAlphaPixel, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, HsiPixel color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.HsiPixel, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(Rectangle rect, LabPixel color, string str)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            this.ThrowIfDisposed();

            using (var native = rect.ToNative())
            using (var pStr = new StdString(str))
                NativeMethods.image_window_add_overlay6(this.NativePtr, native.NativePtr, NativeMethods.Array2DType.LabPixel, ref color, pStr.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region AddOverlay(OverlayLine line)

        public void AddOverlay(OverlayLine line)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (line == null)
                throw new ArgumentNullException(nameof(line));

            line.ThrowIfDisposed();

            NativeMethods.image_window_add_overlay4(this.NativePtr, line.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region AddOverlay(IEnumerable<OverlayLine> lines)

        public void AddOverlay(IEnumerable<OverlayLine> lines)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            using (var vector = new StdVector<OverlayLine>(lines))
                NativeMethods.image_window_add_overlay5(this.NativePtr, vector.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        public void ClearOverlay()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.image_window_clear_overlay(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public bool GetNextDoubleClick(out Point p)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            var ret = NativeMethods.image_window_get_next_double_click(this.NativePtr, out var ptr);
            p = new Point(ptr);
            return ret;
#else
            throw new NotSupportedException();
#endif
        }

        public bool GetNextDoubleClick(out Point p, out uint mouseButton)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            var ret = NativeMethods.image_window_get_next_double_click2(this.NativePtr, out var ptr, out mouseButton);
            p = new Point(ptr);
            return ret;
#else
            throw new NotSupportedException();
#endif
        }

        public bool IsClosed()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            return NativeMethods.image_window_is_closed(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetImage(Array2DBase image)
        {
#if !DLIB_NO_GUI_SUPPORT
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
#else
            throw new NotSupportedException();
#endif
        }

        public void SetImage(MatrixBase matrix)
        {
#if !DLIB_NO_GUI_SUPPORT
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
#else
            throw new NotSupportedException();
#endif
        }

        public void SetImage(MatrixOp matrix)
        {
#if !DLIB_NO_GUI_SUPPORT
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
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{matrix.Array2DType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{matrix.MatrixElementType} is not supported.");
                case NativeMethods.ErrorType.MatrixOpTypeNotSupport:
                    throw new ArgumentException($"{matrix.ElementType} is not supported.");
            }
#else
            throw new NotSupportedException();
#endif
        }

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

            NativeMethods.image_window_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}

#endif
