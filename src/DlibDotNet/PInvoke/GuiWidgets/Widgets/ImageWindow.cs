using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_window_delete(IntPtr ptr);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_window_new();

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_window_new_array2d1(NativeMethods.Array2DType type, IntPtr image);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_window_new_array2d2(NativeMethods.Array2DType type, IntPtr image, byte[] title);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_window_new_matrix1(NativeMethods.MatrixElementType type, IntPtr image);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_window_new_matrix2(NativeMethods.MatrixElementType type, IntPtr image, byte[] title);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op1(NativeMethods.ElementType matrixElementType, NativeMethods.Array2DType type, IntPtr image, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op2(NativeMethods.ElementType matrixElementType, NativeMethods.Array2DType type, IntPtr image, byte[] title, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op3(NativeMethods.ElementType etype,
                                                                   NativeMethods.MatrixElementType type,
                                                                   IntPtr img,
                                                                   int templateRows,
                                                                   int templateColumns,
                                                                   out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op4(NativeMethods.ElementType etype,
                                                                   NativeMethods.MatrixElementType type,
                                                                   IntPtr img,
                                                                   int templateRows,
                                                                   int templateColumns,
                                                                   byte[] title,
                                                                   out IntPtr ret);

        #region image_window_add_overlay

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref HsiPixel color);

        #endregion

        #region image_window_add_overlay2

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, NativeMethods.Array2DType type, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, NativeMethods.Array2DType type, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, NativeMethods.Array2DType type, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, NativeMethods.Array2DType type, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, NativeMethods.Array2DType type, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, NativeMethods.Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, NativeMethods.Array2DType type, ref HsiPixel color);

        #endregion

        #region image_window_add_overlay3

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref byte color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref ushort color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref float color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref double color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref RgbPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, NativeMethods.Array2DType type, ref HsiPixel color);

        #endregion

        #region image_window_add_overlay4

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay4(IntPtr window, IntPtr line);

        #endregion

        #region image_window_add_overlay5

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_add_overlay5(IntPtr window, IntPtr vectorOfLine);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_window_clear_overlay(IntPtr window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern bool image_window_get_next_double_click(IntPtr window, out IntPtr p);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern bool image_window_get_next_double_click2(IntPtr window, out IntPtr p, out uint mouseButton);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern bool image_window_is_closed(IntPtr window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_set_image_array2d(IntPtr window, NativeMethods.Array2DType type, IntPtr image);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_set_image_matrix_op_array2d(IntPtr window, NativeMethods.ElementType matrixElementType, NativeMethods.Array2DType type, IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_set_image_matrix_op_matrix(IntPtr window, NativeMethods.ElementType matrixElementType, NativeMethods.MatrixElementType type, IntPtr matrix);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_window_set_image_matrix(IntPtr window, NativeMethods.MatrixElementType type, IntPtr matrix);

    }

}