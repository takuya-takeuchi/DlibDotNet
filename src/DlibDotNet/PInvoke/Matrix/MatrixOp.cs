using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region delete

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void matrix_op_op_array2d_to_mat_delete(NativeMethods.Array2DType type, IntPtr obj);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void matrix_op_op_heatmap_delete(NativeMethods.Array2DType type, IntPtr obj);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void matrix_op_op_jet_delete(NativeMethods.Array2DType type, IntPtr obj);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void matrix_op_op_std_vect_to_mat_delete(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void matrix_op_op_trans_delete(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void matrix_op_op_join_rows_delete(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns);

        #endregion

        #region nc

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_array2d_to_mat_nc(NativeMethods.Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_heatmap_nc(NativeMethods.Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_heatmap_nc_matrix(NativeMethods.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_jet_nc(NativeMethods.Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_jet_nc_matrix(NativeMethods.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_std_vect_to_mat_nc(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_trans_nc(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_join_rows_nc(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        #endregion

        #region nr

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_array2d_to_mat_nr(NativeMethods.Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_heatmap_nr(NativeMethods.Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_heatmap_nr_matrix(NativeMethods.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_jet_nr(NativeMethods.Array2DType type, IntPtr obj, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_jet_nr_matrix(NativeMethods.MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_std_vect_to_mat_nr(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_trans_nr(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_join_rows_nr(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, out int ret);

        #endregion

        #region operator

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_jet_operator(NativeMethods.Array2DType type, IntPtr matrix, int r, int c, IntPtr rgbPixel);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_heatmap_operator(NativeMethods.Array2DType type, IntPtr matrix, int r, int c, IntPtr rgbPixel);

        #endregion

        #region operator_left_shift

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_std_vect_to_mat_operator_left_shift(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, IntPtr stream);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType matrix_op_op_trans_operator_left_shift(NativeMethods.MatrixElementType type, IntPtr obj, int templateRows, int templateColumns, IntPtr stream);

        #endregion

    }

}