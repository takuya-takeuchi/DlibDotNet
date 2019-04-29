using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region delete

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_op_op_array2d_to_mat_delete(Array2DType type, IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_op_op_heatmap_delete(Array2DType type, IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_op_op_jet_delete(Array2DType type, IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_op_op_std_vect_to_mat_delete(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_op_op_std_vect_to_mat_value_delete(Array2DType type, IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_op_op_trans_delete(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_op_op_join_rows_delete(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

        #endregion

        #region nc

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_array2d_to_mat_nc(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_heatmap_nc(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_heatmap_nc_matrix(MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_jet_nc(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_jet_nc_matrix(MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_std_vect_to_mat_nc(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_std_vect_to_mat_value_nc(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_trans_nc(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_join_rows_nc(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        #endregion

        #region nr

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_array2d_to_mat_nr(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_heatmap_nr(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_heatmap_nr_matrix(MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_jet_nr(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_jet_nr_matrix(MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_std_vect_to_mat_nr(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_std_vect_to_mat_value_nr(Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_trans_nr(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_join_rows_nr(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        #endregion

        #region operator

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_jet_operator(Array2DType type, IntPtr matrix, int r, int c, IntPtr rgbPixel);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_heatmap_operator(Array2DType type, IntPtr matrix, int r, int c, IntPtr rgbPixel);

        #endregion

        #region operator_left_shift

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_std_vect_to_mat_operator_left_shift(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, IntPtr stream);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_std_vect_to_mat_value_operator_left_shift(Array2DType type, IntPtr obj, IntPtr stream);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType matrix_op_op_trans_operator_left_shift(MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, IntPtr stream);

        #endregion

    }

}