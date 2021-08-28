namespace DlibDotNet
{

    public sealed class MatrixTemplateSizeParameter : IParameter
    {

        #region Constructors

        public MatrixTemplateSizeParameter(int templateRows, int templateColumns)
        {
            this.TemplateRows = templateRows;
            this.TemplateColumns = templateColumns;
        }

        #endregion

        #region Properties

        public int TemplateRows
        {
            get;
        }

        public int TemplateColumns
        {
            get;
        }

        #endregion

    }

}
