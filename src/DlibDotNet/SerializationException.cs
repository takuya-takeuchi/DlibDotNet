#if !LITE
using System;

namespace DlibDotNet
{

    /// <summary>
    /// The exception is general exception for serialization and deserialization on dlib. This class cannot be inherited.
    /// </summary>
    public sealed class SerializationException : Exception
    {

        #region Constructors

        internal SerializationException(string message):
            base(message)
        {
        }

        #endregion

    }

}

#endif
