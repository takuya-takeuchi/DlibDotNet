using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RgbAlphaPixel : IEquatable<RgbAlphaPixel>
    {

        #region Fields

        public byte Red;

        public byte Green;

        public byte Blue;

        public byte Alpha;

        #endregion

        #region Methods

        public bool Equals(RgbAlphaPixel other)
        {
            return this.Red == other.Red &&
                   this.Green == other.Green &&
                   this.Blue == other.Blue &&
                   this.Alpha == other.Alpha;
        }

        #region Overrids

        public override bool Equals(object obj)
        {
            return obj is RgbAlphaPixel pixel && Equals(pixel);
        }

        public override int GetHashCode()
        {
            var hashCode = -712724744;
            hashCode = hashCode * -1521134295 + this.Red.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Green.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Blue.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Alpha.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(RgbAlphaPixel obj1, RgbAlphaPixel obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(RgbAlphaPixel obj1, RgbAlphaPixel obj2)
        {
            return !obj1.Equals(obj2);
        }

        #endregion

        #endregion

    }

}
