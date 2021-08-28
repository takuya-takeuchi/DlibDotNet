#if !LITE
using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region SetAllBnRunningStatsWindowSizes

        public static void SetAllBnRunningStatsWindowSizes(LossMetric net, uint newWindowSize)
        {
            if (net == null)
                throw new ArgumentNullException(nameof(net));

            net.ThrowIfDisposed();

            var ret = NativeMethods.LossMetric_set_all_bn_running_stats_window_sizes(net.NetworkType,
                                                                                     net.NativePtr,
                                                                                     newWindowSize);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(net.NetworkType);
        }

        public static void SetAllBnRunningStatsWindowSizes(LossMmod net, uint newWindowSize)
        {
            if (net == null)
                throw new ArgumentNullException(nameof(net));

            net.ThrowIfDisposed();

            var ret = NativeMethods.LossMmod_set_all_bn_running_stats_window_sizes(net.NetworkType,
                                                                                   net.NativePtr,
                                                                                   newWindowSize);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(net.NetworkType);
        }

        public static void SetAllBnRunningStatsWindowSizes(LossMulticlassLog net, uint newWindowSize)
        {
            if (net == null)
                throw new ArgumentNullException(nameof(net));

            net.ThrowIfDisposed();

            var ret = NativeMethods.LossMulticlassLog_set_all_bn_running_stats_window_sizes(net.NetworkType,
                                                                                            net.NativePtr,
                                                                                            newWindowSize);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(net.NetworkType);
        }

        public static void SetAllBnRunningStatsWindowSizes(LossMulticlassLogPerPixel net, uint newWindowSize)
        {
            if (net == null)
                throw new ArgumentNullException(nameof(net));

            net.ThrowIfDisposed();

            var ret = NativeMethods.LossMulticlassLogPerPixel_set_all_bn_running_stats_window_sizes(net.NetworkType,
                                                                                                    net.NativePtr,
                                                                                                    newWindowSize);
            if (ret == NativeMethods.ErrorType.DnnNotSupportNetworkType)
                throw new NotSupportNetworkTypeException(net.NetworkType);
        }

        #endregion

        public static Matrix<double> TestObjectDetectionFunction<T>(LossMmod detector,
                                                                    IEnumerable<Matrix<T>> images,
                                                                    IEnumerable<IEnumerable<MModRect>> truthDets,
                                                                    TestBoxOverlap overlapTester = null,
                                                                    double adjustThreshold = 0,
                                                                    TestBoxOverlap overlapIgnoreTester = null)
           where T : struct
        {
            if (detector == null)
                throw new ArgumentNullException(nameof(detector));
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (truthDets == null)
                throw new ArgumentNullException(nameof(truthDets));

            detector.ThrowIfDisposed();
            images.ThrowIfDisposed();
            truthDets.ThrowIfDisposed();

            var disposeOverlapTester = overlapTester == null;
            var disposeOverlapIgnoreTester = overlapIgnoreTester == null;

            try
            {
                if (disposeOverlapTester)
                    overlapTester = new TestBoxOverlap();
                if (disposeOverlapIgnoreTester)
                    overlapIgnoreTester = new TestBoxOverlap();

                using (var matrixVector = new StdVector<Matrix<T>>(images))
                using (var disposer = new EnumerableDisposer<StdVector<MModRect>>(truthDets.Select(r => new StdVector<MModRect>(r))))
                using (var detsVector = new StdVector<StdVector<MModRect>>(disposer.Collection))
                using (new EnumerableDisposer<StdVector<MModRect>>(detsVector))
                {
                    var type = detector.NetworkType;
                    Matrix<T>.TryParse<T>(out var elementTypes);
                    var matrix = images.FirstOrDefault();
                    var ret = NativeMethods.test_object_detection_function_net(type,
                                                                               detector.NativePtr,
                                                                               elementTypes.ToNativeMatrixElementType(),
                                                                               matrixVector.NativePtr,
                                                                               matrix.TemplateRows,
                                                                               matrix.TemplateColumns,
                                                                               detsVector.NativePtr,
                                                                               overlapTester.NativePtr,
                                                                               adjustThreshold,
                                                                               overlapIgnoreTester.NativePtr,
                                                                               out var result);
                    switch (ret)
                    {
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{elementTypes} is not supported.");
                        case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                            throw new NotSupportNetworkTypeException(type);
                    }

                    return new Matrix<double>(result, 1, 3);
                }
            }
            finally
            {
                if (disposeOverlapTester)
                    overlapTester?.Dispose();
                if (disposeOverlapIgnoreTester)
                    overlapIgnoreTester?.Dispose();
            }
        }

    }

}
#endif
