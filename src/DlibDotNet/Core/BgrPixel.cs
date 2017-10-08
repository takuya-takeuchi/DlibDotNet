using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BgrPixel
    {

        public byte Blue;

        public byte Green;

        public byte Red;

    }

}
