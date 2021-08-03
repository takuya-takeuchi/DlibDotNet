using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void MessageBox(string title, string message)
        {
#if !DLIB_NO_GUI_SUPPORT
            var t = Encoding.GetBytes(title ?? "");
            var m = Encoding.GetBytes(message ?? "");
            NativeMethods.message_box(t, t.Length, m, m.Length);
#else
            throw new NotSupportedException();
#endif
        }

        public static void SaveFileBox(StringActionMediator mediator)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            mediator.ThrowIfDisposed();

            NativeMethods.save_file_box(mediator.NativePtr);
            GC.KeepAlive(mediator);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

    }

}