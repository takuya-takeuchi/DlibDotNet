using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_new(KernelType kernelType,
                                                MatrixElementType type,
                                                int templateRows,
                                                int templateColumns,
                                                IntPtr kernel,
                                                double tolerance,
                                                uint maxDictionarySize,
                                                out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void krls_delete(KernelType kernelType,
                                              MatrixElementType type,
                                              int templateRows,
                                              int templateColumns,
                                              IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_dictionary_size(KernelType kernelType,
                                                            MatrixElementType type,
                                                            int templateRows,
                                                            int templateColumns,
                                                            IntPtr obj,
                                                            out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_get_kernel(KernelType kernelType,
                                                       MatrixElementType type,
                                                       int templateRows,
                                                       int templateColumns,
                                                       IntPtr obj,
                                                       out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_operator_float(KernelType kernelType,
                                                           MatrixElementType type,
                                                           int templateRows,
                                                           int templateColumns,
                                                           IntPtr obj,
                                                           IntPtr sample,
                                                           out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_operator_double(KernelType kernelType,
                                                            MatrixElementType type,
                                                            int templateRows,
                                                            int templateColumns,
                                                            IntPtr obj,
                                                            IntPtr sample,
                                                            out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_train_float(KernelType kernelType,
                                                        MatrixElementType type,
                                                        int templateRows,
                                                        int templateColumns,
                                                        IntPtr obj,
                                                        IntPtr x,
                                                        float y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_train_double(KernelType kernelType,
                                                         MatrixElementType type,
                                                         int templateRows,
                                                         int templateColumns,
                                                         IntPtr obj,
                                                         IntPtr x,
                                                         double y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType krls_get_decision_function(KernelType kernelType,
                                                                  MatrixElementType type,
                                                                  int templateRows,
                                                                  int templateColumns,
                                                                  IntPtr krls,
                                                                  out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType serialize_krls(KernelType kernelType,
                                                      MatrixElementType type,
                                                      int templateRows,
                                                      int templateColumns,
                                                      IntPtr obj, 
                                                      byte[] filName,
                                                      out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_krls(byte[] filName,
                                                        KernelType kernelType,
                                                        MatrixElementType matrixElementType,
                                                        int templateRows,
                                                        int templateColumns,
                                                        IntPtr obj, 
                                                        out IntPtr errorMessage);

    }

}