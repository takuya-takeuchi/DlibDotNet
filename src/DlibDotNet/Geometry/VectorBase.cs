// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class VectorBase<T> : DlibObject
    {

        #region Constructors

        protected VectorBase(bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
        }

        #endregion

        #region Properties

        public abstract double Length
        {
            get;
        }

        public abstract double LengthSquared
        {
            get;
        }

        public abstract T X
        {
            get;
        }

        public abstract T Y
        {
            get;
        }

        public abstract T Z
        {
            get;
        }

        #endregion

    }

}
