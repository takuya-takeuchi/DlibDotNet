using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if DEBUG
using System.Drawing;
#endif

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

#if DEBUG
                    using (var bmp = Image.FromFile(path.FullName))
                    using (var g = Graphics.FromImage(bmp))
                    using (var p = new Pen(Color.Blue, 1f))
                    {
                        foreach (var r in rects)
                            g.DrawRectangle(p, r.Left, r.Top, r.Width, r.Height);

                        bmp.Save($"{Path.Combine(this.GetOutDir(type, "FindCandidateObjectLocations"), $"Lenna_{test.Type}.bmp")}");
                    }
#endif
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
                    if (rects != null)
                        this.DisposeAndCheckDisposedState(rects);
                    if (inImg != null)
                        this.DisposeAndCheckDisposedState(inImg);
                }
            }
        }

    }

}
