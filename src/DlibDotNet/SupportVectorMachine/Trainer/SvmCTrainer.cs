using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class SvmCTrainer<TScalar, TKernel> : Trainer<TScalar>
        where TScalar : struct
        where TKernel : KernelBase
    {

        #region Fields

        private readonly KernelBaseParameter _Parameter;

        private readonly Bridge<TScalar> _Bridge;

        #endregion

        #region Constructors

        public SvmCTrainer()
        {
            var kernelType = typeof(TKernel).GetGenericTypeDefinition();
            if (!KernelTypesRepository.KernelTypes.TryGetValue(kernelType, out var svmKernelType))
                throw new ArgumentException();
            var elementType = typeof(TKernel).GenericTypeArguments[0];
            if (!KernelTypesRepository.ElementTypes.TryGetValue(elementType, out var sampleType))
                throw new ArgumentException();

            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            var error = NativeMethods.svm_c_trainer_new(svmKernelType.ToNativeKernelType(),
                                                        sampleType.ToNativeMatrixElementType(),
                                                        out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{this._Parameter.SampleType} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{this._Parameter.KernelType} is not supported.");
            }

            this._Bridge = CreateBridge(this._Parameter);
            this.NativePtr = ret;
        }

        public SvmCTrainer(TKernel kernelBase,
                           float c)
        {
            if (kernelBase == null)
                throw new ArgumentNullException(nameof(kernelBase));

            kernelBase.ThrowIfDisposed();

            this._Parameter = new KernelBaseParameter(kernelBase.KernelType, kernelBase.SampleType, 0, 0);
            var error = NativeMethods.svm_c_trainer_new2(kernelBase.KernelType.ToNativeKernelType(),
                                                         kernelBase.SampleType.ToNativeMatrixElementType(),
                                                         kernelBase.NativePtr,
                                                         c,
                                                         out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{this._Parameter.SampleType} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{this._Parameter.KernelType} is not supported.");
            }

            this._Bridge = CreateBridge(this._Parameter);
            this.NativePtr = ret;
        }

        #endregion

        #region Properties

        public int CacheSize
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.svm_c_trainer_get_cache_size_double(this._Parameter.KernelType.ToNativeKernelType(),
                                                                            this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                            this.NativePtr,
                                                                            out var cacheSize);
                return cacheSize;
            }
            set
            {
                this.ThrowIfDisposed();

                if (value <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_cache_size_double(this._Parameter.KernelType.ToNativeKernelType(),
                                                                            this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                            this.NativePtr,
                                                                            value);
            }
        }

        public TScalar CClass1
        {
            get
            {
                this.ThrowIfDisposed();
                this._Bridge.GetCClass1(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Bridge.SetCClass1(this.NativePtr, value);
            }
        }

        public TScalar CClass2
        {
            get
            {
                this.ThrowIfDisposed();
                this._Bridge.GetCClass2(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Bridge.SetCClass2(this.NativePtr, value);
            }
        }

        public TKernel Kernel
        {
            get
            {
                this.ThrowIfDisposed();
                var error = NativeMethods.svm_c_trainer_get_kernel(this._Parameter.KernelType.ToNativeKernelType(),
                                                                   this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                   this.NativePtr,
                                                                   out var ret);
                return KernelFactory.Create<TKernel, TScalar, Matrix<TScalar>>(ret,
                                                                               this._Parameter.KernelType,
                                                                               this._Parameter.TemplateRows,
                                                                               this._Parameter.TemplateColumns,
                                                                               false);
            }
            set
            {
                this.ThrowIfDisposed();

                if (value == null)
                    throw new ArgumentNullException();
                value.ThrowIfDisposed();

                var error = NativeMethods.svm_c_trainer_set_kernel(this._Parameter.KernelType.ToNativeKernelType(),
                                                                   this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                                   this.NativePtr,
                                                                   value.NativePtr);
            }
        }

        internal KernelBaseParameter Parameter => this._Parameter;

        #endregion

        #region Methods

        public void SetC(TScalar c)
        {
            this.ThrowIfDisposed();
            this._Bridge.SetC(this.NativePtr, c);
        }

        public DecisionFunction<TScalar, TKernel> Train(IEnumerable<Matrix<TScalar>> x, IEnumerable<TScalar> y)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));
            if (y == null)
                throw new ArgumentNullException(nameof(y));

            this.ThrowIfDisposed();
            x.ThrowIfDisposed();

            return this._Bridge.Train(this.NativePtr, x, y);
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

            NativeMethods.svm_c_trainer_delete(this._Parameter.KernelType.ToNativeKernelType(),
                                               this._Parameter.SampleType.ToNativeMatrixElementType(),
                                               this._Parameter.TemplateRows,
                                               this._Parameter.TemplateColumns,
                                               this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<TScalar> CreateBridge(KernelBaseParameter parameter)
        {
            switch (parameter.SampleType)
            {
                case MatrixElementTypes.Float:
                    return new FloatBridge(parameter) as Bridge<TScalar>;
                case MatrixElementTypes.Double:
                    return new DoubleBridge(parameter) as Bridge<TScalar>;
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

            protected Bridge(KernelBaseParameter parameter)
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

            public abstract void SetC(IntPtr trainer, T c);

            public abstract void SetCClass1(IntPtr trainer, T cClass1);

            public abstract void GetCClass1(IntPtr trainer, out T cClass1);

            public abstract void SetCClass2(IntPtr trainer, T cClass2);

            public abstract void GetCClass2(IntPtr trainer, out T cClass2);

            public abstract void SetEpsilon(IntPtr trainer, T cClass2);

            public abstract void GetEpsilon(IntPtr trainer, out T cClass2);

            public abstract DecisionFunction<T, TKernel> Train(IntPtr trainer, IEnumerable<Matrix<T>> x, IEnumerable<T> y);

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Constructors

            public DoubleBridge(KernelBaseParameter parameter) :
                base(parameter)
            {
            }

            #endregion

            #region Methods

            public override void SetC(IntPtr trainer, double c)
            {
                if (c <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(c)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_c_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                   this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                   trainer,
                                                                   c);
            }

            public override void SetCClass1(IntPtr trainer, double cClass1)
            {
                if (cClass1 <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(cClass1)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_c_class1_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                          this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                          trainer,
                                                                          cClass1);
            }

            public override void GetCClass1(IntPtr trainer, out double cClass1)
            {
                var ret = NativeMethods.svm_c_trainer_get_c_class1_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                          this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                          trainer,
                                                                          out cClass1);
            }

            public override void SetCClass2(IntPtr trainer, double cClass2)
            {
                if (cClass2 <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(cClass2)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_c_class2_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                          this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                          trainer,
                                                                          cClass2);
            }

            public override void GetCClass2(IntPtr trainer, out double cClass2)
            {
                var ret = NativeMethods.svm_c_trainer_get_c_class2_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                          this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                          trainer,
                                                                          out cClass2);
            }

            public override void SetEpsilon(IntPtr trainer, double epsilon)
            {
                if (epsilon <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(epsilon)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_epsilon_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         epsilon);
            }

            public override void GetEpsilon(IntPtr trainer, out double epsilon)
            {
                var ret = NativeMethods.svm_c_trainer_get_epsilon_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         out epsilon);
            }

            public override DecisionFunction<double, TKernel> Train(IntPtr trainer, IEnumerable<Matrix<double>> x, IEnumerable<double> y)
            {
                using (var vectorX = new StdVector<Matrix<double>>(x))
                using (var vectorY = new StdVector<double>(y))
                {
                    var err = NativeMethods.svm_c_trainer_train_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                       this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                       trainer,
                                                                       vectorX.NativePtr,
                                                                       vectorY.NativePtr,
                                                                       out var ret);
                    return new DecisionFunction<double, TKernel>(ret, this.Parameter);
                }
            }

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Constructors

            public FloatBridge(KernelBaseParameter parameter) :
                base(parameter)
            {
            }

            #endregion

            #region Methods

            public override void SetC(IntPtr trainer, float c)
            {
                if (c <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(c)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_c_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                  this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                  trainer,
                                                                  c);
            }

            public override void SetCClass1(IntPtr trainer, float cClass1)
            {
                if (cClass1 <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(cClass1)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_c_class1_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         cClass1);
            }

            public override void GetCClass1(IntPtr trainer, out float cClass1)
            {
                var ret = NativeMethods.svm_c_trainer_get_c_class1_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         out cClass1);
            }

            public override void SetCClass2(IntPtr trainer, float cClass2)
            {
                if (cClass2 <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(cClass2)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_c_class2_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         cClass2);
            }

            public override void GetCClass2(IntPtr trainer, out float cClass2)
            {
                var ret = NativeMethods.svm_c_trainer_get_c_class2_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         out cClass2);
            }

            public override void SetEpsilon(IntPtr trainer, float epsilon)
            {
                if (epsilon <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(epsilon)} must be greater than 0.");

                var ret = NativeMethods.svm_c_trainer_set_epsilon_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                        this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                        trainer,
                                                                        epsilon);
            }

            public override void GetEpsilon(IntPtr trainer, out float epsilon)
            {
                var ret = NativeMethods.svm_c_trainer_get_epsilon_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                        this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                        trainer,
                                                                        out epsilon);
            }

            public override DecisionFunction<float, TKernel> Train(IntPtr trainer, IEnumerable<Matrix<float>> x, IEnumerable<float> y)
            {
                using (var vectorX = new StdVector<Matrix<float>>(x))
                using (var vectorY = new StdVector<float>(y))
                {
                    var err = NativeMethods.svm_c_trainer_train_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                      this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                      trainer,
                                                                      vectorX.NativePtr,
                                                                      vectorY.NativePtr,
                                                                      out var ret);
                    return new DecisionFunction<float, TKernel>(ret, this.Parameter);
                }
            }

            #endregion

        }

    }

}
