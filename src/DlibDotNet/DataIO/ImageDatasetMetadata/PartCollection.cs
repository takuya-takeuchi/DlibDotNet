#if !LITE
using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class PartCollection : IEnumerable<KeyValuePair<string, Point>>
    {

        #region Fields

        private readonly PartCollectionBridge _Bridge;

        #endregion

        #region Constructors

        internal PartCollection(PartCollectionBridge bridge)
        {
            if (bridge == null)
                throw new ArgumentNullException(nameof(bridge));

            this._Bridge = bridge;
        }

        #endregion

        #region Properties

        public int Count => this._Bridge.Count;

        public Point this[string key]
        {
            get => this._Bridge[key];
            set => this._Bridge[key] = value;
        }

        #endregion

        #region Methods

        public void Clear()
        {
            this._Bridge.Clear();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string, Point>> Implements

        public IEnumerator<KeyValuePair<string, Point>> GetEnumerator()
        {
            return this._Bridge.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._Bridge).GetEnumerator();
        }

        #endregion

        internal abstract class PartCollectionBridge : IEnumerable<KeyValuePair<string, Point>>
        {
            
            #region Fields

            private readonly DlibObject _Parent;

            #endregion

            #region Constructors

            protected PartCollectionBridge(DlibObject parent)
            {
                this._Parent = parent;
            }

            #endregion

            #region Properties

            protected DlibObject Parent => this._Parent;

            public abstract int Count
            {
                get;
            }

            public abstract Point this[string key]
            {
                get;
                set;
            }

            #endregion

            #region Methods

            public abstract void Clear();

            #region Overrids

            public abstract IEnumerator<KeyValuePair<string, Point>> GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #endregion

        }

    }

}
#endif
