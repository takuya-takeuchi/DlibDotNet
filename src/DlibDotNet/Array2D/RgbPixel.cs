using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RgbPixel : IEquatable<RgbPixel>
    {

        #region Fields

        public byte Red;

        public byte Green;

        public byte Blue;

        #endregion

        #region Constructors

        public RgbPixel(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        #endregion

        #region Methods

        public bool Equals(RgbPixel other)
        {
            return this.Red == other.Red &&
                   this.Green == other.Green &&
                   this.Blue == other.Blue;
        }

        #region Overrids

        public override bool Equals(object obj)
        {
            return obj is RgbPixel pixel && Equals(pixel);
        }

        public override int GetHashCode()
        {
            var hashCode = -1058441243;
            hashCode = hashCode * -1521134295 + this.Red.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Green.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Blue.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(RgbPixel obj1, RgbPixel obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(RgbPixel obj1, RgbPixel obj2)
        {
            return !obj1.Equals(obj2);
        }

        #endregion

        #endregion

    }

}
