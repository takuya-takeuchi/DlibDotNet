using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static MatrixRangeExp<double> Linspace(double start, double end, int num)
        {
            var matrixRange = Native.linspace(start, end, num);
            return new MatrixRangeExp<double>(matrixRange);
        }

        public static Point MaxPoint(MatrixOp matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.Array2DType;
            var ret = Native.matrix_max_point(type, matrix.NativePtr, out var point);
            switch (ret)
            {
                case Native.ErrorType.ArrayTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return new Point(point);
        }

        public static MatrixOp Trans(MatrixBase matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_trans(type, matrix.NativePtr, out var matrixOp);
            switch (ret)
            {
                case Native.ErrorType.ElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            var imageType = matrix.MatrixElementType.ToImageTypes();
            return new MatrixOp(Native.ElementType.OpTrans, imageType, matrixOp);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr linspace(double start, double end, int num);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max_point(Array2DType array2DType, IntPtr matrix_op, out IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_trans(MatrixElementType elementType, IntPtr matrix, out IntPtr matrix_op);
        }

    }

}