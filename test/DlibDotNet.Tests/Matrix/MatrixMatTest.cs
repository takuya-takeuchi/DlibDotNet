using System;
using DlibDotNet.Tests.Array2D;
using Xunit;

namespace DlibDotNet.Tests.Matrix
{

    public class MatrixMatTest : TestBase
    {

        [Fact]
        public void Create()
        {
            const string testName = "Create";
            var tests = new[]
            {
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true}
            };

            foreach (var output in tests)
            {
                var expectResult = output.ExpectResult;
                var outputImageAction = new Func<bool, MatrixOp>(expect =>
                {
                    using (var array = Array2DTest.CreateArray2DHelp(output.Type))
                        return Dlib.Mat(array);
                });

                var successAction = new Action<MatrixOp>(mat =>
                {
                    if (mat != null)
                        this.DisposeAndCheckDisposedState(mat);
                });

                var failAction = new Action(() =>
                {
                    Assert.True(false, $"{testName} should throw exception for InputType: {output.Type}.");
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

        [Fact]
        public void CreateWithSize()
        {
            const int width = 150;
            const int height = 100;

            const string testName = "CreateWithSize";
            var tests = new[]
            {
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true}
            };

            foreach (var output in tests)
            {
                var expectResult = output.ExpectResult;
                var outputImageAction = new Func<bool, MatrixOp>(expect =>
                {
                    using (var array = Array2DTest.CreateArray2DHelp(output.Type, width, height))
                        return Dlib.Mat(array);
                });

                var successAction = new Action<MatrixOp>(mat =>
                {
                    if (mat != null)
                        this.DisposeAndCheckDisposedState(mat);
                });

                var failAction = new Action(() =>
                {
                    Assert.True(false, $"{testName} should throw exception for InputType: {output.Type}.");
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

        [Fact]
        public void CheckSize()
        {
            const int width = 150;
            const int height = 100;

            const string testName = "CreateWithSize";
            var tests = new[]
            {
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true},
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true}
            };

            foreach (var output in tests)
            {
                var expectResult = output.ExpectResult;
                var outputImageAction = new Func<bool, Array2DBase>(expect => Array2DTest.CreateArray2DHelp(output.Type, height, width));

                var successAction = new Action<Array2DBase>(array =>
                {
                    try
                    {
                        using (var mat = Dlib.Mat(array))
                        {
                            Assert.True(mat.Columns == width, $"Columns should equal to {width}.");
                            Assert.True(mat.Rows == height, $"Rows should equal to {height}.");
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
                    Assert.True(false, $"{testName} should throw exception for InputType: {output.Type}.");
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
