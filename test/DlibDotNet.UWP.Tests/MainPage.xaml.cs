using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using DlibDotNet;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace DlibDotNet.UWP.Tests
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        #region Fields

        private Matrix<RgbPixel> _LoadedMatrix;

        private Array2D<RgbPixel> _Result;

        #endregion

        #region Constructors

        public MainPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        #region Event Handlers

        private async void LoadButtonClick(object sender, RoutedEventArgs e)
        {
            this._Result?.Dispose();
            this._LoadedMatrix?.Dispose();

            this._Result = Dlib.LoadImage<RgbPixel>("lenna.bmp");
            this._LoadedMatrix = new Matrix<RgbPixel>(this._Result);

            this._Image.Source = await ToBitmapSource(this._LoadedMatrix);
        }

        private async void DetectButtonClick(object sender, RoutedEventArgs e)
        {
            using (var faceDetector = Dlib.GetFrontalFaceDetector())
            {
                var dets = faceDetector.Operator(this._LoadedMatrix);
                foreach (var r in dets)
                    Dlib.DrawRectangle(this._Result, r, new RgbPixel { Green = 255 });
            }

            BitmapImage image;
            using (var tmp = new Matrix<RgbPixel>(this._Result))
                image = await ToBitmapSource(tmp);

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this._Image.Source = image;
            });
        }

        #endregion

        #region Helpers

        private static async Task<BitmapImage> ToBitmapSource(Matrix<RgbPixel> matrix)
        {
            var width = matrix.Columns;
            var height = matrix.Rows;

            var bitmap = new WriteableBitmap(width, height);
            using (var imras = new InMemoryRandomAccessStream())
            {
                var array = matrix.ToArray();
                var data = new byte[array.Length * 4];
                for (int d = 0, s = 0; d < data.Length; d += 4, s++)
                {
                    data[d] = array[s].Blue;
                    data[d + 1] = array[s].Green;
                    data[d + 2] = array[s].Red;
                }

                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, imras);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)bitmap.PixelWidth,
                    (uint)bitmap.PixelHeight,
                    96.0,
                    96.0,
                    data);

                await encoder.FlushAsync();
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(imras);

                return bitmapImage;
            }
        }

        #endregion

        #endregion

    }
}
