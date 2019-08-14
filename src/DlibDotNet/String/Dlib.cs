// ReSharper disable once CheckNamespace

using System;

namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static string WrapString(string text, uint firstPad = 0, uint restPad = 0, uint maxPerLine = 79)
        {
            var str = Encoding.GetBytes(text ?? "");
            var strLength = str.Length;
            Array.Resize(ref str, strLength + 1);
            str[strLength] = (byte)'\0';
            var ret = NativeMethods.wrap_string_char(str, firstPad, restPad, maxPerLine);
            return StringHelper.FromStdString(ret, true);
        }

        #endregion

    }

}