using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_new(PyramidType pyramidType,
                                                                                                 uint pyramidRate,
                                                                                                 FHogFeatureExtractorType featureExtractorType,
                                                                                                 IntPtr scanner,
                                                                                                 out IntPtr pyramid);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void structural_object_detection_trainer_scan_fhog_pyramid_delete(PyramidType pyramidType,
                                                                                               uint pyramidRate,
                                                                                               FHogFeatureExtractorType featureExtractorType,
                                                                                               IntPtr pyramid);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_be_verbose(PyramidType pyramid_type,
                                                                                                        uint pyramid_rate,
                                                                                                        FHogFeatureExtractorType extractor_type,
                                                                                                        IntPtr obj);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_set_c(PyramidType pyramid_type,
                                                                                                   uint pyramid_rate,
                                                                                                   FHogFeatureExtractorType extractor_type,
                                                                                                   IntPtr obj,
                                                                                                   double c);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon(PyramidType pyramid_type,
                                                                                                         uint pyramid_rate,
                                                                                                         FHogFeatureExtractorType extractor_type,
                                                                                                         IntPtr obj,
                                                                                                         double epsilon);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads(PyramidType pyramid_type,
                                                                                                             uint pyramid_rate,
                                                                                                             FHogFeatureExtractorType extractor_type,
                                                                                                             IntPtr obj,
                                                                                                             uint thread);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType structural_object_detection_trainer_scan_fhog_pyramid_train_rectangle(PyramidType pyramid_type,
                                                                                                             uint pyramid_rate,
                                                                                                             FHogFeatureExtractorType extractor_type,
                                                                                                             IntPtr obj,
                                                                                                             MatrixElementType elementType,
                                                                                                             IntPtr images,
                                                                                                             IntPtr objects,
                                                                                                             out IntPtr detector);

    }

}