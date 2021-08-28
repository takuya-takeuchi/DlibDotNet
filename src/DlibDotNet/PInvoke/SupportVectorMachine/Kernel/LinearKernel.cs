#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType linear_kernel_new(MatrixElementType matrixElementType,
                                                         int templateRow,
                                                         int templateColumn,
                                                         out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void linear_kernel_delete(MatrixElementType matrixElementType,
                                                       IntPtr linerKernel, 
                                                       int templateRow,
                                                       int templateColumn);

    }

}
#endif
