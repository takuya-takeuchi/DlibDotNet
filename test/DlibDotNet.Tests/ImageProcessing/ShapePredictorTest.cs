using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageProcessing
{

    [TestClass]
    public class ShapePredictorTest : TestBase
    {

        private ShapePredictor _ShapePredictor;

        [TestMethod]
        public void CreateShapePredictor()
        {
            var predictor = new ShapePredictor();
            this.DisposeAndCheckDisposedState(predictor);
        }

        [TestInitialize]
        [TestMethod]
        public void CreateShapePredictorFromFile()
        {
            var path = this.GetDataFile("shape_predictor_68_face_landmarks.dat");
            this._ShapePredictor = ShapePredictor.Deserialize(path.FullName);
        }

        [TestMethod]
        public void DetectFace()
        {
            if (this._ShapePredictor == null)
                Assert.Fail("ShapePredictor is not initialized!!");

            var faceDetector = Dlib.GetFrontalFaceDetector();
            //Interpolation.PyramidUp(image);

            var path = this.GetDataFile("Lenna.jpg");
            var image = Dlib.LoadImage<RgbPixel>(path.FullName);

            var dets = faceDetector.Operator(image);
            Assert.AreEqual(dets.Length, 1);

            var rects = new List<Rectangle>();

            const int offset = 1;
            var shapes = dets.Select(r => this._ShapePredictor.Detect(image, r)).ToList();
            foreach (var shape in shapes)
            {
                var r = shape.Rect;
                var parts = shape.Parts;
                for (uint i = 0; i < parts; i++)
                {
                    var part = shape.GetPart(i);
                    var pr = new Rectangle(
                        part.X - offset, part.Y - offset, part.X + offset, part.Y + offset);
                    rects.Add(pr);
                }

                rects.Add(r);
            }
            
            foreach (var r in rects)
                Dlib.DrawRectangle(image, r, new RgbPixel { Green = 255 });

            Dlib.SaveBmp(image, Path.Combine(this.GetOutDir(this.GetType().Name), "DetectFace.bmp"));
            
            this.DisposeAndCheckDisposedState(faceDetector);
            this.DisposeAndCheckDisposedState(image);
        }

        [TestMethod]
        public void DetectFace2()
        {
            if (this._ShapePredictor == null)
                Assert.Fail("ShapePredictor is not initialized!!");

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
            
            using (var faceDetector = Dlib.GetFrontalFaceDetector())
                foreach (var input in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImage(input.Type, path);
                    Rectangle[] dets = null;

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        dets = faceDetector.Operator(imageObj);
                        return imageObj;
                    });

                    var successAction = new Action<Array2DBase>(image =>
                    {
                        var rects = new List<Rectangle>();
                        const int offset = 1;
                        var shapes = dets.Select(r => this._ShapePredictor.Detect(image, r)).ToList();
                        foreach (var shape in shapes)
                        {
                            var r = shape.Rect;
                            var parts = shape.Parts;
                            for (uint i = 0; i < parts; i++)
                            {
                                var part = shape.GetPart(i);
                                var pr = new Rectangle(
                                    part.X - offset, part.Y - offset, part.X + offset, part.Y + offset);
                                rects.Add(pr);
                            }

                            rects.Add(r);
                        }

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
            if (this._ShapePredictor == null)
                Assert.Fail("ShapePredictor is not initialized!!");

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

            using (var faceDetector = Dlib.GetFrontalFaceDetector())
                foreach (var input in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImageAsMatrix(input.Type, path);
                    Rectangle[] dets = null;

                    var outputImageAction = new Func<bool, MatrixBase>(expect =>
                    {
                        dets = faceDetector.Operator(imageObj);
                        return imageObj;
                    });

                    var successAction = new Action<MatrixBase>(image =>
                    {
                        var rects = new List<Rectangle>();
                        const int offset = 1;
                        var shapes = dets.Select(r => this._ShapePredictor.Detect(image, r)).ToList();
                        foreach (var shape in shapes)
                        {
                            var r = shape.Rect;
                            var parts = shape.Parts;
                            for (uint i = 0; i < parts; i++)
                            {
                                var part = shape.GetPart(i);
                                var pr = new Rectangle(
                                    part.X - offset, part.Y - offset, part.X + offset, part.Y + offset);
                                rects.Add(pr);
                            }

                            rects.Add(r);
                        }

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
        public void DetectFaceMModRect()
        {
            if (this._ShapePredictor == null)
                Assert.Fail("ShapePredictor is not initialized!!");

            const string testName = "DetectFaceMModRect";
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

            using (var faceDetector = Dlib.GetFrontalFaceDetector())
                foreach (var input in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImage(input.Type, path);
                    MModRect[] dets = null;

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        dets = faceDetector.Operator(imageObj).Select(r => new MModRect { Rect = r }).ToArray();
                        return imageObj;
                    });

                    var successAction = new Action<Array2DBase>(image =>
                    {
                        var rects = new List<Rectangle>();
                        const int offset = 1;
                        var shapes = dets.Select(r => this._ShapePredictor.Detect(image, r)).ToList();
                        foreach (var shape in shapes)
                        {
                            var r = shape.Rect;
                            var parts = shape.Parts;
                            for (uint i = 0; i < parts; i++)
                            {
                                var part = shape.GetPart(i);
                                var pr = new Rectangle(part.X - offset, part.Y - offset, part.X + offset, part.Y + offset);
                                rects.Add(pr);
                            }

                            rects.Add(r);
                        }

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
        public void DetectFaceMModRect2()
        {
            if (this._ShapePredictor == null)
                Assert.Fail("ShapePredictor is not initialized!!");

            const string testName = "DetectFaceMModRect2";
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

            using (var faceDetector = Dlib.GetFrontalFaceDetector())
                foreach (var input in tests)
                {
                    var expectResult = input.ExpectResult;
                    var imageObj = DlibTest.LoadImageAsMatrix(input.Type, path);
                    MModRect[] dets = null;

                    var outputImageAction = new Func<bool, MatrixBase>(expect =>
                    {
                        dets = faceDetector.Operator(imageObj).Select(r => new MModRect{ Rect = r }).ToArray();
                        return imageObj;
                    });

                    var successAction = new Action<MatrixBase>(image =>
                    {
                        var rects = new List<Rectangle>();
                        const int offset = 1;
                        var shapes = dets.Select(r => this._ShapePredictor.Detect(image, r)).ToList();
                        foreach (var shape in shapes)
                        {
                            var r = shape.Rect;
                            var parts = shape.Parts;
                            for (uint i = 0; i < parts; i++)
                            {
                                var part = shape.GetPart(i);
                                var pr = new Rectangle(part.X - offset, part.Y - offset, part.X + offset, part.Y + offset);
                                rects.Add(pr);
                            }

                            rects.Add(r);
                        }

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

        [TestCleanup]
        public void Dispose()
        {
            if (this._ShapePredictor != null)
                this.DisposeAndCheckDisposedState(this._ShapePredictor);
        }

    }

}
