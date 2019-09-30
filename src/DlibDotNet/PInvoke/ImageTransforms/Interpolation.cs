using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType add_image_left_right_flips_rectangle(MatrixElementType elementType, IntPtr images, IntPtr objects);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType flip_image_left_right(Array2DType type, IntPtr img);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType flip_image_left_right2(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType flip_image_up_down(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr flip_rect_left_right(IntPtr rect, IntPtr window);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType pyramid_up(Array2DType type, IntPtr img);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType pyramid_up_matrix(MatrixElementType type, IntPtr img);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType pyramid_up_matrix2(MatrixElementType type, IntPtr img, IntPtr pyramid_down, uint pyramid_rate, out IntPtr matrix);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType pyramid_up_pyramid_matrix(PyramidType pyramid_type,
                                                                 uint pyramid_rate,
                                                                 MatrixElementType elementType,
                                                                 IntPtr image);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType resize_image(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType resize_image2(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, InterpolationTypes interpolationTypes);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType resize_image3(Array2DType inType, IntPtr inImg, double scaleSize);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType resize_image_matrix(MatrixElementType type, IntPtr matrix, uint templateRows, uint templateColumns, IntPtr out_matrix, InterpolationTypes interpolationTypes);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType resize_image_matrix_scale(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumns, double scaleSize);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType rotate_image(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, double angle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType rotate_image2(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, double angle, InterpolationTypes interpolationTypes);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType transform_image(Array2DType inType, IntPtr inImg, Array2DType outType, IntPtr outImg, PointMappingTypes pointMappingTypes, IntPtr mappingObj, InterpolationTypes interpolationTypes);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType get_face_chip_details(IntPtr dets, uint size, double padding, IntPtr vectoChips);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType get_face_chip_details2(IntPtr det, uint size, double padding, out IntPtr chips);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_chip(Array2DType img_type, IntPtr in_img, IntPtr chip_location, Array2DType array_type, IntPtr out_chip);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_chip2(Array2DType img_type, IntPtr in_img, IntPtr chip_location, Array2DType array_type, InterpolationTypes type, IntPtr out_chip);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_chips(Array2DType img_type, IntPtr in_img, IntPtr chip_locations, Array2DType array_type, IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_chips_matrix(MatrixElementType img_type, IntPtr in_img, IntPtr chip_locations, MatrixElementType array_type, IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_chip_matrix(MatrixElementType img_type, IntPtr in_img, IntPtr chip_location, MatrixElementType array_type, IntPtr out_chip);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_chip_matrix2(MatrixElementType img_type, IntPtr in_img, IntPtr chip_location, MatrixElementType array_type, InterpolationTypes type, IntPtr out_chip);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType jitter_image(MatrixElementType in_type, IntPtr in_img, IntPtr rand, out IntPtr out_img);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType upsample_image_dataset_pyramid_down_rect(uint pyramid_rate,
                                                                                MatrixElementType elementType,
                                                                                IntPtr images,
                                                                                IntPtr objects,
                                                                                uint maxImageSize);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType upsample_image_dataset_pyramid_down_mmod_rect(uint pyramid_rate,
                                                                                     MatrixElementType elementType,
                                                                                     IntPtr images,
                                                                                     IntPtr objects,
                                                                                     uint maxImageSize);

        #region extract_image_4points

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_4points(Array2DType type,
                                                             IntPtr array,
                                                             IntPtr[] points,
                                                             int width,
                                                             int height,
                                                             out IntPtr output);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_image_4points_matrix(MatrixElementType element_type,
                                                                    IntPtr matrix,
                                                                    int templateRows,
                                                                    int templateColumns,
                                                                    IntPtr[] points,
                                                                    int width,
                                                                    int height,
                                                                    out IntPtr output);

        #endregion

    }

}