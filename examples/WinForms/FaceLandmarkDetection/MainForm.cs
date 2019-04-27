using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DlibDotNet;
using DlibDotNet.Extensions;

namespace FaceLandmarkDetection
{

    public partial class MainForm : Form
    {

        #region Fields

        private readonly BackgroundWorker _BackgroundWorker;

        private readonly ShapePredictor _ShapePredictor;

        #endregion

        #region Constructors

        public MainForm()
        {
            this.InitializeComponent();
            this._ShapePredictor = ShapePredictor.Deserialize("shape_predictor_68_face_landmarks.dat");
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

                    var wb = img.ToBitmap();
                    this.pictureBoxImage.Image?.Dispose();
                    this.pictureBoxImage.Image = wb;

                    foreach (var l in lines)
                        l.Dispose();

                    var chipLocations = Dlib.GetFaceChipDetails(shapes);
                    using (var faceChips = Dlib.ExtractImageChips<RgbPixel>(img, chipLocations))
                    using (var tileImage = Dlib.TileImages(faceChips))
                    {
                        // It is NOT necessary to re-convert WriteableBitmap to Matrix.
                        // This sample demonstrate converting managed image class to
                        // dlib class and vice versa.
                        using (var tile = tileImage.ToBitmap())
                        using (var mat = tile.ToMatrix<RgbPixel>())
                        {
                            var tile2 = mat.ToBitmap();
                            this.pictureBoxTileImage.Image?.Dispose();
                            this.pictureBoxTileImage.Image = tile2;
                        }
                    }

                    foreach (var c in chipLocations)
                        c.Dispose();
                }

                foreach (var s in shapes)
                    s.Dispose();
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
