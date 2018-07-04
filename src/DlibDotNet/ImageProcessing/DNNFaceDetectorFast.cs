using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

namespace DlibDotNet
{
    //https://github.com/davisking/dlib/blob/master/examples/dnn_mmod_face_detection_ex.cpp
    public sealed class DNNFaceDetectorFast : DNNFaceDetectorBase
    {
        #region Constructors

        private DNNFaceDetectorFast(IntPtr ptr)
            : base(ptr)
        {
        }

        #endregion

        #region Methods

        public override MModRect[] Detect(Array2DBase image, int upsampleNumTimes = 0)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var dets = new StdVector<MModRect>())
            {
                var inType = image.ImageType.ToNativeArray2DType();
                var ret = Native.dnn_mmod_face_detector_fast_operator(this.NativePtr, inType, image.NativePtr, upsampleNumTimes, dets.NativePtr);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return dets.ToArray();
            }
        }

        public static DNNFaceDetectorFast GetDNNFaceDetectorFast(string model_filename)
        {
            if (!System.IO.File.Exists(model_filename))
            {
                throw new System.IO.FileNotFoundException(null, model_filename);
            }
            var str = Encoding.UTF8.GetBytes(model_filename);
            var ret = Native.get_dnn_mmod_face_detector_fast(str);
            return new DNNFaceDetectorFast(ret);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.dnn_mmod_face_detector_fast_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr get_dnn_mmod_face_detector_fast(byte[] model_filename);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType dnn_mmod_face_detector_fast_operator(
                IntPtr detector,
                Dlib.Native.Array2DType imgType,
                IntPtr img,
                int upsampleNumTimes,
                IntPtr dets);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void dnn_mmod_face_detector_fast_delete(IntPtr point);

        }
    }
}
