using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageTransforms
{

    [TestClass]
    public class FindCandidateObjectLocationsTest : TestBase
    {

        [TestMethod]
        public void FindCandidateObjectLocations()
        {
            var path = this.GetDataFile("Lenna.jpg");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true },
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true },
                new { Type = ImageTypes.UInt8,         ExpectResult = true },
                new { Type = ImageTypes.UInt16,        ExpectResult = true },
                new { Type = ImageTypes.UInt32,        ExpectResult = true },
                new { Type = ImageTypes.Int8,          ExpectResult = true },
                new { Type = ImageTypes.Int16,         ExpectResult = true },
                new { Type = ImageTypes.Int32,         ExpectResult = true },
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false },
                new { Type = ImageTypes.Float,         ExpectResult = false },
                new { Type = ImageTypes.Double,        ExpectResult = false }
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                Array2DBase inImg = null;
                IEnumerable<Rectangle> rects = null;

                try
                {
                    inImg = DlibTest.LoadImage(test.Type, path);
                    rects = Dlib.FindCandidateObjectLocations(inImg);
                    if (rects == null || !rects.Any())
                        Assert.Fail($"FindCandidateObjectLocations should detect any rectangles.");

                    switch (test.Type)
                    {
                        case ImageTypes.RgbPixel:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, new RgbPixel { Blue = 255 });
                            break;
                        case ImageTypes.RgbAlphaPixel:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, new RgbAlphaPixel { Blue = 255, Alpha = 255 });
                            break;
                        case ImageTypes.UInt8:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, (byte)0);
                            break;
                        case ImageTypes.UInt16:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, (ushort)0);
                            break;
                        case ImageTypes.Int32:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, (int)(255 << 16));
                            break;
                        case ImageTypes.HsiPixel:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, new HsiPixel { H = 255 });
                            break;
                        case ImageTypes.Float:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, 0f);
                            break;
                        case ImageTypes.Double:
                            foreach (var r in rects)
                                Dlib.DrawRectangle(inImg, r, 0d);
                            break;
                    }

                    Dlib.SaveBmp(inImg, $"{Path.Combine(this.GetOutDir(type, "FindCandidateObjectLocations"), $"Lenna_{test.Type}.bmp")}");
                }
                catch (Exception e)
                {
                    if (!test.ExpectResult)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        Console.WriteLine(e.StackTrace);
                        Console.WriteLine($"Failed to execute FindCandidateObjectLocations to Type: {test.Type}.");
                        throw;
                    }
                }
                finally
                {
                    if (inImg != null)
                        this.DisposeAndCheckDisposedState(inImg);
                }
            }
        }

    }

}
