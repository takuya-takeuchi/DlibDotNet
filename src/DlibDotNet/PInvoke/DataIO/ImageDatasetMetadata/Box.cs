using System;
using System.Runtime.InteropServices;
using DlibDotNet.ImageDatasetMetadata;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_dataset_metadata_box_new();

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double image_dataset_metadata_box_get_age(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_age(IntPtr dataset, double age);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double image_dataset_metadata_box_get_angle(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_angle(IntPtr dataset, double angle);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double image_dataset_metadata_box_get_detection_score(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_detection_score(IntPtr dataset, double detectionScore);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_difficult(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_difficult(IntPtr dataset, bool difficult);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern Gender image_dataset_metadata_box_get_gender(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_gender(IntPtr dataset, Gender gender);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_has_label(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_ignore(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_ignore(IntPtr dataset, bool ignore);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_dataset_metadata_box_get_label(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_label(IntPtr dataset, byte[] label);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_occluded(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_occluded(IntPtr dataset, bool occluded);

        #region part

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_get_parts_get_all(IntPtr box, IntPtr strings, IntPtr points);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_parts_get_value(IntPtr box, byte[] key, out IntPtr result);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_get_parts_set_value(IntPtr box, byte[] key, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern int image_dataset_metadata_box_get_parts_get_size(IntPtr overlayRect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_parts_clear(IntPtr box);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern double image_dataset_metadata_box_get_pose(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr image_dataset_metadata_box_get_rect(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_rect(IntPtr dataset, IntPtr rect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_pose(IntPtr dataset, double pose);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_truncated(IntPtr dataset);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_set_truncated(IntPtr dataset, bool truncated);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void image_dataset_metadata_box_delete(IntPtr dataset);

    }

}
