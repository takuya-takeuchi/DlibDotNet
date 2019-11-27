using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mmod_rect_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mmod_rect_new2(IntPtr rectangle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mmod_rect_get_ignore(IntPtr mmod, out bool ignore);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_rect_set_ignore(IntPtr mmod, bool ignore);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mmod_rect_get_detection_confidence(IntPtr mmod, out double confidence);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_rect_set_detection_confidence(IntPtr mmod, double confidence);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mmod_rect_get_rect(IntPtr mmod, out IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_rect_set_rect(IntPtr mmod, IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mmod_rect_get_label(IntPtr mmod, out IntPtr label);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_rect_set_label(IntPtr mmod, IntPtr label);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_rect_delete(IntPtr mmod);

    }

}