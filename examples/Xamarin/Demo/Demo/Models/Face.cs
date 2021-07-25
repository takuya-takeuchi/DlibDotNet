using System.Collections.Generic;
using DlibDotNet;

namespace Demo.Models
{

    public sealed class Face
    {

        #region Constructors

        public Face(IEnumerable<FacePoint> points, Rectangle rect)
        {
            this.Points = points;
            this.Rect = rect;
        }

        #endregion

        #region Properties

        public IEnumerable<FacePoint> Points
        {
            get;
        }

        public Rectangle Rect
        {
            get;
        }

        #endregion

    }

}