// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class StdPair<T,U> : DlibObject
    {

        #region Properties

        public  abstract T First
        {
            get;
            set;
        }

        public abstract U Second
        {
            get;
            set;
        }

        #endregion

    }

}