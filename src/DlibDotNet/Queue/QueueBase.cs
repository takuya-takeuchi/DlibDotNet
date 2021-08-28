#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed partial class Queue<TItem>
    {

        private abstract class Bridge<T>
        {

            #region Methods

            public abstract IntPtr Create();

            public abstract void Dispose(IntPtr ptr);

            public abstract void Clear(IntPtr ptr);

            public abstract void Dequeue(IntPtr ptr, out T item);

            public abstract T Element(IntPtr ptr);

            public abstract void Enqueue(IntPtr ptr, T item);

            public abstract int GetSize(IntPtr ptr);

            public abstract bool MoveNext(IntPtr ptr);

            public abstract void Reset(IntPtr ptr);

            #endregion

        }

    }

}
#endif
