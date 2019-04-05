using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint popup_menu_add_menu_item_menu_item_text(IntPtr popup_menu, IntPtr new_item);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint popup_menu_add_menu_item_menu_item_separator(IntPtr popup_menu, IntPtr new_item);

    }

}