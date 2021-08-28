#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_image_new(byte[] filename, int filenameLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_image_new2();

        #region boxes

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_dataset_get_boxes(IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_dataset_get_boxes_at(IntPtr image, int index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int image_dataset_metadata_dataset_get_boxes_get_size(IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_get_boxes_clear(IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_dataset_get_boxes_push_back(IntPtr image, IntPtr box);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_dataset_metadata_image_get_filename(IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_image_set_filename(IntPtr image, byte[] filename, int filenameLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void image_dataset_metadata_image_delete(IntPtr image);

    }

}
#endif
