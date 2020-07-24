using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_window_delete(IntPtr ptr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_window_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_window_new_array2d1(Array2DType type, IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_window_new_array2d2(Array2DType type, IntPtr image, byte[] title, int titleLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_window_new_matrix1(MatrixElementType type, IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_window_new_matrix2(MatrixElementType type, IntPtr image, byte[] title, int titleLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op1(ElementType matrixElementType, Array2DType type, IntPtr image, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op2(ElementType matrixElementType, Array2DType type, IntPtr image, byte[] title, int titleLength, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op3(ElementType etype,
                                                                   MatrixElementType type,
                                                                   IntPtr img,
                                                                   int templateRows,
                                                                   int templateColumns,
                                                                   out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_new_matrix_op4(ElementType etype,
                                                                   MatrixElementType type,
                                                                   IntPtr img,
                                                                   int templateRows,
                                                                   int templateColumns,
                                                                   byte[] title,
                                                                   int titleLength,
                                                                   out IntPtr ret);

        #region image_window_add_overlay

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay(IntPtr window, IntPtr rect, Array2DType type, ref LabPixel color);

        #endregion

        #region image_window_add_overlay2

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay2(IntPtr window, IntPtr vectorOfRect, Array2DType type, ref LabPixel color);

        #endregion

        #region image_window_add_overlay3

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref byte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref ushort color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref uint color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref sbyte color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref short color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref int color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref float color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref double color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref RgbPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref RgbAlphaPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref HsiPixel color);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay3(IntPtr window, IntPtr rect, Array2DType type, ref LabPixel color);

        #endregion

        #region image_window_add_overlay4

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay4(IntPtr window, IntPtr line);

        #endregion

        #region image_window_add_overlay5

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay5(IntPtr window, IntPtr vectorOfLine);

        #endregion

        #region image_window_add_overlay6

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref byte color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref ushort color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref uint color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref sbyte color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref short color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref int color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref float color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref double color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref RgbPixel color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref RgbAlphaPixel color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref HsiPixel color, IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_add_overlay6(IntPtr window, IntPtr rect, Array2DType type, ref LabPixel color, IntPtr str);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_window_clear_overlay(IntPtr window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern bool image_window_get_next_double_click(IntPtr window, out IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern bool image_window_get_next_double_click2(IntPtr window, out IntPtr p, out uint mouseButton);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern bool image_window_is_closed(IntPtr window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_set_image_array2d(IntPtr window, Array2DType type, IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_set_image_matrix_op_array2d(IntPtr window, ElementType matrixElementType, Array2DType type, IntPtr matrix);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_set_image_matrix_op_matrix(IntPtr window, ElementType matrixElementType, MatrixElementType type, IntPtr matrix);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType image_window_set_image_matrix(IntPtr window, MatrixElementType type, IntPtr matrix);

    }

}