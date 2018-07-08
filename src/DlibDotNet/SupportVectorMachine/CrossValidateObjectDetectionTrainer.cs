using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        public static Matrix<double> TestObjectDetectionFunction<T, U>(ObjectDetector<T> detector,
                                                                       IEnumerable<Matrix<U>> images,
                                                                       IEnumerable<IEnumerable<Rectangle>> objects)
            where T : ImageScanner
            where U : struct
        {
            if (detector == null)
                throw new ArgumentNullException(nameof(detector));
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            detector.ThrowIfDisposed();
            images.ThrowIfDisposed();

            return detector.TestObjectDetectionFunction(images, objects);
        }

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
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

}
