#if !LITE
using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class Queue<TItem>
    {
        
        public sealed class Kernel1A : DlibObject, IReadOnlyCollection<TItem>
        {

            #region Fields

            private readonly Bridge<TItem> _Bridge;

            #endregion

            #region Constructors

            public Kernel1A()
            {
                this._Bridge = CreateImp();
                this.NativePtr = this._Bridge.Create();
            }

            internal Kernel1A(IntPtr ptr)
            {
                this._Bridge = CreateImp();
                this.NativePtr = ptr;
            }

            #endregion

            #region Properties

            public bool MoveNext
            {
                get
                {
                    this.ThrowIfDisposed();
                    return this._Bridge.MoveNext(this.NativePtr);
                }
            }

            #endregion

            #region Methods

            public void Clear()
            {
                this.ThrowIfDisposed();
                this._Bridge.Clear(this.NativePtr);
            }

            public void Dequeue(out TItem item)
            {
                this.ThrowIfDisposed();
                this._Bridge.Dequeue(this.NativePtr, out item);
            }

            public TItem Element()
            {
                this.ThrowIfDisposed();
                return this._Bridge.Element(this.NativePtr);
            }

            public void Enqueue(TItem item)
            {
                this.ThrowIfDisposed();
                this._Bridge.Enqueue(this.NativePtr, item);
            }

            public void Reset()
            {
                this.ThrowIfDisposed();
                this._Bridge.Reset(this.NativePtr);
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
                if (QueueElementTypesRepository.SupportTypes.TryGetValue(typeof(TItem), out var type))
                {
                    switch (type)
                    {
                        case QueueElementTypesRepository.ElementTypes.Int32:
                            return new Int32Bridge() as Bridge<TItem>;
                        case QueueElementTypesRepository.ElementTypes.UInt32:
                            return new UInt32Bridge() as Bridge<TItem>;
                    }
                }

                throw new ArgumentOutOfRangeException(nameof(type), type, null);
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
                    return this._Bridge.GetSize(this.NativePtr);
                }
            }

            #endregion

            #region Bridge

            private sealed class Int32Bridge : Bridge<int>
            {

                #region Methods

                public override IntPtr Create()
                {
                    return NativeMethods.queue_kernel_1a_int32_t_new();
                }

                public override void Dispose(IntPtr ptr)
                {
                    NativeMethods.queue_kernel_1a_int32_t_delete(ptr);
                }

                public override void Clear(IntPtr ptr)
                {
                    NativeMethods.queue_kernel_1a_int32_t_clear(ptr);
                }

                public override void Dequeue(IntPtr ptr, out int item)
                {
                    NativeMethods.queue_kernel_1a_int32_t_dequeue(ptr, out item);
                }

                public override int Element(IntPtr ptr)
                {
                    return NativeMethods.queue_kernel_1a_int32_t_element(ptr);
                }

                public override void Enqueue(IntPtr ptr, int item)
                {
                    NativeMethods.queue_kernel_1a_int32_t_enqueue(ptr, item);
                }

                public override int GetSize(IntPtr ptr)
                {
                    return NativeMethods.queue_kernel_1a_int32_t_size(ptr);
                }

                public override bool MoveNext(IntPtr ptr)
                {
                    return NativeMethods.queue_kernel_1a_int32_t_move_next(ptr);
                }

                public override void Reset(IntPtr ptr)
                {
                    NativeMethods.queue_kernel_1a_int32_t_reset(ptr);
                }

                #endregion

            }

            private sealed class UInt32Bridge : Bridge<uint>
            {

                #region Methods

                public override IntPtr Create()
                {
                    return NativeMethods.queue_kernel_1a_uint32_t_new();
                }

                public override void Dispose(IntPtr ptr)
                {
                    NativeMethods.queue_kernel_1a_uint32_t_delete(ptr);
                }

                public override void Clear(IntPtr ptr)
                {
                    NativeMethods.queue_kernel_1a_uint32_t_clear(ptr);
                }

                public override void Dequeue(IntPtr ptr, out uint item)
                {
                    NativeMethods.queue_kernel_1a_uint32_t_dequeue(ptr, out item);
                }

                public override uint Element(IntPtr ptr)
                {
                    return NativeMethods.queue_kernel_1a_uint32_t_element(ptr);
                }

                public override void Enqueue(IntPtr ptr, uint item)
                {
                    NativeMethods.queue_kernel_1a_uint32_t_enqueue(ptr, item);
                }

                public override int GetSize(IntPtr ptr)
                {
                    return NativeMethods.queue_kernel_1a_uint32_t_size(ptr);
                }

                public override bool MoveNext(IntPtr ptr)
                {
                    return NativeMethods.queue_kernel_1a_uint32_t_move_next(ptr);
                }

                public override void Reset(IntPtr ptr)
                {
                    NativeMethods.queue_kernel_1a_uint32_t_reset(ptr);
                }

                #endregion

            }

            #endregion

        }

    }

}
#endif
