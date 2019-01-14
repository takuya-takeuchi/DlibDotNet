using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType sobel_edge_detector(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr horz, IntPtr vert);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType suppress_non_maximum_edges(Array2DType inType, IntPtr horz, IntPtr vert, Array2DType outType, IntPtr outImg);

    }

}