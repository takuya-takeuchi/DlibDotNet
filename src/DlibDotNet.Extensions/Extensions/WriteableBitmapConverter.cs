using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Extensions
{

    public static class WriteableBitmapExtensions
    {

        #region Fields

        private static readonly Dictionary<PixelFormat, int> OptimumChannels;

        private static readonly Dictionary<PixelFormat, ConvertInfo> OptimumConvertInfos;

        #endregion

        #region Constructors

        static WriteableBitmapExtensions()
        {
            OptimumChannels = new Dictionary<PixelFormat, int>();
            OptimumChannels[PixelFormats.Gray2] =
            OptimumChannels[PixelFormats.Gray4] =
            OptimumChannels[PixelFormats.Gray8] =
            OptimumChannels[PixelFormats.Gray16] =
            OptimumChannels[PixelFormats.Gray32Float] =
            OptimumChannels[PixelFormats.Indexed1] =
            OptimumChannels[PixelFormats.Indexed2] =
            OptimumChannels[PixelFormats.Indexed4] =
            OptimumChannels[PixelFormats.Indexed8] =
            OptimumChannels[PixelFormats.BlackWhite] = 1;

            OptimumChannels[PixelFormats.Bgr24] =
            OptimumChannels[PixelFormats.Bgr555] =
            OptimumChannels[PixelFormats.Bgr565] =
            OptimumChannels[PixelFormats.Rgb24] =
            OptimumChannels[PixelFormats.Rgb48] =
            OptimumChannels[PixelFormats.Rgb128Float] = 3;

            OptimumChannels[PixelFormats.Bgr32] =
            OptimumChannels[PixelFormats.Bgra32] =
            OptimumChannels[PixelFormats.Cmyk32] =
            OptimumChannels[PixelFormats.Pbgra32] =
            OptimumChannels[PixelFormats.Prgba64] =
            OptimumChannels[PixelFormats.Prgba128Float] =
            OptimumChannels[PixelFormats.Rgba64] =
            OptimumChannels[PixelFormats.Rgba128Float] = 4;

            OptimumConvertInfos = new Dictionary<PixelFormat, ConvertInfo>();
            //OptimumConvertInfos[PixelFormats.Gray8] = new ConvertInfo { Type = ImageTypes.UInt8 };

            //OptimumConvertInfos[PixelFormats.Gray32Float] = new ConvertInfo { Type = ImageTypes.Float };

            //OptimumConvertInfos[PixelFormats.Gray16] = new ConvertInfo { Type = ImageTypes.UInt16 };

            OptimumConvertInfos[PixelFormats.Indexed8] = new ConvertInfo { Type = ImageTypes.RgbPixel };
            OptimumConvertInfos[PixelFormats.Bgr24] = new ConvertInfo { Type = ImageTypes.RgbPixel, RgbReverse = true };
            OptimumConvertInfos[PixelFormats.Rgb24] = new ConvertInfo { Type = ImageTypes.RgbPixel };
            OptimumConvertInfos[PixelFormats.Bgr32] = new ConvertInfo { Type = ImageTypes.RgbPixel, RgbReverse = true };

            OptimumConvertInfos[PixelFormats.Bgra32] = new ConvertInfo { Type = ImageTypes.RgbAlphaPixel, RgbReverse = true };

        }

        #endregion

        #region Methods

        public static WriteableBitmap ToWriteableBitmap<T>(this Array2D<T> array)
            where T : struct
        {
            return ToWriteableBitmap(array, 96, 96);
        }

        public static WriteableBitmap ToWriteableBitmap<T>(
            this Array2D<T> array,
            int dpiX,
            int dpiY)
            where T : struct
        {
            var width = array.Columns;
            var height = array.Rows;
            var rgbReverse = false;
            var format = new PixelFormat();
            var channels = 0;

            switch (array.ImageType)
            {
                case ImageTypes.RgbPixel:
                    // Dlib RgbPixel data
                    // R,G,B,R,G,B,...
                    // But .NET Bitmap data
                    // B,G,R,B,G,R,...
                    rgbReverse = true;
                    format = PixelFormats.Bgr24;
                    channels = 3;
                    break;
                case ImageTypes.RgbAlphaPixel:
                    // Dlib RgbAlphaPixel data
                    // R,G,B,A,R,G,B,A,,...
                    // But .NET Bitmap data
                    // B,G,R,A,B,G,R,A,...
                    rgbReverse = true;
                    format = PixelFormats.Bgra32;
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

            var bitmap = new WriteableBitmap(width, height, dpiX, dpiY, format, null);
            ToManaged(array.ImageType, array.NativePtr, bitmap, rgbReverse, channels);
            return bitmap;
        }

        public static Array2D<T> ToArray2D<T>(this WriteableBitmap bitmap)
            where T : struct
        {
            var format = bitmap.Format;
            if (!OptimumConvertInfos.TryGetValue(format, out var info))
                throw new NotSupportedException($"{format} is not support");
            if (!OptimumChannels.TryGetValue(format, out var channels))
                throw new NotSupportedException($"{format} is not support");

            var width = bitmap.PixelWidth;
            var height = bitmap.PixelHeight;
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

        #region Helpers

        private static void ToManaged(ImageTypes type, IntPtr src, WriteableBitmap bitmap, bool rgbReverse, int channels)
        {
            var width = bitmap.PixelWidth;
            var height = bitmap.PixelHeight;
            var buffer = bitmap.BackBuffer;
            var stride = bitmap.BackBufferStride;
            var srctype = type.ToNativeArray2DType();
            Dlib.Native.extensions_convert_array_to_managed_image(srctype, src, buffer, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
        }

        private static void ToNative(WriteableBitmap bitmap, ImageTypes dstType, IntPtr dst, bool rgbReverse, int channels)
        {
            var width = bitmap.PixelWidth;
            var height = bitmap.PixelHeight;
            var buffer = bitmap.BackBuffer;
            var stride = bitmap.BackBufferStride;
            var usePallete = bitmap.Palette != null && bitmap.Palette.Colors.Count == 256;

            if (!usePallete)
            {
                Dlib.Native.extensions_convert_managed_image_to_array(buffer, dstType.ToNativeArray2DType(), dst, rgbReverse, (uint)height, (uint)width, (uint)stride, (uint)channels);
            }
            else
            {
                var p = bitmap.Palette.Colors.Select(c => new RgbPixel { Blue = c.B, Green = c.G, Red = c.R }).ToArray();
                Dlib.Native.extensions_convert_managed_image_to_array_by_pallete(buffer, dstType.ToNativeArray2DType(), dst, p, (uint)height, (uint)width, (uint)stride, (uint)channels);
            }
        }

        #endregion

        #endregion

        private sealed class ConvertInfo
        {

            public ImageTypes Type
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

}