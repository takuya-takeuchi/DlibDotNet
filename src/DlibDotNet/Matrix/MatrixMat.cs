using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            var ret = Native.mat_array2d(imageType, array.NativePtr, out var matrix);
            switch (ret)
            {
                case Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"{imageType} is not supported.");
            }

            return new MatrixOp(Native.ElementType.OpArray2DToMat, array.ImageType, matrix);
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

            var ret = Native.mat_mat_OpStdVectToMat(elementType.ToNativeMatrixElementType(),
                                                    collection.NativePtr,
                                                    templateRows,
                                                    templateColumns,
                                                    out var matrix);
            switch (ret)
            {
                case Native.ErrorType.ElementTypeNotSupport:
                    throw new ArgumentException($"{elementType} is not supported.");
            }

            return new MatrixOp(Native.ElementType.OpStdVectToMat, elementType, matrix, templateRows, templateColumns);
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
            //        case Native.ErrorType.ElementTypeNotSupport:
            //            throw new ArgumentException($"{elementType} is not supported.");
            //    }

            //    return new MatrixOp(Native.ElementType.OpStdVectToMat, elementType, matrix, templateRows, templateColumns);
            //}
            var ret = Native.mat_mat_OpStdVectToMat(elementType.ToNativeMatrixElementType(),
                                                    results.NativePtr,
                                                    templateRows,
                                                    templateColumns,
                                                    out var matrix);
            switch (ret)
            {
                case Native.ErrorType.ElementTypeNotSupport:
                    throw new ArgumentException($"{elementType} is not supported.");
            }

            return new MatrixOp(Native.ElementType.OpStdVectToMat, elementType, matrix, templateRows, templateColumns);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mat_array2d(Array2DType type, IntPtr array, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mat_mat_OpStdVectToMat(MatrixElementType type, IntPtr vector, int templateRows, int templateColumns, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mat_matrix(Array2DType srcType, IntPtr src, int templateRows, int templateColumns, MatrixElementType dstType, out IntPtr ret);

        }

    }

}