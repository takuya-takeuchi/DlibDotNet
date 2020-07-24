﻿using System;
using System.IO;
using DlibDotNet.Tests.Array2D;
using Xunit;

namespace DlibDotNet.Tests.ImageTransforms
{

    public class AssignImageTest : TestBase
    {
        private const string LoadTarget = "Lenna_mini";

        #region AssignImage

        [Fact]
        public void AssignImage()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.LabPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
                foreach (var output in tests)
                {
                    TwoDimensionObjectBase outObj = null;
                    TwoDimensionObjectBase inObj = null;

                    var expect = input.ExpectResult && output.ExpectResult;

                    try
                    {
                        var inIage = DlibTest.LoadImageHelp(input.Type, path);
                        inObj = inIage;

                        try
                        {
                            var outImage = Array2DTest.CreateArray2DHelp(output.Type);
                            outObj = outImage;

                            Dlib.AssignImage(inIage, outImage);

                            if (!expect)
                            {
                                Assert.True(false, $"AssignImage should throw exception for InputType: {input.Type}, OutputType: {output.Type}");
                            }
                            else
                            {
                                Assert.Equal(inIage.Columns, outImage.Columns);
                                Assert.Equal(inIage.Rows, outImage.Rows);

                                Dlib.SaveBmp(outImage, $"{Path.Combine(this.GetOutDir(type, "AssignImage"), $"{LoadTarget}_{input.Type}_{output.Type}.bmp")}");
                            }
                        }
                        catch (ArgumentException)
                        {
                            if (!expect)
                            {
                                Console.WriteLine("OK");
                            }
                            else
                            {
                                throw;
                            }
                        }
                        catch (NotSupportedException)
                        {
                            if (!expect)
                            {
                                Console.WriteLine("OK");
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to execute AssignImage to InputType: {input.Type}, OutputType: {output.Type}");
                        throw;
                    }
                    finally
                    {
                        if (outObj != null)
                            this.DisposeAndCheckDisposedState(outObj);
                        if (inObj != null)
                            this.DisposeAndCheckDisposedState(inObj);
                    }
                }
        }

        #endregion

    }

}
