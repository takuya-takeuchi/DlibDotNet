using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Represents an Lab colored graphical pixel.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LabPixel : IEquatable<LabPixel>
    {

        #region Fields
        
        /// <summary>
        /// Gets or sets a lightness component of this <see cref="LabPixel"/>.
        /// </summary>
        /// <value>The lightness component of this <see cref="LabPixel"/>.</value>
        public byte L;

        /// <summary>
        /// Gets or sets the component corresponds to between green to red component of this <see cref="LabPixel"/>.
        /// </summary>
        /// <value>The component corresponds to between green to red component of this <see cref="LabPixel"/>.</value>
        public byte A;

        /// <summary>
        /// Gets or sets the component corresponds to between blue to yellow component of this <see cref="LabPixel"/>.
        /// </summary>
        /// <value>The component corresponds to between blue to yellow component of this <see cref="LabPixel"/>.</value>
        public byte B;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LabPixel"/> class with the specified Lab color value.
        /// </summary>
        /// <param name="l">The lightness component of the color.</param>
        /// <param name="a">The component corresponds to between green to red component of the color.</param>
        /// <param name="b">The component corresponds to between blue to yellow component of the color.</param>
        public LabPixel(byte l, byte a, byte b)
        {
            this.L = l;
            this.A = a;
            this.B = b;
        }

        #endregion

        #region Methods

        public bool Equals(LabPixel other)
        {
            return this.L == other.L &&
                   this.A == other.A &&
                   this.B == other.B;
        }

        #region Overrids

        /// <summary>
        /// Specifies whether this <see cref="LabPixel"/> contains the same RGB color value as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="LabPixel"/> and has the same RGB color value as this <see cref="LabPixel"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is LabPixel pixel && Equals(pixel);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="LabPixel"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="LabPixel"/>.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1058441243;
            hashCode = hashCode * -1521134295 + this.L.GetHashCode();
            hashCode = hashCode * -1521134295 + this.A.GetHashCode();
            hashCode = hashCode * -1521134295 + this.B.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two <see cref="LabPixel"/> objects. The result specifies whether the values of the <see cref="L"/>, <see cref="A"/> and <see cref="B"/> properties of the two <see cref="LabPixel"/> objects are equal.
        /// </summary>
        /// <param name="left">A <see cref="LabPixel"/> to compare.</param>
        /// <param name="right">A <see cref="LabPixel"/> to compare.</param>
        /// <returns><code>true</code> if the <see cref="L"/>, <see cref="A"/> and <see cref="B"/> values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(LabPixel left, LabPixel right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="LabPixel"/> objects. The result specifies whether the values of the <see cref="L"/>, <see cref="A"/> or <see cref="B"/> properties of the two <see cref="LabPixel"/> objects are unequal.
        /// </summary>
        /// <param name="left">A <see cref="LabPixel"/> to compare.</param>
        /// <param name="right">A <see cref="LabPixel"/> to compare.</param>
        /// <returns><code>true</code> if the values of either the <see cref="L"/> properties or the <see cref="A"/> properties or the <see cref="B"/> properties of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <code>false</code>.</returns>
        public static bool operator !=(LabPixel left, LabPixel right)
        {
            return !left.Equals(right);
        }

        #endregion

        #endregion

    }

}
