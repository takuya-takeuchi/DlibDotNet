using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr mmod_options_new(IntPtr boxes,
                                                     uint target_size,
                                                     uint min_target_size,
                                                     double min_detector_window_overlap_iou);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_delete(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr mmod_options_get_detector_windows(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_detector_windows(IntPtr options, IntPtr value);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double mmod_options_get_loss_per_false_alarm(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_loss_per_false_alarm(IntPtr options, double value);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double mmod_options_get_loss_per_missed_target(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_loss_per_missed_target(IntPtr options, double value);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double mmod_options_get_truth_match_iou_threshold(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_truth_match_iou_threshold(IntPtr options, double value);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr mmod_options_get_overlaps_nms(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_overlaps_nms(IntPtr options, IntPtr value);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr mmod_options_get_overlaps_ignore(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_overlaps_ignore(IntPtr options, IntPtr value);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mmod_options_get_use_bounding_box_regression(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_use_bounding_box_regression(IntPtr options, bool value);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double mmod_options_get_bbr_lambda(IntPtr options);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mmod_options_set_vbbr_lambda(IntPtr options, double value);

        #region detector_window_details

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr detector_window_details_new(uint w, uint h, byte[] label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void detector_window_details_delete(IntPtr details);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern uint detector_window_details_width(IntPtr details);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern uint detector_window_details_height(IntPtr details);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr detector_window_details_label(IntPtr details);

        #endregion

    }

}