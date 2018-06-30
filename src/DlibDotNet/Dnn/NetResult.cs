using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DlibDotNet.Dnn
{

    public sealed class NetResult<TItem> : DlibObject, IEnumerable<TItem>
    {

        #region Fields

        private readonly TItem[] _Array;

        private readonly StdVector<TItem> _Vector;

        #endregion

        #region Constructors

        internal NetResult(StdVector<TItem> vector)
        {
            this._Vector = vector ?? throw new ArgumentNullException(nameof(vector));
            this._Array = this._Vector.ToArray();
            this.NativePtr = vector.NativePtr;
        }

        #endregion

        #region Properties

        public int Length
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Vector.Size;
            }
        }

        public TItem this[int index]
        {
            get
            {
                this.ThrowIfDisposed();

                if (!(0 <= index && index < this._Array.Length))
                    throw new ArgumentOutOfRangeException();

                return this._Array[index];
            }
        }

        public TItem this[uint index]
        {
            get
            {
                this.ThrowIfDisposed();

                if (!(index < this._Array.Length))
                    throw new ArgumentOutOfRangeException();

                return this._Array[index];
            }
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            foreach (var item in this._Array.Cast<IDisposable>().Where(m => m != null))
                item.Dispose();

            this._Vector.Dispose();
        }

        #endregion

        #endregion

        #region IEnumerable<TItem> Members

        public IEnumerator<TItem> GetEnumerator()
        {
            this.ThrowIfDisposed();

            return ((IEnumerable<TItem>)this._Vector.ToArray()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            this.ThrowIfDisposed();

            return this.GetEnumerator();
        }

        #endregion

    }

}