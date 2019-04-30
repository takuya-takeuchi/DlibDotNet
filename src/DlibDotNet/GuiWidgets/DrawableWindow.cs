using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class DrawableWindow : BaseWindow
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EventDelegate();

        #endregion

    }

}