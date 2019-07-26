using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {
        
        #region array2d

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array2d_new(Array2DType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array2d_new1(Array2DType type, int rows, int cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_delete(Array2DType type, IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_nc(Array2DType type, IntPtr array, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_nr(Array2DType type, IntPtr array, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_size(Array2DType type, IntPtr array, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_set_size(Array2DType type, IntPtr array, int rows, int cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType rectangle_get_rect(Array2DType type, IntPtr array, out IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType rectangle_get_rect_matrix(MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array2d_row(Array2DType type, IntPtr array, int row, out IntPtr ret);

        #region array2d_get_row_column

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_uint8_t(IntPtr row, int column, out byte value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_uint16_t(IntPtr row, int column, out ushort value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_uint32_t(IntPtr row, int column, out uint value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_int8_t(IntPtr row, int column, out sbyte value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_int16_t(IntPtr row, int column, out short value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_int32_t(IntPtr row, int column, out int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_double(IntPtr row, int column, out double value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_float(IntPtr row, int column, out float value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_rgb_pixel(IntPtr row, int column, out RgbPixel value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_bgr_pixel(IntPtr row, int column, out BgrPixel value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_rgb_alpha_pixel(IntPtr row, int column, out RgbAlphaPixel value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_get_row_column_hsi_pixel(IntPtr row, int column, out HsiPixel value);

        #endregion

        #region array2d_set_row_column

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_uint8_t(IntPtr row, int column, byte value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_uint16_t(IntPtr row, int column, ushort value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_uint32_t(IntPtr row, int column, uint value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_int8_t(IntPtr row, int column, sbyte value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_int16_t(IntPtr row, int column, short value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_int32_t(IntPtr row, int column, int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_double(IntPtr row, int column, double value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_float(IntPtr row, int column, float value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_rgb_pixel(IntPtr row, int column, RgbPixel value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_bgr_pixel(IntPtr row, int column, BgrPixel value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_rgb_alpha_pixel(IntPtr row, int column, RgbAlphaPixel value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_set_row_column_hsi_pixel(IntPtr row, int column, HsiPixel value);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_row_delete(Array2DType type, IntPtr row);

        #region array2d_matrix

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array2d_matrix_new(MatrixElementType type, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array2d_matrix_new1(MatrixElementType type, int rows, int cols, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_delete(MatrixElementType type, IntPtr array, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_matrix_nc(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_matrix_nr(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_matrix_size(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out int ret);
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_matrix_set_size(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, int rows, int cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array2d_matrix_get_rect(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out IntPtr rect);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array2d_matrix_row(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, int row, out IntPtr ret);

        #region array2d_matrix_get_row_column

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_uint8_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_uint16_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_uint32_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_int8_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_int16_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_int32_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_double(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_float(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_rgb_pixel(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_bgr_pixel(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_rgb_alpha_pixel(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_get_row_column_hsi_pixel(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        #endregion

        #region array2d_matrix_set_row_column

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_uint8_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_uint16_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_uint32_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_int8_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_int16_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_int32_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_double(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_float(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_rgb_pixel(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_bgr_pixel(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_rgb_alpha_pixel(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_set_row_column_hsi_pixel(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array2d_matrix_row_delete(MatrixElementType type, IntPtr row, int templateRows, int templateColumns);

        #endregion

    }

}