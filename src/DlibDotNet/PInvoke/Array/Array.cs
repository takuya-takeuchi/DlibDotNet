#if !LITE
using System;
using System.Runtime.InteropServices;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region array

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array_new(Array2DType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array_new1(Array2DType type, uint newSize);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array_delete_pixel(Array2DType type, IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_size(Array2DType type, IntPtr array, out uint size);

        #region getitem

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_uint8(Array2DType type, IntPtr array, uint index, out uint8_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_uint16(Array2DType type, IntPtr array, uint index, out uint16_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_uint32(Array2DType type, IntPtr array, uint index, out uint32_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_int8(Array2DType type, IntPtr array, uint index, out int8_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_int16(Array2DType type, IntPtr array, uint index, out int16_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_int32(Array2DType type, IntPtr array, uint index, out int32_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_float(Array2DType type, IntPtr array, uint index, out float item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_double(Array2DType type, IntPtr array, uint index, out double item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_rgb_pixel(Array2DType type, IntPtr array, uint index, out RgbPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_bgr_pixel(Array2DType type, IntPtr array, uint index, out BgrPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_hsi_pixel(Array2DType type, IntPtr array, uint index, out HsiPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_lab_pixel(Array2DType type, IntPtr array, uint index, out LabPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_getitem_rgb_alpha_pixel(Array2DType type, IntPtr array, uint index, out RgbAlphaPixel item);

        #endregion

        #region pushback

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_uint8(Array2DType type, IntPtr array, uint8_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_uint16(Array2DType type, IntPtr array, uint16_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_uint32(Array2DType type, IntPtr array, uint32_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_int8(Array2DType type, IntPtr array, int8_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_int16(Array2DType type, IntPtr array, int16_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_int32(Array2DType type, IntPtr array, int32_t item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_float(Array2DType type, IntPtr array, float item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_double(Array2DType type, IntPtr array, double item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_rgb_pixel(Array2DType type, IntPtr array, RgbPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_bgr_pixel(Array2DType type, IntPtr array, BgrPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_hsi_pixel(Array2DType type, IntPtr array, HsiPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_lab_pixel(Array2DType type, IntPtr array, LabPixel item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_pixel_pushback_rgb_alpha_pixel(Array2DType type, IntPtr array, RgbAlphaPixel item);

        #endregion

        #region array2d

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array_array2d_new(Array2DType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array_array2d_new1(Array2DType type, uint newSize);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array_delete_array2d(Array2DType type, IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_array2d_size(Array2DType type, IntPtr array, out uint size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_array2d_getitem(Array2DType type, IntPtr array, uint index, out IntPtr item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_array2d_pushback(Array2DType type, IntPtr array, IntPtr item);

        #endregion

        #region matrix

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array_matrix_new(MatrixElementType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr array_matrix_new1(MatrixElementType type, uint newSize);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void array_delete_matrix(MatrixElementType type, IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_matrix_size(MatrixElementType type, IntPtr array, out uint size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_matrix_getitem(MatrixElementType type, IntPtr array, uint index, out IntPtr item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType array_matrix_pushback(MatrixElementType type, IntPtr array, IntPtr item);

        #endregion

        #endregion

    }

}
#endif
