using System;
using System.Collections.Generic;
using System.Linq;

using SkiaSharp;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Extensions
{

    public static class SkiaExtensions
    {

        #region Fields

        private static readonly Dictionary<SKColorType, int> OptimumChannels;

        private static readonly Dictionary<SKColorType, ConvertInfo<ImageTypes>[]> OptimumConvertImageInfos;

        private static readonly Dictionary<SKColorType, ConvertInfo<MatrixElementTypes>> OptimumConvertMatrixInfos;
        
        #endregion

        #region Constructors

        static SkiaExtensions()
        {
            OptimumChannels = new Dictionary<SKColorType, int>();
            OptimumChannels[SKColorType.Rgba8888] = 4;

            OptimumConvertImageInfos = new Dictionary<SKColorType, ConvertInfo<ImageTypes>[]>();
            OptimumConvertImageInfos[SKColorType.Gray8] = new[]
            {
                new ConvertInfo<ImageTypes> { Type = ImageTypes.UInt8 }
            };
            OptimumConvertImageInfos[SKColorType.Rgba8888] = new[]
            {
                new ConvertInfo<ImageTypes>{ Type = ImageTypes.RgbPixel, RgbReverse = true },
                new ConvertInfo<ImageTypes>{ Type = ImageTypes.BgrPixel, RgbReverse = false },
                new ConvertInfo<ImageTypes>{ Type = ImageTypes.RgbAlphaPixel, RgbReverse = true }
            };
            OptimumConvertMatrixInfos = new Dictionary<SKColorType, ConvertInfo<MatrixElementTypes>>();
            OptimumConvertMatrixInfos[SKColorType.Rgba8888] = new ConvertInfo<MatrixElementTypes> { Type = MatrixElementTypes.RgbPixel };
        }

        #endregion

        #region Methods

        #region Array2D

        public static SKBitmap ToBitmap<T>(this Array2D<T> array)
            where T : struct
        {
            array.ThrowIfDisposed();

            var width = array.Columns;
            var height = array.Rows;
            var format = SKColorType.Unknown;
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
                    format = SKColorType.Rgba8888;
                    channels = 3;
                    break;
                case ImageTypes.BgrPixel:
                    // Dlib RgbPixel data
                    // R,G,B,R,G,B,...
                    // But .NET Bitmap data
                    // B,G,R,B,G,R,...
                    format = SKColorType.Rgba8888;
                    channels = 3;
                    break;
                case ImageTypes.RgbAlphaPixel:
                    format = SKColorType.Rgba8888;
                    channels = 4;
                    break;
                case ImageTypes.UInt8:
                case ImageTypes.Int32:
                case ImageTypes.UInt16:
                case ImageTypes.Int16:
                case ImageTypes.HsiPixel:
                case ImageTypes.LabPixel:
                case ImageTypes.Float:
                case ImageTypes.Double:
                case ImageTypes.Matrix:
                    throw new NotSupportedException();
            }

            var bitmap = new SKBitmap(width, height, format, SKAlphaType.Unpremul);
            ToManaged(array.ImageType, array.NativePtr, bitmap, rgbReverse, channels);
            return bitmap;
        }

        public static Array2D<T> ToArray2D<T>(this SKBitmap bitmap)
            where T : struct
        {
            var format = bitmap.Info.ColorType;
            if (!OptimumConvertImageInfos.TryGetValue(format, out var infos))
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
                var info = infos.FirstOrDefault(i => i.Type == array.ImageType);
                if (info != null)
                {
                    ToNative(bitmap, info.Type, array.NativePtr, info.RgbReverse, channels);
                    requireDispose = false;
                }
                else
                {
                    throw new NotSupportedException($"Not support converting from {format} to {array.ImageType}");
                }
            }
            finally
            {
                if (requireDispose)
                {
                    array?.Dispose();
                    array = null;
                }
            }

            return array;
        }

        #endregion

        #region Bitmap

        public static SKBitmap To8bppIndexedGrayscale(this SKBitmap bitmap, GrayscalLumaCoefficients coefficients)
        {
            float red;
            float green;
            float blue;

            switch (coefficients)
            {
                case GrayscalLumaCoefficients.ITU_R_BT_601:
                    red = 0.299F;
                    green = 0.587F;
                    blue = 0.1144F;
                    break;
                case GrayscalLumaCoefficients.ITU_R_BT_709:
                    red = 0.2126F;
                    green = 0.7152F;
                    blue = 0.0722F;
                    break;
                case GrayscalLumaCoefficients.SMPTE_240M:
                    red = 0.212F;
                    green = 0.701F;
                    blue = 0.087F;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(coefficients), coefficients, null);
            }

            return To8bppIndexedGrayscale(bitmap, red, green, blue);
        }

        public static SKBitmap To8bppIndexedGrayscale(this SKBitmap bitmap, float rCoefficient, float gCoefficient, float bCoefficient)
        {
            var format = bitmap.Info.ColorType;
            if (format != SKColorType.Rgba8888)
                throw new NotSupportedException($"{format} is not support");

            if (!OptimumChannels.TryGetValue(format, out var channels))
                throw new NotSupportedException($"{format} is not support");

            var width = bitmap.Width;
            var height = bitmap.Height;
            var info = bitmap.Info;

            SKBitmap dst = null;

            try
            {
                dst = new SKBitmap(width, height, SKColorType.Gray8, SKAlphaType.Unpremul);

                var scan0 = bitmap.GetPixels();
                var stride = info.BytesSize / info.Height;
                var srcData = bitmap.GetPixels();
                var dstData = dst.GetPixels();

                var srcStride = stride;
                var dstStride = stride;

                switch (channels)
                {
                    case 1:
                        NativeMethods.cstd_memcpy(dstData, srcData, srcStride * height);
                        break;
                    case 3:
                    case 4:
                        unsafe
                        {
                            for (var y = 0; y < height; y++)
                            {
                                var pSrc = ((byte*)srcData) + y * srcStride;
                                var pDst = ((byte*)dstData) + y * dstStride;
                                for (var x = 0; x < width; x++, pSrc += channels, pDst++)
                                    *pDst = (byte)(pSrc[0] * bCoefficient + pSrc[1] * gCoefficient + pSrc[2] * rCoefficient);
                            }
                        }

                        break;
                }
            }
            catch
            {
                dst?.Dispose();
                throw;
            }

            return dst;
        }

        #endregion

        #region Matrix

        public static SKBitmap ToBitmap<T>(this Matrix<T> matrix)
            where T : struct
        {
            matrix.ThrowIfDisposed();

            var width = matrix.Columns;
            var height = matrix.Rows;
            var format = SKColorType.Unknown;
            int channels;
            var rgbReverse = false;

            switch (matrix.MatrixElementType)
            {
                case MatrixElementTypes.RgbPixel:
                    rgbReverse = true;
                    format = SKColorType.Rgba8888;
                    channels = 3;
                    break;
                case MatrixElementTypes.BgrPixel:
                    format = SKColorType.Rgba8888;
                    channels = 3;
                    break;
                case MatrixElementTypes.RgbAlphaPixel:
                    rgbReverse = true;
                    format = SKColorType.Rgba8888;
                    channels = 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var bitmap = new SKBitmap(width, height, format, SKAlphaType.Unpremul);
            ToManaged(matrix.MatrixElementType, matrix.NativePtr, bitmap, rgbReverse, channels);
            return bitmap;
        }

        public static Matrix<T> ToMatrix<T>(this SKBitmap bitmap)
            where T : struct
        {
            var format = bitmap.Info.ColorType;
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
                else
                {
                    throw new NotSupportedException($"Not support converting from {format} to {matrix.MatrixElementType}");
                }
            }
            finally
            {
                if (requireDispose)
                {
                    matrix?.Dispose();
                    matrix = null;
                }
            }

            return matrix;
        }

        #endregion

        #region Helpers

        private static void ToManaged(ImageTypes type, IntPtr src, SKBitmap bitmap, bool rgbReverse, int channels)
        {
            var info = bitmap.Info;
            var width = bitmap.Width;
            var height = bitmap.Height;

            var scan0 = bitmap.GetPixels();
            var stride = info.BytesSize / info.Height;
            var srcType = type.ToNativeArray2DType();
            NativeMethods.extensions_convert_array_to_managed_image(srcType, src, scan0, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
        }

        private static void ToManaged(MatrixElementTypes type, IntPtr src, SKBitmap bitmap, bool rgbReverse, int channels)
        {
            var info = bitmap.Info;
            var width = bitmap.Width;
            var height = bitmap.Height;

            var scan0 = bitmap.GetPixels();
            var stride = info.BytesSize / info.Height;
            var srcType = type.ToNativeMatrixElementType();
            NativeMethods.extensions_convert_matrix_to_managed_image(srcType, src, scan0, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
        }

        private static void ToNative(SKBitmap bitmap, ImageTypes dstType, IntPtr dst, bool rgbReverse, int channels)
        {
            var info = bitmap.Info;
            var width = bitmap.Width;
            var height = bitmap.Height;

            var scan0 = bitmap.GetPixels();
            var stride = info.BytesSize / info.Height;
            NativeMethods.extensions_convert_managed_image_to_array(scan0, dstType.ToNativeArray2DType(), dst, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
        }

        private static void ToNative(SKBitmap bitmap, MatrixElementTypes dstType, IntPtr dst, bool rgbReverse, int channels)
        {
            var info = bitmap.Info;
            var width = bitmap.Width;
            var height = bitmap.Height;

            var scan0 = bitmap.GetPixels();
            var stride = info.BytesSize / info.Height;
            NativeMethods.extensions_convert_managed_image_to_matrix(scan0, dstType.ToNativeMatrixElementType(), dst, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
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