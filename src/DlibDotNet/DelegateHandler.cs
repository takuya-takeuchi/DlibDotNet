#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed class DelegateHandler<T>
    {

        #region Fields

        private readonly T _Delegate;

        #endregion

        #region Constructors

        public DelegateHandler(T @delegate)
        {
            this._Delegate = @delegate;
            this.Handle = Marshal.GetFunctionPointerForDelegate(this._Delegate);
        }

        #endregion

        #region Properties

        public IntPtr Handle
        {
            get;
        }

        #endregion

    }

}
#endif
