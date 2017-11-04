using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LabPixel
    {

        public byte L;

        public byte A;

        public byte B;

    }

}
