#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class InterestPoint : DlibObject
    {

        #region Constructors

        internal InterestPoint(IntPtr ptr)
            : this(ptr, true)
        {
        }

        internal InterestPoint(IntPtr ptr, bool isEnabledDispose)
            : base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public Vector<double> Center
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.interest_point_get_center(this.NativePtr);

                // Can not dispose because this unmanged data is in interest_point object
                return new Vector<double>(ret, false);
            }
        }

        public double Laplacian
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.interest_point_get_laplacian(this.NativePtr);
            }
        }

        public double Scale
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.interest_point_get_scale(this.NativePtr);
            }
        }

        public double Score
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.interest_point_get_score(this.NativePtr);
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

            NativeMethods.interest_point_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}

#endif
