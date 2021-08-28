using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_rectangle(PyramidType pyramid_type,
                                                                                                                                          uint pyramid_rate,
                                                                                                                                          FHogFeatureExtractorType extractor_type,
                                                                                                                                          IntPtr obj,
                                                                                                                                          MatrixElementType elementType,
                                                                                                                                          IntPtr images,
                                                                                                                                          IntPtr objects,
                                                                                                                                          out IntPtr matrix);

    }

}
