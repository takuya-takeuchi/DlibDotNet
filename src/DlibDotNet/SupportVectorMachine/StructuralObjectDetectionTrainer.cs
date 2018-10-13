using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StructuralObjectDetectionTrainer<T> : DlibObject
        where T : ImageScanner
    {

        #region Fields

        private readonly TrainerImp _Imp;

        #endregion

        #region Constructors

        public StructuralObjectDetectionTrainer(T scanner)
        {
            if (scanner == null)
                throw new ArgumentNullException(nameof(scanner));

            this._Imp = CreateImp(scanner);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void BeVerbose()
        {
            this.ThrowIfDisposed();

            this._Imp.BeVerbose();
        }

        public void SetC(double c)
        {
            this.ThrowIfDisposed();

            this._Imp.SetC(c);
        }

        public void SetEpsilon(double epsilon)
        {
            this.ThrowIfDisposed();

            this._Imp.SetEpsilon(epsilon);
        }

        public void SetNumThreads(uint threads)
        {
            this.ThrowIfDisposed();

            this._Imp.SetNumThreads(threads);
        }

        public ObjectDetector<T> Train<U>(IEnumerable<Matrix<U>> images, IEnumerable<IEnumerable<Rectangle>> objects)
            where U : struct
        {
            this.ThrowIfDisposed();

            return this._Imp.Train(images, objects);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            this._Imp?.Dispose();
        }

        #endregion

        #region Helpers

        private static TrainerImp CreateImp(T scanner)
        {
            switch (scanner.ScannerType)
            {
                case ImageScannerType.FHogPyramid:
                    return new ScanFHogPyramidTrainer(scanner);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #endregion

        private abstract class TrainerImp : DlibObject
        {

            #region Constructors

            protected TrainerImp(ImageScanner scanner)
            {
                if (scanner == null)
                    throw new ArgumentNullException(nameof(scanner));

                this.Scanner = scanner;
            }

            #endregion

            #region Properties

            protected ImageScanner Scanner
            {
                get;
                private set;
            }

            #endregion

            #region Methods

            public abstract void BeVerbose();

            public abstract void SetC(double c);

            public abstract void SetEpsilon(double epsilon);

            public abstract void SetNumThreads(uint threads);

            public abstract ObjectDetector<T> Train<U>(IEnumerable<Matrix<U>> images, IEnumerable<IEnumerable<Rectangle>> objects)
                where U : struct;

            #endregion

        }

        private sealed class ScanFHogPyramidTrainer : TrainerImp
        {

            #region Fields

            private readonly Dlib.Native.FHogFeatureExtractorType _FeatureExtractorType;

            private readonly Dlib.Native.PyramidType _PyramidType;

            private readonly uint _PyramidRate;

            #endregion

            #region Constructors

            public ScanFHogPyramidTrainer(ImageScanner scanner)
                : base(scanner)
            {
                var param = scanner.GetFHogPyramidParameter();
                this._FeatureExtractorType = param.FeatureExtractorType;
                this._PyramidRate = param.PyramidRate;
                this._PyramidType = param.PyramidType;

                var ret = Dlib.Native.structural_object_detection_trainer_scan_fhog_pyramid_new(this._PyramidType,
                                                                                                this._PyramidRate,
                                                                                                this._FeatureExtractorType,
                                                                                                this.Scanner.NativePtr,
                                                                                                out var trainr);

                this.NativePtr = trainr;
            }

            #endregion

            #region Methods

            #region Overrids

            public override void BeVerbose()
            {
                Dlib.Native.structural_object_detection_trainer_scan_fhog_pyramid_be_verbose(this._PyramidType,
                                                                                             this._PyramidRate,
                                                                                             this._FeatureExtractorType,
                                                                                             this.NativePtr);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                Dlib.Native.structural_object_detection_trainer_scan_fhog_pyramid_delete(this._PyramidType,
                                                                                         this._PyramidRate,
                                                                                         this._FeatureExtractorType,
                                                                                         this.NativePtr);
            }

            public override void SetC(double c)
            {
                Dlib.Native.structural_object_detection_trainer_scan_fhog_pyramid_set_c(this._PyramidType,
                                                                                        this._PyramidRate,
                                                                                        this._FeatureExtractorType,
                                                                                        this.NativePtr,
                                                                                        c);
            }

            public override void SetEpsilon(double epsilon)
            {
                Dlib.Native.structural_object_detection_trainer_scan_fhog_pyramid_set_epsilon(this._PyramidType,
                                                                                              this._PyramidRate,
                                                                                              this._FeatureExtractorType,
                                                                                              this.NativePtr,
                                                                                              epsilon);
            }

            public override void SetNumThreads(uint threads)
            {
                Dlib.Native.structural_object_detection_trainer_scan_fhog_pyramid_set_num_threads(this._PyramidType,
                                                                                                  this._PyramidRate,
                                                                                                  this._FeatureExtractorType,
                                                                                                  this.NativePtr,
                                                                                                  threads);
            }

            public override ObjectDetector<T> Train<U>(IEnumerable<Matrix<U>> images, IEnumerable<IEnumerable<Rectangle>> objects)
            {
                using (var vecImage = new StdVector<Matrix<U>>(images))
                {
                    var tmp = objects.Select(rectangles => new StdVector<Rectangle>(rectangles));
                    using (var vecObject = new StdVector<StdVector<Rectangle>>(tmp))
                    {
                        Matrix<U>.TryParse<U>(out var matrixElementType);
                        var ret = Dlib.Native.structural_object_detection_trainer_scan_fhog_pyramid_train_rectangle(this._PyramidType,
                                                                                                                    this._PyramidRate,
                                                                                                                    this._FeatureExtractorType,
                                                                                                                    this.NativePtr,
                                                                                                                    matrixElementType.ToNativeMatrixElementType(),
                                                                                                                    vecImage.NativePtr,
                                                                                                                    vecObject.NativePtr,
                                                                                                                    out var detector);
                        switch (ret)
                        {
                            case Dlib.Native.ErrorType.MatrixElementTypeNotSupport:
                                throw new ArgumentException($"{matrixElementType} is not supported.");
                        }

                        return new ObjectDetector<T>(detector, new ImageScanner.FHogPyramidParameter(this._PyramidType, this._PyramidRate, this._FeatureExtractorType));
                    }
                }
            }

            #endregion

            #endregion

        }

    }

}
