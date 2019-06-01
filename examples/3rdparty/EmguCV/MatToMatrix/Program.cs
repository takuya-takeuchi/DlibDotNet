using DlibDotNet;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace MatToMatrix
{

    internal class Program
    {

        private static void Main()
        {
            using (var mat = CvInvoke.Imread("Lenna.png", ImreadModes.AnyColor))
            {
                var array = new byte[mat.Width * mat.Height * mat.ElementSize];
                mat.CopyTo(array);

                // TODO: support BGR image
                using (var win = new ImageWindow())
                using (var image = new DlibDotNet.Matrix<RgbPixel>(array, mat.Height, mat.Width, mat.ElementSize))
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