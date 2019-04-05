using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_fhog(MatrixElementType img_type,
                                                 IntPtr hog,
                                                 int cell_draw_size,
                                                 float min_response_threshold,
                                                 out IntPtr out_matrix);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType draw_fhog_object_detector_scan_fhog_pyramid(PyramidType pyramid_type,
                                                                                   uint pyramid_rate,
                                                                                   FHogFeatureExtractorType extractor_type,
                                                                                   IntPtr obj,
                                                                                   uint weightIndex,
                                                                                   int cellDrawSize,
                                                                                   out IntPtr out_matrix);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_fhog_features(Array2DType img_type,
                                                             IntPtr img,
                                                             MatrixElementType hog_type,
                                                             IntPtr hog,
                                                             int cell_size,
                                                             int filter_rows_padding,
                                                             int filter_cols_padding);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_fhog_features_array(Array2DType img_type,
                                                                   IntPtr img,
                                                                   Array2DType hog_type,
                                                                   IntPtr hog,
                                                                   int cell_size,
                                                                   int filter_rows_padding,
                                                                   int filter_cols_padding);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType extract_fhog_features2(Array2DType img_type,
                                                              IntPtr img,
                                                              MatrixElementType hog_type,
                                                              int cell_size,
                                                              int filter_rows_padding,
                                                              int filter_cols_padding,
                                                              out IntPtr hog);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr image_to_fhog(IntPtr p,
                                                  int cell_size,
                                                  int filter_rows_padding,
                                                  int filter_cols_padding);

    }

}