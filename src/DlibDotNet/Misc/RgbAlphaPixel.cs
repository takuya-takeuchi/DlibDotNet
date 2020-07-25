using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Represents an ARGB color value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RgbAlphaPixel : IEquatable<RgbAlphaPixel>
    {

        #region Fields

        /// <summary>
        /// Gets or sets the red component of the color of this <see cref="RgbAlphaPixel"/>.
        /// </summary>
        /// <value>The red component of the color of this <see cref="RgbAlphaPixel"/>.</value>
        public byte Red;

        /// <summary>
        /// Gets or sets the green component of the color of this <see cref="RgbAlphaPixel"/>.
        /// </summary>
        /// <value>The green component of the color of this <see cref="RgbAlphaPixel"/>.</value>
        public byte Green;

        /// <summary>
        /// Gets or sets the blue component of the color of this <see cref="RgbAlphaPixel"/>.
        /// </summary>
        /// <value>The blue component of the color of this <see cref="RgbAlphaPixel"/>.</value>
        public byte Blue;

        /// <summary>
        /// Gets or sets the alpha component of the color of this <see cref="RgbAlphaPixel"/>.
        /// </summary>
        /// <value>The alpha component of the color of this <see cref="RgbAlphaPixel"/>.</value>
        public byte Alpha;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RgbAlphaPixel"/> class with the specified ARGB color value.
        /// </summary>
        /// <param name="alpha">The alpha component of the color.</param>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public RgbAlphaPixel(byte alpha, byte red, byte green, byte blue)
        {
            this.Alpha = alpha;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

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

        /// <summary>
        /// Specifies whether this <see cref="RgbAlphaPixel"/> contains the same ARGB color value as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="RgbAlphaPixel"/> and has the same ARGB color value as this <see cref="RgbAlphaPixel"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is RgbAlphaPixel pixel && Equals(pixel);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="RgbAlphaPixel"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="RgbAlphaPixel"/>.</returns>
        public override int GetHashCode()
        {
            var hashCode = -712724744;
            hashCode = hashCode * -1521134295 + this.Red.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Green.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Blue.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Alpha.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two <see cref="RgbAlphaPixel"/> objects. The result specifies whether the values of the <see cref="Alpha"/>, <see cref="Red"/>, <see cref="Green"/> and <see cref="Blue"/> properties of the two <see cref="RgbAlphaPixel"/> objects are equal.
        /// </summary>
        /// <param name="left">A <see cref="RgbAlphaPixel"/> to compare.</param>
        /// <param name="right">A <see cref="RgbAlphaPixel"/> to compare.</param>
        /// <returns><code>true</code> if the <see cref="Alpha"/>, <see cref="Red"/>, <see cref="Green"/> and <see cref="Blue"/> values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(RgbAlphaPixel left, RgbAlphaPixel right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="RgbAlphaPixel"/> objects. The result specifies whether the values of the <see cref="Alpha"/>, <see cref="Red"/>, <see cref="Green"/> or <see cref="Blue"/> properties of the two <see cref="RgbAlphaPixel"/> objects are unequal.
        /// </summary>
        /// <param name="left">A <see cref="RgbAlphaPixel"/> to compare.</param>
        /// <param name="right">A <see cref="RgbAlphaPixel"/> to compare.</param>
        /// <returns><code>true</code> if the values of either the <see cref="Alpha"/> properties or the <see cref="Red"/> properties or the <see cref="Green"/> properties or the <see cref="Blue"/> properties of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <code>false</code>.</returns>
        public static bool operator !=(RgbAlphaPixel left, RgbAlphaPixel right)
        {
            return !left.Equals(right);
        }

        #endregion

        #endregion

    }

}
