using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_dataset_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_dataset_get_comment(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_set_comment(IntPtr dataset, byte[] comment, int commentLength);

        #region images

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_dataset_get_images(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_dataset_get_images_at(IntPtr dataset, int index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int image_dataset_metadata_dataset_get_images_get_size(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_get_images_clear(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_get_images_remove_at(IntPtr dataset, int index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_get_images_push_back(IntPtr dataset, IntPtr image);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_dataset_get_name(IntPtr dataset);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_set_name(IntPtr dataset, byte[] name, int nameLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_delete(IntPtr dataset);

    }

}