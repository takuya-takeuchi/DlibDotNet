using System;
using System.IO;

namespace ImgLab
{

    partial class Program
    {

        #region

        private const int JpegQuality = 90;

        #endregion

        #region Methods

        private static void MakeEmptyFile(string filename)
        {
            try
            {
                File.WriteAllBytes(filename, new byte[] { 0 });
            }
            catch (Exception)
            {
                throw new Exception($"ERROR: Unable to open {filename} for writing.");
            }
        }

        private static string StripPath(string str, string prefix)
        {
            var i = 0;
            for (i = 0; i < str.Length && i < prefix.Length; ++i)
                if (str[i] != prefix[i])
                    return str;

            if (i < str.Length && (str[i] == '/' || str[i] == '\\'))
                ++i;

            return str.Substring(i);
        }

        private static string ToJpgName(string filename)
        {
            return Path.ChangeExtension(filename, ".jpg");
        }

        private static string ToPngName(string filename)
        {
            return Path.ChangeExtension(filename, ".png");
        }

        #endregion

    }

}
