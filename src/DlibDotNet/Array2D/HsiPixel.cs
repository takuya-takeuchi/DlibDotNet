using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HsiPixel : IEquatable<HsiPixel>
    {

        #region Fields

        public byte H;

        public byte S;

        public byte I;

        #endregion

        #region Methods

        public bool Equals(HsiPixel other)
        {
            return this.H == other.H &&
                   this.S == other.S &&
                   this.I == other.I;
        }

        #region Overrids

        public override bool Equals(object obj)
        {
            return obj is HsiPixel pixel && Equals(pixel);
        }

        public override int GetHashCode()
        {
            var hashCode = -1515328067;
            hashCode = hashCode * -1521134295 + this.H.GetHashCode();
            hashCode = hashCode * -1521134295 + this.S.GetHashCode();
            hashCode = hashCode * -1521134295 + this.I.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(HsiPixel obj1, HsiPixel obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(HsiPixel obj1, HsiPixel obj2)
        {
            return !obj1.Equals(obj2);
        }

        #endregion

        #endregion

    }

}
