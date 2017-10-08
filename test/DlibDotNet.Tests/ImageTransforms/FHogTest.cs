using System;
using DlibDotNet.Tests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class FHogTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        #region ExtracFHogFeatures

        [TestMethod]
        public void ExtracFHogFeatures()
        {
            const string testName = "ExtracFHogFeatures";
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
                    var outputObj = Array2DTest.CreateArray2DMatrix(output.Type);

                    var outputImageAction = new Func<bool, Array2DMatrixBase>(expect =>
                    {
                        Dlib.ExtracFHogFeatures(imageObj, outputObj);
                        return outputObj;
                    });

                    var successAction = new Action<Array2DMatrixBase>(image =>
                    {
                        MatrixBase ret = null;

                        try
                        {
                            ret = Dlib.DrawHog(image);
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
                        Assert.Fail($"{testName} should throw excption for InputType: {inputType}, OutputType: {output.Type}.");
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
        public void ExtracFHogFeatures2()
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
                    outputObj = Array2DTest.CreateArray2DMatrix(output.Type);

                    Dlib.ExtracFHogFeatures(imageObj, outputObj);
                    MatrixBase matrix = Dlib.DrawHog(outputObj);

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

        #endregion

    }

}
