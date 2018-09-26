using System;
using System.Runtime.InteropServices;
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
            var ret = Native.matrix_round(type,
                                          matrix.NativePtr,
                                          templateRows,
                                          templateColumns,
                                          out var result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return new Matrix<T>(result, templateRows, templateColumns);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_round(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out IntPtr ret);

        }

    }

}