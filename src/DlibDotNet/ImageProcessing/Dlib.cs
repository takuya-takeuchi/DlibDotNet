#if !LITE
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

        public static double TestShapePredictor<T>(ShapePredictor predictor,
                                                   Array<Array2D<T>> images,
                                                   IEnumerable<IList<FullObjectDetection>> objects,
                                                   IEnumerable<IList<double>> scales)
            where T: struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));
            if (scales == null)
                throw new ArgumentNullException(nameof(scales));

            predictor.ThrowIfDisposed();
            images.ThrowIfDisposed();

            var tmpObjects = objects.ToArray();
            tmpObjects.ThrowIfDisposed();
            var tmpScales = scales.ToArray();

            if (images.Size != tmpObjects.Length)
                throw new ArgumentException();

            if (tmpScales.Any())
            {
                if (tmpObjects.Where((t, index) => t.Count != tmpScales[index].Count).Any())
                    throw new ArgumentException();
            }

            if (!Array2D<T>.TryParse<T>(out var type))
                throw new NotSupportedException();

            using (var disposer1 = new EnumerableDisposer<StdVector<FullObjectDetection>>(tmpObjects.Select(r => new StdVector<FullObjectDetection>(r))))
            using (var objectsVector = new StdVector<StdVector<FullObjectDetection>>(disposer1.Collection))
            using (var disposer2 = new EnumerableDisposer<StdVector<double>>(tmpScales.Select(r => new StdVector<double>(r)).ToArray()))
            using (var scaleVector = new StdVector<StdVector<double>>(disposer2.Collection))
            {
                var ret = NativeMethods.shape_predictor_test_shape_predictor(predictor.NativePtr,
                                                                             type.ToNativeArray2DType(),
                                                                             images.NativePtr,
                                                                             objectsVector.NativePtr,
                                                                             scaleVector.NativePtr,
                                                                             out var value);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {type} is not supported.");
                }

                return value;
            }
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

#endif
