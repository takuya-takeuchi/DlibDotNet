// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class KernelBase : DlibObject
    {

        #region Constructors

        protected KernelBase(int templateRow = 0, int templateColumn = 0)
        {
            this.TemplateRows = templateRow;
            this.TemplateColumns = templateColumn;
        }

        #endregion

        #region Properties

        internal int TemplateColumns
        {
            get;
        }

        internal int TemplateRows
        {
            get;
        }

        #endregion

    }

}