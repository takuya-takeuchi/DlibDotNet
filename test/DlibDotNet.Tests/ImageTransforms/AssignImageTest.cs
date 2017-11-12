using System;
using System.IO;
using DlibDotNet.Tests.Array2D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class AssignImageTest : TestBase
    {
        private const string LoadTarget = "Lenna_mini";

        #region AssignImage

        [TestMethod]
        public void AssignImage()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var input in tests)
                foreach (var output in tests)
                {
                    TwoDimentionObjectBase outObj = null;
                    TwoDimentionObjectBase inObj = null;

                    var expect = input.ExpectResult && output.ExpectResult;

                    try
                    {
                        var inIage = DlibTest.LoadImage(input.Type, path);
                        inObj = inIage;

                        try
                        {
                            var outImage = Array2DTest.CreateArray2D(output.Type);
                            outObj = outImage;

                            Dlib.AssignImage(inIage, outImage);

                            if (!expect)
                            {
                                Assert.Fail($"AssignImage should throw excption for InputType: {input.Type}, OutputType: {output.Type}");
                            }
                            else
                            {
                                Assert.AreEqual(inIage.Columns, outImage.Columns);
                                Assert.AreEqual(inIage.Rows, outImage.Rows);

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
