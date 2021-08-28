#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_array2d(Array2DType type, IntPtr array, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_mat_OpStdVectToMat(MatrixElementType type, IntPtr vector, int templateRows, int templateColumns, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_int8_t(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_int16_t(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_int32_t(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_uint8_t(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_uint16_t(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_uint32_t(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_float(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_double(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_rgb_pixel(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_bgr_pixel(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_hsi_pixel(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_lab_pixel(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mat_StdVect_rgb_alpha_pixel(IntPtr vector, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType mat_matrix(Array2DType srcType, IntPtr src, int templateRows, int templateColumns, MatrixElementType dstType, out IntPtr ret);

    }

}
#endif
