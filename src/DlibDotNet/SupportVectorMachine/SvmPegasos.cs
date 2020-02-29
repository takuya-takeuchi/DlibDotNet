using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class SvmPegasos<TScalar, TKernel> : Trainer<TScalar>
        where TScalar : struct
        where TKernel : KernelBase
    {

        #region Fields

        private readonly KernelBaseParameter _Parameter;

        private readonly Bridge<TScalar> _Bridge;

        #endregion

        #region Constructors

        public SvmPegasos()
        {
            var kernelType = typeof(TKernel).GetGenericTypeDefinition();
            if (!KernelTypesRepository.KernelTypes.TryGetValue(kernelType, out var svmKernelType))
                throw new ArgumentException();
            var elementType = typeof(TKernel).GenericTypeArguments[0];
            if (!KernelTypesRepository.ElementTypes.TryGetValue(elementType, out var sampleType))
                throw new ArgumentException();

            this._Parameter = new KernelBaseParameter(svmKernelType, sampleType, 0, 0);
            this._Bridge = CreateBridge(this._Parameter);

            var error = NativeMethods.svm_pegasos_new(this._Parameter.KernelType.ToNativeKernelType(),
                                                      this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                      out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{this._Parameter.SampleType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(this._Parameter.TemplateColumns)} or {nameof(this._Parameter.TemplateRows)} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{this._Parameter.KernelType} is not supported.");
            }

            this.NativePtr = ret;
        }

        public SvmPegasos(TKernel kernelBase,
                          TScalar lambda,
                          TScalar tolerance,
                          uint maxNumSupportVector)
        {
            if (kernelBase == null)
                throw new ArgumentNullException(nameof(kernelBase));

            kernelBase.ThrowIfDisposed();

            this._Parameter = new KernelBaseParameter(kernelBase);
            this._Bridge = CreateBridge(this._Parameter);

            this.NativePtr = this._Bridge.Create(kernelBase, lambda, tolerance, maxNumSupportVector);
        }

        internal SvmPegasos(IntPtr ptr,
                            KernelBaseParameter parameter,
                            bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            this._Bridge = CreateBridge(parameter);

            this._Parameter = parameter;
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public TScalar LambdaClass1
        {
            get
            {
                this.ThrowIfDisposed();
                this._Bridge.GetLambdaClass1(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Bridge.SetLambdaClass1(this.NativePtr, value);
            }
        }

        public TScalar LambdaClass2
        {
            get
            {
                this.ThrowIfDisposed();
                this._Bridge.GetLambdaClass2(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Bridge.SetLambdaClass2(this.NativePtr, value);
            }
        }

        public uint MaxNumSupportVector
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.svm_pegasos_get_max_num_sv(this._Parameter.KernelType.ToNativeKernelType(),
                                                         this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                         this.NativePtr,
                                                         out var ret);

                return ret;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.svm_pegasos_set_max_num_sv(this._Parameter.KernelType.ToNativeKernelType(),
                                                         this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                         this.NativePtr,
                                                         value);
            }
        }

        internal KernelBaseParameter Parameter => this._Parameter;

        public TKernel Kernel
        {
            get
            {
                this.ThrowIfDisposed();

                var error = NativeMethods.svm_pegasos_get_kernel(this._Parameter.KernelType.ToNativeKernelType(),
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

                var error = NativeMethods.svm_pegasos_set_kernel(this._Parameter.KernelType.ToNativeKernelType(),
                   　　　　　　　　　　　　　　　　　　　　　　　this._Parameter.SampleType.ToNativeMatrixElementType(),
                   　　　　　　　　　　　　　　　　　　　　　　　this.NativePtr,
                   　　　　　　　　　　　　　　　　　　　　　　　value.NativePtr);
            }
        }

        public TScalar Tolerance
        {
            get
            {
                this.ThrowIfDisposed();
                this._Bridge.GetTolerance(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Bridge.SetTolerance(this.NativePtr, value);
            }
        }

        public uint TrainCount
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.svm_pegasos_get_train_count(this._Parameter.KernelType.ToNativeKernelType(),
                                                          this._Parameter.SampleType.ToNativeMatrixElementType(),
                                                          this.NativePtr,
                                                          out var ret);

                return ret;
            }
        }

        #endregion

        #region Methods

        public void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.svm_pegasos_clear(this._Parameter.KernelType.ToNativeKernelType(),
                                            this._Parameter.SampleType.ToNativeMatrixElementType(),
                                            this.NativePtr);
        }

        public TScalar Operator(Matrix<TScalar> sample)
        {
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            this.ThrowIfDisposed();
            sample.ThrowIfDisposed();

            return this._Bridge.Operator(this.NativePtr, sample);
        }

        public void SetLambda(TScalar lambda)
        {
            this.ThrowIfDisposed();
            this._Bridge.SetLambda(this.NativePtr, lambda);
        }

        public TScalar Train(Matrix<TScalar> x, TScalar y)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));

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

            NativeMethods.svm_pegasos_delete(this._Parameter.KernelType.ToNativeKernelType(),
                                             this._Parameter.SampleType.ToNativeMatrixElementType(),
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

            public abstract IntPtr Create(TKernel kernel, T lambda, T tolerance, uint maxNumSupportVector);

            public abstract T Operator(IntPtr obj, Matrix<T> sample);

            public abstract void SetLambda(IntPtr trainer, T lambda);

            public abstract void SetLambdaClass1(IntPtr trainer, T cClass1);

            public abstract void GetLambdaClass1(IntPtr trainer, out T cClass1);

            public abstract void SetLambdaClass2(IntPtr trainer, T cClass2);

            public abstract void GetLambdaClass2(IntPtr trainer, out T cClass2);

            public abstract void SetTolerance(IntPtr trainer, T tolerance);

            public abstract void GetTolerance(IntPtr trainer, out T tolerance);

            public abstract T Train(IntPtr obj, Matrix<T> x, T y);

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
            
            public override IntPtr Create(TKernel kernel, float lambda, float tolerance, uint maxNumSupportVector)
            {
                if (!(lambda > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");
                if (!(tolerance > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(tolerance)} must be greater than 0.");
                if (!(maxNumSupportVector > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(maxNumSupportVector)} must be greater than 0.");

                var err = NativeMethods.svm_pegasos_new2_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                               this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                               kernel.NativePtr,
                                                               lambda,
                                                               tolerance,
                                                               maxNumSupportVector,
                                                               out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }
            
            public override float Operator(IntPtr obj, Matrix<float> sample)
            {
                var err = NativeMethods.svm_pegasos_operator_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                   this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                   obj,
                                                                   sample.NativePtr,
                                                                   out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            public override void SetLambda(IntPtr trainer, float lambda)
            {
                if (lambda <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_lambda_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                     this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                     trainer,
                                                                     lambda);
            }

            public override void SetLambdaClass1(IntPtr trainer, float lambda)
            {
                if (lambda <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_lambda_class1_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                            this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                            trainer,
                                                                            lambda);
            }

            public override void GetLambdaClass1(IntPtr trainer, out float lambda)
            {
                var ret = NativeMethods.svm_pegasos_get_lambda_class1_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                            this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                            trainer,
                                                                            out lambda);
            }

            public override void SetLambdaClass2(IntPtr trainer, float lambda)
            {
                if (lambda <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_lambda_class2_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                            this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                            trainer,
                                                                            lambda);
            }

            public override void GetLambdaClass2(IntPtr trainer, out float lambda)
            {
                var ret = NativeMethods.svm_pegasos_get_lambda_class2_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                            this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                            trainer,
                                                                            out lambda);
            }

            public override void SetTolerance(IntPtr trainer, float tolerance)
            {
                if (tolerance <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(tolerance)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_tolerance_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                        this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                        trainer,
                                                                        tolerance);
            }

            public override void GetTolerance(IntPtr trainer, out float tolerance)
            {
                var ret = NativeMethods.svm_pegasos_get_tolerance_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                        this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                        trainer,
                                                                        out tolerance);
            }

            public override float Train(IntPtr obj, Matrix<float> x, float y)
            {
                var err = NativeMethods.svm_pegasos_train_float(this.Parameter.KernelType.ToNativeKernelType(),
                                                                this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                obj,
                                                                x.NativePtr,
                                                                y,
                                                                out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

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
            
            public override IntPtr Create(TKernel kernel, double lambda, double tolerance, uint maxNumSupportVector)
            {
                if (!(lambda > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");
                if (!(tolerance > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(tolerance)} must be greater than 0.");
                if (!(maxNumSupportVector > 0))
                    throw new ArgumentOutOfRangeException($"{nameof(maxNumSupportVector)} must be greater than 0.");

                var err = NativeMethods.svm_pegasos_new2_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                kernel.NativePtr,
                                                                lambda,
                                                                tolerance,
                                                                maxNumSupportVector,
                                                                out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            public override double Operator(IntPtr obj, Matrix<double> sample)
            {
                var err = NativeMethods.svm_pegasos_operator_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                    this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                    obj,
                                                                    sample.NativePtr,
                                                                    out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            public override void SetLambda(IntPtr trainer, double lambda)
            {
                if (lambda <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_lambda_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                      this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                      trainer,
                                                                      lambda);
            }

            public override void SetLambdaClass1(IntPtr trainer, double lambda)
            {
                if (lambda <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_lambda_class1_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                             this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                             trainer,
                                                                             lambda);
            }

            public override void GetLambdaClass1(IntPtr trainer, out double lambda)
            {
                var ret = NativeMethods.svm_pegasos_get_lambda_class1_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                             this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                             trainer,
                                                                             out lambda);
            }

            public override void SetLambdaClass2(IntPtr trainer, double lambda)
            {
                if (lambda <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(lambda)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_lambda_class2_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                             this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                             trainer,
                                                                             lambda);
            }

            public override void GetLambdaClass2(IntPtr trainer, out double lambda)
            {
                var ret = NativeMethods.svm_pegasos_get_lambda_class2_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                             this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                             trainer,
                                                                             out lambda);
            }


            public override void SetTolerance(IntPtr trainer, double tolerance)
            {
                if (tolerance <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(tolerance)} must be greater than 0.");

                var ret = NativeMethods.svm_pegasos_set_tolerance_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         tolerance);
            }

            public override void GetTolerance(IntPtr trainer, out double tolerance)
            {
                var ret = NativeMethods.svm_pegasos_get_tolerance_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                         this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                         trainer,
                                                                         out tolerance);
            }

            public override double Train(IntPtr obj, Matrix<double> x, double y)
            {
                var err = NativeMethods.svm_pegasos_train_double(this.Parameter.KernelType.ToNativeKernelType(),
                                                                 this.Parameter.SampleType.ToNativeMatrixElementType(),
                                                                 obj,
                                                                 x.NativePtr,
                                                                 y,
                                                                 out var ret);

                switch (err)
                {
                    case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                        throw new ArgumentException($"{this.Parameter.SampleType} is not supported.");
                    case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                        throw new ArgumentException($"{nameof(this.Parameter.TemplateColumns)} or {nameof(this.Parameter.TemplateRows)} is not supported.");
                    case NativeMethods.ErrorType.SvmKernelNotSupport:
                        throw new ArgumentException($"{this.Parameter.KernelType} is not supported.");
                }

                return ret;
            }

            #endregion

        }

    }

}
