using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class HistogramIntersectionKernel<T> : KernelBase
        where T : MatrixBase, new()
    {

        #region Fields

        private readonly NativeMethods.MatrixElementType _ElementType;

        #endregion

        #region Constructors

        public HistogramIntersectionKernel(int templateRow = 0, int templateColumn = 0) :
            base(templateRow, templateColumn)
        {
            if (!KernelTypesRepository.SupportTypes.TryGetValue(typeof(T), out _))
                throw new NotSupportedException();

            using (var tmp = new T())
            {
                this._ElementType = tmp.MatrixElementType.ToNativeMatrixElementType();

                this.NativePtr = NativeMethods.histogram_intersection_kernel_new(this._ElementType, templateRow, templateColumn);
            }
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

            NativeMethods.histogram_intersection_kernel_delete(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
        }

        #endregion

        #endregion

    }

}