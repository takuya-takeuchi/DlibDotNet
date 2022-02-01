namespace DlibDotNet.Dnn
{

    public abstract class Solver : DlibObject
    {

        #region Properties

        public abstract int SolverType
        {
            get;
        }
        
        #endregion

    }

}