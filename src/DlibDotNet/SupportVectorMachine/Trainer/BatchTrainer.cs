using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class BatchTrainer<TScalar, TKernel, TTrainer> : Trainer<TScalar>
        where TScalar : struct
        where TKernel : KernelBase
        where TTrainer : Trainer<TScalar>
    {

        #region Fields

        private readonly KernelBaseParameter _Parameter;

        private readonly NativeMethods.SvmBatchTrainerType _SvmTrainerType;

        private readonly Bridge<TScalar> _Bridge;

        #endregion

        #region Constructors

        public BatchTrainer()
        {
            BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                           out var svmTrainerType,
                                                           out var svmKernelType,
                                                           out var sampleType);

            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._SvmTrainerType = svmTrainerType;
            this._Bridge = CreateBridge(this._Parameter, this._SvmTrainerType);
            var error = NativeMethods.batch_trainer_new(this._Parameter.KernelType.ToNativeKernelType(),
                                                        this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                        svmTrainerType,
                                                        out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{sampleType} is not supported.");
                case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                    throw new ArgumentException($"{svmTrainerType} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{svmKernelType} is not supported.");
            }

            this.NativePtr = ret;
        }

        public BatchTrainer(TTrainer trainer,
                            TScalar minLearningRate,
                            bool verbose,
                            bool useCache,
                            int cacheSize)
        {
            trainer.ThrowIfDisposed();

            BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                           out var svmTrainerType,
                                                           out var svmKernelType,
                                                           out var sampleType);

            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._SvmTrainerType = svmTrainerType;
            this._Bridge = CreateBridge(this._Parameter, svmTrainerType);
            this.NativePtr = this._Bridge.Create(trainer.NativePtr, minLearningRate, verbose, useCache, cacheSize);
        }

        internal BatchTrainer(IntPtr ptr,
                              bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            BatchTrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                           out var svmTrainerType,
                                                           out var svmKernelType,
                                                           out var sampleType);

            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._SvmTrainerType = svmTrainerType;
            this._Bridge = CreateBridge(this._Parameter, this._SvmTrainerType);
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public TScalar MinLearningRate
        {
            get
            {
                this.ThrowIfDisposed();

                return this._Bridge.GetMinLearningRate(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public DecisionFunction<TScalar, TKernel> Train(IEnumerable<Matrix<TScalar>> x, IEnumerable<TScalar> y)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (y == null)
                throw new ArgumentNullException(nameof(y));

            this.ThrowIfDisposed();
            x.ThrowIfDisposed();

            using (var vectorX = new StdVector<Matrix<TScalar>>(x))
            using (var vectorY = new StdVector<TScalar>(y))
            {
                var parameter = this._Parameter;
                var svmKernelType = parameter.KernelType;
                var sampleType = parameter.SampleType;
                var svmTrainerType = this._SvmTrainerType;
                var err = NativeMethods.batch_trainer_train(svmKernelType.ToNativeKernelType(),
                                                            sampleType.ToNativeMatrixElementType(),
                                                            svmTrainerType,
                                                            this.NativePtr,
                                                            vectorX.NativePtr,
                                                            vectorY.NativePtr,
                                                            out var ret);
                switch (err)
                {
                    case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                        throw new ArgumentException($"{svmTrainerType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{svmKernelType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{sampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(parameter.TemplateColumns)} or {nameof(parameter.TemplateRows)} is not supported.");
                }

                return new DecisionFunction<TScalar, TKernel>(ret, parameter);
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

            NativeMethods.batch_trainer_delete(this._Parameter.KernelType.ToNativeKernelType(),
                                               this._Parameter.SampleType.ToNativeMatrixElementType(),
                                               this._SvmTrainerType,
                                               this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<TScalar> CreateBridge(KernelBaseParameter parameter, NativeMethods.SvmBatchTrainerType trainerType)
        {
            switch (parameter.SampleType)
            {
                case MatrixElementTypes.Float:
                    return new FloatBridge(parameter, trainerType) as Bridge<TScalar>;
                case MatrixElementTypes.Double:
                    return new DoubleBridge(parameter, trainerType) as Bridge<TScalar>;
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion

        #endregion

        private abstract class Bridge<T>
            where T : struct
        {

            #region Constructors

            protected Bridge(KernelBaseParameter parameter, NativeMethods.SvmBatchTrainerType trainerType)
            {
                this.Parameter = parameter;
                this.TrainerType = trainerType;
            }

            #endregion

            #region Properties

            protected KernelBaseParameter Parameter
            {
                get;
            }

            protected NativeMethods.SvmBatchTrainerType TrainerType
            {
                get;
            }

            #endregion

            #region Methods

            public abstract IntPtr Create(IntPtr trainer, T minLearningRate, bool verbose, bool useCache, int cacheSize);

            public abstract T GetMinLearningRate(IntPtr trainer);

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Constructors

            public FloatBridge(KernelBaseParameter parameter, NativeMethods.SvmBatchTrainerType trainerType) :
                base(parameter, trainerType)
            {
            }

            #endregion

            #region Methods

            public override IntPtr Create(IntPtr trainer, float minLearningRate, bool verbose, bool useCache, int cacheSize)
            {
                if (!(minLearningRate > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(minLearningRate)} must be greater than 0.");
                if (!(cacheSize > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(cacheSize)} must be greater than 0.");

                var err = NativeMethods.batch_trainer_new2_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                 this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                 this.TrainerType,
                                                                 trainer,
                                                                 minLearningRate,
                                                                 verbose,
                                                                 useCache,
                                                                 cacheSize,
                                                                 out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                        throw new ArgumentException($"{TrainerType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            public override float GetMinLearningRate(IntPtr trainer)
            {
                var err = NativeMethods.batch_trainer_get_min_learning_rate_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                                  this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                                  this.TrainerType,
                                                                                  trainer,
                                                                                  out var ret);

                return ret;
            }
            
            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Constructors

            public DoubleBridge(KernelBaseParameter parameter, NativeMethods.SvmBatchTrainerType trainerType) :
                base(parameter, trainerType)
            {
            }

            #endregion

            #region Methods

            public override IntPtr Create(IntPtr trainer, double minLearningRate, bool verbose, bool useCache, int cacheSize)
            {
                if (!(minLearningRate > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(minLearningRate)} must be greater than 0.");
                if (!(cacheSize > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(cacheSize)} must be greater than 0.");

                var err = NativeMethods.batch_trainer_new2_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                  this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                  this.TrainerType,
                                                                  trainer,
                                                                  minLearningRate,
                                                                  verbose,
                                                                  useCache,
                                                                  cacheSize,
                                                                  out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.SvmBatchTrainerNotSupport:
                        throw new ArgumentException($"{TrainerType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            public override double GetMinLearningRate(IntPtr trainer)
            {
                var err = NativeMethods.batch_trainer_get_min_learning_rate_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                                   this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                                   this.TrainerType,
                                                                                   trainer,
                                                                                   out var ret);

                return ret;
            }

            #endregion

        }

    }

}
