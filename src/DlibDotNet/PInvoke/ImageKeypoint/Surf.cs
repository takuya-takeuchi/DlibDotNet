#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType get_surf_points(Array2DType type, IntPtr img, long max_points, double detection_threshold, IntPtr points);

    }

}
#endif
