using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class FHogTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        #region ExtractFHogFeatures

        [TestMethod]
        public void ExtractFHogFeatures()
        {
            const string testName = nameof(this.ExtractFHogFeatures);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = false},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = false},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = false},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = false},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = false},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = false},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = false},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = false}
            };

            foreach (ImageTypes inputType in Enum.GetValues(typeof(ImageTypes)))
                foreach (var output in tests)
                {
                    if (inputType == ImageTypes.Matrix)
                        continue;

                    var expectResult = output.ExpectResult;
                    var imageObj = DlibTest.LoadImage(inputType, path);
                    Array2DMatrixBase outputObj = null;

                    var outputImageAction = new Func<bool, Array2DMatrixBase>(expect =>
                    {
                        switch (output.Type)
                        {
                            case MatrixElementTypes.UInt8:
                                outputObj = Dlib.ExtracFHogFeatures<byte>(imageObj);
                                break;
                            case MatrixElementTypes.UInt16:
                                outputObj = Dlib.ExtracFHogFeatures<ushort>(imageObj);
                                break;
                            case MatrixElementTypes.UInt32:
                                outputObj = Dlib.ExtracFHogFeatures<uint>(imageObj);
                                break;
                            case MatrixElementTypes.Int8:
                                outputObj = Dlib.ExtracFHogFeatures<sbyte>(imageObj);
                                break;
                            case MatrixElementTypes.Int16:
                                outputObj = Dlib.ExtracFHogFeatures<short>(imageObj);
                                break;
                            case MatrixElementTypes.Int32:
                                outputObj = Dlib.ExtracFHogFeatures<int>(imageObj);
                                break;
                            case MatrixElementTypes.Float:
                                outputObj = Dlib.ExtracFHogFeatures<float>(imageObj);
                                break;
                            case MatrixElementTypes.Double:
                                outputObj = Dlib.ExtracFHogFeatures<double>(imageObj);
                                break;
                            case MatrixElementTypes.RgbPixel:
                                outputObj = Dlib.ExtracFHogFeatures<RgbPixel>(imageObj);
                                break;
                            case MatrixElementTypes.RgbAlphaPixel:
                                outputObj = Dlib.ExtracFHogFeatures<RgbAlphaPixel>(imageObj);
                                break;
                            case MatrixElementTypes.HsiPixel:
                                outputObj = Dlib.ExtracFHogFeatures<HsiPixel>(imageObj);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        return outputObj;
                    });

                    var successAction = new Action<Array2DMatrixBase>(image =>
                    {
                        MatrixBase ret = null;

                        try
                        {
                            ret = Dlib.DrawFHog(image);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        finally
                        {
                            if (ret != null)
                                this.DisposeAndCheckDisposedState(ret);
                        }
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {inputType}, OutputType: {output.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                        if (outputObj != null)
                            this.DisposeAndCheckDisposedState(outputObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}, OutputType: {output.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        [TestMethod]
        public void ExtractFHogFeatures2()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true}
            };

            foreach (var output in tests)
            {
                Array2DBase imageObj = null;
                Array2DMatrixBase outputObj = null;

                try
                {
                    imageObj = DlibTest.LoadImage(ImageTypes.RgbPixel, path);

                    switch (output.Type)
                    {
                        case MatrixElementTypes.Float:
                            outputObj = Dlib.ExtracFHogFeatures<float>(imageObj);
                            break;
                        case MatrixElementTypes.Double:
                            outputObj = Dlib.ExtracFHogFeatures<double>(imageObj);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    MatrixBase matrix = Dlib.DrawFHog(outputObj);

                    if (!this.CanGuiDebug)
                    {
                        var window = new ImageWindow(matrix);
                        window.WaitUntilClosed();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    if (imageObj != null)
                        this.DisposeAndCheckDisposedState(imageObj);
                    if (outputObj != null)
                        this.DisposeAndCheckDisposedState(outputObj);
                }
            }
        }

        [TestMethod]
        public void ExtractFHogFeatures3()
        {
            const string testName = nameof(this.ExtractFHogFeatures3);
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = MatrixElementTypes.Float,         ExpectResult = true},
                new { Type = MatrixElementTypes.Double,        ExpectResult = true},
                new { Type = MatrixElementTypes.RgbPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = MatrixElementTypes.HsiPixel,      ExpectResult = true},
                new { Type = MatrixElementTypes.UInt32,        ExpectResult = true},
                new { Type = MatrixElementTypes.UInt8,         ExpectResult = true},
                new { Type = MatrixElementTypes.UInt16,        ExpectResult = true},
                new { Type = MatrixElementTypes.Int8,          ExpectResult = true},
                new { Type = MatrixElementTypes.Int16,         ExpectResult = true},
                new { Type = MatrixElementTypes.Int32,         ExpectResult = true}
            };

            foreach (ImageTypes inputType in Enum.GetValues(typeof(ImageTypes)))
                foreach (var output in tests)
                {
                    if (inputType == ImageTypes.Matrix)
                        continue;

                    var expectResult = output.ExpectResult;
                    var imageObj = DlibTest.LoadImage(inputType, path);
                    Matrix<double> outputObj = null;

                    var outputImageAction = new Func<bool, Matrix<double>>(expect =>
                    {
                        switch (output.Type)
                        {
                            case MatrixElementTypes.UInt8:
                                Dlib.ExtracFHogFeatures<byte>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.UInt16:
                                Dlib.ExtracFHogFeatures<ushort>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.UInt32:
                                Dlib.ExtracFHogFeatures<uint>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.Int8:
                                Dlib.ExtracFHogFeatures<sbyte>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.Int16:
                                Dlib.ExtracFHogFeatures<short>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.Int32:
                                Dlib.ExtracFHogFeatures<int>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.Float:
                                Dlib.ExtracFHogFeatures<float>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.Double:
                                Dlib.ExtracFHogFeatures<double>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.RgbPixel:
                                Dlib.ExtracFHogFeatures<RgbPixel>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.RgbAlphaPixel:
                                Dlib.ExtracFHogFeatures<RgbAlphaPixel>(imageObj, out outputObj);
                                break;
                            case MatrixElementTypes.HsiPixel:
                                Dlib.ExtracFHogFeatures<HsiPixel>(imageObj, out outputObj);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        return outputObj;
                    });

                    var successAction = new Action<Matrix<double>>(image =>
                    {
                    });

                    var failAction = new Action(() =>
                    {
                        Assert.Fail($"{testName} should throw exception for InputType: {inputType}, OutputType: {output.Type}.");
                    });

                    var finallyAction = new Action(() =>
                    {
                        if (imageObj != null)
                            this.DisposeAndCheckDisposedState(imageObj);
                        if (outputObj != null)
                            this.DisposeAndCheckDisposedState(outputObj);
                    });

                    var exceptionAction = new Action(() =>
                    {
                        Console.WriteLine($"Failed to execute {testName} to InputType: {inputType}, OutputType: {output.Type}.");
                    });

                    DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
                }
        }

        #endregion

    }

}
