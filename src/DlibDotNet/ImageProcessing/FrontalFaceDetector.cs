using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class FrontalFaceDetector : DlibObject
    {

        #region Constructors

        private FrontalFaceDetector(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion
        
        #region Methods

        public Rectangle[] Detect(Array2DBase image, double threshold = 0d)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var dets = new StdVectorOfRectangle())
            {
                var inType = image.ImageType.ToNativeArray2DType();
                var ret = Native.frontal_face_detector_operator(this.NativePtr, inType, image.NativePtr, threshold, dets.NativePtr);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return dets.ToArray();
            }
        }

        public static FrontalFaceDetector GetFrontalFaceDetector()
        {
            var ret = Native.get_frontal_face_detector();
            return new FrontalFaceDetector(ret);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.frontal_face_detector_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr get_frontal_face_detector();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType frontal_face_detector_operator(
                IntPtr detector,
                Dlib.Native.Array2DType imgType,
                IntPtr img,
                double adjustThreshold,
                IntPtr dets);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void frontal_face_detector_delete(IntPtr point);

        }

    }

}