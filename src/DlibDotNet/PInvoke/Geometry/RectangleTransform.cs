using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_transform_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_transform_new1(IntPtr tform);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_transform_get_tform(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_transform_operator(IntPtr obj, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_transform_operator_d(IntPtr obj, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rectangle_transform_delete(IntPtr obj);

    }

}