#if !LITE
using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class CorrelationTracker : DlibObject
    {

        #region Constructors

        public CorrelationTracker(
            uint filterSize = 6,
            uint numScaleLevels = 5,
            uint scaleWindowSize = 23,
            double regularizerSpace = 0.001,
            double nuSpace = 0.025,
            double regularizerScale = 0.001,
            double nuScale = 0.025,
            double scalePyramidAlpha = 1.020)
        {
            this.NativePtr = NativeMethods.correlation_tracker_new(filterSize,
                                                                   numScaleLevels,
                                                                   scaleWindowSize,
                                                                   regularizerSpace,
                                                                   nuSpace,
                                                                   regularizerScale,
                                                                   nuScale,
                                                                   scalePyramidAlpha);
        }

        #endregion

        #region Methods

        public DRectangle GetPosition()
        {
            this.ThrowIfDisposed();
            
            var rect = NativeMethods.correlation_tracker_get_position(this.NativePtr);
            return new DRectangle(rect);
        }

        public void StartTrack(Array2DBase image, DRectangle rect)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if(rect.IsEmpty)
                throw new ArgumentException($"{nameof(rect)} must not be empty");

            var inType = image.ImageType.ToNativeArray2DType();
            using (var native = rect.ToNative())
            {
                var ret = NativeMethods.correlation_tracker_start_track(this.NativePtr, inType, image.NativePtr, native.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }
            }
        }

        public double Update(Array2DBase image, DRectangle guess)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (guess.IsEmpty)
                throw new ArgumentException($"{nameof(guess)} must not be empty");

            var inType = image.ImageType.ToNativeArray2DType();
            using (var native = guess.ToNative())
            {
                var ret = NativeMethods.correlation_tracker_update(this.NativePtr, inType, image.NativePtr, native.NativePtr, out var confident);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return confident;
            }
        }

        public double Update(Array2DBase image)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            var inType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.correlation_tracker_update2(this.NativePtr, inType, image.NativePtr, out var confident);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }

            return confident;
        }

        public double UpdateNoscale(Array2DBase image, DRectangle guess)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            if (guess.IsEmpty)
                throw new ArgumentException($"{nameof(guess)} must not be empty");

            var inType = image.ImageType.ToNativeArray2DType();
            using (var native = guess.ToNative())
            {
                var ret = NativeMethods.correlation_tracker_update_noscale(this.NativePtr, inType, image.NativePtr, native.NativePtr, out var confident);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return confident;
            }
        }

        public double UpdateNoscale(Array2DBase image)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            var inType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.correlation_tracker_update_noscale2(this.NativePtr, inType, image.NativePtr, out var confident);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }

            return confident;
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

            NativeMethods.correlation_tracker_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
#endif
