using DlibDotNet;
namespace PackageReference
{
    using System;
    public static class Program
    {
        public static void Main(string[] args)
        {
            var detector = Dlib.GetFrontalFaceDetector();
            if (detector == null)
            {
                throw new Exception("'DlibDotNet.Native' should be in 'runtime/[win-x64|linux-x64|osx-x64]/native' ");
            }
            detector.ThrowIfDisposed();
            Console.WriteLine(detector.NativePtr.ToInt64());
        }
    }
}