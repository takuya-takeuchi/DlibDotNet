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

            // DlibDotNet can create Array2D from file but this sample demonstrate
            // converting managed image class to dlib class and vice versa.
            using (var faceDetector = FrontalFaceDetector.GetFrontalFaceDetector())
            using (var ms = new MemoryStream(File.ReadAllBytes(path)))
            using (var bitmap = (Bitmap)Image.FromStream(ms))
            {
                using (var image = bitmap.ToArray2D<RgbPixel>())
                {
                    var dets = faceDetector.Detect(image);
                    foreach (var r in dets)
                        Dlib.DrawRectangle(image, r, new RgbPixel { Green = 255 });

                    var result = image.ToBitmap();
                    this.pictureBox.Invoke(new Action(() =>
                    {
                        this.pictureBox.Image?.Dispose();
                        this.pictureBox.Image = result;
                    }));
                }
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
