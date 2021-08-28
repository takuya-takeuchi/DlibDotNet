#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class RectDetection : DlibObject
    {

        #region Constructors

        internal RectDetection(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public double DetectionConfidence
        {
            get
            {
                this.ThrowIfDisposed();
                var detectionConfidence = NativeMethods.rect_detection_get_detection_confidence(this.NativePtr);
                return detectionConfidence;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.rect_detection_set_detection_confidence(this.NativePtr, value);
            }
        }

        public Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                var rect = NativeMethods.rect_detection_get_rect(this.NativePtr);
                return new Rectangle(rect);
            }
            set
            {
                this.ThrowIfDisposed();
                using(var native = value.ToNative())
                    NativeMethods.rect_detection_set_rect(this.NativePtr, native.NativePtr);
            }
        }

        public ulong WeightIndex
        {
            get
            {
                this.ThrowIfDisposed();
                var weightIndex = NativeMethods.rect_detection_get_weight_index(this.NativePtr);
                return weightIndex;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.rect_detection_set_weight_index(this.NativePtr, value);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.rect_detection_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }
}

#endif
