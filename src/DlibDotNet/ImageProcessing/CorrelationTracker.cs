using System;
using System.Runtime.InteropServices;
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
            this.NativePtr = Native.correlation_tracker_new(
                filterSize,
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
            
            var rect = Native.correlation_tracker_get_position(this.NativePtr);
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
                var ret = Native.correlation_tracker_start_track(this.NativePtr, inType, image.NativePtr, native.NativePtr);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
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
                var ret = Native.correlation_tracker_update(this.NativePtr, inType, image.NativePtr, native.NativePtr, out var confident);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
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
            var ret = Native.correlation_tracker_update2(this.NativePtr, inType, image.NativePtr, out var confident);
            switch (ret)
            {
                case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
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
                var ret = Native.correlation_tracker_update_noscale(this.NativePtr, inType, image.NativePtr, native.NativePtr, out var confident);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
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
            var ret = Native.correlation_tracker_update_noscale2(this.NativePtr, inType, image.NativePtr, out var confident);
            switch (ret)
            {
                case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }

            return confident;
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.correlation_tracker_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr correlation_tracker_new(
                uint filterSize,
                uint numScaleLevels,
                uint scaleWindowSize,
                double regularizerSpace,
                double nuSpace,
                double regularizerScale,
                double nuScale,
                double scalePyramidAlpha);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType correlation_tracker_start_track(
                IntPtr tracker,
                Dlib.Native.Array2DType imgType,
                IntPtr img,
                IntPtr p);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr correlation_tracker_get_position(IntPtr tracker);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType correlation_tracker_update_noscale(
                IntPtr tracker,
                Dlib.Native.Array2DType imgType,
                IntPtr img,
                IntPtr guess,
                out double confident);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType correlation_tracker_update_noscale2(
                IntPtr tracker,
                Dlib.Native.Array2DType imgType,
                IntPtr img,
                out double confident);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType correlation_tracker_update(
                IntPtr tracker,
                Dlib.Native.Array2DType imgType,
                IntPtr img,
                IntPtr guess,
                out double confident);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType correlation_tracker_update2(
                IntPtr tracker,
                Dlib.Native.Array2DType imgType,
                IntPtr img,
                out double confident);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void correlation_tracker_delete(IntPtr point);


        }

    }

}