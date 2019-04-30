// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static string WrapString(string text, uint firstPad = 0, uint restPad = 0, uint maxPerLine = 79)
        {
            var str = Encoding.GetBytes(text ?? "");
            var ret = NativeMethods.wrap_string_char(str, firstPad, restPad, maxPerLine);
            return StringHelper.FromStdString(ret, true);
        }

        #endregion

    }

}