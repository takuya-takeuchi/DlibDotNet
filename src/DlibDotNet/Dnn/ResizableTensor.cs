using System;
using System.Runtime.InteropServices;

namespace DlibDotNet.Dnn
{

    public sealed class ResizableTensor : Tensor
    {

        #region Constructors

        public ResizableTensor() :
            base(NativeMethods.resizable_tensor_new())
        {
        }

        #endregion

        #region Methods

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.resizable_tensor_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
