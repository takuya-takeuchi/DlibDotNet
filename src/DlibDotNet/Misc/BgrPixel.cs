#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Represents an BGR color value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BgrPixel : IEquatable<BgrPixel>
    {

        #region Fields

        /// <summary>
        /// Gets or sets the blue component of the color of this <see cref="BgrPixel"/>.
        /// </summary>
        /// <value>The blue component of the color of this <see cref="BgrPixel"/>.</value>
        public byte Blue;

        /// <summary>
        /// Gets or sets the green component of the color of this <see cref="BgrPixel"/>.
        /// </summary>
        /// <value>The green component of the color of this <see cref="BgrPixel"/>.</value>
        public byte Green;

        /// <summary>
        /// Gets or sets the red component of the color of this <see cref="BgrPixel"/>.
        /// </summary>
        /// <value>The red component of the color of this <see cref="BgrPixel"/>.</value>
        public byte Red;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BgrPixel"/> class with the specified BGR color value.
        /// </summary>
        /// <param name="blue">The blue component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="red">The red component of the color.</param>
        public BgrPixel(byte blue, byte red, byte green)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        #endregion

        #region Methods

        public bool Equals(BgrPixel other)
        {
            return this.Blue == other.Blue && this.Green == other.Green && this.Red == other.Red;
        }

        #region Overrids

        /// <summary>
        /// Specifies whether this <see cref="BgrPixel"/> contains the same BGR color value as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="BgrPixel"/> and has the same BGR color value as this <see cref="BgrPixel"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is BgrPixel other && Equals(other);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="BgrPixel"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="BgrPixel"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Blue.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Green.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Red.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Compares two <see cref="BgrPixel"/> objects. The result specifies whether the values of the <see cref="Blue"/>, <see cref="Green"/> and <see cref="Red"/> properties of the two <see cref="BgrPixel"/> objects are equal.
        /// </summary>
        /// <param name="left">A <see cref="BgrPixel"/> to compare.</param>
        /// <param name="right">A <see cref="BgrPixel"/> to compare.</param>
        /// <returns><code>true</code> if the <see cref="Blue"/>, <see cref="Green"/> and <see cref="Red"/> values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(BgrPixel left, BgrPixel right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="BgrPixel"/> objects. The result specifies whether the values of the <see cref="Blue"/>, <see cref="Green"/> or <see cref="Red"/> properties of the two <see cref="BgrPixel"/> objects are unequal.
        /// </summary>
        /// <param name="left">A <see cref="BgrPixel"/> to compare.</param>
        /// <param name="right">A <see cref="BgrPixel"/> to compare.</param>
        /// <returns><code>true</code> if the values of either the <see cref="Red"/> properties or the <see cref="Green"/> properties or the <see cref="Blue"/> properties of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <code>false</code>.</returns>
        public static bool operator !=(BgrPixel left, BgrPixel right)
        {
            return !left.Equals(right);
        }

        #endregion

        #endregion

    }

}

#endif
