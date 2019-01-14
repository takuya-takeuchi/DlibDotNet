using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr random_cropper_new();

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_background_crops_fraction(IntPtr cropper, out double ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_chip_dims(IntPtr cropper, out IntPtr chip);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_max_object_size(IntPtr cropper, out double ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_max_rotation_degrees(IntPtr cropper, out double ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_min_object_length_long_dim(IntPtr cropper, out long ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_min_object_length_short_dim(IntPtr cropper, out long ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_randomly_flip(IntPtr cropper, out bool ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_translate_amount(IntPtr cropper, out double ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_set_chip_dims(IntPtr cropper, uint rows, uint cols);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_set_max_object_size(IntPtr cropper, double value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_set_min_object_size(IntPtr cropper, double longDim, double shortDim);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_set_randomly_flip(IntPtr cropper, bool value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_set_translate_amount(IntPtr cropper, double value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_set_background_crops_fraction(IntPtr cropper, double value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_set_max_rotation_degrees(IntPtr cropper, double value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType random_cropper_operator(IntPtr cropper,
                                                               uint numCrops,
                                                               MatrixElementType type,
                                                               IntPtr images,
                                                               IntPtr rects,
                                                               IntPtr crops,
                                                               IntPtr cropRects);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void random_cropper_delete(IntPtr point);

    }

}