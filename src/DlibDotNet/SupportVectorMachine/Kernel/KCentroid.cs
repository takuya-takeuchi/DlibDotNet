using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class KCentroid<T, K> : DlibObject
        where T : struct
        where K : KernelBase
    {

        #region Fields

        private readonly KernelBase _Kernel;

        private readonly Bridge<T> _Bridge;

        #endregion

        #region Constructors

        public KCentroid(K kernel,
                         double tolerance = 0.001d,
                         uint maxDictionarySize = 1000000,
                         bool removeOldestFirst = false)
        {
            if (kernel == null)
                throw new ArgumentNullException(nameof(kernel));

            kernel.ThrowIfDisposed();

            this._Bridge = CreateBridge(kernel);

            this._Kernel = kernel;
            this.NativePtr = NativeMethods.kcentroid_new(this._Kernel.KernelType.ToNativeKernelType(),
                                                         this._Kernel.SampleType.ToNativeMatrixElementType(),
                                                         this._Kernel.TemplateRows,
                                                         this._Kernel.TemplateColumns,
                                                         kernel.NativePtr,
                                                         tolerance,
                                                         maxDictionarySize,
                                                         removeOldestFirst);
        }

        #endregion

        #region Methods

        public T Operator(Matrix<T> sample)
        {
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            sample.ThrowIfDisposed();

            return this._Bridge.Operator(this.NativePtr, sample);
        }

        public void Train(Matrix<T> sample)
        {
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            sample.ThrowIfDisposed();

            this._Bridge.Train(this.NativePtr, sample);
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

            NativeMethods.kcentroid_delete(this._Kernel.KernelType.ToNativeKernelType(),
                                           this._Kernel.SampleType.ToNativeMatrixElementType(),
                                           this._Kernel.TemplateRows,
                                           this._Kernel.TemplateColumns,
                                           this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<T> CreateBridge(KernelBase kernel)
        {
            switch (kernel.SampleType)
            {
                case MatrixElementTypes.Float:
                    return new FloatBridge(kernel) as Bridge<T>;
                case MatrixElementTypes.Double:
                    return new DoubleBridge(kernel) as Bridge<T>;
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

            public abstract void Train(IntPtr obj, Matrix<T> sample);

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
                var err = NativeMethods.kcentroid_operator_float(this.Kernel.KernelType.ToNativeKernelType(),
                                                                 this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                                 this.Kernel.TemplateRows,
                                                                 this.Kernel.TemplateColumns,
                                                                 obj,
                                                                 sample.NativePtr,
                                                                 out var ret);
                return ret;
            }

            public override void Train(IntPtr obj, Matrix<float> sample)
            {
                var err = NativeMethods.kcentroid_train_float(this.Kernel.KernelType.ToNativeKernelType(),
                                                              this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                              this.Kernel.TemplateRows,
                                                              this.Kernel.TemplateColumns,
                                                              obj,
                                                              sample.NativePtr);
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
                var err = NativeMethods.kcentroid_operator_double(this.Kernel.KernelType.ToNativeKernelType(),
                                                                  this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                                  this.Kernel.TemplateRows,
                                                                  this.Kernel.TemplateColumns,
                                                                  obj,
                                                                  sample.NativePtr,
                                                                  out var ret);
                return ret;
            }

            public override void Train(IntPtr obj, Matrix<double> sample)
            {
                var err = NativeMethods.kcentroid_train_double(this.Kernel.KernelType.ToNativeKernelType(),
                                                               this.Kernel.SampleType.ToNativeMatrixElementType(),
                                                               this.Kernel.TemplateRows,
                                                               this.Kernel.TemplateColumns,
                                                               obj,
                                                               sample.NativePtr);
            }

            #endregion

        }

    }

}
