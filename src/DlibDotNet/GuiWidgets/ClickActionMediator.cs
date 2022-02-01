using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ClickActionMediator : DlibObject
    {

        #region Fields

        private readonly IntPtr _Handle;

        private readonly Action<Point, bool, uint> _Callback;

        #endregion

        #region Constructors

        public ClickActionMediator(Action<Point, bool, uint> callback)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            this._Callback = callback;

            var @delegate = new NativeMethods.ClickActionDelegate(this.NativeCallback);
            this._Handle = Marshal.GetFunctionPointerForDelegate(@delegate);
            this.NativePtr = NativeMethods.click_action_mediator_new(this._Handle);
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

            NativeMethods.click_action_mediator_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Helpers

        private void NativeCallback(IntPtr point, bool isDoubleClick, uint button)
        {
#if !DLIB_NO_GUI_SUPPORT
            this._Callback.Invoke(new Point(point, false), isDoubleClick, button );
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}