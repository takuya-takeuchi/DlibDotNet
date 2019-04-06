using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Represents an HSI color space value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HsiPixel : IEquatable<HsiPixel>
    {

        #region Fields

        /// <summary>
        /// Gets or sets the hue component of this <see cref="HsiPixel"/>.
        /// </summary>
        /// <value>The hue component of this <see cref="HsiPixel"/>.</value>
        public byte H;

        /// <summary>
        /// Gets or sets the saturation component of this <see cref="HsiPixel"/>.
        /// </summary>
        /// <value>The saturation component of this <see cref="HsiPixel"/>.</value>
        public byte S;

        /// <summary>
        /// Gets or sets the intensity component of this <see cref="HsiPixel"/>.
        /// </summary>
        /// <value>The intensity component of this <see cref="HsiPixel"/>.</value>
        public byte I;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HsiPixel"/> class with the specified HSI color space value.
        /// </summary>
        /// <param name="hue">The the hue component of the color space.</param>
        /// <param name="saturation">The saturation component of the color space.</param>
        /// <param name="intensity">The intensity component of the color space.</param>
        public HsiPixel(byte hue, byte saturation, byte intensity)
        {
            this.H = hue;
            this.S = saturation;
            this.I = intensity;
        }

        #endregion

        #region Methods

        public bool Equals(HsiPixel other)
        {
            return this.H == other.H &&
                   this.S == other.S &&
                   this.I == other.I;
        }

        #region Overrids

        /// <summary>
        /// Specifies whether this <see cref="HsiPixel"/> contains the same  HSI color space value as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="HsiPixel"/> and has the same  HSI color space value as this <see cref="HsiPixel"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is HsiPixel pixel && Equals(pixel);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="HsiPixel"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="HsiPixel"/>.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1515328067;
            hashCode = hashCode * -1521134295 + this.H.GetHashCode();
            hashCode = hashCode * -1521134295 + this.S.GetHashCode();
            hashCode = hashCode * -1521134295 + this.I.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two <see cref="HsiPixel"/> objects. The result specifies whether the values of the <see cref="H"/>, <see cref="S"/> and <see cref="I"/> properties of the two <see cref="HsiPixel"/> objects are equal.
        /// </summary>
        /// <param name="left">A <see cref="HsiPixel"/> to compare.</param>
        /// <param name="right">A <see cref="HsiPixel"/> to compare.</param>
        /// <returns><code>true</code> if the <see cref="H"/>, <see cref="S"/> and <see cref="I"/> values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(HsiPixel left, HsiPixel right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="HsiPixel"/> objects. The result specifies whether the values of the <see cref="H"/>, <see cref="S"/> or <see cref="I"/> properties of the two <see cref="HsiPixel"/> objects are unequal.
        /// </summary>
        /// <param name="left">A <see cref="HsiPixel"/> to compare.</param>
        /// <param name="right">A <see cref="HsiPixel"/> to compare.</param>
        /// <returns><code>true</code> if the values of either the <see cref="H"/> properties or the <see cref="S"/> properties or the <see cref="I"/> properties of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <code>false</code>.</returns>
        public static bool operator !=(HsiPixel left, HsiPixel right)
        {
            return !left.Equals(right);
        }

        #endregion

        #endregion

    }

}
