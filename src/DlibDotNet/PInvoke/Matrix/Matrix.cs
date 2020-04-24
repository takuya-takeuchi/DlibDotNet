using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_new(MatrixElementType matrixElementType);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_new1(MatrixElementType matrixElementType, int row, int column);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_new2(MatrixElementType matrixElementType, int row, int column, IntPtr src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern unsafe IntPtr matrix_new3(MatrixElementType matrixElementType, int row, int column, byte* src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_new4(MatrixElementType matrixElementType,
                                                   uint templateRows,
                                                   uint templateColumns,
                                                   out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_new5(MatrixElementType matrixElementType,
                                                   uint templateRows,
                                                   uint templateColumns,
                                                   IntPtr vector,
                                                   out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_new6(MatrixElementType matrixElementType,
                                                int row,
                                                int column,
                                                int stride,
                                                IntPtr src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_delete(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_clone(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_deserialize_matrix_proxy(IntPtr deserializeProxy,
                                                                       MatrixElementType matrixElementType,
                                                                       int templateRows,
                                                                       int templateColumns,
                                                                       out IntPtr matrix,
                                                                       out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_begin(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out IntPtr begin);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_end(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out IntPtr end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool matrix_nc(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool matrix_nr(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, byte[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, ushort[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, uint[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, sbyte[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, short[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, int[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, float[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, double[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, RgbPixel[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, BgrPixel[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, RgbAlphaPixel[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, HsiPixel[] array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_left_shift(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, IntPtr ofstream);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool matrix_size(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_set_size(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_set_size2(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, int rows, int cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_get_one_row_column(MatrixElementType matrixElementType, IntPtr matrix, int index, int templateRows, int templateColumns, IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_set_one_row_column(MatrixElementType matrixElementType, IntPtr matrix, int index, int templateRows, int templateColumns, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_get_row_column(MatrixElementType matrixElementType, IntPtr matrix, int row, int column, int templateRows, int templateColumns, IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_set_row_column(MatrixElementType matrixElementType, IntPtr matrix, int row, int column, int templateRows, int templateColumns, IntPtr value);

        #region operator

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_add(MatrixElementType matrixElementType,
                                                           IntPtr lhs,
                                                           IntPtr rhs,
                                                           int leftTemplateRows,
                                                           int leftTemplateColumns,
                                                           int rightTemplateRows,
                                                           int rightTemplateColumns,
                                                           out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_negative(MatrixElementType matrixElementType,
                                                                IntPtr matrix,
                                                                int templateRows,
                                                                int templateColumns,
                                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_invert(MatrixElementType matrixElementType,
                                                              IntPtr matrix,
                                                              int templateRows,
                                                              int templateColumns,
                                                              out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_subtract(MatrixElementType matrixElementType,
                                                                IntPtr lhs,
                                                                IntPtr rhs,
                                                                int leftTemplateRows,
                                                                int leftTemplateColumns,
                                                                int rightTemplateRows,
                                                                int rightTemplateColumns,
                                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_subtract_dpoint(MatrixElementType matrixElementType,
                                                                       IntPtr lhs,
                                                                       IntPtr rhs,
                                                                       int templateRows,
                                                                       int templateColumns,
                                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_multiply(MatrixElementType matrixElementType,
                                                                IntPtr lhs,
                                                                IntPtr rhs,
                                                                int leftTemplateRows,
                                                                int leftTemplateColumns,
                                                                int rightTemplateRows,
                                                                int rightTemplateColumns,
                                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_multiply_dpoint(MatrixElementType matrixElementType,
                                                                       IntPtr lhs,
                                                                       IntPtr rhs,
                                                                       int templateRows,
                                                                       int templateColumns,
                                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_multiply_left_numeric(NumericType numericType,
                                                                             IntPtr lhs,
                                                                             MatrixElementType type,
                                                                             IntPtr rhs,
                                                                             int templateRows,
                                                                             int templateColumns,
                                                                             out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_multiply_right_numeric(MatrixElementType type,
                                                                              IntPtr lhs,
                                                                              int templateRows,
                                                                              int templateColumns,
                                                                              NumericType numericType,
                                                                              IntPtr rhs,
                                                                              out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_divide(MatrixElementType matrixElementType,
                                                              IntPtr lhs,
                                                              IntPtr rhs,
                                                              int leftTemplateRows,
                                                              int leftTemplateColumns,
                                                              int rightTemplateRows,
                                                              int rightTemplateColumns,
                                                              out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_operator_divide_double(MatrixElementType matrixElementType,
                                                                     IntPtr lhs,
                                                                     double rhs,
                                                                     int leftTemplateRows,
                                                                     int leftTemplateColumns,
                                                                     out IntPtr ret);

        #endregion

    }

}