using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int drawable_get_bottom(IntPtr drawable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int drawable_get_top(IntPtr drawable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int drawable_get_left(IntPtr drawable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int drawable_get_right(IntPtr drawable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint drawable_get_width(IntPtr drawable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint drawable_get_height(IntPtr drawable);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void drawable_set_pos(IntPtr region, int x, int y);

    }

}