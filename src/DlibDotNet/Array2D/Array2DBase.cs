// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Array2DBase : TwoDimentionObjectBase
    {

        #region Constructors

        protected Array2DBase(bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
        }

        #endregion

        public abstract ImageTypes ImageType
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