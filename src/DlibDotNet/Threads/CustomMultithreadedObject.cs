#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class CustomMultithreadedObject : MultithreadedObject
    {

        #region Constructors

        protected CustomMultithreadedObject()
        {
            this.NativePtr = NativeMethods.custom_multithreaded_object_new();
        }

        #endregion

        #region Methods
        
        public override void RegisterThread(VoidActionMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.ThrowIfDisposed();
            mediator.ThrowIfDisposed();

            NativeMethods.custom_multithreaded_object_register_thread(this.NativePtr, mediator.NativePtr);
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

            NativeMethods.custom_multithreaded_object_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
#endif
