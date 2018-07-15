using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using DlibDotNet;
using DlibDotNet.Extensions;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Dlib = DlibDotNet.Dlib;

namespace FaceDetection.ViewModels
{

    public sealed class MainViewModel : BindableBase
    {

        #region Properties

        private WriteableBitmap _Image;

        public WriteableBitmap Image
        {
            get => this._Image;
            private set
            {
                this._Image = value;
                this.RaisePropertyChanged();
            }
        }

        private DelegateCommand _OpenFileCommand;

        public DelegateCommand OpenFileCommand
        {
            get
            {
                return this._OpenFileCommand ?? (this._OpenFileCommand = new DelegateCommand(async () =>
               {
                   var openFileDialog = new OpenFileDialog();
                   var dialogResult = openFileDialog.ShowDialog();
                   if (dialogResult != true)
                       return;

                   var path = openFileDialog.FileName;

                   await Task.Run(() =>
                   {
                       using (var faceDetector = Dlib.GetFrontalFaceDetector())
                       using (var data = new MemoryStream(File.ReadAllBytes(path)))
                       {
                           // DlibDotNet can create Array2D from file but this sample demonstrate
                           // converting managed image class to dlib class and vice versa.
                           var bitmap = new WriteableBitmap(BitmapFrame.Create(data));
                           using (var image = bitmap.ToArray2D<RgbPixel>())
                           {
                               var dets = faceDetector.Operator(image);
                               foreach (var r in dets)
                                   Dlib.DrawRectangle(image, r, new RgbPixel { Green = 255 });

                               var result = image.ToWriteableBitmap();
                               if (result.CanFreeze)
                                   result.Freeze();
                               Application.Current.Dispatcher.Invoke(() =>
                               {
                                   this.Image = result;
                               });
                           }
                       }
                   });
               }, () => true));
            }
        }

        #endregion

    }

}
