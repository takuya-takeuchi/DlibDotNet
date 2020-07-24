using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Represents an RGB color value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RgbPixel : IEquatable<RgbPixel>
    {

        #region Fields

        /// <summary>
        /// Gets or sets the red component of the color of this <see cref="RgbPixel"/>.
        /// </summary>
        /// <value>The red component of the color of this <see cref="RgbPixel"/>.</value>
        public byte Red;

        /// <summary>
        /// Gets or sets the green component of the color. of this <see cref="RgbPixel"/>.
        /// </summary>
        /// <value>The green component of the color of this <see cref="RgbPixel"/>.</value>
        public byte Green;

        /// <summary>
        /// Gets or sets the blue component of the color of this <see cref="RgbPixel"/>.
        /// </summary>
        /// <value>The blue component of the color of this <see cref="RgbPixel"/>.</value>
        public byte Blue;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RgbPixel"/> class with the specified RGB color value.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
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

        /// <summary>
        /// Specifies whether this <see cref="RgbPixel"/> contains the same RGB color value as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="RgbPixel"/> and has the same RGB color value as this <see cref="RgbPixel"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is RgbPixel pixel && Equals(pixel);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="RgbPixel"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="RgbPixel"/>.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1058441243;
            hashCode = hashCode * -1521134295 + this.Red.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Green.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Blue.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two <see cref="RgbPixel"/> objects. The result specifies whether the values of the <see cref="Red"/>, <see cref="Green"/> and <see cref="Blue"/> properties of the two <see cref="RgbPixel"/> objects are equal.
        /// </summary>
        /// <param name="left">A <see cref="RgbPixel"/> to compare.</param>
        /// <param name="right">A <see cref="RgbPixel"/> to compare.</param>
        /// <returns><code>true</code> if the <see cref="Red"/>, <see cref="Green"/> and <see cref="Blue"/> values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(RgbPixel left, RgbPixel right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="RgbPixel"/> objects. The result specifies whether the values of the <see cref="Red"/>, <see cref="Green"/> or <see cref="Blue"/> properties of the two <see cref="RgbPixel"/> objects are unequal.
        /// </summary>
        /// <param name="left">A <see cref="RgbPixel"/> to compare.</param>
        /// <param name="right">A <see cref="RgbPixel"/> to compare.</param>
        /// <returns><code>true</code> if the values of either the <see cref="Red"/> properties or the <see cref="Green"/> properties or the <see cref="Blue"/> properties of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <code>false</code>.</returns>
        public static bool operator !=(RgbPixel left, RgbPixel right)
        {
            return !left.Equals(right);
        }

        #endregion

        #endregion

    }

}
