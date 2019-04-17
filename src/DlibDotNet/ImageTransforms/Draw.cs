using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        #region DrawLine(Array2D<T> image, Point p1, Point p2, pixelType color)

        public static void DrawLine(Array2D<RgbPixel> image, Point p1, Point p2)
        {
            DrawLine(image, p1, p2, new RgbPixel());
        }

        public static void DrawLine(Array2D<byte> image, Point p1, Point p2, byte color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.UInt8,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<ushort> image, Point p1, Point p2, ushort color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.UInt16,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<uint> image, Point p1, Point p2, uint color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.UInt32,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }
        
        public static void DrawLine(Array2D<sbyte> image, Point p1, Point p2, sbyte color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.Int8,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<short> image, Point p1, Point p2, short color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.Int16,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<int> image, Point p1, Point p2, int color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.Int32,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<float> image, Point p1, Point p2, float color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.Float,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<double> image, Point p1, Point p2, double color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.Double,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<RgbPixel> image, Point p1, Point p2, RgbPixel color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.RgbPixel,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<RgbAlphaPixel> image, Point p1, Point p2, RgbAlphaPixel color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.RgbAlphaPixel,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawLine(Array2D<HsiPixel> image, Point p1, Point p2, HsiPixel color)
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
                var ret = NativeMethods.draw_line(NativeMethods.Array2DType.HsiPixel,
                                                  image.NativePtr,
                                                  np1.NativePtr,
                                                  np2.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        #endregion

        #region DrawRectangle(Array2D<T> image, Rectangle rect, pixelType color, uint thickness)

        public static void DrawRectangle(Array2D<byte> image, Rectangle rect, byte color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.UInt8,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<ushort> image, Rectangle rect, ushort color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.UInt16,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<uint> image, Rectangle rect, uint color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.UInt32,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<sbyte> image, Rectangle rect, sbyte color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.Int8,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<short> image, Rectangle rect, short color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.Int16,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<int> image, Rectangle rect, int color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.UInt32,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<float> image, Rectangle rect, float color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.Float,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<double> image, Rectangle rect, double color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.Double,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<RgbPixel> image, Rectangle rect, RgbPixel color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.RgbPixel,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<RgbAlphaPixel> image, Rectangle rect, RgbAlphaPixel color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.RgbAlphaPixel,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void DrawRectangle(Array2D<HsiPixel> image, Rectangle rect, HsiPixel color, uint thickness = 1)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.draw_rectangle(NativeMethods.Array2DType.HsiPixel,
                                                       image.NativePtr,
                                                       native.NativePtr,
                                                       ref color,
                                                       thickness);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        #endregion

        #region FillRect(Array2D<T> image, Rectangle rect, pixelType color)

        public static void FillRect(Array2D<byte> image, Rectangle rect, byte color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.UInt8,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<ushort> image, Rectangle rect, ushort color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.UInt16,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<uint> image, Rectangle rect, uint color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.UInt32,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<sbyte> image, Rectangle rect, sbyte color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.Int8,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<short> image, Rectangle rect, short color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.Int16,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<int> image, Rectangle rect, int color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.UInt32,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<float> image, Rectangle rect, float color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.Float,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<double> image, Rectangle rect, double color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.Double,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<RgbPixel> image, Rectangle rect, RgbPixel color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.RgbPixel,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<RgbAlphaPixel> image, Rectangle rect, RgbAlphaPixel color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.RgbAlphaPixel,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"{color} is not supported.");
                }
            }
        }

        public static void FillRect(Array2D<HsiPixel> image, Rectangle rect, HsiPixel color)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.fill_rect(NativeMethods.Array2DType.HsiPixel,
                                                  image.NativePtr,
                                                  native.NativePtr,
                                                  ref color);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
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
            var ret = NativeMethods.tile_images(imageType.ToNativeArray2DType(), array.NativePtr, out var ret_image);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
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
                var ret = NativeMethods.tile_images_matrix(elementType.ToNativeMatrixElementType(), vector.NativePtr, out var retImage);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{elementType} is not supported.");
                }

                return new Matrix<T>(retImage);
            }
        }

        #endregion

    }

}