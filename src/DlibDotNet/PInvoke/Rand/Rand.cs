using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rand_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double rand_get_random_gaussian(IntPtr rand);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rand_delete(IntPtr rand);

    }

}