using System;
using System.Runtime.InteropServices;

namespace DlibDotNet
{

    public static partial class Dlib
    {

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new(MatrixElementType matrixElementType);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new1(MatrixElementType matrixElementType, int row, int column);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new2(MatrixElementType matrixElementType, int row, int column, IntPtr src);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern unsafe IntPtr matrix_new3(MatrixElementType matrixElementType, int row, int column, byte* src);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new4(MatrixElementType matrixElementType, uint templateRows, uint templateColumns);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_delete(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_begin(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out IntPtr begin);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_end(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out IntPtr end);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_nc(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_nr(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, byte[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, ushort[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, uint[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, sbyte[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, short[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, float[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, double[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbAlphaPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, HsiPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_left_shift(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, IntPtr ofstream);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_size(MatrixElementType matrixElementType, IntPtr matrix, int templateRows, int templateColumns, out int ret);
            
            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_get_one_row_column(MatrixElementType matrixElementType, IntPtr matrix, int index, int templateRows, int templateColumns, IntPtr ret);
            
            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_set_one_row_column(MatrixElementType matrixElementType, IntPtr matrix, int index, int templateRows, int templateColumns, IntPtr value);
            
            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_get_row_column(MatrixElementType matrixElementType, IntPtr matrix, int row, int column, int templateRows, int templateColumns, IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_set_row_column(MatrixElementType matrixElementType, IntPtr matrix, int row, int column, int templateRows, int templateColumns, IntPtr value);
            
            #region operator

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_add(MatrixElementType matrixElementType,
                                                               IntPtr lhs,
                                                               IntPtr rhs,
                                                               int leftTemplateRows,
                                                               int leftTemplateColumns,
                                                               int rightTemplateRows,
                                                               int rightTemplateColumns,
                                                               out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_negative(MatrixElementType matrixElementType,
                                                                    IntPtr matrix,
                                                                    int templateRows,
                                                                    int templateColumns,
                                                                    out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_subtract(MatrixElementType matrixElementType,
                                                                    IntPtr lhs,
                                                                    IntPtr rhs,
                                                                    int leftTemplateRows,
                                                                    int leftTemplateColumns,
                                                                    int rightTemplateRows,
                                                                    int rightTemplateColumns,
                                                                    out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_subtract_dpoint(MatrixElementType matrixElementType,
                                                                           IntPtr lhs,
                                                                           IntPtr rhs,
                                                                           int templateRows,
                                                                           int templateColumns,
                                                                           out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_multiply(MatrixElementType matrixElementType,
                                                                    IntPtr lhs,
                                                                    IntPtr rhs,
                                                                    int leftTemplateRows,
                                                                    int leftTemplateColumns,
                                                                    int rightTemplateRows,
                                                                    int rightTemplateColumns,
                                                                    out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_multiply_dpoint(MatrixElementType matrixElementType,
                                                                           IntPtr lhs,
                                                                           IntPtr rhs,
                                                                           int templateRows,
                                                                           int templateColumns,
                                                                           out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_multiply_left_numeric(NumericType numericType,
                                                                                 IntPtr lhs,
                                                                                 MatrixElementType type,
                                                                                 IntPtr rhs,
                                                                                 int templateRows,
                                                                                 int templateColumns,
                                                                                 out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_multiply_right_numeric(MatrixElementType type,
                                                                                  IntPtr lhs,
                                                                                  int templateRows,
                                                                                  int templateColumns,
                                                                                  NumericType numericType,
                                                                                  IntPtr rhs,
                                                                                  out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_divide(MatrixElementType matrixElementType,
                                                                  IntPtr lhs,
                                                                  IntPtr rhs,
                                                                  int leftTemplateRows,
                                                                  int leftTemplateColumns,
                                                                  int rightTemplateRows,
                                                                  int rightTemplateColumns,
                                                                  out IntPtr ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_divide_double(MatrixElementType matrixElementType,
                                                                         IntPtr lhs,
                                                                         double rhs,
                                                                         int leftTemplateRows,
                                                                         int leftTemplateColumns,
                                                                         out IntPtr ret);

            #endregion

        }

    }

}
