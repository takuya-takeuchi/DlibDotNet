using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

namespace DlibDotNet
{
    public sealed class DNNFaceRecognition : DlibObject
    {
        private ShapePredictor _shapePredictor;

        #region Constructors

        private DNNFaceRecognition(IntPtr ptr, ShapePredictor shapePredictor)
        {
            this.NativePtr = ptr;
            _shapePredictor = shapePredictor;
        }

        #endregion

        #region Methods
        public Matrix<float>[] DetectWithFrontalFaceDetector(Array2DBase image)
        {
            using (var detector = FrontalFaceDetector.GetFrontalFaceDetector())
            {
                var dets = detector.Detect(image);
                return Detect(image, dets);
            }
        }

        public Matrix<float>[] DetectWithDNNFaceDetector(Array2DBase image, DNNFaceDetectorBase detector)
        {
            var dets = detector.Detect(image);
            return Detect(image, dets.Select(d => d.Rect).ToArray());
        }

        public Matrix<float>[] Detect(Array2DBase image, Rectangle[] faces_rectangle)
        {
            using (var vector = new StdVector<Rectangle>(faces_rectangle))
            {
                return Detect(image, vector);
            }
        }

        public Matrix<float>[] Detect(Array2DBase image, StdVector<Rectangle> faces_rectangle)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            using (var dets = new StdVector<Matrix<float>>())
            {
                var inType = image.ImageType.ToNativeArray2DType();
                var ret = Native.dnn_face_recognition_operator(this.NativePtr, _shapePredictor.NativePtr,
                    image.NativePtr, faces_rectangle.NativePtr, dets.NativePtr);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.InputArrayTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return dets.ToArray();
            }
        }

        public static DNNFaceRecognition GetDNNFaceRecognition(string resnet_model_filename, string sharp_model_filename)
        {
            if (!System.IO.File.Exists(resnet_model_filename))
            {
                throw new System.IO.FileNotFoundException(null, resnet_model_filename);
            }
            if (!System.IO.File.Exists(sharp_model_filename))
            {
                throw new System.IO.FileNotFoundException(null, sharp_model_filename);
            }
            var str = Encoding.UTF8.GetBytes(resnet_model_filename);
            var ret = Native.get_dnn_face_recognition(str);
            var ret2 = new ShapePredictor(sharp_model_filename);
            return new DNNFaceRecognition(ret, ret2);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            _shapePredictor.Dispose();
            base.DisposeUnmanaged();
            Native.dnn_face_recognition_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr get_dnn_face_recognition(byte[] model_filename);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType dnn_face_recognition_operator(
                IntPtr detector,
                IntPtr shape_predictor,
                IntPtr img,
                IntPtr faces_rectangle,
                IntPtr dets);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void dnn_face_recognition_delete(IntPtr point);

        }
    }
}
