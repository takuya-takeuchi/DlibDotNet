using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr menu_item_text_new(byte[] str, IntPtr drawable_window, IntPtr event_handler, char hk);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr menu_item_text_new(byte[] str, IntPtr mediator, char hk);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void menu_item_text_delete(IntPtr ptr);

    }

}