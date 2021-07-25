using System.Collections.Generic;
using UltraFaceDotNet;

namespace Demo.Models
{

    public sealed class DetectResult
    {

        #region Constructors

        public DetectResult(int width, int height, IEnumerable<FaceInfo> boxes)
        {
            this.Width = width;
            this.Height = height;
            this.Boxes = new List<FaceInfo>(boxes);
        }

        #endregion

        #region Properties

        public IReadOnlyCollection<FaceInfo> Boxes
        {
            get;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        #endregion

    }

}