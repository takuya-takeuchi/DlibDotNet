using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HsiPixel
    {

        public byte H;

        public byte S;

        public byte I;

    }

}
