using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class CustomLogger : DlibObject
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void LogDelegate(IntPtr logName, LogLevel logLevel, IntPtr levelName, ulong threadId, IntPtr message);

        #endregion

        #region Fields

        private readonly DelegateHandler<LogDelegate> _Log;

        #endregion

        #region Constructors

        protected CustomLogger()
        {
            this._Log = new DelegateHandler<LogDelegate>(this.LogNative);
            this.NativePtr = NativeMethods.custom_logger_new(this._Log.Handle);
        }

        #endregion

        #region Methods

        public abstract void Log(string logName, LogLevel logLevel, string levelName, ulong threadId, string message);

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.custom_logger_delete(this.NativePtr);
        }

        #endregion

        #region Helpers

        private void LogNative(IntPtr logName, LogLevel logLevel, IntPtr levelName, ulong threadId, IntPtr message)
        {
            this.Log(StringHelper.FromStdString(logName),
                     logLevel,
                     StringHelper.FromStdString(levelName),
                     threadId,
                     StringHelper.FromStdString(message));
        }

        #endregion

        #endregion

    }

}