using System.Collections.Generic;

namespace Demo.Models
{

    public sealed class DetectResult
    {

        #region Constructors

        public DetectResult(int width, int height, IEnumerable<Face> faces)
        {
            this.Width = width;
            this.Height = height;
            this.Faces = new List<Face>(faces);
        }

        #endregion

        #region Properties

        public IReadOnlyCollection<Face> Faces
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