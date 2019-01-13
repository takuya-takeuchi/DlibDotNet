using System;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class LinearKernel<T> : DlibObject
        where T : MatrixBase, new()
    {

        #region Fields

        private readonly MatrixElementTypes _MatrixElementTypes;

        private readonly NativeMethods.MatrixElementType _ElementType;

        #endregion

        #region Constructors

        public LinearKernel(int templateRow = 0, int templateColumn = 0)
        {
            using (var tmp = new T())
            {
                this._MatrixElementTypes = tmp.MatrixElementType;
                this._ElementType = this._MatrixElementTypes.ToNativeMatrixElementType();

                this.TemplateRows = templateRow;
                this.TemplateColumns = templateColumn;

                this.NativePtr = NativeMethods.linear_kernel_new(this._ElementType, templateRow, templateColumn);
            }
        }

        #endregion

        #region Properties

        internal int TemplateColumns
        {
            get;
        }

        internal int TemplateRows
        {
            get;
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

            NativeMethods.linear_kernel_delete(this._ElementType, this.NativePtr, this.TemplateRows, this.TemplateColumns);
        }

        #endregion

        #endregion

    }

}
