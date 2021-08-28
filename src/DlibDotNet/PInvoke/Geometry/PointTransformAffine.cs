using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_affine_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_affine_new1(IntPtr matrix, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_affine_get_b(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_affine_get_m(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_transform_affine_operator(IntPtr obj, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void point_transform_affine_delete(IntPtr obj);

    }

}
