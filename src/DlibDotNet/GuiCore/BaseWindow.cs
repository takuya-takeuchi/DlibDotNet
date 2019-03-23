

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class BaseWindow : DlibObject
    {

        #region Properties

        public string Title
        {
            set
            {
                this.ThrowIfDisposed();
                var title = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.base_window_set_title(this.NativePtr, title);
            }
        }

        #endregion

        #region Methods

        public void WaitUntilClosed()
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_wait_until_closed(this.NativePtr);
        }

        #endregion

    }

}
