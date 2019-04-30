using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void decision_function_delete(SvmKernelType kernelType,
                                                           MatrixElementType type,
                                                           int templateRows,
                                                           int templateColumns,
                                                           IntPtr function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType serialize_decision_function(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   int templateRows,
                                                                   int templateColumns,
                                                                   IntPtr function, 
                                                                   byte[] filName,
                                                                   out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_decision_function(byte[] filName,
                                                                     SvmKernelType kernelType,
                                                                     MatrixElementType matrixElementType,
                                                                     int templateRows,
                                                                     int templateColumns,
                                                                     out IntPtr function, 
                                                                     out IntPtr errorMessage);

    }

}