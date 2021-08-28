#if !LITE
namespace DlibDotNet
{

    /// <summary>
    /// The ImagePixelFormat enumeration specifies the format of the color data for each pixel in the image.
    /// </summary>
    public enum ImagePixelFormat
    {

        /// <summary>
        /// The pixel format is B8G8R8 unsigned integer.
        /// </summary>
        Bgr,

        /// <summary>
        /// The pixel format is B8G8R8A8 unsigned integer.
        /// </summary>
        Bgra,

        /// <summary>
        /// The pixel format is R8G8B8 unsigned integer.
        /// </summary>
        Rgb,

        /// <summary>
        /// The pixel format is R8G8B8A8 unsigned integer.
        /// </summary>
        Rgba

    }

}

#endif
