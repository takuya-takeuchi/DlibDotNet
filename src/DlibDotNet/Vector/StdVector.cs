using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class StdVector<T> : DlibObject
    {

        #region Properties

        public  abstract IntPtr ElementPtr
        {
            get;
        }

        public abstract int Size
        {
            get;
        }

        #endregion

        #region Methods

        public abstract T[] ToArray();

        #endregion

    }

}