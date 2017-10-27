using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        #region DrawLine(Array2DBase image, Point p1, Point p2, pixelType color)

        public static void DrawLine(Array2DBase image, Point p1, Point p2)
        {
            DrawLine(image, p1, p2, new RgbPixel());
        }

        public static void DrawLine(Array2DBase image, Point p1, Point p2, byte color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            image.ThrowIfDisposed();
            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            var ret = Native.draw_line(
                Native.Array2DType.UInt8,
                image.NativePtr,
                p1.NativePtr,
                p2.NativePtr,
                ref color);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{color} is not supported.");
            }
        }

        public static void DrawLine(Array2DBase image, Point p1, Point p2, ushort color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            image.ThrowIfDisposed();
            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            var ret = Native.draw_line(
                Native.Array2DType.UInt16,
                image.NativePtr,
                p1.NativePtr,
                p2.NativePtr,
                ref color);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{color} is not supported.");
            }
        }

        public static void DrawLine(Array2DBase image, Point p1, Point p2, float color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            image.ThrowIfDisposed();
            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            var ret = Native.draw_line(
                Native.Array2DType.Float,
                image.NativePtr,
                p1.NativePtr,
                p2.NativePtr,
                ref color);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{color} is not supported.");
            }
        }

        public static void DrawLine(Array2DBase image, Point p1, Point p2, double color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            image.ThrowIfDisposed();
            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            var ret = Native.draw_line(
                Native.Array2DType.Double,
                image.NativePtr,
                p1.NativePtr,
                p2.NativePtr,
                ref color);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{color} is not supported.");
            }
        }

        public static void DrawLine(Array2DBase image, Point p1, Point p2, RgbPixel color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            image.ThrowIfDisposed();
            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            var ret = Native.draw_line(
                Native.Array2DType.RgbPixel,
                image.NativePtr,
                p1.NativePtr,
                p2.NativePtr,
                ref color);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{color} is not supported.");
            }
        }

        public static void DrawLine(Array2DBase image, Point p1, Point p2, RgbAlphaPixel color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            image.ThrowIfDisposed();
            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            var ret = Native.draw_line(
                Native.Array2DType.RgbAlphaPixel,
                image.NativePtr,
                p1.NativePtr,
                p2.NativePtr,
                ref color);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{color} is not supported.");
            }
        }

        public static void DrawLine(Array2DBase image, Point p1, Point p2, HsiPixel color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            image.ThrowIfDisposed();
            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();
            var ret = Native.draw_line(
                Native.Array2DType.HsiPixel,
                image.NativePtr,
                p1.NativePtr,
                p2.NativePtr,
                ref color);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{color} is not supported.");
            }
        }

        #endregion

        public static Matrix<T> TileImages<T>(Array<Array2D<T>> array)
            where T: struct 
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            array.ThrowIfDisposed();

            var imageType = array.ImageType;
            var ret = Native.tile_images(imageType.ToNativeArray2DType(), array.NativePtr, out var ret_image);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{imageType} is not supported.");
            }

            return new Matrix<T>(ret_image, MatrixElementTypes.RgbPixel);
        }

        #endregion

        internal sealed partial class Native
        {

            #region draw_line

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref byte color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref ushort color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref float color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref double color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref RgbPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref RgbAlphaPixel color);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_line(Array2DType pixelType, IntPtr image, IntPtr p1, IntPtr p2, ref HsiPixel color);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType tile_images(Array2DType inType, IntPtr array, out IntPtr ret_image);

        }

    }

}