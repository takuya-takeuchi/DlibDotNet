using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ObjectDetector<T> : DlibObject
        where T : ImageScanner
    {

        #region Fields

        private readonly DetectorImp _Imp;

        private readonly ImageScanner.FHogPyramidParameter _FHogPyramidParameter;

        #endregion

        #region Constructors

        public ObjectDetector(T scanner)
        {
            if (scanner == null)
                throw new ArgumentNullException(nameof(scanner));

            var fhogParam = scanner.GetFHogPyramidParameter();
            if (fhogParam != null)
            {
                this._Imp = new ScanFHogPyramidDetector(fhogParam);
                this._FHogPyramidParameter = fhogParam;
                this.NativePtr = this._Imp.NativePtr;
                return;
            }

            throw new NotSupportedException();
        }

        internal ObjectDetector(IntPtr ptr, ImageScanner.FHogPyramidParameter parameter)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            // Dispose on DetectorImp class
            //this.NativePtr = ptr;

            this._Imp = new ScanFHogPyramidDetector(ptr, parameter);
            this._FHogPyramidParameter = parameter;
            this.NativePtr = this._Imp.NativePtr;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void Deserialize(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            this.ThrowIfDisposed();

            var str = Encoding.UTF8.GetBytes(path);
            this._Imp.Deserialize(str);
        }

        internal Matrix<byte> DrawFHog(uint weightIndex = 0, int cellDrawSize = 15)
        {
            this.ThrowIfDisposed();

            return this._Imp.DrawFHog(weightIndex, cellDrawSize);
        }

        public IEnumerable<Rectangle> Operator<U>(Matrix<U> image)
            where U : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ThrowIfDisposed();
            this.ThrowIfDisposed();

            return this._Imp.Operator(image);
        }

        public void Serialize(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            this.ThrowIfDisposed();

            var str = Encoding.UTF8.GetBytes(path);
            this._Imp.Serialize(str);
        }

        public Matrix<double> TestObjectDetectionFunction<U>(IEnumerable<Matrix<U>> images, IEnumerable<IEnumerable<Rectangle>> objects)
            where U : struct
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            this.ThrowIfDisposed();
            images.ThrowIfDisposed();

            return this._Imp.TestObjectDetectionFunction(images, objects);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            this._Imp.Dispose();
        }

        #endregion

        #region Helpers

        internal ImageScanner.FHogPyramidParameter GetFHogPyramidParameter()
        {
            return this._FHogPyramidParameter;
        }
        #endregion

        #endregion

        private abstract class DetectorImp : DlibObject
        {

            #region Constructors

            protected DetectorImp(IntPtr detector)
            {
                this.NativePtr = detector;
            }

            #endregion

            #region Methods

            public abstract void Deserialize(byte[] filepath);

            public abstract Matrix<byte> DrawFHog(uint weightIndex = 0, int cellDrawSize = 15);

            public abstract IEnumerable<Rectangle> Operator<U>(Matrix<U> image)
                where U : struct;

            public abstract void Serialize(byte[] filepath);

            public abstract Matrix<double> TestObjectDetectionFunction<U>(IEnumerable<Matrix<U>> images, IEnumerable<IEnumerable<Rectangle>> objects)
                where U : struct;

            #endregion

        }

        private sealed class ScanFHogPyramidDetector : DetectorImp
        {

            #region Fields

            private readonly NativeMethods.FHogFeatureExtractorType _FeatureExtractorType;

            private readonly NativeMethods.PyramidType _PyramidType;

            private readonly uint _PyramidRate;

            #endregion

            #region Constructors

            public ScanFHogPyramidDetector(ImageScanner.FHogPyramidParameter parameter)
                : base(IntPtr.Zero)
            {
                this._FeatureExtractorType = parameter.FeatureExtractorType;
                this._PyramidRate = parameter.PyramidRate;
                this._PyramidType = parameter.PyramidType;

                var ret = NativeMethods.object_detector_scan_fhog_pyramid_new(this._PyramidType,
                                                                            this._PyramidRate,
                                                                            this._FeatureExtractorType,
                                                                            out var detector);

                switch (ret)
                {
                    case NativeMethods.ErrorType.FHogNotSupportExtractor:
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                    case NativeMethods.ErrorType.PyramidNotSupportType:
                        throw new NotSupportedException();
                }

                this.NativePtr = detector;
            }

            public ScanFHogPyramidDetector(IntPtr detector, ImageScanner.FHogPyramidParameter parameter)
                : base(detector)
            {
                this._FeatureExtractorType = parameter.FeatureExtractorType;
                this._PyramidRate = parameter.PyramidRate;
                this._PyramidType = parameter.PyramidType;
            }

            #endregion

            #region Methods

            #region Overrids

            public override void Deserialize(byte[] filepath)
            {
                var ret = NativeMethods.object_detector_scan_fhog_pyramid_deserialize(filepath,
                                                                                    this._PyramidType,
                                                                                    this._PyramidRate,
                                                                                    this._FeatureExtractorType,
                                                                                    this.NativePtr);

                switch (ret)
                {
                    case NativeMethods.ErrorType.FHogNotSupportExtractor:
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                    case NativeMethods.ErrorType.PyramidNotSupportType:
                        throw new NotSupportedException();
                }
            }

            public override Matrix<byte> DrawFHog(uint weightIndex = 0, int cellDrawSize = 15)
            {
                var ret = NativeMethods.draw_fhog_object_detector_scan_fhog_pyramid(this._PyramidType,
                                                                                    this._PyramidRate,
                                                                                    this._FeatureExtractorType,
                                                                                    this.NativePtr,
                                                                                    weightIndex,
                                                                                    cellDrawSize,
                                                                                    out var matrix);

                switch (ret)
                {
                    case NativeMethods.ErrorType.FHogNotSupportExtractor:
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                    case NativeMethods.ErrorType.PyramidNotSupportType:
                        throw new NotSupportedException();
                }

                return new Matrix<byte>(matrix);
            }

            public override IEnumerable<Rectangle> Operator<U>(Matrix<U> image)
            {
                if (image == null)
                    throw new ArgumentNullException(nameof(image));

                image.ThrowIfDisposed();

                var type = image.MatrixElementType.ToNativeMatrixElementType();
                var ret = NativeMethods.object_detector_scan_fhog_pyramid_operator(this._PyramidType,
                                                                                   this._PyramidRate,
                                                                                   this._FeatureExtractorType,
                                                                                   this.NativePtr,
                                                                                   type,
                                                                                   image.NativePtr,
                                                                                   out var matrix);
                switch (ret)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{type} is not supported.");
                }

                using (var tmp = new StdVector<Rectangle>(matrix))
                    return tmp.ToArray();
            }

            public override void Serialize(byte[] filepath)
            {
                var ret = NativeMethods.object_detector_scan_fhog_pyramid_serialize(filepath,
                                                                                    this._PyramidType,
                                                                                    this._PyramidRate,
                                                                                    this._FeatureExtractorType,
                                                                                    this.NativePtr);

                switch (ret)
                {
                    case NativeMethods.ErrorType.FHogNotSupportExtractor:
                    case NativeMethods.ErrorType.PyramidNotSupportRate:
                    case NativeMethods.ErrorType.PyramidNotSupportType:
                        throw new NotSupportedException();
                }
            }

            public override Matrix<double> TestObjectDetectionFunction<U>(IEnumerable<Matrix<U>> images, IEnumerable<IEnumerable<Rectangle>> objects)
            {
                using (var vecImage = new StdVector<Matrix<U>>(images))
                {
                    var tmp = objects.Select(rectangles => new StdVector<Rectangle>(rectangles));
                    using (var vecObject = new StdVector<StdVector<Rectangle>>(tmp))
                    {
                        Matrix<U>.TryParse<U>(out var matrixElementType);
                        var ret = NativeMethods.cross_validate_object_detection_trainer_scan_fhog_pyramid_test_object_detection_function_rectangle(this._PyramidType,
                                                                                                                                                   this._PyramidRate,
                                                                                                                                                   this._FeatureExtractorType,
                                                                                                                                                   this.NativePtr,
                                                                                                                                                   matrixElementType.ToNativeMatrixElementType(),
                                                                                                                                                   vecImage.NativePtr,
                                                                                                                                                   vecObject.NativePtr,
                                                                                                                                                   out var matrix);
                        switch (ret)
                        {
                            case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                                throw new ArgumentException($"{matrixElementType} is not supported.");
                        }

                        return new Matrix<double>(matrix, 1, 3);
                    }
                }
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.object_detector_scan_fhog_pyramid_delete(this._PyramidType,
                                                                       this._PyramidRate,
                                                                       this._FeatureExtractorType,
                                                                       this.NativePtr);
            }

            #endregion

            #endregion

        }

    }

}