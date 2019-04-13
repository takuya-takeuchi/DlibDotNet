// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {
        
        public static void Sleep(uint milliseconds)
        {
            NativeMethods.sleep(milliseconds);
        }

    }

}