using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageProcessing
{

    [TestClass]
    public class FrontalFaceDetectorTest : TestBase
    {

        private FrontalFaceDetector _FrontalFaceDetector;

        [TestInitialize]
        [TestMethod]
        public void GetFrontalFaceDetector()
        {
            this._FrontalFaceDetector = Dlib.GetFrontalFaceDetector();
        }

        [TestMethod]
        public void DetectFace()
        {
            if (this._FrontalFaceDetector == null)
                Assert.Fail("ShapePredictor is not initialized!!");

            var faceDetector = this._FrontalFaceDetector;

            var path = this.GetDataFile("2008_001322.jpg");
            var image = Dlib.LoadImage<RgbPixel>(path.FullName);

            //Interpolation.PyramidUp(image);

            var dets = faceDetector.Operator(image);
            Assert.AreEqual(dets.Length, 3);

            foreach (var r in dets)
                Dlib.DrawRectangle(image, r, new RgbPixel { Green = 255 });

            Dlib.SaveBmp(image, Path.Combine(this.GetOutDir(this.GetType().Name), "DetectFace.bmp"));

            this.DisposeAndCheckDisposedState(faceDetector);
            this.DisposeAndCheckDisposedState(image);
        }

        [TestMethod]
        public void DetectFace2()
        {
            if (this._FrontalFaceDetector == null)
                Assert.Fail("ShapePredictor is not initialized!!");

            var faceDetector = this._FrontalFaceDetector;

            const string testName = "DetectFace2";
            var path = this.GetDataFile("Lenna_mini.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var imageObj = DlibTest.LoadImage(input.Type, path);
                Rectangle[] dets = null;

                var outputImageAction = new Func<bool, Rectangle[]>(expect =>
                {
                    dets = faceDetector.Operator(imageObj);
                    return dets;
                });

                var successAction = new Action<Rectangle[]>(image =>
                {
                    // This test does NOT check whether output image and detect face area are correct
                    //Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, testName), $"2008_001322_{input.Type}.jpg")}");
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw exception for InputType: {input.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void DetectFace3()
        {
            if (this._FrontalFaceDetector == null)
                Assert.Fail("ShapePredictor is not initialized!!");

            var faceDetector = this._FrontalFaceDetector;

            const string testName = "DetectFace3";
            var path = this.GetDataFile("Lenna_mini.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var imageObj = DlibTest.LoadImageAsMatrix(input.Type, path);
                Rectangle[] dets = null;

                var outputImageAction = new Func<bool, Rectangle[]>(expect =>
                {
                    dets = faceDetector.Operator(imageObj);
                    return dets;
                });

                var successAction = new Action<Rectangle[]>(image =>
                {
                    // This test does NOT check whether output image and detect face area are correct
                    //Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, testName), $"2008_001322_{input.Type}.jpg")}");
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw exception for InputType: {input.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void DetectFaceRectDetection()
        {
            if (this._FrontalFaceDetector == null)
                Assert.Fail("ShapePredictor is not initialized!!");

            var faceDetector = this._FrontalFaceDetector;

            const string testName = nameof(DetectFaceRectDetection);
            var path = this.GetDataFile("Lenna.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var imageObj = DlibTest.LoadImageAsMatrix(input.Type, path);
                IEnumerable<RectDetection> result = null;

                var outputImageAction = new Func<bool, IEnumerable<RectDetection>>(expect =>
                {
                    faceDetector.Operator(imageObj, out var detections);
                    result = detections;
                    return detections;
                });

                var successAction = new Action<IEnumerable<RectDetection>>(detections =>
                {
                    // This test does NOT check whether output image and detect face area are correct
                    //Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, testName), $"2008_001322_{input.Type}.jpg")}");
                    Console.WriteLine($"{input.Type}");
                    foreach (var d in detections)
                    {
                        var rect = d.Rect;
                        Console.WriteLine($"\tDetectionConfidence: {d.DetectionConfidence}, WeightIndex: {d.WeightIndex}, Rect: \n\t\tLeft: {rect.Left}, Top: {rect.Top}, Right: {rect.Right}, Bottom: {rect.Bottom}");
                    }
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw exception for InputType: {input.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(imageObj);
                    if (result != null)
                        this.DisposeAndCheckDisposedStates(result);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestCleanup]
        public void Dispose()
        {
            if (this._FrontalFaceDetector != null)
                this.DisposeAndCheckDisposedState(this._FrontalFaceDetector);
        }

    }

}
