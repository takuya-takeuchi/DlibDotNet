using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType load_bmp(Array2DType type, IntPtr array, byte[] path, int pathLength, out IntPtr error_message);

    }

}
