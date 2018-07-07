namespace DlibDotNet.Dnn
{

    public abstract class Net : DlibObject
    {

        #region Constructors

        protected Net(int networkType = 0)
        {
            this.NetworkType = networkType;
        }

        #endregion

        #region Properties

        public abstract int NumLayers
        {
            get;
        }

        internal int NetworkType
        {
            get;
            private set;
        }

        #endregion
        
        #region Methods

        public abstract void Clean();

        #endregion

    }

}