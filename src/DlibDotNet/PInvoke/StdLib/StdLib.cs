#if !LITE
using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region stdlib

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdlib_free(IntPtr ptr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdlib_malloc(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdlib_srand(uint seed);

        #endregion

        #region string

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr string_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr string_new2(StringBuilder c_str, int len);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void string_append(IntPtr str, StringBuilder c_str, int len);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr string_c_str(IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void string_delete(IntPtr str);

        #endregion

        #region ostringstream

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr ostringstream_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr ostringstream_str(IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void ostringstream_delete(IntPtr str);

        #endregion

        #region vector

        #region int32

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_new3([In] int[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_int32_delete(IntPtr vector);

        #endregion

        #region uint32

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_new3([In] uint[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_uint32_delete(IntPtr vector);

        #endregion

        #region long

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_long_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_long_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_long_new3([In] long[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_long_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_long_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_long_delete(IntPtr vector);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_new3([In] float[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_float_delete(IntPtr vector);

        #endregion

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_new3([In] double[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_double_delete(IntPtr vector);

        #endregion

        #region full_object_detection

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_full_object_detection_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_full_object_detection_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_full_object_detection_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_full_object_detection_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_full_object_detection_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_full_object_detection_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_full_object_detection_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region rect_detection

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rect_detection_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rect_detection_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rect_detection_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rect_detection_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rect_detection_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_rect_detection_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_rect_detection_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region image_window_overlay_line

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_window_overlay_line_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_window_overlay_line_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_window_overlay_line_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_window_overlay_line_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_window_overlay_line_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_window_overlay_line_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_window_overlay_line_copy(IntPtr vector, IntPtr[] dst);

#endif

        #endregion

        #region image_dataset_metadata_image

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_image_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_image_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_image_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_image_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_image_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_dataset_metadata_image_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_dataset_metadata_image_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region image_window_overlay_line

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_box_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_box_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_box_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_box_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_dataset_metadata_box_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_dataset_metadata_box_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_dataset_metadata_box_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region matrix

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_matrix_new1(MatrixElementType matrixElementType, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_matrix_new2(MatrixElementType matrixElementType, IntPtr size, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_matrix_new3([In] MatrixElementType matrixElementType, [In] IntPtr[] data, IntPtr dataLength, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_matrix_getSize(MatrixElementType matrixElementType, IntPtr vector, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_matrix_getPointer(MatrixElementType matrixElementType, IntPtr vector, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_matrix_delete(MatrixElementType matrixElementType, IntPtr vector, int templateRows, int templateColumns);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_matrix_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region mmod_rect

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_rect_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_rect_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_rect_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_rect_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_rect_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_mmod_rect_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_mmod_rect_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region surf_point

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_surf_point_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_surf_point_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_surf_point_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_surf_point_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_surf_point_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_surf_point_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_surf_point_copy(IntPtr vector, IntPtr[] dst);

#endif

        #endregion

        #region sample_pair

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_sample_pair_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_sample_pair_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_sample_pair_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_sample_pair_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_sample_pair_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_sample_pair_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_sample_pair_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region chip_details

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_chip_details_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_chip_details_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_chip_details_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_chip_details_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_chip_details_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_chip_details_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_chip_details_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region std::string

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_string_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_string_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_string_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_string_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_string_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_string_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_string_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region perspective_window_overlay_dot

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_perspective_window_overlay_dot_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_perspective_window_overlay_dot_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_perspective_window_overlay_dot_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_perspective_window_overlay_dot_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_perspective_window_overlay_dot_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_perspective_window_overlay_dot_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_perspective_window_overlay_dot_copy(IntPtr vector, IntPtr[] dst);

#endif

        #endregion

        #region rectangle

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rectangle_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rectangle_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rectangle_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rectangle_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_rectangle_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_rectangle_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_rectangle_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region point

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_point_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_point_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_point_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_point_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_point_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_point_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_point_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region dpoint

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_dpoint_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_dpoint_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_dpoint_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_dpoint_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_dpoint_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_dpoint_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_dpoint_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region vectordouble

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_vector_double_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_vector_double_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_vector_double_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_vector_double_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_vector_double_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_vector_double_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_vector_double_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region stdvector_double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_double_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_double_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_double_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_double_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_double_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_double_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_double_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region vector_mmod_rect

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_mmod_rect_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_mmod_rect_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_mmod_rect_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_mmod_rect_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_mmod_rect_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_mmod_rect_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_mmod_rect_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region vector_full_object_detection

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_full_object_detection_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_full_object_detection_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_full_object_detection_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_full_object_detection_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_full_object_detection_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_full_object_detection_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_full_object_detection_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region vector_rectangle

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_rectangle_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_rectangle_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_rectangle_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_rectangle_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_stdvector_rectangle_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_rectangle_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_stdvector_rectangle_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region mmod_options::detector_window_details

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_options_detector_window_details_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_options_detector_window_details_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_options_detector_window_details_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_options_detector_window_details_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_mmod_options_detector_window_details_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_mmod_options_detector_window_details_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_mmod_options_detector_window_details_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region image_display::overlay_rect

#if !DLIB_NO_GUI_SUPPORT

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_display_overlay_rect_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_display_overlay_rect_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_display_overlay_rect_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_display_overlay_rect_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_image_display_overlay_rect_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_display_overlay_rect_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_image_display_overlay_rect_copy(IntPtr vector, IntPtr[] dst);

#endif

        #endregion

        #endregion

        #region pair

        #region point_point

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdpair_point_point_new(IntPtr first, IntPtr second);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdpair_point_point_get_first(IntPtr pair);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdpair_point_point_set_first(IntPtr pair, IntPtr first);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdpair_point_point_get_second(IntPtr pair);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdpair_point_point_set_second(IntPtr pair, IntPtr second);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdpair_point_point_delete(IntPtr pair);

        #endregion

        #endregion

    }

}
#endif
