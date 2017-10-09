using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RgbPixel
    {

        public byte Red;

        public byte Green;

        public byte Blue;

    }

}
