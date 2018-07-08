using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RgbPixel
    {

        public RgbPixel(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public byte Red;

        public byte Green;

        public byte Blue;

    }

}
