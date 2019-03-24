using System;
using System.Runtime.InteropServices;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        internal enum Array2DType
        {

            UInt8 = 0,

            UInt16,

            UInt32,

            Int8,

            Int16,

            Int32,

            Float,

            Double,

            RgbPixel,

            RgbAlphaPixel,

            HsiPixel,

            Matrix

        }

        internal enum ElementType
        {

            OpHeatmap,

            OpJet,

            OpArray2DToMat,

            OpTrans,

            OpStdVectToMat,

            OpJoinRows

        }

        internal enum MatrixElementType
        {

            UInt8 = 0,

            UInt16,

            UInt32,

            UInt64,

            Int8,

            Int16,

            Int32,

            Int64,

            Float,

            Double,

            RgbPixel,

            RgbAlphaPixel,

            HsiPixel

        }

        internal enum VectorElementType
        {

            UInt8 = 0,

            UInt16,

            UInt32,

            Int8,

            Int16,

            Int32,

            Float,

            Double

        }

        internal enum NumericType
        {

            UInt8 = 0,

            UInt16,

            UInt32,

            UInt64,

            Int8,

            Int16,

            Int32,

            Int64,

            Float,

            Double

        }

        internal enum InterpolationTypes
        {

            NearestNeighbor = 0,

            Bilinear,

            Quadratic

        }

        internal enum PointMappingTypes
        {

            Rotator = 0,

            Transform,

            TransformAffine,

            TransformProjective

        }

        internal enum MlpKernelType
        {

            Kernel1 = 0

        }

        internal enum RunningStatsType
        {

            Float = 0,

            Double

        }

        internal enum PyramidType
        {

            Down = 0

        }

        internal enum FHogFeatureExtractorType
        {

            Default = 0

        }

        internal enum ImagePixelType
        {

            Bgr = 0,

            Bgra,

            Rgb,

            Rgba

        }

        internal enum ErrorType
        {

            OK = 0x00000000,

            #region Array2D

            Array2DError = 0x7B000000,

            Array2DTypeTypeNotSupport = -(Array2DError | 0x00000001),

            #endregion

            ElementTypeNotSupport = -4,

            InputElementTypeNotSupport = -5,

            #region Matrix

            MatrixError = 0x7C000000,

            MatrixElementTypeNotSupport = -(MatrixError | 0x00000001),

            MatrixElementTemplateSizeNotSupport = -(MatrixError | 0x00000002),

            #endregion

            //InputOutputArrayNotSameSize = -8,

            //InputOutputMatrixNotSameSize = -9

            #region Mlp

            MlpError = 0x7A000000,

            MlpKernelNotSupport = -(MlpError | 0x00000001),

            #endregion

            #region RunningStats

            RunningStatsError = 0x78000000,

            RunningStatsTypeNotSupport = -(RunningStatsError | 0x00000001),

            #endregion

            #region Vector

            VectorError = 0x79000000,

            VectorTypeNotSupport = -(VectorError | 0x00000001),

            #endregion

            #region FHog

            FHogError = 0x7D000000,

            FHogNotSupportExtractor = -(FHogError | 0x00000001),

            #endregion

            #region Pyramid

            PyramidError = 0x7E000000,

            PyramidNotSupportRate = -(PyramidError | 0x00000001),

            PyramidNotSupportType = -(PyramidError | 0x00000002),

            #endregion

            #region Dnn

            DnnError = 0x7F000000,

            DnnNotSupportNetworkType = -(DnnError | 0x00000001),

            DnnPropagateException    = -(DnnError | 0x00000002),

            #endregion

            #region Dnn

            CudaError = 0x77000000,

            CudaOutOfMemory = -(CudaError | 0x00000001)

            #endregion

        }

        #region array

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array_new(Array2DType type);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array_new1(Array2DType type, uint newSize);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array_delete_pixel(Array2DType type, IntPtr array);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_size(Array2DType type, IntPtr array, out uint size);

        #region getitem

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_uint8(Array2DType type, IntPtr array, uint index, out uint8_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_uint16(Array2DType type, IntPtr array, uint index, out uint16_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_uint32(Array2DType type, IntPtr array, uint index, out uint32_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_int8(Array2DType type, IntPtr array, uint index, out int8_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_int16(Array2DType type, IntPtr array, uint index, out int16_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_int32(Array2DType type, IntPtr array, uint index, out int32_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_float(Array2DType type, IntPtr array, uint index, out float item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_double(Array2DType type, IntPtr array, uint index, out double item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_rgb_pixel(Array2DType type, IntPtr array, uint index, out RgbPixel item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_hsi_pixel(Array2DType type, IntPtr array, uint index, out HsiPixel item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_getitem_rgb_alpha_pixel(Array2DType type, IntPtr array, uint index, out RgbAlphaPixel item);

        #endregion

        #region pushback

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_uint8(Array2DType type, IntPtr array, uint8_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_uint16(Array2DType type, IntPtr array, uint16_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_uint32(Array2DType type, IntPtr array, uint32_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_int8(Array2DType type, IntPtr array, int8_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_int16(Array2DType type, IntPtr array, int16_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_int32(Array2DType type, IntPtr array, int32_t item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_float(Array2DType type, IntPtr array, float item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_double(Array2DType type, IntPtr array, double item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_rgb_pixel(Array2DType type, IntPtr array, RgbPixel item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_hsi_pixel(Array2DType type, IntPtr array, HsiPixel item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_pixel_pushback_rgb_alpha_pixel(Array2DType type, IntPtr array, RgbAlphaPixel item);

        #endregion

        #region array2d

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array_array2d_new(Array2DType type);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array_array2d_new1(Array2DType type, uint newSize);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array_delete_array2d(Array2DType type, IntPtr array);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_array2d_size(Array2DType type, IntPtr array, out uint size);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_array2d_getitem(Array2DType type, IntPtr array, uint index, out IntPtr item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_array2d_pushback(Array2DType type, IntPtr array, IntPtr item);

        #endregion

        #region matrix

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array_matrix_new(MatrixElementType type);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array_matrix_new1(MatrixElementType type, uint newSize);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array_delete_matrix(MatrixElementType type, IntPtr array);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_matrix_size(MatrixElementType type, IntPtr array, out uint size);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_matrix_getitem(MatrixElementType type, IntPtr array, uint index, out IntPtr item);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array_matrix_pushback(MatrixElementType type, IntPtr array, IntPtr item);

        #endregion

        #endregion

        #region array2d

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array2d_new(Array2DType type);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array2d_new1(Array2DType type, int rows, int cols);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_delete(Array2DType type, IntPtr array);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_nc(Array2DType type, IntPtr array, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_nr(Array2DType type, IntPtr array, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_size(Array2DType type, IntPtr array, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType rectangle_get_rect(Array2DType type, IntPtr array, out IntPtr rect);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType rectangle_get_rect_matrix(MatrixElementType type, IntPtr img, int templateRows, int templateColumns, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array2d_row(Array2DType type, IntPtr array, int row, out IntPtr ret);

        #region array2d_get_row_column

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_uint8_t(IntPtr row, int column, out byte value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_uint16_t(IntPtr row, int column, out ushort value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_uint32_t(IntPtr row, int column, out uint value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_int8_t(IntPtr row, int column, out sbyte value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_int16_t(IntPtr row, int column, out short value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_int32_t(IntPtr row, int column, out int value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_double(IntPtr row, int column, out double value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_float(IntPtr row, int column, out float value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_rgb_pixel(IntPtr row, int column, out RgbPixel value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_rgb_alpha_pixel(IntPtr row, int column, out RgbAlphaPixel value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_get_row_column_hsi_pixel(IntPtr row, int column, out HsiPixel value);

        #endregion

        #region array2d_set_row_column

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_uint8_t(IntPtr row, int column, byte value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_uint16_t(IntPtr row, int column, ushort value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_uint32_t(IntPtr row, int column, uint value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_int8_t(IntPtr row, int column, sbyte value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_int16_t(IntPtr row, int column, short value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_int32_t(IntPtr row, int column, int value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_double(IntPtr row, int column, double value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_float(IntPtr row, int column, float value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_rgb_pixel(IntPtr row, int column, RgbPixel value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_rgb_alpha_pixel(IntPtr row, int column, RgbAlphaPixel value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_set_row_column_hsi_pixel(IntPtr row, int column, HsiPixel value);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_row_delete(Array2DType type, IntPtr row);

        #region array2d_matrix

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array2d_matrix_new(MatrixElementType type, int templateRows, int templateColumns);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr array2d_matrix_new1(MatrixElementType type, int rows, int cols, int templateRows, int templateColumns);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_delete(MatrixElementType type, IntPtr array, int templateRows, int templateColumns);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_matrix_nc(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_matrix_nr(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool array2d_matrix_size(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array2d_matrix_get_rect(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, out IntPtr rect);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType array2d_matrix_row(MatrixElementType type, IntPtr array, int templateRows, int templateColumns, int row, out IntPtr ret);

        #region array2d_matrix_get_row_column

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_uint8_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_uint16_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_uint32_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_int8_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_int16_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_int32_t(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_double(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_float(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_rgb_pixel(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_rgb_alpha_pixel(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_get_row_column_hsi_pixel(IntPtr row, int templateRows, int templateColumns, int column, out IntPtr value);

        #endregion

        #region array2d_matrix_set_row_column

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_uint8_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_uint16_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_uint32_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_int8_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_int16_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_int32_t(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_double(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_float(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_rgb_pixel(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_rgb_alpha_pixel(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_set_row_column_hsi_pixel(IntPtr row, int templateRows, int templateColumns, int column, IntPtr value);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void array2d_matrix_row_delete(MatrixElementType type, IntPtr row, int templateRows, int templateColumns);

        #endregion

        #region assign_pixel

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void assign_pixel_rgb_rgbalpha(ref RgbPixel dest, ref RgbAlphaPixel src);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void assign_pixel_rgbalpha_rgb(ref RgbAlphaPixel dest, ref RgbPixel src);

        #endregion

        #region load_bmp

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_bmp(Array2DType type, IntPtr array, byte[] path);

        #endregion

        #region load_dng

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_dng(Array2DType type, IntPtr array, byte[] path);

        #endregion

        #region load_image

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_image(Array2DType type, IntPtr array, byte[] path);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_image_matrix(MatrixElementType type, byte[] path, out IntPtr matrix);

        #endregion

        #region load_jpeg

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_jpeg(Array2DType type, IntPtr array, byte[] path);

        #endregion

        #region load_png

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType load_png(Array2DType type, IntPtr array, byte[] path);

        #endregion

        #region save_bmp

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_bmp(Array2DType type, IntPtr array, byte[] path);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_bmp_matrix(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumn, byte[] path);

        #endregion

        #region save_dng

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_dng(Array2DType type, IntPtr array, byte[] path);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_dng_matrix(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumn, byte[] path);

        #endregion

        #region save_jpeg

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_jpeg(Array2DType type, IntPtr array, byte[] path, int quality);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_jpeg_matrix(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumn, byte[] path, int quality);

        #endregion

        #region save_png

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_png(Array2DType type, IntPtr array, byte[] path);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType save_png_matrix(MatrixElementType type, IntPtr matrix, int templateRows, int templateColumn, byte[] path);

        #endregion

        #region scan_fhog_pyramid

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType scan_fhog_pyramid_new(PyramidType pyramidType,
                                                             uint pyramidRate,
                                                             FHogFeatureExtractorType featureExtractorType,
                                                             out IntPtr pyramid);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void scan_fhog_pyramid_delete(PyramidType pyramidType,
                                                           uint pyramidRate,
                                                           FHogFeatureExtractorType featureExtractorType,
                                                           IntPtr pyramid);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType scan_fhog_pyramid_set_detection_window_size(PyramidType pyramid_type,
                                                                                   uint pyramid_rate,
                                                                                   FHogFeatureExtractorType extractor_type,
                                                                                   IntPtr obj,
                                                                                   uint width,
                                                                                   uint height);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType scan_fhog_pyramid_set_nuclear_norm_regularization_strength(PyramidType pyramid_type,
                                                                                                  uint pyramid_rate,
                                                                                                  FHogFeatureExtractorType extractor_type,
                                                                                                  IntPtr obj,
                                                                                                  double strength);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType scan_fhog_pyramid_evaluate_detectors(PyramidType pyramid_type,
                                                                            uint pyramid_rate,
                                                                            FHogFeatureExtractorType extractor_type,
                                                                            IntPtr[] objects,
                                                                            int objects_num,
                                                                            MatrixElementType elementType,
                                                                            IntPtr image,
                                                                            double adjust_threshold,
                                                                            out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType scan_fhog_pyramid_num_separable_filters(PyramidType pyramid_type,
                                                                               uint pyramid_rate,
                                                                               FHogFeatureExtractorType extractor_type,
                                                                               IntPtr obj,
                                                                               uint weight_index,
                                                                               out uint ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType scan_fhog_pyramid_threshold_filter_singular_values(PyramidType pyramid_type,
                                                                                          uint pyramid_rate,
                                                                                          FHogFeatureExtractorType extractor_type,
                                                                                          IntPtr obj,
                                                                                          double thresh,
                                                                                          uint weight_index,
                                                                                          out IntPtr ret);

        #endregion

        #region shape_predictor

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr normalizing_tform(IntPtr rect);

        #endregion

        #region structural_object_detection_trainer

        #region scan_fhog_pyramid

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_new(PyramidType pyramidType,
                                                                                                 uint pyramidRate,
                                                                                                 FHogFeatureExtractorType featureExtractorType,
                                                                                                 IntPtr scanner,
                                                                                                 out IntPtr pyramid);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void structural_object_detection_trainer_scan_fhog_pyramid_delete(PyramidType pyramidType,
                                                                                               uint pyramidRate,
                                                                                               FHogFeatureExtractorType featureExtractorType,
                                                                                               IntPtr pyramid);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_be_verbose(PyramidType pyramid_type,
                                                                                                        uint pyramid_rate,
                                                                                                        FHogFeatureExtractorType extractor_type,
                                                                                                        IntPtr obj);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_set_c(PyramidType pyramid_type,
                                                                                                   uint pyramid_rate,
                                                                                                   FHogFeatureExtractorType extractor_type,
                                                                                                   IntPtr obj,
                                                                                                   double c);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon(PyramidType pyramid_type,
                                                                                                         uint pyramid_rate,
                                                                                                         FHogFeatureExtractorType extractor_type,
                                                                                                         IntPtr obj,
                                                                                                         double epsilon);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads(PyramidType pyramid_type,
                                                                                                             uint pyramid_rate,
                                                                                                             FHogFeatureExtractorType extractor_type,
                                                                                                             IntPtr obj,
                                                                                                             uint thread);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_train_rectangle(PyramidType pyramid_type,
                                                                                                             uint pyramid_rate,
                                                                                                             FHogFeatureExtractorType extractor_type,
                                                                                                             IntPtr obj,
                                                                                                             MatrixElementType elementType,
                                                                                                             IntPtr images,
                                                                                                             IntPtr objects,
                                                                                                             out IntPtr detector);

        #endregion

        #endregion

        #region object_detector

        #region scan_fhog_pyramid

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_new(PyramidType pyramidType,
                                                                             uint pyramidRate,
                                                                             FHogFeatureExtractorType featureExtractorType,
                                                                             out IntPtr detector);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void object_detector_scan_fhog_pyramid_delete(PyramidType pyramidType,
                                                                           uint pyramidRate,
                                                                           FHogFeatureExtractorType featureExtractorType,
                                                                           IntPtr detector);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_deserialize(byte[] fileName,
                                                                                     PyramidType pyramidType,
                                                                                     uint pyramidRate,
                                                                                     FHogFeatureExtractorType featureExtractorType,
                                                                                     IntPtr obj);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_operator(PyramidType pyramidType,
                                                                                  uint pyramidRate,
                                                                                  FHogFeatureExtractorType featureExtractorType,
                                                                                  IntPtr detector,
                                                                                  MatrixElementType elementType,
                                                                                  IntPtr matrix,
                                                                                  out IntPtr dets);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_serialize(byte[] fileName,
                                                                                   PyramidType pyramidType,
                                                                                   uint pyramidRate,
                                                                                   FHogFeatureExtractorType featureExtractorType,
                                                                                   IntPtr obj);

        #endregion

        #endregion

        #region linear_kernel

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr linear_kernel_new(MatrixElementType matrixElementType, int templateRow, int templateColumn);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void linear_kernel_delete(MatrixElementType matrixElementType, IntPtr linerKernel, int templateRow, int templateColumn);

        #endregion

        #region matrix_range_exp

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct matrix_range_exp_create_param
        {
            // uint8_t
            public uint8_t uint8_t_start;
            public uint8_t uint8_t_inc;
            public uint8_t uint8_t_end;
            public bool use_uint8_t_inc;

            // uint16_t
            public uint16_t uint16_t_start;
            public uint16_t uint16_t_inc;
            public uint16_t uint16_t_end;
            bool use_uint16_t_inc;

            // int8_t
            public int8_t int8_t_start;
            public int8_t int8_t_inc;
            public int8_t int8_t_end;
            bool use_int8_t_inc;

            // int16_t
            public int16_t int16_t_start;
            public int16_t int16_t_inc;
            public int16_t int16_t_end;
            bool use_int16_t_inc;

            // int32_t
            public int32_t int32_t_start;
            public int32_t int32_t_inc;
            public int32_t int32_t_end;
            public bool use_int32_t_inc;

            // float
            public float float_start;
            public float float_inc;
            public float float_end;
            public bool use_float_inc;

            // double
            public double double_start;
            public double double_inc;
            public double double_end;
            public bool use_double_inc;

            public bool use_num;
            public int num;
        }

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr matrix_range_exp_create(MatrixElementType matrixElementType, ref matrix_range_exp_create_param param);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void matrix_range_exp_delete(IntPtr array);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool matrix_range_exp_nc(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool matrix_range_exp_nr(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

        #endregion

        #region mlp

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr mlp_kernel_new(MlpKernelType kernel_type, int nodes_in_input_layer, int nodes_in_first_hidden_layer, int nodes_in_second_hidden_layer, int nodes_in_output_layer, double alpha, double momentum);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType mlp_kernel_train(MlpKernelType kernel_type, IntPtr kernel, IntPtr example_in, double example_out);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType mlp_kernel_train_matrix(MlpKernelType kernel_type, IntPtr kernel, IntPtr example_in, IntPtr example_out);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType mlp_kernel_operator(MlpKernelType kernel_type, IntPtr kernel, MatrixElementType type, IntPtr data, out IntPtr ret_mat);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void mlp_kernel_delete(MlpKernelType kernel_type, IntPtr kernel);

        #endregion

        #region extract_image_4points

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType extract_image_4points(Array2DType type,
                                                             IntPtr array,
                                                             IntPtr[] points,
                                                             int width,
                                                             int height,
                                                             out IntPtr output);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType extract_image_4points_matrix(MatrixElementType element_type,
                                                                    IntPtr matrix,
                                                                    int templateRows,
                                                                    int templateColumns,
                                                                    IntPtr[] points,
                                                                    int width,
                                                                    int height,
                                                                    out IntPtr output);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr find_similarity_transform_dpoint(IntPtr from_points, IntPtr to_points);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr find_similarity_transform_point(IntPtr from_points, IntPtr to_points);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr rotation_matrix(double angle);

        #region running_stats

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr running_stats_new(RunningStatsType type);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref float val);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref double val);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_clear(RunningStatsType type, IntPtr stats);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out float n);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out double n);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out float ex_kurtosis);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out double ex_kurtosis);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out float max);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out double max);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out float mean);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out double mean);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out float min);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out double min);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref float scale, out float ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref double scale, out double ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out float skewness);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out double skewness);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out float stddev);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out double stddev);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out float variance);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out double variance);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void running_stats_delete(RunningStatsType type, IntPtr stats);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr running_stats_operator_add(RunningStatsType type, IntPtr left, IntPtr right);

        #endregion

        #region vector

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new(VectorElementType type);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_uint8_t(byte x, byte y, byte z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_uint16_t(ushort x, ushort y, ushort z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_uint32_t(uint x, uint y, uint z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_int8_t(sbyte x, sbyte y, sbyte z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_int16_t(short x, short y, short z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_int32_t(int x, int y, int z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_float(float x, float y, float z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_new1_double(double x, double y, double z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_delete(VectorElementType type, IntPtr vector);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType vector_operator_left_shift(VectorElementType type, IntPtr vector, IntPtr ofstream);

        #region vector_get_xyz

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_uint8_t(IntPtr vector, out byte x, out byte y, out byte z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_uint16_t(IntPtr vector, out ushort x, out ushort y, out ushort z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_uint32_t(IntPtr vector, out uint x, out uint y, out uint z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_int8_t(IntPtr vector, out sbyte x, out sbyte y, out sbyte z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_int16_t(IntPtr vector, out short x, out short y, out short z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_int32_t(IntPtr vector, out int x, out int y, out int z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_float(IntPtr vector, out float x, out float y, out float z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_get_xyz_double(IntPtr vector, out double x, out double y, out double z);

        #endregion

        #region vector_set_xyz

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_uint8_t(IntPtr vector, byte x, byte y, byte z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_uint16_t(IntPtr vector, ushort x, ushort y, ushort z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_uint32_t(IntPtr vector, uint x, uint y, uint z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_int8_t(IntPtr vector, sbyte x, sbyte y, sbyte z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_int16_t(IntPtr vector, short x, short y, short z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_int32_t(IntPtr vector, int x, int y, int z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_float(IntPtr vector, float x, float y, float z);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void vector_set_xyz_double(IntPtr vector, double x, double y, double z);

        #endregion

        #region vector_operator_add

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_uint8_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_uint16_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_uint32_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_int8_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_int16_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_int32_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_float(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_add_double(IntPtr left, IntPtr right, out IntPtr ret);

        #endregion

        #region vector_operator_div

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_uint8_t(IntPtr vector, byte value, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_uint16_t(IntPtr vector, ushort value, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_uint32_t(IntPtr vector, uint value, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_int8_t(IntPtr vector, sbyte value, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_int16_t(IntPtr vector, short value, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_int32_t(IntPtr vector, int value, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_float(IntPtr vector, float value, out IntPtr ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr vector_operator_div_double(IntPtr vector, double value, out IntPtr ret);

        #endregion

        #endregion

        #region dnn

        #region loss_metric

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr dnn_trainer_loss_metric_new(IntPtr net, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_metric_delete(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_metric_be_verbose(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_metric_set_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_get_learning_rate(IntPtr trainer, int type, out double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_metric_set_min_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_metric_set_mini_batch_size(IntPtr trainer, int type, uint size);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_set_synchronization_file(IntPtr trainer, int type, byte[] filename, uint second);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_test_one_step(IntPtr trainer,
                                                                             int type,
                                                                             NativeMethods.MatrixElementType dataElementType,
                                                                             IntPtr data,
                                                                             NativeMethods.MatrixElementType labelElementType,
                                                                             IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_train(IntPtr trainer,
                                                                     int type,
                                                                     NativeMethods.MatrixElementType dataElementType,
                                                                     IntPtr data,
                                                                     NativeMethods.MatrixElementType labelElementType,
                                                                     IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_train_one_step(IntPtr trainer,
                                                                              int type,
                                                                              NativeMethods.MatrixElementType dataElementType,
                                                                              IntPtr data,
                                                                              NativeMethods.MatrixElementType labelElementType,
                                                                              IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_set_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                         int type,
                                                                                                         uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_set_test_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                              int type,
                                                                                                              uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_get_net(IntPtr trainer, int type, out IntPtr ret);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_metric_operator_left_shift(IntPtr trainer, int type, IntPtr stream);

        #endregion

        #region loss_mmod

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr dnn_trainer_loss_mmod_new(IntPtr net, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_delete(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_be_verbose(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_set_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_get_learning_rate(IntPtr trainer, int type, out double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_set_min_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_mmod_set_mini_batch_size(IntPtr trainer, int type, uint size);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_set_synchronization_file(IntPtr trainer, int type, byte[] filename, uint second);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_test_one_step(IntPtr trainer,
                                                                           int type,
                                                                           NativeMethods.MatrixElementType dataElementType,
                                                                           IntPtr data,
                                                                           NativeMethods.MatrixElementType labelElementType,
                                                                           IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_train(IntPtr trainer,
                                                                   int type,
                                                                   NativeMethods.MatrixElementType dataElementType,
                                                                   IntPtr data,
                                                                   NativeMethods.MatrixElementType labelElementType,
                                                                   IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_train_one_step(IntPtr trainer,
                                                                            int type,
                                                                            NativeMethods.MatrixElementType dataElementType,
                                                                            IntPtr data,
                                                                            NativeMethods.MatrixElementType labelElementType,
                                                                            IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_set_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                       int type,
                                                                                                       uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_set_test_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                            int type,
                                                                                                            uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_get_net(IntPtr trainer, int type, out IntPtr ret);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_mmod_operator_left_shift(IntPtr trainer, int type, IntPtr stream);

        #endregion

        #region loss_multiclass_log

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr dnn_trainer_loss_multiclass_log_new(IntPtr net, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_delete(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_be_verbose(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_set_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_get_learning_rate(IntPtr trainer, int type, out double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_set_min_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_set_mini_batch_size(IntPtr trainer, int type, uint size);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_set_synchronization_file(IntPtr trainer, int type, byte[] filename, uint second);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_test_one_step(IntPtr trainer,
                                                                                     int type,
                                                                                     NativeMethods.MatrixElementType dataElementType,
                                                                                     IntPtr data,
                                                                                     NativeMethods.MatrixElementType labelElementType,
                                                                                     IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_train(IntPtr trainer,
                                                                             int type,
                                                                             NativeMethods.MatrixElementType dataElementType,
                                                                             IntPtr data,
                                                                             NativeMethods.MatrixElementType labelElementType,
                                                                             IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_train_one_step(IntPtr trainer,
                                                                                      int type,
                                                                                      NativeMethods.MatrixElementType dataElementType,
                                                                                      IntPtr data,
                                                                                      NativeMethods.MatrixElementType labelElementType,
                                                                                      IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_set_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                                 int type,
                                                                                                                 uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_set_test_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                                      int type,
                                                                                                                      uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_get_net(IntPtr trainer, int type, out IntPtr ret);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_operator_left_shift(IntPtr trainer, int type, IntPtr stream);

        #endregion

        #region loss_multiclass_log_per_pixel

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr dnn_trainer_loss_multiclass_log_per_pixel_new(IntPtr net, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_per_pixel_delete(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_per_pixel_be_verbose(IntPtr trainer, int type);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_per_pixel_set_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_get_learning_rate(IntPtr trainer, int type, out double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_per_pixel_set_min_learning_rate(IntPtr trainer, int type, double lr);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void dnn_trainer_loss_multiclass_log_per_pixel_set_mini_batch_size(IntPtr trainer, int type, uint size);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_set_synchronization_file(IntPtr trainer, int type, byte[] filename, uint second);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_test_one_step(IntPtr trainer,
                                                                                               int type,
                                                                                               NativeMethods.MatrixElementType dataElementType,
                                                                                               IntPtr data,
                                                                                               NativeMethods.MatrixElementType labelElementType,
                                                                                               IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_train(IntPtr trainer,
                                                                                       int type,
                                                                                       NativeMethods.MatrixElementType dataElementType,
                                                                                       IntPtr data,
                                                                                       NativeMethods.MatrixElementType labelElementType,
                                                                                       IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_train_one_step(IntPtr trainer,
                                                                                                int type,
                                                                                                NativeMethods.MatrixElementType dataElementType,
                                                                                                IntPtr data,
                                                                                                NativeMethods.MatrixElementType labelElementType,
                                                                                                IntPtr label);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_set_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                                           int type,
                                                                                                                           uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_set_test_iterations_without_progress_threshold(IntPtr trainer,
                                                                                                                                int type,
                                                                                                                                uint thresh);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_get_net(IntPtr trainer, int type, out IntPtr ret);

        [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType dnn_trainer_loss_multiclass_log_per_pixel_operator_left_shift(IntPtr trainer, int type, IntPtr stream);

        #endregion

        #endregion

        #region  extensions

        #region extensions_load_image_data

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, byte[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data2(Array2DType dst_type, Array2DType src_type, ImagePixelType pixel_type, byte[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, ushort[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, uint[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, sbyte[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, short[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, int[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, float[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, double[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, RgbPixel[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, RgbAlphaPixel[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, HsiPixel[] data, uint rows, uint columns, uint steps);

        #endregion

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern NativeMethods.ErrorType extensions_matrix_to_array(IntPtr src, MatrixElementType type, int templateRows, int templateColumns, IntPtr dst);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType extensions_convert_array_to_bytes(Array2DType src_type, IntPtr src, byte[] dst, uint rows, uint columns);

        #endregion

    }

}