#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_new1(int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void point_delete(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void point_operator_left_shift(IntPtr point, IntPtr ofstream);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double point_length(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double point_length_squared(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int point_x(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int point_y(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_operator_add(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_operator_sub(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_operator_mul_point_int32(IntPtr point, int rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_operator_mul_int32_point(int lhs, IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_operator_mul_double_point(double lhs, IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr point_operator_div(IntPtr point, int rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool point_operator_equal(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rotate_point(IntPtr center, IntPtr p, double rhs);

    }

}
#endif
