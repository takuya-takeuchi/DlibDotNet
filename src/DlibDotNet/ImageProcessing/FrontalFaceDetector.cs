#if !LITE
using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class FrontalFaceDetector : DlibObject
    {

        #region Constructors

        internal FrontalFaceDetector(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion
        
        #region Methods

        public Rectangle[] Operator(Array2DBase image, double threshold = 0d)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var dets = new StdVector<Rectangle>())
            {
                var inType = image.ImageType.ToNativeArray2DType();
                var ret = NativeMethods.frontal_face_detector_operator(this.NativePtr, inType, image.NativePtr, threshold, dets.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return dets.ToArray();
            }
        }

        public Rectangle[] Operator(MatrixBase image, double threshold = 0d)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var dets = new StdVector<Rectangle>())
            {
                var inType = image.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.frontal_face_detector_matrix_operator(this.NativePtr, inType, image.NativePtr, threshold, dets.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return dets.ToArray();
            }
        }

        public void Operator(MatrixBase image, out IEnumerable<RectDetection> detections, double threshold = 0d)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var dets = new StdVector<RectDetection>())
            {
                var inType = image.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.frontal_face_detector_matrix_operator2(this.NativePtr,
                                                                               inType, 
                                                                               image.NativePtr,
                                                                               threshold,
                                                                               dets.NativePtr);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                detections = dets.ToArray();
            }
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

            NativeMethods.frontal_face_detector_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
#endif
