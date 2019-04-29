using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Krls<TScalar, TKernel> : DlibObject
        where TScalar : struct
        where TKernel : KernelBase
    {

        #region Fields

        private readonly KernelBase _KernelBase;

        private readonly Bridge<TScalar> _Bridge;

        #endregion

        #region Constructors

        public Krls(TKernel kernelBase,
                    double tolerance = 0.001d,
                    uint maxDictionarySize = 1000000)
        {
            if (kernelBase == null)
                throw new ArgumentNullException(nameof(kernelBase));

            kernelBase.ThrowIfDisposed();

            this._Bridge = CreateBridge(kernelBase);

            this._KernelBase = kernelBase;
            var error = NativeMethods.krls_new(this._KernelBase.KernelType.ToNativeKernelType(),
                                               this._KernelBase.SampleType.ToNativeMatrixElementType(),
                                               this._KernelBase.TemplateRows,
                                               this._KernelBase.TemplateColumns,
                                               kernelBase.NativePtr,
                                               tolerance,
                                               maxDictionarySize,
                                               out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{this._KernelBase.SampleType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(this._KernelBase.TemplateColumns)} or {nameof(this._KernelBase.TemplateRows)} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{this._KernelBase.KernelType} is not supported.");
            }

            this.NativePtr = ret;
        }

        internal Krls(IntPtr ptr,
                           KernelBase kernelBase,
                           bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            this._Bridge = CreateBridge(kernelBase);

            this._KernelBase = kernelBase;
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public uint DictionarySize
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.krls_dictionary_size(this._KernelBase.KernelType.ToNativeKernelType(),
                                                   this._KernelBase.SampleType.ToNativeMatrixElementType(),
                                                   this._KernelBase.TemplateRows,
                                                   this._KernelBase.TemplateColumns,
                                                   this.NativePtr,
                                                   out var ret);

                return ret;
            }
        }

        internal KernelBase KernelBase => this._KernelBase;

        public TKernel Kernel
        {
            get
            {
                this.ThrowIfDisposed();

                var error = NativeMethods.krls_get_kernel(this._KernelBase.KernelType.ToNativeKernelType(),
                                                          this._KernelBase.SampleType.ToNativeMatrixElementType(),
                                                          this._KernelBase.TemplateRows,
                                                          this._KernelBase.TemplateColumns,
                                                          this.NativePtr,
                                                          out var ret);

                return KernelFactory.Create<TKernel, TScalar, Matrix<TScalar>>(ret, 
                                                                               this._KernelBase.KernelType,
                                                                               this._KernelBase.TemplateRows,
                                                                               this._KernelBase.TemplateColumns,
                                                                               false);
            }
        }

        #endregion

        #region Methods

        public TScalar Operator(Matrix<TScalar> sample)
        {
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            sample.ThrowIfDisposed();

            return this._Bridge.Operator(this.NativePtr, sample);
        }

        public void Train(Matrix<TScalar> x, TScalar y)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));

            x.ThrowIfDisposed();

            this._Bridge.Train(this.NativePtr, x, y);
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

            NativeMethods.krls_delete(this._KernelBase.KernelType.ToNativeKernelType(),
                                      this._KernelBase.SampleType.ToNativeMatrixElementType(),
                                      this._KernelBase.TemplateRows,
                                      this._KernelBase.TemplateColumns,
                                      this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<TScalar> CreateBridge(KernelBase kernel)
        {
            switch (kernel.SampleType)
            {
                case MatrixElementTypes.Float:
                    return new FloatBridge(kernel) as Bridge<TScalar>;
                case MatrixElementTypes.Double:
                    return new DoubleBridge(kernel) as Bridge<TScalar>;
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

            protected Bridge(KernelBase kernel)
            {
                this.Kernel = kernel;
            }

            #endregion

            #region Properties

            protected KernelBase Kernel
            {
                get;
            }

            #endregion

            #region Methods

            public abstract T Operator(IntPtr obj, Matrix<T> sample);

            public abstract void Train(IntPtr obj, Matrix<T> x, T y);

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Constructors

            public FloatBridge(KernelBase kernel) :
                base(kernel)
            {
            }

            #endregion

            #region Methods

            public override float Operator(IntPtr obj, Matrix<float> sample)
            {
                var err = NativeMethods.krls_operator_float(this.Kernel.KernelType.ToNativeKernelType(),
                                                            this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                            this.Kernel.TemplateRows,
                                                            this.Kernel.TemplateColumns,
                                                            obj,
                                                            sample.NativePtr,
                                                            out var ret);
                return ret;
            }

            public override void Train(IntPtr obj, Matrix<float> x, float y)
            {
                var err = NativeMethods.krls_train_float(this.Kernel.KernelType.ToNativeKernelType(),
                                                         this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                         this.Kernel.TemplateRows,
                                                         this.Kernel.TemplateColumns,
                                                         obj,
                                                         x.NativePtr,
                                                         y);
            }

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Constructors

            public DoubleBridge(KernelBase kernel) :
                base(kernel)
            {
            }

            #endregion

            #region Methods

            public override double Operator(IntPtr obj, Matrix<double> sample)
            {
                var err = NativeMethods.krls_operator_double(this.Kernel.KernelType.ToNativeKernelType(),
                                                             this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                             this.Kernel.TemplateRows,
                                                             this.Kernel.TemplateColumns,
                                                             obj,
                                                             sample.NativePtr,
                                                             out var ret);
                return ret;
            }

            public override void Train(IntPtr obj, Matrix<double> x, double y)
            {
                var err = NativeMethods.krls_train_double(this.Kernel.KernelType.ToNativeKernelType(),
                                                          this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                          this.Kernel.TemplateRows,
                                                          this.Kernel.TemplateColumns,
                                                          obj,
                                                          x.NativePtr,
                                                          y);
            }

            #endregion

        }

    }

}
