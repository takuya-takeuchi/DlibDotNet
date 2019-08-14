using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void MessageBox(string title, string message)
        {
            var t = Encoding.GetBytes(title ?? "");
            var strLength = t.Length;
            Array.Resize(ref t, strLength + 1);
            t[strLength] = (byte)'\0';
            var m = Encoding.GetBytes(message ?? "");
            strLength = m.Length;
            Array.Resize(ref m, strLength + 1);
            m[strLength] = (byte)'\0';
            NativeMethods.message_box(t, m);
        }

        public static void SaveFileBox(StringActionMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            mediator.ThrowIfDisposed();

            NativeMethods.save_file_box(mediator.NativePtr);
            GC.KeepAlive(mediator);
        }

        #endregion

    }

}