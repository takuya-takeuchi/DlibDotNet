using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ReducedDecisionFunctionTrainer2<TScalar, TKernel, TTrainer> : Trainer<TScalar>
        where TScalar : struct
        where TKernel : KernelBase
        where TTrainer : Trainer<TScalar>
    {

        #region Fields

        private readonly KernelBaseParameter _Parameter;

        private readonly NativeMethods.SvmTrainerType _SvmTrainerType;

        private readonly Imp<TScalar> _Imp;

        #endregion

        #region Constructors

        public ReducedDecisionFunctionTrainer2(TTrainer trainer,
                                               uint numBaseVector, 
                                               double eps = 1e-3)
        {
            TrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                      out var svmTrainerType,
                                                      out var svmKernelType,
                                                      out var sampleType);

            if (!(numBaseVector > 0))
                throw new ArgumentOutOfRangeException(nameof(numBaseVector));
            if (!(eps > 0))
                throw new ArgumentOutOfRangeException(nameof(eps));
            
            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._SvmTrainerType = svmTrainerType;
            this._Imp = CreateImp(this._Parameter);
            var error = NativeMethods.reduced_decision_function_trainer2_new(this._Parameter.KernelType.ToNativeKernelType(),
                                                                             this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                             this._Parameter.TemplateRows,
                                                                             this._Parameter.TemplateColumns,
                                                                             svmTrainerType,
                                                                             trainer.NativePtr,
                                                                             numBaseVector,
                                                                             eps,
                                                                             out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.SvmTrainerNotSupport:
                    throw new ArgumentException($"{trainerType} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{svmKernelType} is not supported.");
            }

            this.NativePtr = ret;
        }

        internal ReducedDecisionFunctionTrainer2(IntPtr ptr,
                                                 bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            TrainerHelper.GetTypes<TScalar, TTrainer>(out _,
                                                      out var svmTrainerType,
                                                      out var svmKernelType,
                                                      out var sampleType);

            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._SvmTrainerType = svmTrainerType;
            this._Imp = CreateImp(this._Parameter);

            this.NativePtr = ptr;
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

            return this._Imp.Train(this.NativePtr, x, y);
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

            NativeMethods.reduced_decision_function_trainer2_delete(this._Parameter.KernelType.ToNativeKernelType(),
                                                                    this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                    this._Parameter.TemplateRows,
                                                                    this._Parameter.TemplateColumns,
                                                                    this._SvmTrainerType,
                                                                    this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Imp<TScalar> CreateImp(KernelBaseParameter parameter)
        {
            switch (parameter.SampleType)
            {
                case MatrixElementTypes.Double:
                    return new DoubleImp(parameter) as Imp<TScalar>;
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion

        #endregion

        private abstract class Imp<T>
            where T : struct
        {

            #region Constructors

            protected Imp(KernelBaseParameter parameter)
            {
                this.Parameter = parameter;
            }

            #endregion

            #region Properties

            protected KernelBaseParameter Parameter
            {
                get;
            }

            #endregion

            #region Methods

            public abstract DecisionFunction<T, TKernel> Train(IntPtr trainer, IEnumerable<Matrix<T>> x, IEnumerable<T> y);

            #endregion

        }

        private sealed class DoubleImp : Imp<double>
        {

            #region Constructors

            public DoubleImp(KernelBaseParameter parameter) :
                base(parameter)
            {
            }

            #endregion

            #region Methods

            public override DecisionFunction<double, TKernel> Train(IntPtr trainer, IEnumerable<Matrix<double>> x, IEnumerable<double> y)
            {
                using (var vectorX = new StdVector<Matrix<double>>(x))
                using (var vectorY = new StdVector<double>(y))
                {
                    var parameter = this.Parameter;

                    TrainerHelper.GetTypes<TScalar, TTrainer>(out var trainerType,
                                                              out var svmTrainerType,
                                                              out var svmKernelType,
                                                              out var sampleType);

                    var err = NativeMethods.reduced_decision_function_trainer2_train_double(svmKernelType.ToNativeKernelType(),
                                                                                            sampleType.ToNativeMatrixElementType(),
                                                                                            parameter.TemplateColumns,
                                                                                            parameter.TemplateRows,
                                                                                            svmTrainerType,
                                                                                            trainer,
                                                                                            vectorX.NativePtr,
                                                                                            vectorY.NativePtr,
                                                                                            out var ret);
                    switch (err)
                    {
                        case NativeMethods.ErrorType.SvmTrainerNotSupport:
                            throw new ArgumentException($"{trainerType} is not supported.");
                        case NativeMethods.ErrorType.SvmKernelNotSupport:
                            throw new ArgumentException($"{svmKernelType} is not supported.");
                        case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                            throw new ArgumentException($"{sampleType} is not supported.");
                        case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                            throw new ArgumentException($"{nameof(parameter.TemplateColumns)} or {nameof(parameter.TemplateRows)} is not supported.");
                    }

                    return new DecisionFunction<double, TKernel>(ret, this.Parameter);
                }
            }

            #endregion

        }

    }

}
