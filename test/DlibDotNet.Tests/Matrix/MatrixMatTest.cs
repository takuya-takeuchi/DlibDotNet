using System;
using DlibDotNet.Tests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Matrix
{

    [TestClass]
    public class MatrixMatTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            const string testName = "Create";
            var tests = new[]
            {
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true}
            };

            foreach (var output in tests)
            {
                var expectResult = output.ExpectResult;
                var outputImageAction = new Func<bool, MatrixOp>(expect =>
                {
                    using (var array = Array2DTest.CreateArray2D(output.Type))
                        return Dlib.Mat(array);
                });

                var successAction = new Action<MatrixOp>(mat =>
                {
                    if (mat != null)
                        this.DisposeAndCheckDisposedState(mat);
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {output.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {output.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int width = 150;
            const int height = 100;

            const string testName = "CreateWithSize";
            var tests = new[]
            {
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true}
            };

            foreach (var output in tests)
            {
                var expectResult = output.ExpectResult;
                var outputImageAction = new Func<bool, MatrixOp>(expect =>
                {
                    using (var array = Array2DTest.CreateArray2D(output.Type, width, height))
                        return Dlib.Mat(array);
                });

                var successAction = new Action<MatrixOp>(mat =>
                {
                    if (mat != null)
                        this.DisposeAndCheckDisposedState(mat);
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {output.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {output.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

        [TestMethod]
        public void CheckSize()
        {
            const int width = 150;
            const int height = 100;

            const string testName = "CreateWithSize";
            var tests = new[]
            {
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true}
            };

            foreach (var output in tests)
            {
                var expectResult = output.ExpectResult;
                var outputImageAction = new Func<bool, Array2DBase>(expect => Array2DTest.CreateArray2D(output.Type, height, width));

                var successAction = new Action<Array2DBase>(array =>
                {
                    try
                    {
                        using (var mat = Dlib.Mat(array))
                        {
                            Assert.AreEqual(mat.Columns, width, $"Columns should equal to {width}.");
                            Assert.AreEqual(mat.Rows, height, $"Rows should equal to {height}.");
                        }
                    }
                    finally
                    {
                        if (array != null)
                            this.DisposeAndCheckDisposedState(array);
                    }
                });

                var failAction = new Action(() =>
                {
                    Assert.Fail($"{testName} should throw excption for InputType: {output.Type}.");
                });

                var finallyAction = new Action(() =>
                {
                });

                var exceptionAction = new Action(() =>
                {
                    Console.WriteLine($"Failed to execute {testName} to InputType: {output.Type}.");
                });

                DoTest(outputImageAction, expectResult, successAction, finallyAction, failAction, exceptionAction);
            }
        }

    }

}
