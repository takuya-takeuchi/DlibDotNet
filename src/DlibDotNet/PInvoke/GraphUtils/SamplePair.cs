using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr sample_pair_new(uint idx1, uint idx2, double distance);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint sample_pair_get_index1(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint sample_pair_get_index2(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double sample_pair_get_distance(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void sample_pair_delete(IntPtr obj);

    }

}