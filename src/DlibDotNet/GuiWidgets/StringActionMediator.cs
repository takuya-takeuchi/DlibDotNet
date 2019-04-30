using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StringActionMediator : DlibObject
    {

        #region Fields

        private readonly IntPtr _Handle;

        private readonly Action<string> _Callback;

        #endregion

        #region Constructors

        public StringActionMediator(Action<string> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            this._Callback = callback;

            var @delegate = new NativeMethods.StringActionDelegate(this.NativeCallback);
            this._Handle = Marshal.GetFunctionPointerForDelegate(@delegate);
            this.NativePtr = NativeMethods.string_action_mediator_new(this._Handle);
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

            NativeMethods.string_action_mediator_delete(this.NativePtr);
        }

        #endregion

        #region Helpers

        private void NativeCallback(IntPtr file)
        {
            this._Callback.Invoke(StringHelper.FromStdString(file));
        }

        #endregion

        #endregion

    }

}