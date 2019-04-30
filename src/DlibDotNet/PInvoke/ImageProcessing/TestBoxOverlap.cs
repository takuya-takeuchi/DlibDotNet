using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr test_box_overlap_new(double iou_thresh, double percent_covered_thresh);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void test_box_overlap_delete(IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double test_box_overlap_get_iou_thresh(IntPtr overlap);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double test_box_overlap_get_percent_covered_thresh(IntPtr overlap);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool test_box_overlap_operator(IntPtr overlap, IntPtr a, IntPtr b);

    }

}