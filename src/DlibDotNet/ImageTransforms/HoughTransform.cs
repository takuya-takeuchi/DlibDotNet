using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class HoughTransform : DlibObject
    {

        #region Constructors

        public HoughTransform(uint size)
        {
            this.NativePtr = NativeMethods.hough_transform_new(size);
        }

        #endregion

        #region Properties

        public int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.hough_transform_nc(this.NativePtr, out var value);
                return value;
            }
        }

        public int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.hough_transform_nr(this.NativePtr, out var value);
                return value;
            }
        }

        public uint Size
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.hough_transform_size(this.NativePtr, out var value);
                return value;
            }
        }

        #endregion

        #region Methods

        public Point GetBestHoughPoint(Array2DBase image, Point point)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            image.ThrowIfDisposed();

            var inType = image.ImageType.ToNativeArray2DType();
            using (var native = point.ToNative())
            {
                var ret = NativeMethods.hough_transform_get_best_hough_point(
                this.NativePtr,
                native.NativePtr,
                inType,
                image.NativePtr,
                out var resultPoint);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {image.ImageType} is not supported.");
                }

                return new Point(resultPoint);
            }
        }

        public Tuple<Point, Point> GetLine(Point point)
        {
            this.ThrowIfDisposed();

            if (point == null)
                throw new ArgumentNullException(nameof(point));

            using (var native = point.ToNative())
            {
                var ret = NativeMethods.hough_transform_get_line(this.NativePtr, native.NativePtr);
                using (var pairt = new StdPair<Point, Point>(ret))
                    return new Tuple<Point, Point>(pairt.First, pairt.Second);
            }
        }

        public void Operator(Array2DBase inImage, Rectangle rect, Array2DBase outImage)
        {
            this.ThrowIfDisposed();

            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (outImage == null)
                throw new ArgumentNullException(nameof(outImage));

            inImage.ThrowIfDisposed();
            outImage.ThrowIfDisposed();

            var inType = inImage.ImageType.ToNativeArray2DType();
            var outType = outImage.ImageType.ToNativeArray2DType();
            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.hough_transform_operator(
                    this.NativePtr,
                    inType,
                    inImage.NativePtr,
                    outType,
                    outImage.NativePtr,
                    native.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException("Output or input type is not supported.");
                }
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

            NativeMethods.hough_transform_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}