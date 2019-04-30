using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_new1(double x, double y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void dpoint_delete(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double dpoint_length(IntPtr dpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double dpoint_length_squared(IntPtr dpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double dpoint_x(IntPtr dpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double dpoint_y(IntPtr dpoint);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_add(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_add2(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_sub(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_sub2(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_mul_dpoint_double(IntPtr point, double rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_mul_int32_dpoint(int lhs, IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_mul_double_dpoint(double rhs, IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr dpoint_operator_div(IntPtr point, double rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool dpoint_operator_equal(IntPtr point, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rotate_dpoint(IntPtr center, IntPtr p, double rhs);

    }

}