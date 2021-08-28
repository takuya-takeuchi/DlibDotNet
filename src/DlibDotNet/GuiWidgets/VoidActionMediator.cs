#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class VoidActionMediator : DlibObject
    {

        #region Fields

        private readonly IntPtr _Handle;

        private readonly Action _Callback;

        #endregion

        #region Constructors

        public VoidActionMediator(Action callback)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            this._Callback = callback;

            var @delegate = new NativeMethods.VoidActionDelegate(this.NativeCallback);
            this._Handle = Marshal.GetFunctionPointerForDelegate(@delegate);
            this.NativePtr = NativeMethods.void_action_mediator_new(this._Handle);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
#if !DLIB_NO_GUI_SUPPORT
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.void_action_mediator_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Helpers

        private void NativeCallback()
        {
            this._Callback.Invoke();
        }

        #endregion

        #endregion

    }

}
#endif
