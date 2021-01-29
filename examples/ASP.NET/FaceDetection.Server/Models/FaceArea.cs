namespace FaceDetection.Models
{

    /// <summary>
    /// Describes the left, top, right and bottom location of a face.
    /// </summary>
    public class FaceArea
    {

        #region Properties

        /// <summary>
        /// The y-axis value of the bottom of the rectangle of face.
        /// </summary>
        public int Bottom
        {
            get;
            set;
        }

        /// <summary>
        /// The x-axis value of the left side of the rectangle of face.
        /// </summary>
        public int Left
        {
            get;
            set;
        }

        /// <summary>
        /// The x-axis value of the right side of the rectangle of face.
        /// </summary>
        public int Right
        {
            get;
            set;
        }

        /// <summary>
        /// The y-axis value of the top of the rectangle of face.
        /// </summary>
        public int Top
        {
            get;
            set;
        }

        #endregion

    }

}
