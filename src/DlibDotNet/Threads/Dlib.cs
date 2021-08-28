#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ThreadDelegate(IntPtr param);

        #endregion

        #region Methods

        public static bool CreateNewThread(ThreadAction action, IntPtr param)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            return NativeMethods.create_new_thread(action.FunctionPointer, IntPtr.Zero);
        }

        #endregion

        public sealed class ThreadAction
        {

            #region Fields

            private readonly ThreadDelegate _Delegate;

            #endregion

            #region Constructors

            public ThreadAction(Action<IntPtr> action)
            {
                this._Delegate = new ThreadDelegate(action);
                this.FunctionPointer = Marshal.GetFunctionPointerForDelegate(this._Delegate);
            }

            #endregion

            #region Properties

            internal IntPtr FunctionPointer
            {
                get;
            }

            #endregion

        }

    }

}

#endif
