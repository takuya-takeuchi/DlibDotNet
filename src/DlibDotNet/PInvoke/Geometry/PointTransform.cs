#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_new1(double angle, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_get_b(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_get_m(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_operator(IntPtr obj, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void point_transform_delete(IntPtr obj);

    }

}
#endif
