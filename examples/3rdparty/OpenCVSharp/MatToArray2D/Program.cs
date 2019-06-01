using System.Runtime.InteropServices;
using DlibDotNet;
using OpenCvSharp;

namespace MatToArray2D
{

    internal class Program
    {

        private static void Main()
        {
            using (var mat = Cv2.ImRead("Lenna.png", ImreadModes.AnyColor))
            {
                var array = new byte[mat.Width * mat.Height * mat.ElemSize()];
                Marshal.Copy(mat.Data, array, 0, array.Length);

                using (var win = new ImageWindow())
                using (var image = Dlib.LoadImageData<RgbPixel>(array, (uint)mat.Height, (uint)mat.Width, (uint)(mat.Width * mat.ElemSize())))
                {
                    // Display it all on the screen
                    win.ClearOverlay();
                    win.SetImage(image);

                    win.WaitUntilClosed();
                }
            }
        }

    }

}