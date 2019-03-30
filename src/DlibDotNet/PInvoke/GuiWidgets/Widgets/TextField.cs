using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr text_field_new(IntPtr drawable_window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_delete(IntPtr text_field);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_get_text(IntPtr text_field, out IntPtr str);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool text_field_has_input_focus(IntPtr text_field);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_select_all_text(IntPtr text_field);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_give_input_focus(IntPtr text_field);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_set_text(IntPtr text_field, byte[] text);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_set_width(IntPtr text_field, uint width);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void text_field_set_text_modified_handler(IntPtr text_field, IntPtr mediator);

    }

}