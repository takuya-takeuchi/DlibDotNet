﻿using System;

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
                case ImageTypes.UInt32:
                    return Dlib.Native.Array2DType.UInt32;
                case ImageTypes.Int8:
                    return Dlib.Native.Array2DType.Int8;
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
                case MatrixElementTypes.UInt64:
                    return Dlib.Native.MatrixElementType.UInt64;
                case MatrixElementTypes.Int8:
                    return Dlib.Native.MatrixElementType.Int8;
                case MatrixElementTypes.Int16:
                    return Dlib.Native.MatrixElementType.Int16;
                case MatrixElementTypes.Int32:
                    return Dlib.Native.MatrixElementType.Int32;
                case MatrixElementTypes.Int64:
                    return Dlib.Native.MatrixElementType.Int64;
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

        internal static Dlib.Native.VectorElementType ToNativeVectorElementType(this VectorElementTypes types)
        {
            switch (types)
            {
                case VectorElementTypes.UInt8:
                    return Dlib.Native.VectorElementType.UInt8;
                case VectorElementTypes.UInt16:
                    return Dlib.Native.VectorElementType.UInt16;
                case VectorElementTypes.UInt32:
                    return Dlib.Native.VectorElementType.UInt32;
                case VectorElementTypes.Int8:
                    return Dlib.Native.VectorElementType.Int8;
                case VectorElementTypes.Int16:
                    return Dlib.Native.VectorElementType.Int16;
                case VectorElementTypes.Int32:
                    return Dlib.Native.VectorElementType.Int32;
                case VectorElementTypes.Float:
                    return Dlib.Native.VectorElementType.Float;
                case VectorElementTypes.Double:
                    return Dlib.Native.VectorElementType.Double;
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }

        internal static ImageTypes ToImageTypes(this MatrixElementTypes matrixElementTypes)
        {
            switch (matrixElementTypes)
            {
                case MatrixElementTypes.UInt8:
                    return ImageTypes.UInt8;
                case MatrixElementTypes.UInt16:
                    return ImageTypes.UInt16;
                //case MatrixElementTypes.UInt32:
                //    return ImageTypes.UInt32;
                //case MatrixElementTypes.Int8:
                //    return ImageTypes.Int8;
                //case MatrixElementTypes.Int16:
                //    return ImageTypes.Int16;
                case MatrixElementTypes.Int32:
                    return ImageTypes.Int32;
                case MatrixElementTypes.Float:
                    return ImageTypes.Float;
                case MatrixElementTypes.Double:
                    return ImageTypes.Double;
                case MatrixElementTypes.RgbPixel:
                    return ImageTypes.RgbPixel;
                case MatrixElementTypes.RgbAlphaPixel:
                    return ImageTypes.HsiPixel;
                case MatrixElementTypes.HsiPixel:
                    return ImageTypes.HsiPixel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matrixElementTypes), matrixElementTypes, null);
            }
        }

        internal static Dlib.Native.InterpolationTypes ToNativeInterpolationTypes(this InterpolationTypes interpolationTypes)
        {
            switch (interpolationTypes)
            {
                case InterpolationTypes.NearestNeighbor:
                    return Dlib.Native.InterpolationTypes.NearestNeighbor;
                case InterpolationTypes.Bilinear:
                    return Dlib.Native.InterpolationTypes.Bilinear;
                case InterpolationTypes.Quadratic:
                    return Dlib.Native.InterpolationTypes.Quadratic;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interpolationTypes), interpolationTypes, null);
            }
        }

        internal static Dlib.Native.PointMappingTypes GetNativePointMappingTypes(this PointTransformBase pointTransform)
        {
            if (pointTransform is PointRotator)
                return Dlib.Native.PointMappingTypes.Rotator;
            if (pointTransform is PointTransform)
                return Dlib.Native.PointMappingTypes.Transform;
            if (pointTransform is PointTransformAffine)
                return Dlib.Native.PointMappingTypes.TransformAffine;
            if (pointTransform is PointTransformProjective)
                return Dlib.Native.PointMappingTypes.TransformProjective;

            throw new ArgumentOutOfRangeException(nameof(pointTransform));
        }

        internal static Dlib.Native.MlpKernelType ToNativeMlpKernelType(this MultilayerPerceptronKernelType type)
        {
            switch (type)
            {
                case MultilayerPerceptronKernelType.Kernel1:
                    return Dlib.Native.MlpKernelType.Kernel1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        internal static Dlib.Native.RunningStatsType ToRunningStatsType(this RunningStatsType type)
        {
            switch (type)
            {
                case RunningStatsType.Float:
                    return Dlib.Native.RunningStatsType.Float;
                case RunningStatsType.Double:
                    return Dlib.Native.RunningStatsType.Double;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }

}
