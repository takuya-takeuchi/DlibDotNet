namespace DlibDotNet.Dnn
{

    public abstract class Net : DlibObject
    {

        #region Constructors

        protected Net(int type = 0)
        {
            this.Type = type;
        }

        #endregion

        #region Properties

        protected int Type
        {
            get;
            private set;
        }

        #endregion

    }

}