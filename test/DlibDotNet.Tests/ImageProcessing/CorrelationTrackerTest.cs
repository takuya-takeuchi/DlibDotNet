using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if DEBUG
using System.Drawing;
using System.IO;
#endif

namespace DlibDotNet.Tests.ImageProcessing
{

    [TestClass]
    public class CorrelationTrackerTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        [TestMethod]
        public void Create()
        {
            var tracker = new CorrelationTracker();
            this.DisposeAndCheckDisposedState(tracker);
        }

        [TestMethod]
        public void StartTrack()
        {
            const string testName = "StartTrack";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var inputType = input.Type;
                var imageObj = DlibTest.LoadImage(input.Type, path);
                var tracker = new CorrelationTracker();
                var rect = new DRectangle(46, 15, 216, 205);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    tracker.StartTrack(imageObj, rect);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {inputType}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(rect);
                    this.DisposeAndCheckDisposedState(tracker);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void UpdateNoscale()
        {
            const string testName = "UpdateNoscale";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var inputType = input.Type;
                var imageObj = DlibTest.LoadImage(input.Type, path);
                var tracker = new CorrelationTracker();
                var rect = new DRectangle(46, 15, 216, 205);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    tracker.StartTrack(imageObj, rect);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                    tracker.UpdateNoscale(image, rect);
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {inputType}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(rect);
                    this.DisposeAndCheckDisposedState(tracker);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void UpdateNoscale2()
        {
            const string testName = "UpdateNoscale2";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var inputType = input.Type;
                var imageObj = DlibTest.LoadImage(input.Type, path);
                var tracker = new CorrelationTracker();
                var rect = new DRectangle(46, 15, 216, 205);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    tracker.StartTrack(imageObj, rect);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                    tracker.UpdateNoscale(image);
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {inputType}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(rect);
                    this.DisposeAndCheckDisposedState(tracker);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void Update()
        {
            const string testName = "Update";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var inputType = input.Type;
                var imageObj = DlibTest.LoadImage(input.Type, path);
                var tracker = new CorrelationTracker();
                var rect = new DRectangle(46, 15, 216, 205);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    tracker.StartTrack(imageObj, rect);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                    tracker.Update(image, rect);
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {inputType}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(rect);
                    this.DisposeAndCheckDisposedState(tracker);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void Update2()
        {
            const string testName = "Update2";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var inputType = input.Type;
                var imageObj = DlibTest.LoadImage(input.Type, path);
                var tracker = new CorrelationTracker();
                var rect = new DRectangle(46, 15, 216, 205);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    tracker.StartTrack(imageObj, rect);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                    tracker.Update(image);
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {inputType}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(rect);
                    this.DisposeAndCheckDisposedState(tracker);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void GetPosition()
        {
            const string testName = "GetPosition";
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var inputType = input.Type;
                var imageObj = DlibTest.LoadImage(input.Type, path);
                var tracker = new CorrelationTracker();
                var rect = new DRectangle(46, 15, 216, 205);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    tracker.StartTrack(imageObj, rect);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                    tracker.Update(image);
                    using (var pos = tracker.GetPosition())
                        Assert.IsFalse(pos.IsEmpty);
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {inputType}.");
                });

                var finallyAction = new Action(() =>
                {
                    this.DisposeAndCheckDisposedState(rect);
                    this.DisposeAndCheckDisposedState(tracker);
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void TryTrack()
        {
            const string testName = "TryTrack";

            var tests = new[]
            {
                new { Type = ImageTypes.UInt8,         ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
            {
                var tracker = new CorrelationTracker();
                try
                {
                    var index = 0;
                    foreach (var file in this.GetDataFiles("video_frames").Where(info => info.Name.EndsWith("jpg")))
                    {
                        var expectResult = input.ExpectResult;
                        var inputType = input.Type;
                        var imageObj = DlibTest.LoadImage(input.Type, file);

                        if (index == 0)
                            using (var rect = DRectangle.CenteredRect(93, 110, 38, 86))
                                tracker.StartTrack(imageObj, rect);

                        var outputImageAction = new Func<bool, Array2DBase>(expect =>
                        {
                            if (index != 0)
                                tracker.Update(imageObj);

                            return imageObj;
                        });

                        var successAction = new Action<Array2DBase>(image =>
                        {
                            if (index != 0)
                                tracker.Update(image);
                            using (var r = tracker.GetPosition())
                            {
#if DEBUG
                                using (var tmp = Image.FromFile(file.FullName))
                                using (var bmp = new Bitmap(tmp))
                                using (var g = Graphics.FromImage(bmp))
                                using (var p = new Pen(Color.Red, 3f))
                                {
                                    g.DrawRectangle(p, (float)r.Left, (float)r.Top, (float)r.Width, (float)r.Height);
                                    bmp.Save(Path.Combine(this.GetOutDir(type), $"{Path.GetFileNameWithoutExtension(file.FullName)}.jpg"));
                                }
#endif
                            }
                        });

                        var failAction = new Action(() =>
                        {
                            Assert.Fail($"{testName} should throw excption for InputType: {inputType}.");
                        });

                        var finallyAction = new Action(() =>
                        {
                            index++;
                            
                            if (imageObj != null)
                                this.DisposeAndCheckDisposedState(imageObj);
                        });

                        var exceptionAction = new Action(() =>
                        {
                            Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}.");
                        });

                        DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                    }
                }
                finally
                {
                    this.DisposeAndCheckDisposedState(tracker);
                }
            }
        }

    }

}
