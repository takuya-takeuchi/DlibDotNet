using System;

namespace DlibDotNet
{

    /// <summary>
    /// The exception is general exception for image loading on dlib. This class cannot be inherited.
    /// </summary>
    public sealed class ImageLoadException : Exception
    {

        #region Constructors

        internal ImageLoadException(string message) :
            base(message)
        {
        }

        internal ImageLoadException(string filepath, string message):
            base(message)
        {
            this.FilePath = filepath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the file path which occurs error.
        /// </summary>
        public string FilePath
        {
            get;
        }

        #endregion

    }

}
