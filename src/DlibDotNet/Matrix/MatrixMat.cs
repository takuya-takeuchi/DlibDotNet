using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static MatrixOp Mat(Array2DBase array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            array.ThrowIfDisposed();

            var imageType = array.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.mat_array2d(imageType, array.NativePtr, out var matrix);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"{imageType} is not supported.");
            }

            return new MatrixOp(NativeMethods.ElementType.OpArray2DToMat, array.ImageType, matrix);
        }

        public static MatrixOp Mat<T>(IEnumerable<T> collection)
            where T : struct
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            var collectionArray = collection.ToArray();
            if (!collectionArray.Any())
                throw new ArgumentException();

            Matrix<T>.TryParse<T>(out var elementType);

            // Do not dispose until MatrixOp will be disposed
            var vector = new StdVector<T>(collectionArray);
            {
                IntPtr ret;
                switch (elementType)
                {
                    case MatrixElementTypes.UInt8:
                        NativeMethods.mat_StdVect_uint8_t(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.UInt8, ret);
                    case MatrixElementTypes.UInt16:
                        NativeMethods.mat_StdVect_uint16_t(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.UInt16, ret);
                    case MatrixElementTypes.UInt32:
                        NativeMethods.mat_StdVect_uint32_t(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.UInt32, ret);
                    case MatrixElementTypes.Int8:
                        NativeMethods.mat_StdVect_int8_t(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.Int8, ret);
                    case MatrixElementTypes.Int16:
                        NativeMethods.mat_StdVect_int16_t(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.Int16, ret);
                    case MatrixElementTypes.Int32:
                        NativeMethods.mat_StdVect_int32_t(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.Int32, ret);
                    case MatrixElementTypes.Float:
                        NativeMethods.mat_StdVect_float(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.Float, ret);
                    case MatrixElementTypes.Double:
                        NativeMethods.mat_StdVect_double(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.Double, ret);
                    case MatrixElementTypes.RgbPixel:
                        NativeMethods.mat_StdVect_rgb_pixel(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.RgbPixel, ret);
                    case MatrixElementTypes.BgrPixel:
                        NativeMethods.mat_StdVect_bgr_pixel(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.BgrPixel, ret);
                    case MatrixElementTypes.RgbAlphaPixel:
                        NativeMethods.mat_StdVect_rgb_alpha_pixel(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.RgbAlphaPixel, ret);
                    case MatrixElementTypes.HsiPixel:
                        NativeMethods.mat_StdVect_hsi_pixel(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.HsiPixel, ret);
                    case MatrixElementTypes.LabPixel:
                        NativeMethods.mat_StdVect_lab_pixel(vector.NativePtr, out ret);
                        return new MatrixOp(NativeMethods.ElementType.OpStdVectToMatValue, ImageTypes.LabPixel, ret);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static MatrixOp Mat<T>(OutputLabels<Matrix<T>> collection)
            where T : struct
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (!collection.Any())
                throw new ArgumentException();
            if (collection.Any(obj => obj == null))
                throw new ArgumentException();

            collection.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var elementType);

            var first = collection.First();
            var templateRows = first.TemplateRows;
            var templateColumns = first.TemplateColumns;

            var ret = NativeMethods.mat_mat_OpStdVectToMat(elementType.ToNativeMatrixElementType(),
                                                           collection.NativePtr,
                                                           templateRows,
                                                           templateColumns,
                                                           out var matrix);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{elementType} is not supported.");
            }

            return new MatrixOp(NativeMethods.ElementType.OpStdVectToMat, elementType, matrix, templateRows, templateColumns);
        }

        public static MatrixOp Mat<T>(NetResult<Matrix<T>> results)
            where T : struct
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));
            if (!results.Any())
                throw new ArgumentException();
            if (results.Any(m => m == null))
                throw new ArgumentException();

            results.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var elementType);

            var first = results.First();
            var templateRows = first.TemplateRows;
            var templateColumns = first.TemplateColumns;

            var ret = NativeMethods.mat_mat_OpStdVectToMat(elementType.ToNativeMatrixElementType(),
                                                           results.NativePtr,
                                                           templateRows,
                                                           templateColumns,
                                                           out var matrix);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{elementType} is not supported.");
            }

            return new MatrixOp(NativeMethods.ElementType.OpStdVectToMat, elementType, matrix, templateRows, templateColumns);
        }

        #endregion

    }

}