using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_new1(int left, int top, int right, int bottom);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_new2(uint width, uint height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_new3(IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_new4(IntPtr p1, IntPtr p2);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rectangle_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int rectangle_bottom(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int rectangle_left(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int rectangle_right(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int rectangle_top(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rectangle_set_bottom(IntPtr rect, int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rectangle_set_left(IntPtr rect, int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rectangle_set_right(IntPtr rect, int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void rectangle_set_top(IntPtr rect, int value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool rectangle_is_empty(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_bl_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_br_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_tl_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_tr_corner(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint rectangle_area(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint rectangle_height(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint rectangle_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool rectangle_contains(IntPtr rect, IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool rectangle_contains1(IntPtr rect, int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_centered_rect(int x, int y, uint width, uint height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_centered_rect1(IntPtr point, uint width, uint height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_centered_rect2(IntPtr rect, uint width, uint height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_intersect(IntPtr rect, IntPtr target);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_move_rect(IntPtr rect, IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_move_rect2(IntPtr rect, int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_center(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_dcenter(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_set_aspect_ratio(IntPtr rect, double ratio);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_translate_rect(IntPtr rect, IntPtr p);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_translate_rect_xy(IntPtr rect, int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_operator_add(IntPtr rect, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rectangle_operator_add_point(IntPtr rect, IntPtr rhs);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool rectangle_operator_equal(IntPtr drect, IntPtr rhs);

    }

}