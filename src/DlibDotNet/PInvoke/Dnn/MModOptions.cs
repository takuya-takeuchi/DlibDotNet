#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mmod_options_new(IntPtr boxes,
                                                     uint target_size,
                                                     uint min_target_size,
                                                     double min_detector_window_overlap_iou);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_delete(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mmod_options_get_detector_windows(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_detector_windows(IntPtr options, IntPtr value);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern double mmod_options_get_loss_per_false_alarm(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_loss_per_false_alarm(IntPtr options, double value);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern double mmod_options_get_loss_per_missed_target(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_loss_per_missed_target(IntPtr options, double value);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern double mmod_options_get_truth_match_iou_threshold(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_truth_match_iou_threshold(IntPtr options, double value);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mmod_options_get_overlaps_nms(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_overlaps_nms(IntPtr options, IntPtr value);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr mmod_options_get_overlaps_ignore(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_overlaps_ignore(IntPtr options, IntPtr value);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mmod_options_get_use_bounding_box_regression(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_use_bounding_box_regression(IntPtr options, bool value);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern double mmod_options_get_bbr_lambda(IntPtr options);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void mmod_options_set_vbbr_lambda(IntPtr options, double value);

        #region detector_window_details

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr detector_window_details_new(uint w, uint h, byte[] label, int labelLength);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void detector_window_details_delete(IntPtr details);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern uint detector_window_details_width(IntPtr details);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern uint detector_window_details_height(IntPtr details);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr detector_window_details_label(IntPtr details);

        #endregion

    }

}
#endif
