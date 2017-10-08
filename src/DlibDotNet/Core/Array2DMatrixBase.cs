// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Array2DMatrixBase : TwoDimentionObjectBase
    {

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

    }

}