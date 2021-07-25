using System;
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
                var result = await this._FileAccessService.GetFile();
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
                    var rect = face.Rect;
                    paint.Color = SKColors.Red;
                    paint.Style = SKPaintStyle.Stroke;
                    surface.Canvas.DrawRect(rect.Left, rect.Top, rect.Width, rect.Height, paint);
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