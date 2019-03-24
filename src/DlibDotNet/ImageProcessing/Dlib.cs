using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static IEnumerable<Rectangle> EvaluateDetectors<T, U>(IEnumerable<ObjectDetector<ScanFHogPyramid<T, U>>> detectors,
                                                                     MatrixBase matrix, 
                                                                     double adjustThreshold = 0)
            where T : class
            where U : class
        {
            if (detectors == null)
                throw new ArgumentNullException(nameof(detectors));
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            var count = detectors.Count();
            if (count == 0)
                throw new ArgumentException();

            detectors.ThrowIfDisposed();

            var @params = detectors.Select(detector => detector.GetFHogPyramidParameter());

            var param = @params.First();
            var not = @params.Any(p => p.PyramidRate != param.PyramidRate ||
                                       p.PyramidType != param.PyramidType ||
                                       p.FeatureExtractorType != param.FeatureExtractorType);
            if (not)
                throw new ArgumentException();

            var detectorArray = detectors.Select(det => det.NativePtr).ToArray();
            var elementType = matrix.MatrixElementType.ToNativeMatrixElementType();
            var ret = NativeMethods.scan_fhog_pyramid_evaluate_detectors(param.PyramidType,
                                                                         param.PyramidRate,
                                                                         param.FeatureExtractorType,
                                                                         detectorArray,
                                                                         count,
                                                                         elementType,
                                                                         matrix.NativePtr,
                                                                         adjustThreshold,
                                                                         out var vector);

            switch (ret)
            {
                case NativeMethods.ErrorType.FHogNotSupportExtractor:
                case NativeMethods.ErrorType.PyramidNotSupportRate:
                case NativeMethods.ErrorType.PyramidNotSupportType:
                    throw new NotSupportedException();
            }

            using(var tmp = new StdVector<Rectangle>(vector))
                return tmp.ToArray();
        }

        public static PointTransformAffine NormalizingTForm(Rectangle rectangle)
        {
            using (var native = rectangle.ToNative())
            {
                var ptr = NativeMethods.normalizing_tform(native.NativePtr);
                return new PointTransformAffine(ptr);
            }
        }

        public static uint NumSeparableFilters<T, U>(ObjectDetector<ScanFHogPyramid<T, U>> detector, uint weightIndex = 0)
            where T : class
            where U : class
        {
            if (detector == null)
                throw new ArgumentNullException(nameof(detector));

            detector.ThrowIfDisposed();

            var param = detector.GetFHogPyramidParameter();
            var ret = NativeMethods.scan_fhog_pyramid_num_separable_filters(param.PyramidType,
                                                                            param.PyramidRate,
                                                                            param.FeatureExtractorType,
                                                                            detector.NativePtr,
                                                                            weightIndex,
                                                                            out var num);

            switch (ret)
            {
                case NativeMethods.ErrorType.FHogNotSupportExtractor:
                case NativeMethods.ErrorType.PyramidNotSupportRate:
                case NativeMethods.ErrorType.PyramidNotSupportType:
                    throw new NotSupportedException();
            }

            return num;
        }

        public static ObjectDetector<ScanFHogPyramid<T, U>> ThresholdFilterSingularValues<T, U>(ObjectDetector<ScanFHogPyramid<T, U>> detector,
                                                                                                double threshold,
                                                                                                uint weightIndex = 0)
            where T : class
            where U : class
        {
            if (detector == null)
                throw new ArgumentNullException(nameof(detector));
            if (threshold < 0)
                throw new ArgumentOutOfRangeException();

            detector.ThrowIfDisposed();

            var param = detector.GetFHogPyramidParameter();
            var ret = NativeMethods.scan_fhog_pyramid_threshold_filter_singular_values(param.PyramidType,
                                                                                       param.PyramidRate,
                                                                                       param.FeatureExtractorType,
                                                                                       detector.NativePtr,
                                                                                       threshold,
                                                                                       weightIndex,
                                                                                       out var newDetector);

            switch (ret)
            {
                case NativeMethods.ErrorType.FHogNotSupportExtractor:
                case NativeMethods.ErrorType.PyramidNotSupportRate:
                case NativeMethods.ErrorType.PyramidNotSupportType:
                    throw new NotSupportedException();
            }

            return new ObjectDetector<ScanFHogPyramid<T, U>>(newDetector, param);
        }

        #endregion

    }

}
