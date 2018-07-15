using System;
using System.Runtime.InteropServices;

namespace DlibDotNet.Dnn
{

    public sealed class ResizableTensor : Tensor
    {

        #region Constructors

        public ResizableTensor() :
            base(Native.resizable_tensor_new())
        {
        }

        #endregion

        #region Methods

        #region Overrids

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.resizable_tensor_delete(this.NativePtr);
        }

        #endregion

        #endregion

        private sealed class Native
        {

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr resizable_tensor_new();

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void resizable_tensor_delete(IntPtr tensor);

        }

    }

}
