using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;

namespace FaceDetection
{

    public partial class MainForm : Form
    {

        #region Fields

        private readonly BackgroundWorker _BackgroundWorker;

        #endregion

        #region Constructors

        public MainForm()
        {
            this.InitializeComponent();
            this._BackgroundWorker = new BackgroundWorker();
            this._BackgroundWorker.DoWork += this.BackgroundWorkerOnDoWork;
        }

        #endregion

        #region Methods

        #region Event Handlers

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var path = doWorkEventArgs.Argument as string;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return;
            try
            {
                // DlibDotNet can create Array2D from file but this sample demonstrate
                // converting managed image class to dlib class and vice versa.
                using (var ms = new MemoryStream(File.ReadAllBytes(path)))
                using (var bitmap = (Bitmap)Image.FromStream(ms))
                {
                    using (var outimage = bitmap.ToArray2D<RgbPixel>())
                    {
                        int pyramid_up = 0;
                        int smallestSize = Math.Min(bitmap.Width, bitmap.Height);
                        while (smallestSize < 1800)
                        {
                            pyramid_up++;
                            smallestSize += smallestSize;
                        }
                        using (var faceDetector = FrontalFaceDetector.GetFrontalFaceDetector())
                        using (var image = bitmap.ToArray2D<RgbPixel>())
                        {
                            for (int i = 0; i < pyramid_up; i++)
                            {
                                Dlib.PyramidUp(image);
                            }
                            var dets = faceDetector.Detect(image);
                            System.Diagnostics.Debug.WriteLine("FrontalFaceDetector: " + dets.Length);
                            int div = Convert.ToInt32(Math.Pow(2, pyramid_up));
                            foreach (var r in dets)
                            {
                                var downR = new DlibDotNet.Rectangle(r.Left / div, r.Top / div, r.Right / div, r.Bottom / div);
                                Dlib.DrawRectangle(outimage, downR, new RgbPixel { Green = 255 }, 3);
                            }
                        }
                        using (var dnnFaceDetectorFast = DNNFaceDetectorFast.GetDNNFaceDetectorFast("mmod_human_face_detector.dat"))
                        using (var image = bitmap.ToArray2D<RgbPixel>())
                        {
                            var dets = dnnFaceDetectorFast.Detect(image, pyramid_up);
                            System.Diagnostics.Debug.WriteLine("DNNFaceDetectorFast: " + dets.Length);
                            foreach (var r in dets)
                                Dlib.DrawRectangle(outimage, r.Rect, new RgbPixel { Red = 255 }, 3);
                        }
                        using (var dnnFaceDetector = DNNFaceDetector.GetDNNFaceDetector("mmod_human_face_detector.dat"))
                        using (var image = bitmap.ToArray2D<RgbPixel>())
                        {
                            var dets = dnnFaceDetector.Detect(image, pyramid_up);
                            System.Diagnostics.Debug.WriteLine("DNNFaceDetector: " + dets.Length);
                            foreach (var r in dets)
                                Dlib.DrawRectangle(outimage, r.Rect, new RgbPixel { Blue = 255 }, 3);
                        }
                        var result = outimage.ToBitmap();
                        this.pictureBox.Invoke(new Action(() =>
                        {
                            this.pictureBox.Image?.Dispose();
                            this.pictureBox.Image = result;
                        }));
                        var result = outimage.ToBitmap();
                        this.pictureBox.Invoke(new Action(() =>
                        {
                            this.pictureBox.Image?.Dispose();
                            this.pictureBox.Image = result;
                        }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    this._BackgroundWorker.RunWorkerAsync(dialog.FileName);
                }
            }
        }

        #endregion

        #endregion

    }

}
