using System;
using System.Collections.Generic;
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

            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            {
                var ret = Native.draw_line(
                    Native.Array2DType.UInt8,
                    image.NativePtr,
                    np1.NativePtr,
                    np2.NativePtr,
                    ref color);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            {
                var ret = Native.draw_line(
                    Native.Array2DType.UInt16,
                    image.NativePtr,
                    np1.NativePtr,
                    np2.NativePtr,
                    ref color);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            {
                var ret = Native.draw_line(
                    Native.Array2DType.Float,
                    image.NativePtr,
                    np1.NativePtr,
                    np2.NativePtr,
                    ref color);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            {
                var ret = Native.draw_line(
                    Native.Array2DType.Double,
                    image.NativePtr,
                    np1.NativePtr,
                    np2.NativePtr,
                    ref color);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            {
                var ret = Native.draw_line(
                    Native.Array2DType.RgbPixel,
                    image.NativePtr,
                    np1.NativePtr,
                    np2.NativePtr,
                    ref color);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            {
                var ret = Native.draw_line(
                    Native.Array2DType.RgbAlphaPixel,
                    image.NativePtr,
                    np1.NativePtr,
                    np2.NativePtr,
                    ref color);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
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

            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            {
                var ret = Native.draw_line(
                    Native.Array2DType.HsiPixel,
                    image.NativePtr,
                    np1.NativePtr,
                    np2.NativePtr,
                    ref color);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        #endregion

        #region DrawRectangle(Array2DBase image, Rectangle rect, pixelType color, uint thickness)

        public static void DrawRectangle(Array2DBase image, Rectangle rect, byte color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.UInt8,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2DBase image, Rectangle rect, ushort color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.UInt16,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2DBase image, Rectangle rect, int color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.Int32,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2DBase image, Rectangle rect, float color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.Float,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2DBase image, Rectangle rect, double color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.Double,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2DBase image, Rectangle rect, RgbPixel color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.RgbPixel,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2DBase image, Rectangle rect, RgbAlphaPixel color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.RgbAlphaPixel,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2DBase image, Rectangle rect, HsiPixel color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = Native.draw_rectangle(
                    Native.Array2DType.HsiPixel,
                    image.NativePtr,
                    native.NativePtr,
                    ref color,
                    thickness);
                switch (ret)
                {
                    case Native.ErrorType.ArrayTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        #endregion

        public static Matrix<T> TileImages<T>(Array<Array2D<T>> array)
            where T : struct
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

            return new Matrix<T>(ret_image);
        }

        public static Matrix<T> TileImages<T>(IEnumerable<Matrix<T>> images)
            where T : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));

            images.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var elementType);
            using (var vector = new StdVector<Matrix<T>>(images))
            {
                var ret = Native.tile_images_matrix(elementType.ToNativeMatrixElementType(), vector.NativePtr, out var retImage);
                switch (ret)
                {
                    case Native.ErrorType.ElementTypeNotSupport:
                        throw new ArgumentException($"{elementType} is not supported.");
                }

                return new Matrix<T>(retImage);
            }
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

            #region draw_rectangle

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref byte color, uint thickness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref ushort color, uint thickness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref int color, uint thickness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref float color, uint thickness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref double color, uint thickness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref RgbPixel color, uint thickness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref RgbAlphaPixel color, uint thickness);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType draw_rectangle(Array2DType pixelType, IntPtr image, IntPtr rect, ref HsiPixel color, uint thickness);

            #endregion

            #region tile_images

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType tile_images(Array2DType inType, IntPtr array, out IntPtr ret_image);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType tile_images_matrix(MatrixElementType in_type, IntPtr images, out IntPtr ret_image);

            #endregion

        }

    }

}