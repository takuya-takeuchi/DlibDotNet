using System.ComponentModel.DataAnnotations;

namespace FaceDetection.Models
{

    /// <summary>
    /// Represents a image data.
    /// </summary>
    public class Image
    {

        #region Properties

        /// <summary>
        /// The image data.
        /// </summary>
        [Required]
        public byte[] Data
        {
            get;
            set;
        }

        #endregion

    }

}
