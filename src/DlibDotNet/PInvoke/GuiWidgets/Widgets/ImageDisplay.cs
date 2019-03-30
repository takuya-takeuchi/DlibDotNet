using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_display_new(IntPtr drawable_window);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_delete(IntPtr display);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_set_image_clicked_handler(IntPtr display, IntPtr mediator);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_set_overlay_rects_changed_handler(IntPtr display, IntPtr mediator);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_set_overlay_rect_selected_handler(IntPtr display, IntPtr mediator);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_set_default_overlay_rect_color(IntPtr display, RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_set_default_overlay_rect_label(IntPtr display, byte[] label);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_clear_overlay(IntPtr display);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType image_display_set_image(IntPtr detector,
                                                               Array2DType imgType,
                                                               IntPtr img);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType image_display_add_overlay(IntPtr display,
                                                                               IntPtr vectorOfRect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_display_get_overlay_rects(IntPtr display);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_display_get_default_overlay_rect_label(IntPtr display);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_add_labelable_part_name(IntPtr display, byte[] name);

        #region overlay_rect

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_display_overlay_rect_new();

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_display_overlay_rect_delete(IntPtr overlayRect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_display_overlay_rect_get_crossed_out(IntPtr overlayRect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_set_crossed_out(IntPtr overlayRect, bool ignore);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_display_overlay_rect_get_label(IntPtr overlayRect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_set_label(IntPtr overlayRect, byte[] label);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_display_overlay_rect_get_rect(IntPtr overlayRect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_set_rect(IntPtr overlayRect, IntPtr rect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_get_color(IntPtr overlayRect, ref RgbAlphaPixel color);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_set_color(IntPtr overlayRect, RgbAlphaPixel color);

        #region parts
        
        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_get_parts_get_all(IntPtr box, IntPtr strings, IntPtr points);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_display_overlay_rect_get_parts_get_value(IntPtr overlayRect, byte[] key, out IntPtr result);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_get_parts_set_value(IntPtr overlayRect, byte[] key, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern int image_display_overlay_rect_get_parts_get_size(IntPtr overlayRect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_display_overlay_rect_get_parts_clear(IntPtr overlayRect);

        #endregion

        #endregion

    }

}