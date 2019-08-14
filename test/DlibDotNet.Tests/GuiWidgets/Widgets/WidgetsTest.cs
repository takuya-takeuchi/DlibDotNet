using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.GuiWidgets
{

    [TestClass]
    public class ImageWindowTest : TestBase
    {

        [TestMethod]
        public void AddOverlay()
        {
            if (!this.CanGuiDebug)
            {
                Console.WriteLine("Build and run as Release mode if you wanna show Gui!!");
                return;
            }

            var path = this.GetDataFile("Lenna.bmp");
            var tests = new[]
            {
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                try
                {
                    var rect = new Rectangle(10, 10, 100, 100);
                    var array = Array2D.Array2DTest.CreateArray2D(test.Type, path.FullName);
                    using (var window = new ImageWindow(array))
                    {
                        switch (test.Type)
                        {
                            case ImageTypes.UInt8:
                                window.AddOverlay(rect, (byte)0, test.Type.ToString());
                                break;
                            case ImageTypes.UInt16:
                                window.AddOverlay(rect, (ushort)0, test.Type.ToString());
                                break;
                            case ImageTypes.UInt32:
                                window.AddOverlay(rect, 0u, test.Type.ToString());
                                break;
                            case ImageTypes.Int8:
                                window.AddOverlay(rect, (sbyte)0, test.Type.ToString());
                                break;
                            case ImageTypes.Int16:
                                window.AddOverlay(rect, (short)0, test.Type.ToString());
                                break;
                            case ImageTypes.Int32:
                                window.AddOverlay(rect, 0, test.Type.ToString());
                                break;
                            case ImageTypes.Float:
                                window.AddOverlay(rect, (short)0f, test.Type.ToString());
                                break;
                            case ImageTypes.Double:
                                window.AddOverlay(rect, 0d, test.Type.ToString());
                                break;
                            case ImageTypes.RgbAlphaPixel:
                                window.AddOverlay(rect, new RgbAlphaPixel(127, 0, 0, 0), test.Type.ToString());
                                break;
                            case ImageTypes.RgbPixel:
                                window.AddOverlay(rect, new RgbPixel(0, 0, 0), test.Type.ToString());
                                break;
                            case ImageTypes.HsiPixel:
                                window.AddOverlay(rect, new HsiPixel(0, 0, 0), test.Type.ToString());
                                break;
                        }

                        window.WaitUntilClosed();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine($"Failed to create ImageWindow from Array2D Type: {test.Type}");
                    throw;
                }
            }
        }

        [TestMethod]
        public void Create()
        {
            if (!this.CanGuiDebug)
            {
                Console.WriteLine("Build and run as Release mode if you wanna show Gui!!");
                return;
            }

            this.DisposeAndCheckDisposedState(new ImageWindow());
        }

        [TestMethod]
        public void CreateFromArray2D()
        {
            if (!this.CanGuiDebug)
            {
                Console.WriteLine("Build and run as Release mode if you wanna show Gui!!");
                return;
            }

            var path = this.GetDataFile("Lenna.bmp");
            var tests = new[]
            {
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                try
                {
                    var array = Array2D.Array2DTest.CreateArray2D(test.Type, path.FullName);
                    var window = new ImageWindow(array);
                    this.DisposeAndCheckDisposedState(window);
                    this.DisposeAndCheckDisposedState(array);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine($"Failed to create ImageWindow from Array2D Type: {test.Type}");
                    throw;
                }
            }
        }

        [TestMethod]
        public void CreateFromArray2DWithTitle()
        {
            if (!this.CanGuiDebug)
            {
                Console.WriteLine("Build and run as Release mode if you wanna show Gui!!");
                return;
            }

            var path = this.GetDataFile("Lenna.bmp");
            var tests = new[]
            {
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.BgrPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                try
                {
                    var array = Array2D.Array2DTest.CreateArray2D(test.Type, path.FullName);
                    var window = new ImageWindow(array, test.Type.ToString());
                    this.DisposeAndCheckDisposedState(window);
                    this.DisposeAndCheckDisposedState(array);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine($"Failed to create ImageWindow from Array2D Type: {test.Type}");
                    throw;
                }
            }
        }

        [TestMethod]
        public void WaitUntilClosed()
        {
            if (!this.CanGuiDebug)
            {
                Console.WriteLine("Build and run as Release mode if you wanna show Gui!!");
                return;
            }

            var window = new ImageWindow();
            window.WaitUntilClosed();

            this.DisposeAndCheckDisposedState(window);
        }

    }

}
