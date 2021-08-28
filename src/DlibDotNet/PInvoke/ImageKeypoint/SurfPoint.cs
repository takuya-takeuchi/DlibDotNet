#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double surf_point_get_angle(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr surf_point_get_p(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr surf_point_get_des(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void surf_point_delete(IntPtr surfpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType surf_point_des_matrix_operator_left_shift(IntPtr matrix, IntPtr ofstream);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType surf_point_des_matrix_nc(IntPtr matrix, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType surf_point_des_matrix_nr(IntPtr matrix, out int ret);

    }

}
#endif
