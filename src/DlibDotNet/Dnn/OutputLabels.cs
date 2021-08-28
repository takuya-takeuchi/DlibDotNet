#if !LITE
using System;
using System.Collections;
using System.Collections.Generic;

namespace DlibDotNet.Dnn
{

    public abstract class OutputLabels<T> : DlibObject, IEnumerable<T>
    {

        #region Constructors

        protected OutputLabels(IntPtr vector)
        {
            if (vector == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(vector));

            this.NativePtr = vector;
        }

        #endregion

        #region IUndisposableElementCollection<T> Members

        public abstract int Count
        {
            get;
        }

        public abstract T this[int index]
        {
            get;
        }

        public abstract T this[uint index]
        {
            get;
        }

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }

}
#endif
