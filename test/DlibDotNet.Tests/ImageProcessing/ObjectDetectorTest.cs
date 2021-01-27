using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace DlibDotNet.Tests.ImageProcessing
{

    public class ObjectDetectorTest : TestBase
    {

        private ScanFHogPyramid<PyramidDown, DefaultFHogFeatureExtractor> _Scanner;

        private ObjectDetector<ScanFHogPyramid<PyramidDown, DefaultFHogFeatureExtractor>> _ObjectDetector;

        public ObjectDetectorTest()
        {
            var path = this.GetDataFile("face_detector.svm");
            this._Scanner = new ScanFHogPyramid<PyramidDown, DefaultFHogFeatureExtractor>(6);
            this._ObjectDetector = new ObjectDetector<ScanFHogPyramid<PyramidDown, DefaultFHogFeatureExtractor>>(this._Scanner);
            this._ObjectDetector.Deserialize(path.FullName);
        }

        [Fact]
        public void DetectFace()
        {
            if (this._ObjectDetector == null)
                Assert.True(false, "ObjectDetector is not initialized!!");

            var path = this.GetDataFile("Lenna.jpg");
            var image = Dlib.LoadImageAsMatrix<RgbPixel>(path.FullName);

            this._ObjectDetector.Operator(image, out IEnumerable<Rectangle> rects);
            Assert.Equal(rects.Count(), 1);

            foreach (var r in rects)
                Dlib.DrawRectangle(image, r, new RgbPixel { Green = 255 });

            Dlib.SaveBmp(image, Path.Combine(this.GetOutDir(this.GetType().Name), "DetectFace.bmp"));

            this.DisposeAndCheckDisposedState(image);
        }

        [Fact]
        public void DetectFaceWithConfidence()
        {
            if (this._ObjectDetector == null)
                Assert.True(false, "ObjectDetector is not initialized!!");

            var path = this.GetDataFile("Lenna.jpg");
            var image = Dlib.LoadImageAsMatrix<RgbPixel>(path.FullName);

            this._ObjectDetector.Operator(image, out IEnumerable<Tuple<double, Rectangle>> tuples);
            var array = tuples.ToArray();
            Assert.Equal(array.Length, 1);

            foreach (var tuple in tuples)
                Dlib.DrawRectangle(image, tuple.Item2, new RgbPixel { Green = 255 });
            Assert.True(array[0].Item1 - 0.3173714 < 0.01);

            Dlib.SaveBmp(image, Path.Combine(this.GetOutDir(this.GetType().Name), "DetectFaceWithConfidence.bmp"));

            this.DisposeAndCheckDisposedState(image);
        }

    }

}
