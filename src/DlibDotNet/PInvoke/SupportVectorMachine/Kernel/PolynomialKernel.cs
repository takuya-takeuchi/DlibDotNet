using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr polynomial_kernel_new(MatrixElementType matrixElementType, int templateRow, int templateColumn);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void polynomial_kernel_delete(MatrixElementType matrixElementType, IntPtr linerKernel, int templateRow, int templateColumn);

    }

}