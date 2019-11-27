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
        public static extern IntPtr rand_new2(ulong seed);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern long rand_get_integer_in_range(IntPtr rand, long begin, long end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double rand_get_random_gaussian(IntPtr rand);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double rand_get_random_double(IntPtr rand);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern byte rand_get_random_8bit_number(IntPtr rand);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint rand_get_random_32bit_number(IntPtr rand);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rand_delete(IntPtr rand);

    }

}