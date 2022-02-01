using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr text_field_new(IntPtr drawable_window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void text_field_delete(IntPtr text_field);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void text_field_get_text(IntPtr text_field, out IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool text_field_has_input_focus(IntPtr text_field);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void text_field_select_all_text(IntPtr text_field);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void text_field_give_input_focus(IntPtr text_field);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void text_field_set_text(IntPtr text_field, byte[] text, int textLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void text_field_set_width(IntPtr text_field, uint width);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void text_field_set_text_modified_handler(IntPtr text_field, IntPtr mediator);

#endif

    }

}