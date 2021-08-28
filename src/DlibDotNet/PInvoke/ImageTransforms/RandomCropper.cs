#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr random_cropper_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_background_crops_fraction(IntPtr cropper, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_chip_dims(IntPtr cropper, out IntPtr chip);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_max_object_size(IntPtr cropper, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_max_rotation_degrees(IntPtr cropper, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_min_object_length_long_dim(IntPtr cropper, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_min_object_length_short_dim(IntPtr cropper, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_randomly_flip(IntPtr cropper, out bool ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool random_cropper_get_translate_amount(IntPtr cropper, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_chip_dims(IntPtr cropper, uint rows, uint cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_max_object_size(IntPtr cropper, double value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_min_object_size(IntPtr cropper, int longDim, int shortDim);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_randomly_flip(IntPtr cropper, bool value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_translate_amount(IntPtr cropper, double value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_background_crops_fraction(IntPtr cropper, double value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_max_rotation_degrees(IntPtr cropper, double value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_set_seed(IntPtr cropper, IntPtr value);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType random_cropper_operator(IntPtr cropper,
                                                               uint numCrops,
                                                               MatrixElementType type,
                                                               IntPtr images,
                                                               IntPtr rects,
                                                               IntPtr crops,
                                                               IntPtr cropRects);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType random_cropper_operator2(IntPtr cropper,
                                                                MatrixElementType type,
                                                                IntPtr image,
                                                                IntPtr rects,
                                                                out IntPtr crop,
                                                                IntPtr cropRects);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_operator_left_shift(IntPtr obj, IntPtr ofstream);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void random_cropper_delete(IntPtr point);

    }

}
#endif
