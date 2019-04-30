using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType save_dng(Array2DType type, IntPtr array, byte[] path);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType save_dng_matrix(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumn, byte[] path);

    }

}