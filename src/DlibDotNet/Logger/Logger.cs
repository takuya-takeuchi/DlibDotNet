using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Logger : DlibObject
    {

        #region Constructors

        public Logger(string name)
        {
            var nameByte = Dlib.Encoding.GetBytes(name ?? "");
            var strLength = nameByte.Length;
            Array.Resize(ref nameByte, strLength + 1);
            nameByte[strLength] = (byte)'\0';
            this.NativePtr = NativeMethods.logger_new(nameByte);
        }

        #endregion

        #region Methods

        public void SetLevel(LogLevel level)
        {
            this.ThrowIfDisposed();

            NativeMethods.logger_set_level(this.NativePtr, level);
        }

        public void WriteLine(LogLevel level, string message)
        {
            this.ThrowIfDisposed();

            var messageByte = Dlib.Encoding.GetBytes(message ?? "");
            var strLength = messageByte.Length;
            Array.Resize(ref messageByte, strLength + 1);
            messageByte[strLength] = (byte)'\0';
            NativeMethods.logger_operator_left_shift(this.NativePtr, level, messageByte);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.logger_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}