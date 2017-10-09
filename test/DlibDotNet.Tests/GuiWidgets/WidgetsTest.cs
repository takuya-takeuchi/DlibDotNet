using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.GuiWidgets
{

    [TestClass]
    public class WidgetsTest : TestBase
    {

        #region ImageWindow

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
                new { Type = ImageTypes.RgbPixel      },
                new { Type = ImageTypes.RgbAlphaPixel },
                new { Type = ImageTypes.UInt8         },
                new { Type = ImageTypes.UInt16        },
                new { Type = ImageTypes.HsiPixel      },
                new { Type = ImageTypes.Float         },
                new { Type = ImageTypes.Double        }
            };

            foreach (var test in tests)
            {
                try
                {
                    switch (test.Type)
                    {
                        case ImageTypes.UInt8:
                            {
                                var image = Dlib.LoadBmp<byte>(path.FullName);
                                var window = new ImageWindow(image);
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.UInt16:
                            {
                                var image = Dlib.LoadBmp<ushort>(path.FullName);
                                var window = new ImageWindow(image);
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Float:
                            {
                                var image = Dlib.LoadBmp<float>(path.FullName);
                                var window = new ImageWindow(image);
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Double:
                            {
                                var image = Dlib.LoadBmp<double>(path.FullName);
                                var window = new ImageWindow(image);
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.RgbPixel:
                            {
                                var image = Dlib.LoadBmp<RgbPixel>(path.FullName);
                                var window = new ImageWindow(image);
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.RgbAlphaPixel:
                            {
                                var image = Dlib.LoadBmp<RgbAlphaPixel>(path.FullName);
                                var window = new ImageWindow(image);
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.HsiPixel:
                            {
                                var image = Dlib.LoadBmp<HsiPixel>(path.FullName);
                                var window = new ImageWindow(image);
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(test.Type), test.Type, null);
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
                new { Type = ImageTypes.RgbPixel      },
                new { Type = ImageTypes.RgbAlphaPixel },
                new { Type = ImageTypes.UInt8         },
                new { Type = ImageTypes.UInt16        },
                new { Type = ImageTypes.HsiPixel      },
                new { Type = ImageTypes.Float         },
                new { Type = ImageTypes.Double        }
            };

            foreach (var test in tests)
            {
                try
                {
                    switch (test.Type)
                    {
                        case ImageTypes.UInt8:
                            {
                                var image = Dlib.LoadBmp<byte>(path.FullName);
                                var window = new ImageWindow(image, test.Type.ToString());
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.UInt16:
                            {
                                var image = Dlib.LoadBmp<ushort>(path.FullName);
                                var window = new ImageWindow(image, test.Type.ToString());
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Float:
                            {
                                var image = Dlib.LoadBmp<float>(path.FullName);
                                var window = new ImageWindow(image, test.Type.ToString());
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Double:
                            {
                                var image = Dlib.LoadBmp<double>(path.FullName);
                                var window = new ImageWindow(image, test.Type.ToString());
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.RgbPixel:
                            {
                                var image = Dlib.LoadBmp<RgbPixel>(path.FullName);
                                var window = new ImageWindow(image, test.Type.ToString());
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.RgbAlphaPixel:
                            {
                                var image = Dlib.LoadBmp<RgbAlphaPixel>(path.FullName);
                                var window = new ImageWindow(image, test.Type.ToString());
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.HsiPixel:
                            {
                                var image = Dlib.LoadBmp<HsiPixel>(path.FullName);
                                var window = new ImageWindow(image, test.Type.ToString());
                                this.DisposeAndCheckDisposedState(window);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(test.Type), test.Type, null);
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

        #endregion

    }

}
