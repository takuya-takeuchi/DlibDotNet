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

        private readonly Imp<TScalar> _Imp;

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

            this._Imp = CreateImp(this._Parameter);
            this.NativePtr = ret;
        }

        public SvmCTrainer(TKernel kernelBase,
                           TScalar c)
        {
            if (kernelBase == null)
                throw new ArgumentNullException(nameof(kernelBase));

            kernelBase.ThrowIfDisposed();

            this._Parameter = new KernelBaseParameter(kernelBase.KernelType, kernelBase.SampleType, 0, 0);
            this._Imp = CreateImp(this._Parameter);
            this.NativePtr = this._Imp.Create(kernelBase.NativePtr, c);
        }

        #endregion

        #region Properties

        public int CacheSize
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.svm_c_trainer_get_cache_size(this._Parameter.KernelType.ToNativeKernelType(),
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

                var ret = NativeMethods.svm_c_trainer_set_cache_size(this._Parameter.KernelType.ToNativeKernelType(),
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
                this._Imp.GetCClass1(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Imp.SetCClass1(this.NativePtr, value);
            }
        }

        public TScalar CClass2
        {
            get
            {
                this.ThrowIfDisposed();
                this._Imp.GetCClass2(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Imp.SetCClass2(this.NativePtr, value);
            }
        }
        
        public TScalar Epsilon
        {
            get
            {
                this.ThrowIfDisposed();
                this._Imp.GetEpsilon(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Imp.SetEpsilon(this.NativePtr, value);
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
            this._Imp.SetC(this.NativePtr, c);
        }

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
                var err = NativeMethods.svm_c_trainer_train(this.Parameter.KernelType.ToNativeKernelType(),
                                                            this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                            this.NativePtr,
                                                            vectorX.NativePtr,
                                                            vectorY.NativePtr,
                                                            out var ret);

                return new DecisionFunction<TScalar, TKernel>(ret, this.Parameter);
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

            NativeMethods.svm_c_trainer_delete(this._Parameter.KernelType.ToNativeKernelType(),
                                               this._Parameter.SampleType.ToNativeMatrixElementType(),
                                               this._Parameter.TemplateRows,
                                               this._Parameter.TemplateColumns,
                                               this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Imp<TScalar> CreateImp(KernelBaseParameter parameter)
        {
            switch (parameter.SampleType)
            {
                case MatrixElementTypes.Float:
                    return new FloatImp(parameter) as Imp<TScalar>;
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

            public abstract IntPtr Create(IntPtr kernel, T c);

            public abstract void SetC(IntPtr trainer, T c);

            public abstract void SetCClass1(IntPtr trainer, T cClass1);

            public abstract void GetCClass1(IntPtr trainer, out T cClass1);

            public abstract void SetCClass2(IntPtr trainer, T cClass2);

            public abstract void GetCClass2(IntPtr trainer, out T cClass2);

            public abstract void SetEpsilon(IntPtr trainer, T cClass2);

            public abstract void GetEpsilon(IntPtr trainer, out T cClass2);

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

            public override IntPtr Create(IntPtr kernel, double c)
            {
                var error = NativeMethods.svm_c_trainer_new2_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                    this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                    kernel,
                                                                    c,
                                                                    out var ret);
                switch (error)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

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

            #endregion

        }

        private sealed class FloatImp : Imp<float>
        {

            #region Constructors

            public FloatImp(KernelBaseParameter parameter) :
                base(parameter)
            {
            }

            #endregion

            #region Methods

            public override IntPtr Create(IntPtr kernel, float c)
            {
                var error = NativeMethods.svm_c_trainer_new2_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                   this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                   kernel,
                                                                   c,
                                                                   out var ret);
                switch (error)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

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

            #endregion

        }

    }

}
