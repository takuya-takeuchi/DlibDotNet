using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType sigmoid_kernel_new(MatrixElementType matrixElementType,
                                                          int templateRow, 
                                                          int templateColumn,
                                                          out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void sigmoid_kernel_delete(MatrixElementType matrixElementType,
                                                        IntPtr linerKernel,
                                                        int templateRow, 
                                                        int templateColumn);

    }

}
