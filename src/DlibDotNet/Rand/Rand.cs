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

        #endregion

        #region Methods

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
