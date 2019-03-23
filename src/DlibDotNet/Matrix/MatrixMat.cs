using System;
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

        public static MatrixOp Mat<T>(IUndisposableElementCollection<Matrix<T>> collection)
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
                case NativeMethods.ErrorType.ElementTypeNotSupport:
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

            //using (var vector = new StdVector<Matrix<T>>(results, new[] { templateRows, templateColumns }))
            //{
            //    var ret = Native.mat_mat_OpStdVectToMat(elementType.ToNativeMatrixElementType(),
            //                                            vector.NativePtr,
            //                                            templateRows,
            //                                            templateColumns,
            //                                            out var matrix);
            //    switch (ret)
            //    {
            //        case ErrorType.ElementTypeNotSupport:
            //            throw new ArgumentException($"{elementType} is not supported.");
            //    }

            //    return new MatrixOp(ElementType.OpStdVectToMat, elementType, matrix, templateRows, templateColumns);
            //}
            var ret = NativeMethods.mat_mat_OpStdVectToMat(elementType.ToNativeMatrixElementType(),
                                                           results.NativePtr,
                                                           templateRows,
                                                           templateColumns,
                                                           out var matrix);
            switch (ret)
            {
                case NativeMethods.ErrorType.ElementTypeNotSupport:
                    throw new ArgumentException($"{elementType} is not supported.");
            }

            return new MatrixOp(NativeMethods.ElementType.OpStdVectToMat, elementType, matrix, templateRows, templateColumns);
        }

        #endregion

    }

}