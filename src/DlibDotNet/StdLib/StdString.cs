using System;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdString : DlibObject
    {

        #region Constructors

        public StdString()
        {
            this.NativePtr = NativeMethods.string_new();
        }

        public StdString(string str)
        {
            var sb = new StringBuilder(str ?? "");
            this.NativePtr = NativeMethods.string_new2(sb, sb.Length);
        }

        internal StdString(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            if (this.IsDisposed)
                return;

            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.string_delete(this.NativePtr);
            this.NativePtr = IntPtr.Zero;
        }
        
        public override string ToString()
        {
            this.ThrowIfDisposed();
            return StringHelper.FromStdString(this.NativePtr);
        }

        #endregion

        #endregion

    }

}