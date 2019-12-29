using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void SRand(uint seed)
        {
            NativeMethods.stdlib_srand(seed);
        }

        #endregion

    }

}
