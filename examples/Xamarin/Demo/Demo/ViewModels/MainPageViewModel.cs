using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Models;
using Prism.Commands;
using Prism.Navigation;
using SkiaSharp;
using Xamarin.Forms;

using Demo.Services;
using Demo.Services.Interfaces;
using Demo.ViewModels.Interfaces;

namespace Demo.ViewModels
{

    public sealed class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {

        #region Fields

        private readonly IFileAccessService _FileAccessService;

        private readonly IDetectService _DetectService;

        #endregion

        #region Constructors

        public MainPageViewModel(INavigationService navigationService,
                                 IFileAccessService fileAccessService,
                                 IDetectService detectService)
            : base(navigationService)
        {
            this.Title = "Demo";

            this._FileAccessService = fileAccessService;
            this._DetectService = detectService;
            this._FilePickCommand = new Lazy<DelegateCommand>(this.FilePickCommandFactory);
        }

        #endregion

        #region Properties

        private readonly Lazy<DelegateCommand> _FilePickCommand;

        private DelegateCommand FilePickCommandFactory()
        {
            return new DelegateCommand(async () =>
            {
                var result = await this._FileAccessService.GetFileContent();
                if (result == null)
                    return;

                var detectResult = this._DetectService.Detect(result);
                if (detectResult == null)
                    return;

                var surface = SKSurface.Create(new SKImageInfo(detectResult.Width, detectResult.Height, SKColorType.Rgba8888));
                using var paint = new SKPaint();
                using var bitmap = SKBitmap.Decode(result);

                surface.Canvas.DrawBitmap(bitmap, 0, 0, paint);
                paint.StrokeWidth = 3;
                paint.TextSize = 48;
                paint.IsAntialias = true;
                paint.TextEncoding = SKTextEncoding.Utf8;

                foreach (var face in detectResult.Faces)
                {
                    paint.Color = SKColors.Red;
                    paint.Style = SKPaintStyle.Stroke;

                    var rect = face.Rect;
                    surface.Canvas.DrawRect(rect.Left, rect.Top, rect.Width, rect.Height, paint);

                    var results = new Dictionary<FacePart, IEnumerable<FacePoint>>();
                    var landmarkTuple = face.Points;
                    results.Add(FacePart.Chin,         Enumerable.Range(0, 17).Select(i => landmarkTuple[i]).ToArray());
                    results.Add(FacePart.LeftEyebrow,  Enumerable.Range(17, 5).Select(i => landmarkTuple[i]).ToArray());
                    results.Add(FacePart.RightEyebrow, Enumerable.Range(22, 5).Select(i => landmarkTuple[i]).ToArray());
                    results.Add(FacePart.NoseBridge,   Enumerable.Range(27, 5).Select(i => landmarkTuple[i]).ToArray());
                    results.Add(FacePart.NoseTip,      Enumerable.Range(31, 5).Select(i => landmarkTuple[i]).ToArray());
                    results.Add(FacePart.LeftEye,      Enumerable.Range(36, 6).Select(i => landmarkTuple[i]).ToArray());
                    results.Add(FacePart.RightEye,     Enumerable.Range(42, 6).Select(i => landmarkTuple[i]).ToArray());
                    results.Add(FacePart.TopLip,       Enumerable.Range(48, 7).Select(i => landmarkTuple[i])
                                                                              .Concat(new[] { landmarkTuple[64] })
                                                                              .Concat(new[] { landmarkTuple[63] })
                                                                              .Concat(new[] { landmarkTuple[62] })
                                                                              .Concat(new[] { landmarkTuple[61] })
                                                                              .Concat(new[] { landmarkTuple[60] }));
                    results.Add(FacePart.BottomLip, Enumerable.Range(54, 6).Select(i => landmarkTuple[i])
                                                                           .Concat(new[] { landmarkTuple[48] })
                                                                           .Concat(new[] { landmarkTuple[60] })
                                                                           .Concat(new[] { landmarkTuple[67] })
                                                                           .Concat(new[] { landmarkTuple[66] })
                                                                           .Concat(new[] { landmarkTuple[65] })
                                                                           .Concat(new[] { landmarkTuple[64] }));

                    paint.Color = SKColors.LimeGreen;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = 2;
                    foreach (var kvp in results)
                    {
                        for (var index = 0; index < kvp.Value.ToArray().Length - 1; index++)
                        {
                            var part = kvp.Value.ToArray()[index];
                            var part2 = kvp.Value.ToArray()[index + 1];
                            surface.Canvas.DrawLine(part.Point.X, part.Point.Y, part2.Point.X, part2.Point.Y, paint);
                        }
                    }
                }

                this.SelectedImage = ImageSource.FromStream(() => surface.Snapshot().Encode().AsStream());
            });
        }

        public DelegateCommand FilePickCommand => this._FilePickCommand.Value;

        private ImageSource _SelectedImage;

        public ImageSource SelectedImage
        {
            get => this._SelectedImage;
            private set
            {
                this._SelectedImage = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

    }

}