#if !LITE
using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Pipe<TItem> : DlibObject, IReadOnlyCollection<TItem>
    {

        #region Fields

        private readonly Bridge<TItem> _Bridge;

        #endregion

        #region Constructors

        public Pipe(ulong maximumSize)
        {
            this._Bridge = CreateImp();
            this.NativePtr = this._Bridge.Create(maximumSize);
        }

        internal Pipe(IntPtr ptr)
        {
            this._Bridge = CreateImp();
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public bool IsEnabled
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Bridge.IsEnabled(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public bool Dequeue(out TItem item)
        {
            this.ThrowIfDisposed();
            return this._Bridge.Dequeue(this.NativePtr, out item);
        }

        public void Disable()
        {
            this.ThrowIfDisposed();
            this._Bridge.Disable(this.NativePtr);
        }

        public void Enqueue(TItem item)
        {
            this.ThrowIfDisposed();
            this._Bridge.Enqueue(this.NativePtr, item);
        }

        public void WaitUntilEmpty()
        {
            this.ThrowIfDisposed();
            this._Bridge.WaitUntilEmpty(this.NativePtr);
        }

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            this._Bridge?.Dispose(this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<TItem> CreateImp()
        {
            //if (PipeElementTypesRepository.SupportTypes.TryGetValue(typeof(TItem), out var type))
            //{
            //    //switch (type)
            //    //{
            //    //    case PipeElementTypesRepository.ElementTypes.UInt32:
            //    //        return new UInt32Bridge() as Bridge<TItem>;
            //    //}
            //}
            //else
            //{
                
            //}
            return new GenericBridge<TItem>();
        }

        #endregion

        #endregion

        #region IReadOnlyCollection<TItem> Implements

        public IEnumerator<TItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get
            {
                this.ThrowIfDisposed();
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Bridge

        private abstract class Bridge<T>
        {

            #region Methods

            public abstract IntPtr Create(ulong maxSize);

            public abstract void Dispose(IntPtr pipe);

            public abstract bool Dequeue(IntPtr pipe, out T item);

            public abstract void Disable(IntPtr pipe);

            public abstract void Enqueue(IntPtr pipe, T item);

            public abstract bool IsEnabled(IntPtr pipe);

            public abstract void WaitUntilEmpty(IntPtr pipe);

            #endregion

        }

        private sealed class GenericBridge<T> : Bridge<T>
        {

            #region Fields

            private readonly ContainerBridge<T> _Bridge;

            #endregion

            #region Constructors

            public GenericBridge()
            {
                this._Bridge = ContainerBridgeRepository.Get<T>();
            }

            #endregion

            #region Methods

            public override IntPtr Create(ulong maxSize)
            {
                return NativeMethods.pipe_generic_new(maxSize);
            }

            public override void Dispose(IntPtr pipe)
            {
                NativeMethods.pipe_generic_delete(pipe);
            }

            public override bool Dequeue(IntPtr pipe, out T item)
            {
                var b = NativeMethods.pipe_generic_dequeue(pipe, out var ret);
                item = this._Bridge.Create(ret);
                return b;
            }
        
            public override void Disable(IntPtr pipe)
            {
                NativeMethods.pipe_generic_disable(pipe);
            }

            public override void Enqueue(IntPtr pipe, T item)
            {
                var p = this._Bridge.GetPtr(item);
                NativeMethods.pipe_generic_enqueue(pipe, p);
            }

            public override bool IsEnabled(IntPtr pipe)
            {
                return NativeMethods.pipe_generic_is_enabled(pipe);
            }

            public override void WaitUntilEmpty(IntPtr pipe)
            {
                NativeMethods.pipe_generic_wait_until_empty(pipe);
            }

            #endregion

        }

        #endregion

    }

}
#endif
