using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

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

    }

}