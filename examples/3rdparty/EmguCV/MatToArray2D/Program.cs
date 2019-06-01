using DlibDotNet;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace MatToArray2D
{

    internal class Program
    {

        private static void Main()
        {
            using (var mat = CvInvoke.Imread("Lenna.png", ImreadModes.AnyColor))
            {
                var array = new byte[mat.Width * mat.Height * mat.ElementSize];
                mat.CopyTo(array);

                using (var win = new ImageWindow())
                using (var image = Dlib.LoadImageData<RgbPixel>(array, (uint)mat.Height, (uint)mat.Width, (uint)(mat.Width * mat.ElementSize)))
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