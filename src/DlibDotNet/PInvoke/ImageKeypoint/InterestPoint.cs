using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr interest_point_get_center(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double interest_point_get_laplacian(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double interest_point_get_scale(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double interest_point_get_score(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void interest_point_delete(IntPtr surfpoint);

    }

}