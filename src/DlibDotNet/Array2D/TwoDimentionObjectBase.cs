// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class TwoDimentionObjectBase : DlibObject
    {

        #region Constructors

        protected TwoDimentionObjectBase(bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
        }

        #endregion

        #region Properties

        public abstract int Columns
        {
            get;
        }

        public abstract int Rows
        {
            get;
        }

        #endregion

    }

}
