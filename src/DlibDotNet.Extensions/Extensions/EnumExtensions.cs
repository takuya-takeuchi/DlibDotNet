using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Extensions
{
    internal static class EnumExtensions
    {

        internal static NativeMethods.Array2DType ToNativeArray2DType(this ImageTypes imageType)
        {
            switch (imageType)
            {
                case ImageTypes.RgbPixel:
                    return NativeMethods.Array2DType.RgbPixel;
                case ImageTypes.BgrPixel:
                    return NativeMethods.Array2DType.BgrPixel;
                case ImageTypes.RgbAlphaPixel:
                    return NativeMethods.Array2DType.RgbAlphaPixel;
                case ImageTypes.UInt8:
                    return NativeMethods.Array2DType.UInt8;
                case ImageTypes.UInt16:
                    return NativeMethods.Array2DType.UInt16;
                case ImageTypes.UInt32:
                    return NativeMethods.Array2DType.UInt32;
                case ImageTypes.Int8:
                    return NativeMethods.Array2DType.Int8;
                case ImageTypes.Int16:
                    return NativeMethods.Array2DType.Int16;
                case ImageTypes.Int32:
                    return NativeMethods.Array2DType.Int32;
                case ImageTypes.HsiPixel:
                    return NativeMethods.Array2DType.HsiPixel;
                case ImageTypes.Float:
                    return NativeMethods.Array2DType.Float;
                case ImageTypes.Double:
                    return NativeMethods.Array2DType.Double;
                case ImageTypes.Matrix:
                    return NativeMethods.Array2DType.Matrix;
                default:
                    throw new ArgumentOutOfRangeException(nameof(imageType), imageType, null);
            }
        }

        internal static NativeMethods.MatrixElementType ToNativeMatrixElementType(this MatrixElementTypes matrixElementTypes)
        {
            switch (matrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    return NativeMethods.MatrixElementType.UInt8;
                case MatrixElementTypes.UInt16:
                    return NativeMethods.MatrixElementType.UInt16;
                case MatrixElementTypes.UInt32:
                    return NativeMethods.MatrixElementType.UInt32;
                case MatrixElementTypes.Int8:
                    return NativeMethods.MatrixElementType.Int8;
                case MatrixElementTypes.Int16:
                    return NativeMethods.MatrixElementType.Int16;
                case MatrixElementTypes.Int32:
                    return NativeMethods.MatrixElementType.Int32;
                case MatrixElementTypes.Float:
                    return NativeMethods.MatrixElementType.Float;
                case MatrixElementTypes.Double:
                    return NativeMethods.MatrixElementType.Double;
                case MatrixElementTypes.RgbPixel:
                    return NativeMethods.MatrixElementType.RgbPixel;
                case MatrixElementTypes.BgrPixel:
                    return NativeMethods.MatrixElementType.BgrPixel;
                case MatrixElementTypes.RgbAlphaPixel:
                    return NativeMethods.MatrixElementType.RgbAlphaPixel;
                case MatrixElementTypes.HsiPixel:
                    return NativeMethods.MatrixElementType.HsiPixel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matrixElementTypes), matrixElementTypes, null);
            }
        }

    }
}
