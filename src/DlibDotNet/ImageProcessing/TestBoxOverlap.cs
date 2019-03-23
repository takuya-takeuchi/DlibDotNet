using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class TestBoxOverlap : DlibObject
    {

        #region Constructors

        internal TestBoxOverlap(IntPtr ptr, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            this.NativePtr = ptr;
        }

        public TestBoxOverlap(double iouThresh = 0.5, double percentCoveredThresh = 1.0)
        {
            this.NativePtr = NativeMethods.test_box_overlap_new(iouThresh, percentCoveredThresh);
        }

        #endregion

        #region Methods

        public double GetIouThresh()
        {
            this.ThrowIfDisposed();
            var ret = NativeMethods.test_box_overlap_get_iou_thresh(this.NativePtr);
            return ret;
        }

        public double GetPercentCoveredThresh()
        {
            this.ThrowIfDisposed();
            var ret = NativeMethods.test_box_overlap_get_percent_covered_thresh(this.NativePtr);
            return ret;
        }

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.test_box_overlap_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }
}
