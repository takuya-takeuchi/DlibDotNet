using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static MatrixRangeExp<double> Linspace(double start, double end, int num)
        {
            var matrixRange = Native.linspace(start, end, num);
            return new MatrixRangeExp<double>(matrixRange);
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr linspace(double start, double end, int num);
        }

    }

}