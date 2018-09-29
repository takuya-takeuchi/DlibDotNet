using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static MatrixOp JoinRows(MatrixBase matrix1, MatrixBase matrix2)
        {
            if (matrix1 == null)
                throw new ArgumentNullException(nameof(matrix1));
            if (matrix2 == null)
                throw new ArgumentNullException(nameof(matrix2));

            matrix1.ThrowIfDisposed();
            matrix2.ThrowIfDisposed();

            // Need not to check whether both TemplateColumns and TemplateRows are same
            var templateRows = matrix1.TemplateRows;
            var templateColumns = matrix1.TemplateColumns;

            // In the future, these statement shold be removed because above comment
            if (templateRows != matrix2.TemplateRows)
                throw new ArgumentException();
            if (templateColumns != matrix2.TemplateColumns)
                throw new ArgumentException();

            var type1 = matrix1.MatrixElementType.ToNativeMatrixElementType();
            var type2 = matrix2.MatrixElementType.ToNativeMatrixElementType();
            if (type1 != type2)
                throw new ArgumentException();

            var ret = Native.matrix_join_rows(type1,
                                              matrix1.NativePtr,
                                              matrix2.NativePtr,
                                              templateRows,
                                              templateColumns,
                                              out var matrixOp);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type1} is not supported.");
            }

            var imageType = matrix1.MatrixElementType;
            return new MatrixOp(Native.ElementType.OpJoinRows, imageType, matrixOp, templateRows, templateColumns);
        }
        
        #region Length

        public static byte Length(Matrix<byte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out byte length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static ushort Length(Matrix<ushort> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out ushort length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static uint Length(Matrix<uint> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out uint length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static sbyte Length(Matrix<sbyte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out sbyte length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static short Length(Matrix<short> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out short length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static int Length(Matrix<int> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out int length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static float Length(Matrix<float> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out float length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static double Length(Matrix<double> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out double length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        #endregion

        #region LengthSquared

        public static byte LengthSquared(Matrix<byte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out byte length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static ushort LengthSquared(Matrix<ushort> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out ushort length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static uint LengthSquared(Matrix<uint> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out uint length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static sbyte LengthSquared(Matrix<sbyte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out sbyte length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static short LengthSquared(Matrix<short> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out short length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static int LengthSquared(Matrix<int> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out int length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static float LengthSquared(Matrix<float> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out float length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        public static double LengthSquared(Matrix<double> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_length_squared(type, matrix.NativePtr, matrix.TemplateRows, matrix.TemplateColumns, out double length);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
                case Native.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(matrix.TemplateColumns)} or {nameof(matrix.TemplateRows)} is not supported.");
            }

            return length;
        }

        #endregion

        public static Matrix<double> Linspace(double start, double end, int num)
        {
            var matrix = Native.linspace(start, end, num);
            return new Matrix<double>(matrix);
        }

        public static Matrix<T> MatrixCast<T>(MatrixBase matrix)
            where T : struct
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            Matrix<T>.TryParse<T>(out var destElementType);
            var ret = Native.matrix_cast(type,
                                         matrix.NativePtr,
                                         matrix.TemplateRows,
                                         matrix.TemplateColumns,
                                         destElementType.ToNativeMatrixElementType(),
                                         out var mat);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return new Matrix<T>(mat, matrix.TemplateRows, matrix.TemplateColumns);
        }

        #region Max

        public static byte Max(Matrix<byte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out byte result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static ushort Max(Matrix<ushort> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out ushort result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static uint Max(Matrix<uint> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out uint result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static sbyte Max(Matrix<sbyte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out sbyte result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static short Max(Matrix<short> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out short result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static int Max(Matrix<int> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out int result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static float Max(Matrix<float> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out float result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static double Max(Matrix<double> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_max(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out double result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        #endregion

        #region Min

        public static byte Min(Matrix<byte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out byte result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static ushort Min(Matrix<ushort> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out ushort result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static uint Min(Matrix<uint> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out uint result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static sbyte Min(Matrix<sbyte> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out sbyte result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static short Min(Matrix<short> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out short result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static int Min(Matrix<int> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out int result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static float Min(Matrix<float> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out float result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        public static double Min(Matrix<double> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_min(type,
                                        matrix.NativePtr,
                                        matrix.TemplateRows,
                                        matrix.TemplateColumns,
                                        out double result);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return result;
        }

        #endregion

        public static Point MaxPoint(MatrixOp matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var type = matrix.Array2DType;
            var ret = Native.matrix_max_point(type, matrix.NativePtr, out var point);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return new Point(point);
        }

        public static Matrix<T> MaxPointWise<T>(Matrix<T> matrix1, Matrix<T> matrix2)
            where T : struct
        {
            if (matrix1 == null)
                throw new ArgumentNullException(nameof(matrix1));
            if (matrix2 == null)
                throw new ArgumentNullException(nameof(matrix2));

            matrix1.ThrowIfDisposed();
            matrix2.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var type);
            var ret = Native.matrix_max_pointwise_matrix(type.ToNativeMatrixElementType(),
                                                         matrix1.NativePtr,
                                                         matrix2.NativePtr,
                                                         out var value);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return new Matrix<T>(value, matrix1.TemplateRows, matrix1.TemplateColumns);
        }

        public static Matrix<T> Mean<T>(MatrixOp matrix)
            where T : struct
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            Matrix<T>.TryParse<T>(out var type);
            var ret = Native.matrix_mean(type.ToNativeMatrixElementType(),
                                         matrix.NativePtr,
                                         matrix.TemplateRows,
                                         matrix.TemplateColumns,
                                         matrix.ElementType,
                                         out var value);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            return new Matrix<T>(value, matrix.TemplateRows, matrix.TemplateColumns);
        }

        public static MatrixOp Trans(MatrixBase matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            var templateRows = matrix.TemplateRows;
            var templateColumns = matrix.TemplateColumns;

            var type = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.matrix_trans(type, matrix.NativePtr, templateRows, templateColumns, out var matrixOp);
            switch (ret)
            {
                case Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{type} is not supported.");
            }

            var imageType = matrix.MatrixElementType;
            return new MatrixOp(Native.ElementType.OpTrans, imageType, matrixOp, templateRows, templateColumns);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr linspace(double start, double end, int num);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_join_rows(MatrixElementType type, IntPtr matrix1, IntPtr matrix2, int templateRows, int templateColumns, out IntPtr ret);
            
            #region matrix_length

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

            #endregion

            #region matrix_length_squared

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_cast(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, MatrixElementType desttype, out IntPtr ret);

            #region matrix_max

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

            #endregion

            #region matrix_min

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_mean(MatrixElementType array2DType, IntPtr matrix_op, int templateRows, int templateColumns, ElementType type, out IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max_point(Array2DType array2DType, IntPtr matrix_op, out IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_max_pointwise_matrix(MatrixElementType type, IntPtr matrix1, IntPtr matrix2, out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_trans(MatrixElementType elementType, IntPtr matrix, int templateRows, int templateColumns, out IntPtr matrix_op);
        }

    }

}