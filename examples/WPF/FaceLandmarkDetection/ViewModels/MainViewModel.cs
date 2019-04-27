using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DlibDotNet;
using DlibDotNet.Extensions;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace FaceLandmarkDetection.ViewModels
{

    public sealed class MainViewModel : BindableBase
    {

        #region Fields

        private readonly ShapePredictor _ShapePredictor;

        #endregion

        #region Constructors

        public MainViewModel()
        {
            this._ShapePredictor = ShapePredictor.Deserialize("shape_predictor_68_face_landmarks.dat");
        }

        #endregion

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

        private WriteableBitmap _TileImage;

        public WriteableBitmap TileImage
        {
            get => this._TileImage;
            private set
            {
                this._TileImage = value;
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
                       using (var img = Dlib.LoadImage<RgbPixel>(path))
                       {
                           Dlib.PyramidUp(img);

                           var dets = faceDetector.Operator(img);

                           var shapes = new List<FullObjectDetection>();
                           foreach (var rect in dets)
                           {
                               var shape = this._ShapePredictor.Detect(img, rect);
                               if (shape.Parts <= 2)
                                   continue;
                               shapes.Add(shape);
                           }

                           if (shapes.Any())
                           {
                               var lines = Dlib.RenderFaceDetections(shapes);
                               foreach (var line in lines)
                                   Dlib.DrawLine(img, line.Point1, line.Point2, new RgbPixel
                                   {
                                       Green = 255
                                   });

                               var wb = img.ToWriteableBitmap();
                               if (wb.CanFreeze)
                                   wb.Freeze();
                               this.Image = wb;

                               foreach (var l in lines)
                                   l.Dispose();

                               var chipLocations = Dlib.GetFaceChipDetails(shapes);
                               using (var faceChips = Dlib.ExtractImageChips<RgbPixel>(img, chipLocations))
                               using (var tileImage = Dlib.TileImages(faceChips))
                               {
                                   // It is NOT necessary to re-convert WriteableBitmap to Matrix.
                                   // This sample demonstrate converting managed image class to
                                   // dlib class and vice versa.
                                   var tile = tileImage.ToWriteableBitmap();
                                   using (var mat = tile.ToMatrix<RgbPixel>())
                                   {
                                       var tile2 = mat.ToWriteableBitmap();
                                       if (tile2.CanFreeze)
                                           tile2.Freeze();
                                       this.TileImage = tile2;
                                   }
                               }

                               foreach (var c in chipLocations)
                                   c.Dispose();
                           }

                           foreach (var s in shapes)
                               s.Dispose();
                       }
                   });
               }, () => true));
            }
        }

        #endregion

    }

}
