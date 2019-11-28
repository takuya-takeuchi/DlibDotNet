using System;
using System.Collections.Generic;
using System.Linq;

namespace DlibDotNet.Dnn
{

    /// <summary>
    /// This object contains all the parameters that control the behavior of <see cref="LossMmod"/>. This class cannot be inherited.
    /// </summary>
    public sealed class MModOptions : DlibObject
    {

        #region Constructors

        public MModOptions(IEnumerable<IEnumerable<MModRect>> boxes,
                           uint targetSize,
                           uint minTargetSize,
                           double minDetectorWindowOverlapIou = 0.75)
        {
            if (!((0 < minTargetSize && minTargetSize <= targetSize)))
                throw new ArgumentOutOfRangeException($"{nameof(minTargetSize)} should be greater than {nameof(minTargetSize)} and less than or equal to {nameof(targetSize)}.");
            if (!(0.5 < minDetectorWindowOverlapIou && minDetectorWindowOverlapIou < 1))
                throw new ArgumentOutOfRangeException($"{nameof(minDetectorWindowOverlapIou)} should be greater than 0.5 and less than 1.0.");

            var boxCount = boxes.Sum(rects => rects.Count(rect => !rect.Ignore));
            if (boxCount == 0)
                throw new ArgumentException($"{nameof(boxes)} has no element or only contains ignored boxes.", nameof(boxes));

            using (var disposer = new EnumerableDisposer<StdVector<MModRect>>(boxes.Select(b => new StdVector<MModRect>(b))))
            using (var rects = new StdVector<StdVector<MModRect>>(disposer.Collection))
            using (new EnumerableDisposer<StdVector<MModRect>>(rects))
            {
                this.NativePtr = NativeMethods.mmod_options_new(rects.NativePtr,
                                                                targetSize,
                                                                minTargetSize,
                                                                minDetectorWindowOverlapIou);
            }
        }

        #endregion

        #region Properties

        public IEnumerable<DetectorWindowDetails> DetectorWindows
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_detector_windows(this.NativePtr);
                using (var vector = new StdVector<DetectorWindowDetails>(ret))
                    return vector.ToArray();
            }
            set
            {
                this.ThrowIfDisposed();
                using (var vector = new StdVector<DetectorWindowDetails>(value))
                    NativeMethods.mmod_options_set_detector_windows(this.NativePtr, vector.NativePtr);
            }
        }

        public double LossPerFalseAlarm
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_loss_per_false_alarm(this.NativePtr);
                return ret;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_options_set_loss_per_false_alarm(this.NativePtr, value);
            }
        }

        public double LossPerMissedTarget
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_loss_per_missed_target(this.NativePtr);
                return ret;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_options_set_loss_per_missed_target(this.NativePtr, value);
            }
        }

        public double TruthMatchIouThreshold
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_truth_match_iou_threshold(this.NativePtr);
                return ret;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_options_set_truth_match_iou_threshold(this.NativePtr, value);
            }
        }

        public TestBoxOverlap OverlapsNms
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_overlaps_nms(this.NativePtr);
                return new TestBoxOverlap(ret, false);
            }
            set
            {
                this.ThrowIfDisposed();
                if (value == null)
                    throw new ArgumentNullException();
                value.ThrowIfDisposed();
                NativeMethods.mmod_options_set_overlaps_nms(this.NativePtr, value.NativePtr);
            }
        }

        public TestBoxOverlap OverlapsIgnore
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_overlaps_ignore(this.NativePtr);
                return new TestBoxOverlap(ret, false);
            }
            set
            {
                this.ThrowIfDisposed();
                if (value == null)
                    throw new ArgumentNullException();
                value.ThrowIfDisposed();
                NativeMethods.mmod_options_set_overlaps_ignore(this.NativePtr, value.NativePtr);
            }
        }

        public bool UseBoundingBoxRegression
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_use_bounding_box_regression(this.NativePtr);
                return ret;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_options_set_use_bounding_box_regression(this.NativePtr, value);
            }
        }

        public double bbrLambda
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.mmod_options_get_bbr_lambda(this.NativePtr);
                return ret;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mmod_options_set_vbbr_lambda(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.mmod_options_delete(this.NativePtr);
        }

        #endregion

        #endregion

        public sealed class DetectorWindowDetails : DlibObject
        {

            #region Constructors

            internal DetectorWindowDetails(IntPtr ptr, bool isEnabledDispose = true) :
                base(isEnabledDispose)
            {
                this.NativePtr = ptr;
            }

            public DetectorWindowDetails(uint width,
                                         uint height,
                                         string label = null)
            {
                var str = Dlib.Encoding.GetBytes(label ?? "");
                this.NativePtr = NativeMethods.detector_window_details_new(width, height, str);
            }

            #endregion

            #region Properties

            public uint Height
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.detector_window_details_height(this.NativePtr);
                }
            }

            public uint Width
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.detector_window_details_width(this.NativePtr);
                }
            }

            public string Label
            {
                get
                {
                    this.ThrowIfDisposed();
                    var ret = NativeMethods.detector_window_details_label(this.NativePtr);
                    return StringHelper.FromStdString(ret, true);
                }
            }

            #endregion

            #region Methods

            #region Overrides

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.detector_window_details_delete(this.NativePtr);
            }

            #endregion

            #endregion

        }

    }

}
