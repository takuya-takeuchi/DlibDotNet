using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Extensions
{

    public static class BitmapExtensions
    {

        #region Fields

        private static readonly Dictionary<PixelFormat, int> OptimumChannels;

        private static readonly Dictionary<PixelFormat, ConvertInfo<ImageTypes>> OptimumConvertImageInfos;

        private static readonly Dictionary<PixelFormat, ConvertInfo<MatrixElementTypes>> OptimumConvertMatrixInfos;

        #endregion

        #region Constructors

        static BitmapExtensions()
        {
            OptimumChannels = new Dictionary<PixelFormat, int>();
            OptimumChannels[PixelFormat.Format24bppRgb] = 3;
            OptimumChannels[PixelFormat.Format8bppIndexed] = 1;
            OptimumChannels[PixelFormat.Format32bppRgb] =
            OptimumChannels[PixelFormat.Format32bppArgb] = 4;

            OptimumConvertImageInfos = new Dictionary<PixelFormat, ConvertInfo<ImageTypes>>();
            OptimumConvertImageInfos[PixelFormat.Format8bppIndexed] = new ConvertInfo<ImageTypes> { Type = ImageTypes.RgbPixel };
            OptimumConvertImageInfos[PixelFormat.Format24bppRgb] = new ConvertInfo<ImageTypes> { Type = ImageTypes.RgbPixel, RgbReverse = true };
            OptimumConvertImageInfos[PixelFormat.Format32bppArgb] = new ConvertInfo<ImageTypes> { Type = ImageTypes.RgbAlphaPixel, RgbReverse = true };

            OptimumConvertMatrixInfos = new Dictionary<PixelFormat, ConvertInfo<MatrixElementTypes>>();
            OptimumConvertMatrixInfos[PixelFormat.Format8bppIndexed] = new ConvertInfo<MatrixElementTypes> { Type = MatrixElementTypes.RgbPixel };
            OptimumConvertMatrixInfos[PixelFormat.Format24bppRgb] = new ConvertInfo<MatrixElementTypes> { Type = MatrixElementTypes.RgbPixel, RgbReverse = true };
            OptimumConvertMatrixInfos[PixelFormat.Format32bppArgb] = new ConvertInfo<MatrixElementTypes> { Type = MatrixElementTypes.RgbAlphaPixel, RgbReverse = true };
        }

        #endregion

        #region Methods

        #region Array2D

        public static Bitmap ToBitmap<T>(this Array2D<T> array)
            where T : struct
        {
            array.ThrowIfDisposed();

            var width = array.Columns;
            var height = array.Rows;
            var format = PixelFormat.Undefined;
            var channels = 0;
            var rgbReverse = false;

            switch (array.ImageType)
            {
                case ImageTypes.RgbPixel:
                    // Dlib RgbPixel data
                    // R,G,B,R,G,B,...
                    // But .NET Bitmap data
                    // B,G,R,B,G,R,...
                    rgbReverse = true;
                    format = PixelFormat.Format24bppRgb;
                    channels = 3;
                    break;
                case ImageTypes.RgbAlphaPixel:
                    format = PixelFormat.Format32bppArgb;
                    channels = 4;
                    break;
                case ImageTypes.UInt8:
                case ImageTypes.Int32:
                case ImageTypes.UInt16:
                case ImageTypes.Int16:
                case ImageTypes.HsiPixel:
                case ImageTypes.Float:
                case ImageTypes.Double:
                case ImageTypes.Matrix:
                    throw new NotSupportedException();
            }

            var bitmap = new Bitmap(width, height, format);
            ToManaged(array.ImageType, array.NativePtr, bitmap, rgbReverse, channels);
            return bitmap;
        }

        public static Array2D<T> ToArray2D<T>(this Bitmap bitmap)
            where T : struct
        {
            var format = bitmap.PixelFormat;
            if (!OptimumConvertImageInfos.TryGetValue(format, out var info))
                throw new NotSupportedException($"{format} is not support");
            if (!OptimumChannels.TryGetValue(format, out var channels))
                throw new NotSupportedException($"{format} is not support");

            var width = bitmap.Width;
            var height = bitmap.Height;
            var requireDispose = true;

            Array2D<T> array = null;

            try
            {
                array = new Array2D<T>(height, width);
                if (array.ImageType == info.Type)
                {
                    ToNative(bitmap, info.Type, array.NativePtr, info.RgbReverse, channels);
                    requireDispose = false;
                }
            }
            finally
            {
                if (requireDispose)
                {
                    array?.Dispose();
                    throw new NotSupportedException();
                }
            }

            return array;
        }

        #endregion

        #region Matrix

        public static Bitmap ToBitmap<T>(this Matrix<T> matrix)
            where T : struct
        {
            matrix.ThrowIfDisposed();

            var width = matrix.Columns;
            var height = matrix.Rows;
            PixelFormat format;
            int channels;
            var rgbReverse = false;

            switch (matrix.MatrixElementType)
            {
                case MatrixElementTypes.RgbPixel:
                    rgbReverse = true;
                    format = PixelFormat.Format24bppRgb;
                    channels = 3;
                    break;
                case MatrixElementTypes.RgbAlphaPixel:
                    rgbReverse = true;
                    format = PixelFormat.Format32bppArgb;
                    channels = 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var bitmap = new Bitmap(width, height, format);
            ToManaged(matrix.MatrixElementType, matrix.NativePtr, bitmap, rgbReverse, channels);
            return bitmap;
        }

        public static Matrix<T> ToMatrix<T>(this Bitmap bitmap)
            where T : struct
        {
            var format = bitmap.PixelFormat;
            if (!OptimumConvertMatrixInfos.TryGetValue(format, out var info))
                throw new NotSupportedException($"{format} is not support");
            if (!OptimumChannels.TryGetValue(format, out var channels))
                throw new NotSupportedException($"{format} is not support");

            var width = bitmap.Width;
            var height = bitmap.Height;
            var requireDispose = true;

            Matrix<T> matrix = null;

            try
            {
                matrix = new Matrix<T>(height, width);
                if (matrix.MatrixElementType == info.Type)
                {
                    ToNative(bitmap, info.Type, matrix.NativePtr, info.RgbReverse, channels);
                    requireDispose = false;
                }
            }
            finally
            {
                if (requireDispose)
                {
                    matrix?.Dispose();
                    throw new NotSupportedException();
                }
            }

            return matrix;
        }

        #endregion

        #region Helpers

        private static void ToManaged(ImageTypes type, IntPtr src, Bitmap bitmap, bool rgbReverse, int channels)
        {
            var format = bitmap.PixelFormat;
            var width = bitmap.Width;
            var height = bitmap.Height;

            BitmapData bitmapData = null;
            try
            {
                bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly,
                    format);

                var scan0 = bitmapData.Scan0;
                var stride = bitmapData.Stride;
                var srctype = type.ToNativeArray2DType();
                Dlib.Native.extensions_convert_array_to_managed_image(srctype, src, scan0, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
            }
            finally
            {
                if (bitmapData != null)
                    bitmap.UnlockBits(bitmapData);
            }
        }

        private static void ToManaged(MatrixElementTypes type, IntPtr src, Bitmap bitmap, bool rgbReverse, int channels)
        {
            var format = bitmap.PixelFormat;
            var width = bitmap.Width;
            var height = bitmap.Height;

            BitmapData bitmapData = null;
            try
            {
                bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly,
                    format);

                var scan0 = bitmapData.Scan0;
                var stride = bitmapData.Stride;
                var srctype = type.ToNativeMatrixElementType();
                Dlib.Native.extensions_convert_matrix_to_managed_image(srctype, src, scan0, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
            }
            finally
            {
                if (bitmapData != null)
                    bitmap.UnlockBits(bitmapData);
            }
        }

        private static void ToNative(Bitmap bitmap, ImageTypes dstType, IntPtr dst, bool rgbReverse, int channels)
        {
            var format = bitmap.PixelFormat;
            var width = bitmap.Width;
            var height = bitmap.Height;
            var pallete = bitmap.Palette;
            var usePallete = bitmap.Palette.Entries.Length == 256;

            BitmapData bitmapData = null;
            try
            {
                bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly,
                    format);

                var scan0 = bitmapData.Scan0;
                var stride = bitmapData.Stride;
                if (!usePallete)
                {
                    Dlib.Native.extensions_convert_managed_image_to_array(scan0, dstType.ToNativeArray2DType(), dst, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
                }
                else
                {
                    var p = pallete.Entries.Select(c => new RgbPixel { Blue = c.B, Green = c.G, Red = c.R }).ToArray();
                    Dlib.Native.extensions_convert_managed_image_to_array_by_pallete(scan0, dstType.ToNativeArray2DType(), dst, p, (uint)height, (uint)width, (uint)stride, (uint)channels);
                }
            }
            finally
            {
                if (bitmapData != null)
                    bitmap.UnlockBits(bitmapData);
            }
        }

        private static void ToNative(Bitmap bitmap, MatrixElementTypes dstType, IntPtr dst, bool rgbReverse, int channels)
        {
            var format = bitmap.PixelFormat;
            var width = bitmap.Width;
            var height = bitmap.Height;
            var pallete = bitmap.Palette;
            var usePallete = bitmap.Palette.Entries.Length == 256;

            BitmapData bitmapData = null;
            try
            {
                bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly,
                    format);

                var scan0 = bitmapData.Scan0;
                var stride = bitmapData.Stride;
                if (!usePallete)
                {
                    Dlib.Native.extensions_convert_managed_image_to_matrix(scan0, dstType.ToNativeMatrixElementType(), dst, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
                }
                else
                {
                    var p = pallete.Entries.Select(c => new RgbPixel { Blue = c.B, Green = c.G, Red = c.R }).ToArray();
                    Dlib.Native.extensions_convert_managed_image_to_matrix_by_pallete(scan0, dstType.ToNativeMatrixElementType(), dst, p, (uint)height, (uint)width, (uint)stride, (uint)channels);
                }
            }
            finally
            {
                if (bitmapData != null)
                    bitmap.UnlockBits(bitmapData);
            }
        }

        #endregion

        #endregion

    }

    internal sealed class ConvertInfo<T>
    {

        public T Type
        {
            get;
            set;
        }

        public bool RgbReverse
        {
            get;
            set;
        }

    }

}
