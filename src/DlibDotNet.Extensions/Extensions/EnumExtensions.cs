using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Extensions
{
    internal static class EnumExtensions
    {

        internal static Dlib.Native.Array2DType ToNativeArray2DType(this ImageTypes imageType)
        {
            switch (imageType)
            {
                case ImageTypes.RgbPixel:
                    return Dlib.Native.Array2DType.RgbPixel;
                case ImageTypes.RgbAlphaPixel:
                    return Dlib.Native.Array2DType.RgbAlphaPixel;
                case ImageTypes.UInt8:
                    return Dlib.Native.Array2DType.UInt8;
                case ImageTypes.UInt16:
                    return Dlib.Native.Array2DType.UInt16;
                case ImageTypes.Int16:
                    return Dlib.Native.Array2DType.Int16;
                case ImageTypes.Int32:
                    return Dlib.Native.Array2DType.Int32;
                case ImageTypes.HsiPixel:
                    return Dlib.Native.Array2DType.HsiPixel;
                case ImageTypes.Float:
                    return Dlib.Native.Array2DType.Float;
                case ImageTypes.Double:
                    return Dlib.Native.Array2DType.Double;
                case ImageTypes.Matrix:
                    return Dlib.Native.Array2DType.Matrix;
                default:
                    throw new ArgumentOutOfRangeException(nameof(imageType), imageType, null);
            }
        }

    }
}
