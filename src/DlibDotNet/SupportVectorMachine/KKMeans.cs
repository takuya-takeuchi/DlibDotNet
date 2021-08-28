#if !LITE
using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class KKMeans<TScalar, TKernel> : DlibObject
        where TScalar : struct
        where TKernel : KernelBase
    {

        #region Fields

        private readonly KCentroid<TScalar, TKernel> _KCentroid;

        #endregion

        #region Constructors

        public KKMeans(KCentroid<TScalar, TKernel> kcentroid)
        {
            if (kcentroid == null)
                throw new ArgumentNullException(nameof(kcentroid));

            kcentroid.ThrowIfDisposed();

            this._KCentroid = kcentroid;
            var error = NativeMethods.kkmeans_new(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                                  this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                                  this._KCentroid.Parameter.TemplateRows,
                                                  this._KCentroid.Parameter.TemplateColumns,
                                                  kcentroid.NativePtr,
                                                  out var ret);
            switch (error)
            {
                case NativeMethods.ErrorType.MatrixElementTypeNotSupport:
                    throw new ArgumentException($"{this._KCentroid.Parameter.SampleType} is not supported.");
                case NativeMethods.ErrorType.MatrixElementTemplateSizeNotSupport:
                    throw new ArgumentException($"{nameof(this._KCentroid.Parameter.TemplateColumns)} or {nameof(this._KCentroid.Parameter.TemplateRows)} is not supported.");
                case NativeMethods.ErrorType.SvmKernelNotSupport:
                    throw new ArgumentException($"{this._KCentroid.Parameter.KernelType} is not supported.");
            }

            this.NativePtr = ret;
        }

        #endregion

        #region Properties

        public uint NumberOfCenters
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.kkmeans_get_number_of_centers(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                                            this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                                            this._KCentroid.Parameter.TemplateRows,
                                                            this._KCentroid.Parameter.TemplateColumns,
                                                            this.NativePtr,
                                                            out var ret);

                return ret;
            }
            set
            {
                if (!(value > 0))
                    throw new ArgumentOutOfRangeException();

                this.ThrowIfDisposed();
                NativeMethods.kkmeans_set_number_of_centers(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                                            this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                                            this._KCentroid.Parameter.TemplateRows,
                                                            this._KCentroid.Parameter.TemplateColumns,
                                                            this.NativePtr,
                                                            value);
            }
        }

        public TKernel Kernel
        {
            get
            {
                this.ThrowIfDisposed();

                var kernelBase = this._KCentroid.Parameter;
                var error = NativeMethods.kkmeans_get_kernel(kernelBase.KernelType.ToNativeKernelType(),
                                                             kernelBase.SampleType.ToNativeMatrixElementType(),
                                                             kernelBase.TemplateRows,
                                                             kernelBase.TemplateColumns,
                                                             this.NativePtr,
                                                             out var ret);

                return KernelFactory.Create<TKernel, TScalar, Matrix<TScalar>>(ret,
                                                                               kernelBase.KernelType,
                                                                               kernelBase.TemplateRows,
                                                                               kernelBase.TemplateColumns,
                                                                               false);
            }
        }

        #endregion

        #region Methods

        public KCentroid<TScalar, TKernel> GetKCentroid(int index)
        {
            if (!(0 <= index && index < this.NumberOfCenters))
                throw new ArgumentOutOfRangeException();

            var ret = NativeMethods.kkmeans_get_kcentroid(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                                          this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                                          this._KCentroid.Parameter.TemplateRows,
                                                          this._KCentroid.Parameter.TemplateColumns,
                                                          this.NativePtr,
                                                          (uint)index,
                                                          out var kcentroid);

            return new KCentroid<TScalar, TKernel>(kcentroid, this._KCentroid.Parameter, false);
        }

        public uint Operator(Matrix<TScalar> sample)
        {
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));

            sample.ThrowIfDisposed();

            var error = NativeMethods.kkmeans_operator(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                                       this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                                       this._KCentroid.Parameter.TemplateRows,
                                                       this._KCentroid.Parameter.TemplateColumns,
                                                       this.NativePtr,
                                                       sample.NativePtr,
                                                       out var ret);

            return ret;
        }

        public void SetKCentroid(KCentroid<TScalar, TKernel> kcentroid)
        {
            if (kcentroid == null)
                throw new ArgumentNullException(nameof(kcentroid));

            kcentroid.ThrowIfDisposed();

            var ret = NativeMethods.kkmeans_set_kcentroid(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                                          this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                                          this._KCentroid.Parameter.TemplateRows,
                                                          this._KCentroid.Parameter.TemplateColumns,
                                                          this.NativePtr,
                                                          kcentroid.NativePtr);
        }

        public void Train(IEnumerable<Matrix<TScalar>> samples, IEnumerable<Matrix<TScalar>> initialCenters, int maxIterator = 1000)
        {
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));
            if (initialCenters == null)
                throw new ArgumentNullException(nameof(initialCenters));

            var samplesArray = samples.ToArray();
            var initialCentersArray = initialCenters.ToArray();
            samplesArray.ThrowIfDisposed();
            initialCentersArray.ThrowIfDisposed();

            using (var samplesVector = new StdVector<Matrix<TScalar>>(samplesArray))
            using (var initialCentersVector = new StdVector<Matrix<TScalar>>(initialCentersArray))
            {
                var ret = NativeMethods.kkmeans_train(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                                      this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                                      this._KCentroid.Parameter.TemplateRows,
                                                      this._KCentroid.Parameter.TemplateColumns,
                                                      this.NativePtr,
                                                      samplesVector.NativePtr,
                                                      initialCentersVector.NativePtr,
                                                      (uint)maxIterator);
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

            NativeMethods.kkmeans_delete(this._KCentroid.Parameter.KernelType.ToNativeKernelType(),
                                         this._KCentroid.Parameter.SampleType.ToNativeMatrixElementType(),
                                         this._KCentroid.Parameter.TemplateRows,
                                         this._KCentroid.Parameter.TemplateColumns,
                                         this.NativePtr);
        }

        #endregion

        #endregion

    }

}
#endif
