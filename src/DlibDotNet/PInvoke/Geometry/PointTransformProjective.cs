using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_projective_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_projective_new1(IntPtr matrix);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_projective_get_m(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_projective_operator(IntPtr obj, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void point_transform_projective_delete(IntPtr obj);

    }

}
