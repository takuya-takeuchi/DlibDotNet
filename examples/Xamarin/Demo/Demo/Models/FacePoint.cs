using DlibDotNet;

namespace Demo.Models
{

    public sealed class FacePoint
    {

        #region Constructors

        public FacePoint(Point point, int index)
        {
            this.Point = point;
            this.Index = index;
        }

        #endregion

        #region Properties

        public Point Point
        {
            get;
        }

        public int Index
        {
            get;
        }

        #endregion

    }

}