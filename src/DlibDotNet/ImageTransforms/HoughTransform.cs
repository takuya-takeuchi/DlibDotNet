using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class HoughTransform : DlibObject
    {

        #region Constructors

        public HoughTransform(uint size)
        {
            this.NativePtr = Native.hough_transform_new(size);
        }

        #endregion

        #region Properties

        public int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                Native.hough_transform_nc(this.NativePtr, out var value);
                return value;
            }
        }

        public int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Native.hough_transform_nr(this.NativePtr, out var value);
                return value;
            }
        }

        public uint Size
        {
            get
            {
                this.ThrowIfDisposed();
                Native.hough_transform_size(this.NativePtr, out var value);
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
            point.ThrowIfDisposed();

            var inType = image.ImageType.ToNativeArray2DType();
            var ret = Native.hough_transform_get_best_hough_point(
                this.NativePtr,
                point.NativePtr,
                inType,
                image.NativePtr,
                out var resultPoint);
            switch (ret)
            {
                case Dlib.Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"Input {image.ImageType} is not supported.");
            }

            return new Point(resultPoint);
        }

        public StdPair<Point, Point> GetLine(Point point)
        {
            this.ThrowIfDisposed();

            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            var ret = Native.hough_transform_get_line(this.NativePtr, point.NativePtr);
            return new StdPair<Point, Point>(ret);
        }

        public void Operator(Array2DBase inImage, Rectangle rect, Array2DBase outImage)
        {
            this.ThrowIfDisposed();

            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));
            if (outImage == null)
                throw new ArgumentNullException(nameof(outImage));

            inImage.ThrowIfDisposed();
            rect.ThrowIfDisposed();
            outImage.ThrowIfDisposed();

            var inType = inImage.ImageType.ToNativeArray2DType();
            var outType = outImage.ImageType.ToNativeArray2DType();
            var ret = Native.hough_transform_operator(
                this.NativePtr,
                inType,
                inImage.NativePtr,
                outType,
                outImage.NativePtr,
                rect.NativePtr);
            switch (ret)
            {
                case Dlib.Native.ErrorType.OutputArrayTypeNotSupport:
                    throw new ArgumentException($"Output {outImage.ImageType} is not supported.");
                case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inImage.ImageType} is not supported.");
            }
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.hough_transform_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr hough_transform_new(uint size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool hough_transform_nc(IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool hough_transform_nr(IntPtr obj, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool hough_transform_size(IntPtr obj, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType hough_transform_get_best_hough_point(IntPtr obj, IntPtr p, Dlib.Native.Array2DType type, IntPtr img, out IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType hough_transform_operator(
                IntPtr obj,
                Dlib.Native.Array2DType in_type,
                IntPtr in_img,
                Dlib.Native.Array2DType out_type,
                IntPtr out_img,
                IntPtr rectangle);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr hough_transform_get_line(IntPtr obj, IntPtr p);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool hough_transform_get_rect(IntPtr obj, out IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void hough_transform_delete(IntPtr obj);

        }

    }

}