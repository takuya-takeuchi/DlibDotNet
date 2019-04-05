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
        public static extern ErrorType mat_matrix(Array2DType srcType, IntPtr src, int templateRows, int templateColumns, MatrixElementType dstType, out IntPtr ret);

    }

}