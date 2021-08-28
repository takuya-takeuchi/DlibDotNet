#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class CustomDrawableWindow : DrawableWindow
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnConstructorDelegate(IntPtr windows);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnDestructorDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnWindowResizedDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnKeyDownDelegate(uint key, bool isPrintable, uint state);

        #endregion

        #region Events
        #endregion

        #region Fields

        private readonly DelegateHandler<OnConstructorDelegate> _Constructor;

        private readonly DelegateHandler<OnDestructorDelegate> _Destructor;

        private readonly DelegateHandler<OnWindowResizedDelegate> _OnWindowResized;

        private readonly DelegateHandler<OnKeyDownDelegate> _OnKeyDown;

        #endregion

        #region Constructors

        protected CustomDrawableWindow(bool resizable = true,
                                       bool undecorated = false)
        {
#if !DLIB_NO_GUI_SUPPORT
            this._Constructor = new DelegateHandler<OnConstructorDelegate>(this.Constructor);
            this._Destructor = new DelegateHandler<OnDestructorDelegate>(this.Destructor);
            this._OnWindowResized = new DelegateHandler<OnWindowResizedDelegate>(this.WindowResized);
            this._OnKeyDown = new DelegateHandler<OnKeyDownDelegate>(this.KeyDown);

            this.NativePtr = NativeMethods.custom_drawable_window_new(resizable,
                                                                      undecorated,
                                                                      this._Constructor.Handle,
                                                                      this._Destructor.Handle,
                                                                      this._OnWindowResized.Handle,
                                                                      this._OnKeyDown.Handle);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        protected virtual void OnConstructor(IntPtr window)
        {

        }

        protected virtual void OnDestructor()
        {

        }

        protected virtual void OnWindowResized()
        {
        }

        protected virtual void OnKeyDown(uint key, bool isPrintable, uint state)
        {
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
#if !DLIB_NO_GUI_SUPPORT
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.custom_drawable_window_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers

        private void Constructor(IntPtr window)
        {
            this.OnConstructor(window);
        }

        private void Destructor()
        {
            this.OnDestructor();
        }

        private void WindowResized()
        {
            this.OnWindowResized();
        }

        private void KeyDown(uint key, bool isPrintable, uint state)
        {
            this.OnKeyDown(key, isPrintable, state);
        }

        #endregion

        #endregion

    }

}
#endif
