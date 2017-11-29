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

        internal static Dlib.Native.MatrixElementType ToNativeMatrixElementType(this MatrixElementTypes matrixElementTypes)
        {
            switch (matrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    return Dlib.Native.MatrixElementType.UInt8;
                case MatrixElementTypes.UInt16:
                    return Dlib.Native.MatrixElementType.UInt16;
                case MatrixElementTypes.UInt32:
                    return Dlib.Native.MatrixElementType.UInt32;
                case MatrixElementTypes.Int8:
                    return Dlib.Native.MatrixElementType.Int8;
                case MatrixElementTypes.Int16:
                    return Dlib.Native.MatrixElementType.Int16;
                case MatrixElementTypes.Int32:
                    return Dlib.Native.MatrixElementType.Int32;
                case MatrixElementTypes.Float:
                    return Dlib.Native.MatrixElementType.Float;
                case MatrixElementTypes.Double:
                    return Dlib.Native.MatrixElementType.Double;
                case MatrixElementTypes.RgbPixel:
                    return Dlib.Native.MatrixElementType.RgbPixel;
                case MatrixElementTypes.RgbAlphaPixel:
                    return Dlib.Native.MatrixElementType.RgbAlphaPixel;
                case MatrixElementTypes.HsiPixel:
                    return Dlib.Native.MatrixElementType.HsiPixel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matrixElementTypes), matrixElementTypes, null);
            }
        }

    }
}
