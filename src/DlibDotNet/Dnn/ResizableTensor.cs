using System;
using System.Runtime.InteropServices;

namespace DlibDotNet.Dnn
{

    public sealed class ResizableTensor : Tensor
    {

        #region Constructors

        public ResizableTensor() :
            base(ResizableTensorNative.resizable_tensor_new())
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

            ResizableTensorNative.resizable_tensor_delete(this.NativePtr);
        }

        #endregion

        #endregion

        private static class ResizableTensorNative
        {

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr resizable_tensor_new();

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void resizable_tensor_delete(IntPtr tensor);

        }

    }

}
