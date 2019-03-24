using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType gaussian_blur(Array2DType inType, IntPtr inImg, Array2DType outtype, IntPtr outImg, double sigma, int maxSize);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType sum_filter(Array2DType inType, IntPtr inImg, Array2DType outtype, IntPtr outImg, IntPtr rect);

    }

}