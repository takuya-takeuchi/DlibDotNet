using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class SamplePair : DlibObject
    {

        #region Constructors

        public SamplePair() :
            this(0, 0, 1)
        {
        }

        public SamplePair(uint index1, uint index2) :
            this(index1, index2, 1)
        {
        }

        public SamplePair(uint index1, uint index2, double distance)
        {
            this.NativePtr = NativeMethods.sample_pair_new(index1, index2, distance);
        }

        internal SamplePair(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public uint Index1
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.sample_pair_get_index1(this.NativePtr);
            }
        }

        public uint Index2
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.sample_pair_get_index2(this.NativePtr);
            }
        }

        public double Distance
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.sample_pair_get_distance(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.sample_pair_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
