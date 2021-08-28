#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_rotator_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_rotator_new1(double angle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_rotator_get_m(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_rotator_operator(IntPtr obj, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void point_rotator_delete(IntPtr obj);

    }

}
#endif
