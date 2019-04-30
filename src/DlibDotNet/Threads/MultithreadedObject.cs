using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public class MultithreadedObject : DlibObject
    {

        #region Fields
        #endregion

        #region Methods

        public void Pause()
        {
            this.ThrowIfDisposed();
            NativeMethods.multithreaded_object_pause(this.NativePtr);
        }

        public virtual void RegisterThread(VoidActionMediator mediator)
        {
        }

        public void Start()
        {
            this.ThrowIfDisposed();
            NativeMethods.multithreaded_object_start(this.NativePtr);
        }

        public void Stop()
        {
            this.ThrowIfDisposed();
            NativeMethods.multithreaded_object_stop(this.NativePtr);
        }

        public void Wait()
        {
            this.ThrowIfDisposed();
            NativeMethods.multithreaded_object_wait(this.NativePtr);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            //NativeMethods.custom_multithreaded_object_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}