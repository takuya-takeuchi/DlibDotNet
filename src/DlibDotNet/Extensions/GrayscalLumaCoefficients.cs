// ReSharper disable once CheckNamespace
namespace DlibDotNet.Extensions
{

    /// <summary>
    /// Define the standard for converting RGB values to luminance when gray-scaled.
    /// </summary>
    public enum GrayscalLumaCoefficients
    {

        /// <summary>
        /// Operate gray-scaled by BT.601 of ITU-R (ITU Radiocommunication Sector) Recommendation. Luminance of formula is 0.299 R + 0.587 G + 0.114 B.
        /// </summary>
        ITU_R_BT_601,

        /// <summary>
        /// Operate gray-scaled by BT.709 of ITU-R (ITU Radiocommunication Sector) Recommendation. Luminance of formula is 0.2126 R + 0.7152 G + 0.0722 B.
        /// </summary>
        ITU_R_BT_709,

        /// <summary>
        /// Operate gray-scaled by 240M specification SMPTER (Society of Motion Picture and Television Engineers) established. Luminance of formula is 0.212 R + 0.701 G + 0.087 B.
        /// </summary>
        SMPTE_240M

    }

}
