using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_new(SvmKernelType kernelType,
                                                MatrixElementType type,
                                                int templateRows,
                                                int templateColumns,
                                                IntPtr kernel,
                                                double tolerance,
                                                uint maxDictionarySize,
                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void krls_delete(SvmKernelType kernelType,
                                              MatrixElementType type,
                                              int templateRows,
                                              int templateColumns,
                                              IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_dictionary_size(SvmKernelType kernelType,
                                                            MatrixElementType type,
                                                            int templateRows,
                                                            int templateColumns,
                                                            IntPtr obj,
                                                            out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_get_kernel(SvmKernelType kernelType,
                                                       MatrixElementType type,
                                                       int templateRows,
                                                       int templateColumns,
                                                       IntPtr obj,
                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_operator_float(SvmKernelType kernelType,
                                                           MatrixElementType type,
                                                           int templateRows,
                                                           int templateColumns,
                                                           IntPtr obj,
                                                           IntPtr sample,
                                                           out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_operator_double(SvmKernelType kernelType,
                                                            MatrixElementType type,
                                                            int templateRows,
                                                            int templateColumns,
                                                            IntPtr obj,
                                                            IntPtr sample,
                                                            out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_train_float(SvmKernelType kernelType,
                                                        MatrixElementType type,
                                                        int templateRows,
                                                        int templateColumns,
                                                        IntPtr obj,
                                                        IntPtr x,
                                                        float y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_train_double(SvmKernelType kernelType,
                                                         MatrixElementType type,
                                                         int templateRows,
                                                         int templateColumns,
                                                         IntPtr obj,
                                                         IntPtr x,
                                                         double y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_get_decision_function(SvmKernelType kernelType,
                                                                  MatrixElementType type,
                                                                  int templateRows,
                                                                  int templateColumns,
                                                                  IntPtr krls,
                                                                  out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType serialize_krls(SvmKernelType kernelType,
                                                      MatrixElementType type,
                                                      int templateRows,
                                                      int templateColumns,
                                                      IntPtr obj, 
                                                      byte[] fileName,
                                                      int fileNameLength,
                                                      out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_krls(byte[] fileName,
                                                        int fileNameLength,
                                                        SvmKernelType kernelType,
                                                        MatrixElementType matrixElementType,
                                                        int templateRows,
                                                        int templateColumns,
                                                        IntPtr obj, 
                                                        out IntPtr errorMessage);

    }

}