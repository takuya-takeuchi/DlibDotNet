// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Provides the methods of dlib.
    /// </summary>
    public static partial class Dlib
    {

        #region Methods

        public static void MessageBox(string title, string message)
        {
            var t = Dlib.Encoding.GetBytes(title ?? "");
            var m = Dlib.Encoding.GetBytes(message ?? "");
            NativeMethods.message_box(t, m);
        }

        #endregion

    }

}