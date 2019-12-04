using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Rand : DlibObject
    {

        #region Constructors

        public Rand()
        {
            this.NativePtr = NativeMethods.rand_new();
        }

        public Rand(ulong seed)
        {
            this.NativePtr = NativeMethods.rand_new2(seed);
        }

        #endregion

        #region Methods

        public long GetIntegerInRange(long begin, long end)
        {
            this.ThrowIfDisposed();

            if (!(begin <= end))
                throw new ArgumentOutOfRangeException();

            return NativeMethods.rand_get_integer_in_range(this.NativePtr, begin, end);
        }

        public double GetRandomDouble()
        {
            this.ThrowIfDisposed();
            return NativeMethods.rand_get_random_double(this.NativePtr);
        }

        public byte GetRandom8BitNumber()
        {
            this.ThrowIfDisposed();
            return NativeMethods.rand_get_random_8bit_number(this.NativePtr);
        }

        public uint GetRandom32BitNumber()
        {
            this.ThrowIfDisposed();
            return NativeMethods.rand_get_random_32bit_number(this.NativePtr);
        }

        public double GetRandomGaussian()
        {
            this.ThrowIfDisposed();
            return NativeMethods.rand_get_random_gaussian(this.NativePtr);
        }

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.rand_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
