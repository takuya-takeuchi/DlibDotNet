using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ShapePredictor : DlibObject
    {

        #region Constructors

        public ShapePredictor()
        {
            this.NativePtr = Native.shape_predictor_new();
        }

        internal ShapePredictor(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public uint Features
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.shape_predictor_num_parts(this.NativePtr);
            }
        }

        public uint Parts
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.shape_predictor_num_parts(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public static ShapePredictor Deserialize(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);
            var ptr = Native.deserialize_shape_predictor(str);
            return new ShapePredictor(ptr);
        }

        public static ShapePredictor Deserialize(ProxyDeserialize deserialize)
        {
            if (deserialize == null)
                throw new ArgumentNullException(nameof(deserialize));

            deserialize.ThrowIfDisposed();

            var ret = Native.deserialize_shape_predictor_proxy(deserialize.NativePtr);
            return new ShapePredictor(ret);
        }

        public FullObjectDetection Detect(Array2DBase image, Rectangle rect)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            var inType = image.ImageType.ToNativeArray2DType();
            using (var native = rect.ToNative())
            {
                var ret = Native.shape_predictor_operator(this.NativePtr, inType, image.NativePtr, native.NativePtr, out var fullObjDetect);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return new FullObjectDetection(fullObjDetect);
            }
        }

        public FullObjectDetection Detect(Array2DBase image, MModRect rect)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));

            image.ThrowIfDisposed();
            rect.ThrowIfDisposed();

            var inType = image.ImageType.ToNativeArray2DType();
            var ret = Native.shape_predictor_operator_mmod_rect(this.NativePtr,
                                                                inType, 
                                                                image.NativePtr,
                                                                rect.NativePtr, 
                                                                out var fullObjDetect);
            switch (ret)
            {
                case Dlib.Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }

            return new FullObjectDetection(fullObjDetect);
        }
        
        public FullObjectDetection Detect(MatrixBase image, Rectangle rect)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();

            var inType = image.MatrixElementType.ToNativeMatrixElementType();
            using (var native = rect.ToNative())
            {
                var ret = Native.shape_predictor_matrix_operator(this.NativePtr,
                                                                 inType,
                                                                 image.NativePtr,
                                                                 native.NativePtr,
                                                                 out var fullObjDetect);
                switch (ret)
                {
                    case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"Input {inType} is not supported.");
                }

                return new FullObjectDetection(fullObjDetect);
            }
        }

        public FullObjectDetection Detect(MatrixBase image, MModRect rect)
        {
            this.ThrowIfDisposed();

            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));

            image.ThrowIfDisposed();
            rect.ThrowIfDisposed();

            var inType = image.MatrixElementType.ToNativeMatrixElementType();
            var ret = Native.shape_predictor_matrix_operator_mmod_rect(this.NativePtr,
                                                                       inType,
                                                                       image.NativePtr,
                                                                       rect.NativePtr,
                                                                       out var fullObjDetect);
            switch (ret)
            {
                case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }

            return new FullObjectDetection(fullObjDetect);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.shape_predictor_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr shape_predictor_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint shape_predictor_num_parts(IntPtr predictor);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint shape_predictor_num_features(IntPtr predictor);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr deserialize_shape_predictor(byte[] filName);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr deserialize_shape_predictor_proxy(IntPtr proxy_deserialize);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType shape_predictor_operator(IntPtr detector,
                                                                                Dlib.Native.Array2DType imgType,
                                                                                IntPtr img,
                                                                                IntPtr rect,
                                                                                out IntPtr fullObjDetect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType shape_predictor_matrix_operator(IntPtr detector,
                                                                                       Dlib.Native.MatrixElementType imgType,
                                                                                       IntPtr img,
                                                                                       IntPtr rect,
                                                                                       out IntPtr fullObjDetect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType shape_predictor_operator_mmod_rect(IntPtr detector,
                                                                                          Dlib.Native.Array2DType imgType,
                                                                                          IntPtr img,
                                                                                          IntPtr rect,
                                                                                          out IntPtr fullObjDetect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Dlib.Native.ErrorType shape_predictor_matrix_operator_mmod_rect(IntPtr detector,
                                                                                                 Dlib.Native.MatrixElementType imgType,
                                                                                                 IntPtr img,
                                                                                                 IntPtr rect,
                                                                                                 out IntPtr fullObjDetect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void shape_predictor_delete(IntPtr point);

        }

    }

}
