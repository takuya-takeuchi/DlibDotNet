// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class TwoDimensionObjectBase : DlibObject
    {

        #region Constructors

        protected TwoDimensionObjectBase(bool isEnabledDispose = true)
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
