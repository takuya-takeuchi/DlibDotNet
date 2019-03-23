namespace DlibDotNet.Dnn
{

    public abstract class Net : DlibObject
    {

        #region Constructors

        protected Net(int networkType = 0, bool isEnabledDispose = true)
            : base(isEnabledDispose)
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

        internal abstract DPoint InputTensorToOutputTensor(DPoint p);

        public abstract bool TryGetInputLayer<T>(T layer)
            where T : Input;

        #endregion

    }

}