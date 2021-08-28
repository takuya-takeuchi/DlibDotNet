using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_new1(double left, double top, double right, double bottom);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_new2(IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_new3(IntPtr p1, IntPtr p2);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_new4(IntPtr drect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_operator(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void drectangle_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double drectangle_bottom(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double drectangle_left(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double drectangle_right(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double drectangle_top(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool drectangle_is_empty(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_bl_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_br_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_tl_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_tr_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double drectangle_area(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double drectangle_height(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double drectangle_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_intersect(IntPtr rect, IntPtr target);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool drectangle_contains(IntPtr rect, IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool drectangle_contains2(IntPtr rect, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_center(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_dcenter(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_centered_rect(IntPtr dpoint, double width, double height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_centered_rect1(IntPtr drect, double width, double height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_set_aspect_ratio(IntPtr rect, double ratio);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_translate_rect(IntPtr rect, IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_translate_rect_d(IntPtr rect, IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_operator_add(IntPtr drect, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_operator_mul(IntPtr drect, double rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr drectangle_operator_div(IntPtr drect, double rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool drectangle_operator_equal(IntPtr drect, IntPtr rhs);

    }

}
