using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kcentroid_new(SvmKernelType kernelType,
                                                     MatrixElementType type,
                                                     int templateRows,
                                                     int templateColumns,
                                                     IntPtr kernel,
                                                     double tolerance,
                                                     uint maxDictionarySize,
                                                     bool removeOldestFirst,
                                                     out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void kcentroid_delete(SvmKernelType kernelType,
                                                   MatrixElementType type,
                                                   int templateRows,
                                                   int templateColumns,
                                                   IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kcentroid_dictionary_size(SvmKernelType kernelType,
                                                                 MatrixElementType type,
                                                                 int templateRows,
                                                                 int templateColumns,
                                                                 IntPtr obj,
                                                                 out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kcentroid_get_kernel(SvmKernelType kernelType,
                                                            MatrixElementType type,
                                                            int templateRows,
                                                            int templateColumns,
                                                            IntPtr obj,
                                                            out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kcentroid_operator_float(SvmKernelType kernelType,
                                                                MatrixElementType type,
                                                                int templateRows,
                                                                int templateColumns,
                                                                IntPtr obj,
                                                                IntPtr sample,
                                                                out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kcentroid_operator_double(SvmKernelType kernelType,
                                                                 MatrixElementType type,
                                                                 int templateRows,
                                                                 int templateColumns,
                                                                 IntPtr obj,
                                                                 IntPtr sample,
                                                                 out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kcentroid_train_float(SvmKernelType kernelType,
                                                             MatrixElementType type,
                                                             int templateRows,
                                                             int templateColumns,
                                                             IntPtr obj,
                                                             IntPtr sample);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType kcentroid_train_double(SvmKernelType kernelType,
                                                              MatrixElementType type,
                                                              int templateRows,
                                                              int templateColumns,
                                                              IntPtr obj,
                                                              IntPtr sample);

    }

}