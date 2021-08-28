#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType equalize_histogram_array2d(Array2DType img_type, IntPtr img);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType equalize_histogram_array2d_2(Array2DType in_type,
                                                                    IntPtr in_img,
                                                                    Array2DType out_type, 
                                                                    IntPtr out_img);

    }

}
#endif
