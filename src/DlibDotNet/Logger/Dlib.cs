#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void SetAllLoggingOutputHooks(CustomLogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.ThrowIfDisposed();

            NativeMethods.custom_set_all_logging_output_hooks(logger.NativePtr);
        }

        #endregion

    }

}
#endif
