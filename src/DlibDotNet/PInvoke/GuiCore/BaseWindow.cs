using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_close_window(IntPtr window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_get_size(IntPtr window, out uint width, out uint height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_get_display_size(IntPtr window, out uint width, out uint height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_set_pos(IntPtr window, int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_set_size(IntPtr window, int width, int height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_set_title(IntPtr window, byte[] title);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_wait_until_closed(IntPtr window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void base_window_show(IntPtr window);

    }

}