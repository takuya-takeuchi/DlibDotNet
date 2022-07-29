using System;
using System.Runtime.InteropServices;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int64_t = System.Int64;
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

            BgrPixel,

            RgbAlphaPixel,

            HsiPixel,

            LabPixel,

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

            BgrPixel,

            RgbAlphaPixel,

            HsiPixel,

            LabPixel

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

            Sigmoid,

            SparseHistogramIntersection,

            SparseLinear,

            SparsePolynomial,

            SparseRadialBasis,

            SparseSigmoid

        }

        internal enum SvmFunctionType
        {

            Decision,

            ProbabilisticDecision,

            Distance,

            Projection,

            MulticlassLinearDecision

        }

        internal enum SvmTrainerType
        {

            C,

            Nu,

        }

        internal enum SvmBatchTrainerType
        {

            Pegasos,

        }

        internal enum NormalizerType
        {

            Vector,

            VectorPca

        }

        internal enum OptimizerType
        {

            Sgd,

            Adam

        }

        internal enum ErrorType
        {

            OK = 0x00000000,

            #region General

            GeneralError = 0x76000000,

            GeneralFileIOError      = -(GeneralError | 0x00000001),

            GeneralFileImageLoad    = -(GeneralError | 0x00000002),

            GeneralSerialization    = -(GeneralError | 0x00000003),

            GeneralInvalidParameter = -(GeneralError | 0x00000004),

            GeneralNotSupport       = -(GeneralError | 0x00000005),

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

            // "C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v10.0\include\driver_types.h"
            // 
            // Any unhandled CUDA driver error is added to this value and returned via
            // the runtime. Production releases of CUDA should not return such errors.
            // \deprecated
            // This error return is deprecated as of CUDA 4.1.
            // cudaErrorApiFailureBase = 10000
            CudaErrorApiFailureBase = -(CudaError | 10000),

            #endregion

            #region Svm

            SvmError                  =              0x75000000,
                                      
            SvmKernelNotSupport       = -(SvmError | 0x00000001),
                                      
            SvmFunctionNotSupport     = -(SvmError | 0x00000002),
                                      
            SvmTrainerNotSupport      = -(SvmError | 0x00000003),

            SvmBatchTrainerNotSupport = -(SvmError | 0x00000004),

            #endregion

        }
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr get_version();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool is_support_gui();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool is_support_cuda();

        #region assign_pixel

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgb_rgbalpha(ref RgbPixel dest, ref RgbAlphaPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgbalpha_rgb(ref RgbAlphaPixel dest, ref RgbPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgb_hsi(ref RgbPixel dest, ref HsiPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgb_lab(ref RgbPixel dest, ref LabPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgbalpha_hsi(ref RgbAlphaPixel dest, ref HsiPixel src);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void assign_pixel_rgbalpha_lab(ref RgbAlphaPixel dest, ref LabPixel src);

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
                                                                                     int filepathLength,
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
        public static extern ErrorType object_detector_scan_fhog_pyramid_operator_with_confidence(PyramidType pyramidType,
                                                                                                  uint pyramidRate,
                                                                                                  FHogFeatureExtractorType featureExtractorType,
                                                                                                  IntPtr detector,
                                                                                                  MatrixElementType elementType,
                                                                                                  IntPtr matrix,
                                                                                                  out IntPtr rects,
                                                                                                  out IntPtr confidences);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType object_detector_scan_fhog_pyramid_serialize(byte[] fileName,
                                                                                   int filepathLength,
                                                                                   PyramidType pyramidType,
                                                                                   uint pyramidRate,
                                                                                   FHogFeatureExtractorType featureExtractorType,
                                                                                   IntPtr obj,
                                                                                   out IntPtr errorMessage);

        #endregion

        #endregion

        #region matrix_range_exp

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int8_t_new1(int8_t start, int8_t inc, int8_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int8_t_new2(int8_t start, int8_t end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int8_t_new3(int8_t start, int8_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int16_t_new1(int16_t start, int16_t inc, int16_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int16_t_new2(int16_t start, int16_t end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int16_t_new3(int16_t start, int16_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int32_t_new1(int32_t start, int32_t inc, int32_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int32_t_new2(int32_t start, int32_t end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int32_t_new3(int32_t start, int32_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int64_t_new1(int64_t start, int64_t inc, int64_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int64_t_new2(int64_t start, int64_t end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_int64_t_new3(int64_t start, int64_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_uint8_t_new1(uint8_t start, uint8_t inc, uint8_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_uint8_t_new2(uint8_t start, uint8_t end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_uint8_t_new3(uint8_t start, uint8_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_uint16_t_new1(uint16_t start, uint16_t inc, uint16_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_uint16_t_new2(uint16_t start, uint16_t end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_uint16_t_new3(uint16_t start, uint16_t end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_float_new1(float start, float inc, float end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_float_new2(float start, float end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_float_new3(float start, float end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_double_new1(double start, double inc, double end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_double_new2(double start, double end, int num);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr matrix_range_exp_create_double_new3(double start, double end);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void matrix_range_exp_delete(MatrixElementType matrixElementType, IntPtr array);

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

        #region  extensions

        #region extensions_load_image_data

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, IntPtr data, uint rows, uint columns, uint steps);

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
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, BgrPixel[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, RgbAlphaPixel[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, HsiPixel[] data, uint rows, uint columns, uint steps);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr extensions_load_image_data(Array2DType dst_type, Array2DType src_type, LabPixel[] data, uint rows, uint columns, uint steps);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extensions_matrix_to_array(IntPtr src, MatrixElementType type, int templateRows, int templateColumns, IntPtr dst);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extensions_convert_array_to_bytes(Array2DType src_type, IntPtr src, byte[] dst, uint rows, uint columns);

        #endregion

    }

}
