using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {


        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_int8_t(MatrixElementType matrixElementType,
                                                                      int templateRow,
                                                                      int templateColumn,
                                                                      sbyte gamma,
                                                                      out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_int16_t(MatrixElementType matrixElementType,
                                                                       int templateRow,
                                                                       int templateColumn,
                                                                       short gamma,
                                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_int32_t(MatrixElementType matrixElementType,
                                                                       int templateRow,
                                                                       int templateColumn,
                                                                       int gamma,
                                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_uint8_t(MatrixElementType matrixElementType,
                                                                       int templateRow,
                                                                       int templateColumn,
                                                                       byte gamma,
                                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_uint16_t(MatrixElementType matrixElementType,
                                                                        int templateRow,
                                                                        int templateColumn,
                                                                        ushort gamma,
                                                                        out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_uint32_t(MatrixElementType matrixElementType,
                                                                        int templateRow,
                                                                        int templateColumn,
                                                                        uint gamma,
                                                                        out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_float(MatrixElementType matrixElementType,
                                                                     int templateRow,
                                                                     int templateColumn,
                                                                     float gamma,
                                                                     out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType radial_basis_kernel_new_double(MatrixElementType matrixElementType,
                                                                      int templateRow,
                                                                      int templateColumn,
                                                                      double gamma,
                                                                      out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void radial_basis_kernel_delete(MatrixElementType matrixElementType,
                                                             IntPtr linerKernel, 
                                                             int templateRow,
                                                             int templateColumn);

    }

}
