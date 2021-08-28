using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType load_image(Array2DType type, IntPtr array, byte[] path, int pathLength, out IntPtr error_message);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType load_image_matrix(MatrixElementType type,
                                                         byte[] path,
                                                         int pathLength, 
                                                         out IntPtr matrix, 
                                                         out IntPtr error_message);

    }

}
