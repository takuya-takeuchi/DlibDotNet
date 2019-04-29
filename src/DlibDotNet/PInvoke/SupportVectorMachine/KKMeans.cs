using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_new(KernelType kernelType, 
                                                   MatrixElementType type,
                                                   int templateRows,
                                                   int templateColumns,
                                                   IntPtr kcentroid,
                                                   out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void kkmeans_delete(KernelType kernelType,
                                                 MatrixElementType type,
                                                 int templateRows,
                                                 int templateColumns,
                                                 IntPtr kkmeans);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_get_kernel(KernelType kernelType,
                                                          MatrixElementType type,
                                                          int templateRows,
                                                          int templateColumns,
                                                          IntPtr obj,
                                                          out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_set_number_of_centers(KernelType kernelType,
                                                                     MatrixElementType type,
                                                                     int templateRows,
                                                                     int templateColumns,
                                                                     IntPtr kkmeans,
                                                                     uint num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_get_number_of_centers(KernelType kernelType,
                                                                     MatrixElementType type,
                                                                     int templateRows,
                                                                     int templateColumns,
                                                                     IntPtr kkmeans,
                                                                     out uint num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_set_kcentroid(KernelType kernelType,
                                                             MatrixElementType type,
                                                             int templateRows,
                                                             int templateColumns,
                                                             IntPtr kkmeans,
                                                             IntPtr kcentroid);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_get_kcentroid(KernelType kernelType,
                                                             MatrixElementType type,
                                                             int templateRows,
                                                             int templateColumns,
                                                             IntPtr kkmeans,
                                                             uint i,
                                                             out IntPtr kcentroid);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_operator(KernelType kernelType,
                                                        MatrixElementType type,
                                                        int templateRows,
                                                        int templateColumns,
                                                        IntPtr kkmeans,
                                                        IntPtr sample,
                                                        out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kkmeans_train(KernelType kernelType,
                                                     MatrixElementType type,
                                                     int templateRows,
                                                     int templateColumns,
                                                     IntPtr kkmeans,
                                                     IntPtr samples,
                                                     IntPtr centers,
                                                     uint max_iter);

    }

}