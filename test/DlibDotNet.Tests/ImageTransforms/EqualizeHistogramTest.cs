using System;
using System.IO;
using Xunit;

namespace DlibDotNet.Tests.ImageTransforms
{

    public class EqualizeHistogramTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        #region EqualizeHistogram

        [Fact]
        public void EqualizeHistogram()
        {
            const string testName = nameof(EqualizeHistogram);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true },
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true },
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false },
                new { Type = ImageTypes.UInt8,         ExpectResult = true },
                new { Type = ImageTypes.UInt16,        ExpectResult = true },
                new { Type = ImageTypes.UInt32,        ExpectResult = false },
                new { Type = ImageTypes.Int8,          ExpectResult = false },
                new { Type = ImageTypes.Int16,         ExpectResult = false },
                new { Type = ImageTypes.Int32,         ExpectResult = false },
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true },
                new { Type = ImageTypes.LabPixel,      ExpectResult = true },
                new { Type = ImageTypes.Float,         ExpectResult = false },
                new { Type = ImageTypes.Double,        ExpectResult = false }
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
            {
                var expectResult = input.ExpectResult;
                var imageObj = DlibTest.LoadImageHelp(input.Type, path);

                var outputImageAction = new Func<bool, Array2DBase>(expect =>
                {
                    Dlib.EqualizeHistogram(imageObj);
                    return imageObj;
                });

                var successAction = new Action<Array2DBase>(image =>
                {
                    Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}.bmp")}");
                });

                var failAction = new Action(() =>
                {
                    Assert.True(false, $"{testName} should throw exception for InputType: {input.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, Type: {input.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [Fact]
        public void EqualizeHistogram2()
        {
            const string testName = nameof(EqualizeHistogram2);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var inTests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true },
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true },
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false },
                new { Type = ImageTypes.UInt8,         ExpectResult = true },
                new { Type = ImageTypes.UInt16,        ExpectResult = true },
                new { Type = ImageTypes.UInt32,        ExpectResult = false },
                new { Type = ImageTypes.Int8,          ExpectResult = false },
                new { Type = ImageTypes.Int16,         ExpectResult = false },
                new { Type = ImageTypes.Int32,         ExpectResult = false },
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true },
                new { Type = ImageTypes.LabPixel,      ExpectResult = true },
                new { Type = ImageTypes.Float,         ExpectResult = false },
                new { Type = ImageTypes.Double,        ExpectResult = false }
            };

            var outTests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true },
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true },
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false },
                new { Type = ImageTypes.UInt8,         ExpectResult = true },
                new { Type = ImageTypes.UInt16,        ExpectResult = true },
                new { Type = ImageTypes.UInt32,        ExpectResult = true },
                new { Type = ImageTypes.Int8,          ExpectResult = false },
                new { Type = ImageTypes.Int16,         ExpectResult = false },
                new { Type = ImageTypes.Int32,         ExpectResult = false },
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true },
                new { Type = ImageTypes.LabPixel,      ExpectResult = true },
                new { Type = ImageTypes.Float,         ExpectResult = false },
                new { Type = ImageTypes.Double,        ExpectResult = false }
            };

            var type = this.GetType().Name;
            foreach (var input in inTests)
                foreach (var output in outTests)
                {
                    var expectResult = input.ExpectResult && output.ExpectResult;
                    var imageObj = DlibTest.LoadImageHelp(input.Type, path);

                    var outputImageAction = new Func<bool, Array2DBase>(expect =>
                    {
                        switch (output.Type)
                        {
                            case ImageTypes.BgrPixel:
                                {
                                    Dlib.EqualizeHistogram<BgrPixel>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.RgbPixel:
                                {
                                    Dlib.EqualizeHistogram<RgbPixel>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.RgbAlphaPixel:
                                {
                                    Dlib.EqualizeHistogram<RgbAlphaPixel>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.UInt8:
                                {
                                    Dlib.EqualizeHistogram<byte>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.UInt16:
                                {
                                    Dlib.EqualizeHistogram<ushort>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.UInt32:
                                {
                                    Dlib.EqualizeHistogram<uint>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.Int8:
                                {
                                    Dlib.EqualizeHistogram<sbyte>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.Int16:
                                {
                                    Dlib.EqualizeHistogram<short>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.Int32:
                                {
                                    Dlib.EqualizeHistogram<int>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.HsiPixel:
                                {
                                    Dlib.EqualizeHistogram<HsiPixel>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.LabPixel:
                                {
                                    Dlib.EqualizeHistogram<LabPixel>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.Float:
                                {
                                    Dlib.EqualizeHistogram<float>(imageObj, out var ret);
                                    return ret;
                                }
                            case ImageTypes.Double:
                                {
                                    Dlib.EqualizeHistogram<double>(imageObj, out var ret);
                                    return ret;
                                }
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    });

                    var successAction = new Action<Array2DBase>(image =>
                    {
                        Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, testName), $"{LoadTarget}_{input.Type}_{output.Type}.bmp")}");
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.True(false, $"{testName} should throw exception for InputType: {input.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {input.Type}, Type: {input.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        #endregion

    }

}
