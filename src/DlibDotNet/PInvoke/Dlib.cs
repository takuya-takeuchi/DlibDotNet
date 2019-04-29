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

            OpStdVectToMatValue,

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

        internal enum SvmKernelType
        {

            Histogramintersection,

            Linear,

            Offset,

            Polynomial,

            RadialBasis,

            Sigmoid

        }

        internal enum ErrorType
        {

            OK = 0x00000000,

            #region General

            GeneralError = 0x76000000,

            GeneralFileIOError   = -(GeneralError | 0x00000001),

            GeneralFileImageLoad = -(GeneralError | 0x00000002),

            GeneralSerialization = -(GeneralError | 0x00000003),

            #endregion

            #region Array2D

            Array2DError = 0x7B000000,

            Array2DTypeTypeNotSupport = -(Array2DError | 0x00000001),

            #endregion

            #region Matrix

            MatrixError = 0x7C000000,

            MatrixElementTypeNotSupport         = -(MatrixError | 0x00000001),

            MatrixElementTemplateSizeNotSupport = -(MatrixError | 0x00000002),

            MatrixOpTypeNotSupport              = -(MatrixError | 0x00000003),

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

            #region Cuda

            CudaError = 0x77000000,

            CudaOutOfMemory = -(CudaError | 0x00000001),

            #endregion

            #region Svm

            SvmError            =              0x75000000,

            SvmKernelNotSupport = -(SvmError | 0x00000001),

            #endregion

        }

        #region assign_pixel

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgb_rgbalpha(ref RgbPixel dest, ref RgbAlphaPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgbalpha_rgb(ref RgbAlphaPixel dest, ref RgbPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgb_hsi(ref RgbPixel dest, ref HsiPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgbalpha_hsi(ref RgbAlphaPixel dest, ref HsiPixel src);

        #endregion

        #region shape_predictor

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr normalizing_tform(IntPtr rect);

        #endregion
        
        #region object_detector

        #region scan_fhog_pyramid

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_new(PyramidType pyramidType,
                                                                             uint pyramidRate,
                                                                             FHogFeatureExtractorType featureExtractorType,
                                                                             out IntPtr detector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void object_detector_scan_fhog_pyramid_delete(PyramidType pyramidType,
                                                                           uint pyramidRate,
                                                                           FHogFeatureExtractorType featureExtractorType,
                                                                           IntPtr detector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_deserialize(byte[] fileName,
                                                                                     PyramidType pyramidType,
                                                                                     uint pyramidRate,
                                                                                     FHogFeatureExtractorType featureExtractorType,
                                                                                     IntPtr obj,
                                                                                     out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_operator(PyramidType pyramidType,
                                                                                  uint pyramidRate,
                                                                                  FHogFeatureExtractorType featureExtractorType,
                                                                                  IntPtr detector,
                                                                                  MatrixElementType elementType,
                                                                                  IntPtr matrix,
                                                                                  out IntPtr dets);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_serialize(byte[] fileName,
                                                                                   PyramidType pyramidType,
                                                                                   uint pyramidRate,
                                                                                   FHogFeatureExtractorType featureExtractorType,
                                                                                   IntPtr obj,
                                                                                   out IntPtr errorMessage);

        #endregion

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

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create(MatrixElementType matrixElementType, ref matrix_range_exp_create_param param);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_range_exp_delete(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool matrix_range_exp_nc(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool matrix_range_exp_nr(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

        #endregion
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr find_similarity_transform_dpoint(IntPtr from_points, IntPtr to_points);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr find_similarity_transform_point(IntPtr from_points, IntPtr to_points);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr rotation_matrix(double angle);

        #region running_stats

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr running_stats_new(RunningStatsType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref float val);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref double val);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_clear(RunningStatsType type, IntPtr stats);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out float n);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out double n);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out float ex_kurtosis);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out double ex_kurtosis);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out float max);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out double max);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out float mean);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out double mean);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out float min);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out double min);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref float scale, out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref double scale, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out float skewness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out double skewness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out float stddev);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out double stddev);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out float variance);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out double variance);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void running_stats_delete(RunningStatsType type, IntPtr stats);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr running_stats_operator_add(RunningStatsType type, IntPtr left, IntPtr right);

        #endregion

        #region  extensions

        #region extensions_load_image_data

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, byte[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data2(Array2DType dst_type, Array2DType src_type, ImagePixelType pixel_type, byte[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, ushort[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, uint[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, sbyte[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, short[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, int[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, float[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, double[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, RgbPixel[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, RgbAlphaPixel[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, HsiPixel[] data, uint rows, uint columns, uint steps);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extensions_matrix_to_array(IntPtr src, MatrixElementType type, int templateRows, int templateColumns, IntPtr dst);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extensions_convert_array_to_bytes(Array2DType src_type, IntPtr src, byte[] dst, uint rows, uint columns);

        #endregion

    }

}