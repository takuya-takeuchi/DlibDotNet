#if !LITE
using System;
using System.Runtime.InteropServices;
using DlibDotNet.ImageDatasetMetadata;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_box_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double image_dataset_metadata_box_get_age(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_age(IntPtr dataset, double age);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double image_dataset_metadata_box_get_angle(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_angle(IntPtr dataset, double angle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double image_dataset_metadata_box_get_detection_score(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_detection_score(IntPtr dataset, double detectionScore);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_difficult(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_difficult(IntPtr dataset, bool difficult);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern Gender image_dataset_metadata_box_get_gender(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_gender(IntPtr dataset, Gender gender);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_has_label(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_ignore(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_ignore(IntPtr dataset, bool ignore);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_box_get_label(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_label(IntPtr dataset, byte[] label, int labelLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_occluded(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_occluded(IntPtr dataset, bool occluded);

        #region part

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_get_parts_get_all(IntPtr box, IntPtr strings, IntPtr points);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_parts_get_value(IntPtr box, byte[] key, int keyLength, out IntPtr result);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_get_parts_set_value(IntPtr box, byte[] key, int keyLength, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int image_dataset_metadata_box_get_parts_get_size(IntPtr overlayRect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_parts_clear(IntPtr box);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double image_dataset_metadata_box_get_pose(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_box_get_rect(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_rect(IntPtr dataset, IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_pose(IntPtr dataset, double pose);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool image_dataset_metadata_box_get_truncated(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_set_truncated(IntPtr dataset, bool truncated);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_box_delete(IntPtr dataset);

    }

}

#endif
