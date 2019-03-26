using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void scrollable_region_set_pos(IntPtr region, int x, int y);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void scrollable_region_set_size(IntPtr region, uint width, uint height);

        #region menu_item_text

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr menu_item_text_new(byte[] str, IntPtr drawable_window, IntPtr event_handler, char hk);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void menu_item_text_delete(IntPtr ptr);

        #endregion

        #region menu_item_text

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr menu_item_separator_new();

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void menu_item_separator_delete(IntPtr ptr);

        #endregion

    }

}