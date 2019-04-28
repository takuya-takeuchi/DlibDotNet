using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PolynomialKernel<TScalar, TSample> : KernelBase
        where TScalar : struct
        where TSample : Matrix<TScalar>, new()
    {

        #region Fields

        private readonly NativeMethods.MatrixElementType _ElementType;

        #endregion

        #region Constructors

        public PolynomialKernel(int templateRow = 0, int templateColumn = 0) :
            base(KernelType.Polynomial, templateRow, templateColumn)
        {
            if (!NumericKernelTypesRepository.SupportTypes.TryGetValue(typeof(TScalar), out _))
                throw new NotSupportedException();

            if (!Matrix<TScalar>.TryParse<TScalar>(out var type))
                throw new NotSupportedException();

            this.SampleType = type;
            this._ElementType = type.ToNativeMatrixElementType();

            this.NativePtr = NativeMethods.polynomial_kernel_new(this._ElementType, templateRow, templateColumn);
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.polynomial_kernel_delete(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
        }

        #endregion

        #endregion

    }

}