using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr linspace(double start, double end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_join_rows(MatrixElementType type, IntPtr matrix1, IntPtr matrix2, int templateRows, int templateColumns, out IntPtr ret);

        #region matrix_length

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_length_point(IntPtr point, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_length_dpoint(IntPtr point, out double ret);

        #endregion

        #region matrix_length_squared

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_length_squared(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_cast(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, MatrixElementType desttype, out IntPtr ret);

        #region matrix_max

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

        #endregion

        #region matrix_min

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out byte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out ushort ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out sbyte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out short ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_min(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, out double ret);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_mean(MatrixElementType array2DType, IntPtr matrix_op, int templateRows, int templateColumns, ElementType type, out IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max_point(Array2DType array2DType, IntPtr matrix_op, out IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_max_pointwise_matrix(MatrixElementType type, IntPtr matrix1, IntPtr matrix2, int templateRows, int templateColumns, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_trans(MatrixElementType elementType, IntPtr matrix, int templateRows, int templateColumns, out IntPtr matrix_op);
    }

}