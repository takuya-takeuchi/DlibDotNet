// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Array2DMatrixBase : TwoDimensionObjectBase
    {

        protected Array2DMatrixBase(int templateRows = 0, int temlateColumns = 0, bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
            this.TemplateRows = templateRows;
            this.TemplateColumns = temlateColumns;
        }

        public abstract MatrixElementTypes MatrixElementType
        {
            get;
        }

        public abstract Rectangle Rect
        {
            get;
        }

        public abstract int Size
        {
            get;
        }

        internal int TemplateColumns
        {
            get;
        }

        internal int TemplateRows
        {
            get;
        }

    }

}