using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static Matrix<T> Round<T>(Matrix<T> matrix)
            where T : struct
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var templateRows = matrix.TemplateRows;
            var templateColumns = matrix.TemplateColumns;

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.matrix_round(type,
                                                 matrix.NativePtr,
                                                 templateRows,
                                                 templateColumns,
                                                 out var result);
            switch (ret)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return new Matrix<T>(result, templateRows, templateColumns);
        }

        #endregion

    }

}