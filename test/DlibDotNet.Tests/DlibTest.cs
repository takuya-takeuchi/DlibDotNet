using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests
{

    [TestClass]
    public class DlibTest : TestBase
    {

        private const string LoadTarget = "Lenna_mini";

        private const int LoadTargetWidth = 256;

        private const int LoadTargetHeight = 256;

        #region LoadBmp

        [TestMethod]
        public void LoadBmp()
        {
            const int cols = LoadTargetWidth;
            const int rows = LoadTargetHeight;
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                TwoDimentionObjectBase image;
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        image = Dlib.LoadBmp<RgbPixel>(path.FullName);
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        image = Dlib.LoadBmp<RgbAlphaPixel>(path.FullName);
                        break;
                    case ImageTypes.UInt8:
                        image = Dlib.LoadBmp<byte>(path.FullName);
                        break;
                    case ImageTypes.UInt16:
                        image = Dlib.LoadBmp<ushort>(path.FullName);
                        break;
                    case ImageTypes.UInt32:
                        image = Dlib.LoadBmp<uint>(path.FullName);
                        break;
                    case ImageTypes.Int8:
                        image = Dlib.LoadBmp<sbyte>(path.FullName);
                        break;
                    case ImageTypes.Int16:
                        image = Dlib.LoadBmp<short>(path.FullName);
                        break;
                    case ImageTypes.Int32:
                        image = Dlib.LoadBmp<int>(path.FullName);
                        break;
                    case ImageTypes.HsiPixel:
                        image = Dlib.LoadBmp<HsiPixel>(path.FullName);
                        break;
                    case ImageTypes.Float:
                        image = Dlib.LoadBmp<float>(path.FullName);
                        break;
                    case ImageTypes.Double:
                        image = Dlib.LoadBmp<double>(path.FullName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Assert.AreEqual(image.Columns, cols, $"Failed to load {test.Type}.");
                Assert.AreEqual(image.Rows, rows, $"Failed to load {test.Type}.");

                this.DisposeAndCheckDisposedState(image);
            }
        }

        #endregion

        #region LoadDng

        [TestMethod]
        public void LoadDng()
        {
            const int cols = LoadTargetWidth;
            const int rows = LoadTargetHeight;
            var path = this.GetDataFile($"{LoadTarget}.dng");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                TwoDimentionObjectBase image;
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        image = Dlib.LoadDng<RgbPixel>(path.FullName);
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        image = Dlib.LoadDng<RgbAlphaPixel>(path.FullName);
                        break;
                    case ImageTypes.UInt8:
                        image = Dlib.LoadDng<byte>(path.FullName);
                        break;
                    case ImageTypes.UInt16:
                        image = Dlib.LoadDng<ushort>(path.FullName);
                        break;
                    case ImageTypes.UInt32:
                        image = Dlib.LoadDng<uint>(path.FullName);
                        break;
                    case ImageTypes.Int8:
                        image = Dlib.LoadDng<sbyte>(path.FullName);
                        break;
                    case ImageTypes.Int16:
                        image = Dlib.LoadDng<short>(path.FullName);
                        break;
                    case ImageTypes.Int32:
                        image = Dlib.LoadDng<int>(path.FullName);
                        break;
                    case ImageTypes.HsiPixel:
                        image = Dlib.LoadDng<HsiPixel>(path.FullName);
                        break;
                    case ImageTypes.Float:
                        image = Dlib.LoadDng<float>(path.FullName);
                        break;
                    case ImageTypes.Double:
                        image = Dlib.LoadDng<double>(path.FullName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Assert.AreEqual(image.Columns, cols, $"Failed to load {test.Type}.");
                Assert.AreEqual(image.Rows, rows, $"Failed to load {test.Type}.");

                this.DisposeAndCheckDisposedState(image);
            }
        }

        #endregion

        #region LoadImage

        [TestMethod]
        public void LoadImage()
        {
            const int cols = LoadTargetWidth;
            const int rows = LoadTargetHeight;

            var exts = new[]
            {
                "png",
                "gif",
                "dng",
                "bmp",
                "jpg"
            };

            foreach (var ext in exts)
            {
                var path = this.GetDataFile($"{LoadTarget}.{ext}");
                var tests = new[]
                {
                    new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                    new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                    new { Type = ImageTypes.UInt8,         ExpectResult = true},
                    new { Type = ImageTypes.UInt16,        ExpectResult = true},
                    new { Type = ImageTypes.UInt32,        ExpectResult = true},
                    new { Type = ImageTypes.Int8,          ExpectResult = true},
                    new { Type = ImageTypes.Int16,         ExpectResult = true},
                    new { Type = ImageTypes.Int32,         ExpectResult = true},
                    new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                    new { Type = ImageTypes.Float,         ExpectResult = true},
                    new { Type = ImageTypes.Double,        ExpectResult = true}
                };

                foreach (var test in tests)
                {
                    TwoDimentionObjectBase image;
                    switch (test.Type)
                    {
                        case ImageTypes.RgbPixel:
                            image = Dlib.LoadImage<RgbPixel>(path.FullName);
                            break;
                        case ImageTypes.RgbAlphaPixel:
                            image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName);
                            break;
                        case ImageTypes.UInt8:
                            image = Dlib.LoadImage<byte>(path.FullName);
                            break;
                        case ImageTypes.UInt16:
                            image = Dlib.LoadImage<ushort>(path.FullName);
                            break;
                        case ImageTypes.UInt32:
                            image = Dlib.LoadImage<uint>(path.FullName);
                            break;
                        case ImageTypes.Int8:
                            image = Dlib.LoadImage<sbyte>(path.FullName);
                            break;
                        case ImageTypes.Int16:
                            image = Dlib.LoadImage<short>(path.FullName);
                            break;
                        case ImageTypes.Int32:
                            image = Dlib.LoadImage<int>(path.FullName);
                            break;
                        case ImageTypes.HsiPixel:
                            image = Dlib.LoadImage<HsiPixel>(path.FullName);
                            break;
                        case ImageTypes.Float:
                            image = Dlib.LoadImage<float>(path.FullName);
                            break;
                        case ImageTypes.Double:
                            image = Dlib.LoadImage<double>(path.FullName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    Assert.AreEqual(image.Columns, cols, $"Failed to load {test.Type} for {ext}.");
                    Assert.AreEqual(image.Rows, rows, $"Failed to load {test.Type} for {ext}.");

                    this.DisposeAndCheckDisposedState(image);
                }
            }
        }

        #endregion

        #region LoadJpeg

        [TestMethod]
        public void LoadJpeg()
        {
            const int cols = LoadTargetWidth;
            const int rows = LoadTargetHeight;
            var path = this.GetDataFile($"{LoadTarget}.jpg");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                TwoDimentionObjectBase image;
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        image = Dlib.LoadJpeg<RgbPixel>(path.FullName);
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        image = Dlib.LoadJpeg<RgbAlphaPixel>(path.FullName);
                        break;
                    case ImageTypes.UInt8:
                        image = Dlib.LoadJpeg<byte>(path.FullName);
                        break;
                    case ImageTypes.UInt16:
                        image = Dlib.LoadJpeg<ushort>(path.FullName);
                        break;
                    case ImageTypes.UInt32:
                        image = Dlib.LoadJpeg<uint>(path.FullName);
                        break;
                    case ImageTypes.Int8:
                        image = Dlib.LoadJpeg<sbyte>(path.FullName);
                        break;
                    case ImageTypes.Int16:
                        image = Dlib.LoadJpeg<short>(path.FullName);
                        break;
                    case ImageTypes.Int32:
                        image = Dlib.LoadJpeg<int>(path.FullName);
                        break;
                    case ImageTypes.HsiPixel:
                        image = Dlib.LoadJpeg<HsiPixel>(path.FullName);
                        break;
                    case ImageTypes.Float:
                        image = Dlib.LoadJpeg<float>(path.FullName);
                        break;
                    case ImageTypes.Double:
                        image = Dlib.LoadJpeg<double>(path.FullName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Assert.AreEqual(image.Columns, cols, $"Failed to load {test.Type}.");
                Assert.AreEqual(image.Rows, rows, $"Failed to load {test.Type}.");

                this.DisposeAndCheckDisposedState(image);
            }
        }

        #endregion

        #region LoadPng

        [TestMethod]
        public void LoadPng()
        {
            const int cols = LoadTargetWidth;
            const int rows = LoadTargetHeight;
            var path = this.GetDataFile($"{LoadTarget}.png");

            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                TwoDimentionObjectBase image;
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        image = Dlib.LoadPng<RgbPixel>(path.FullName);
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        image = Dlib.LoadPng<RgbAlphaPixel>(path.FullName);
                        break;
                    case ImageTypes.UInt8:
                        image = Dlib.LoadPng<byte>(path.FullName);
                        break;
                    case ImageTypes.UInt16:
                        image = Dlib.LoadPng<ushort>(path.FullName);
                        break;
                    case ImageTypes.UInt32:
                        image = Dlib.LoadPng<uint>(path.FullName);
                        break;
                    case ImageTypes.Int8:
                        image = Dlib.LoadPng<sbyte>(path.FullName);
                        break;
                    case ImageTypes.Int16:
                        image = Dlib.LoadPng<short>(path.FullName);
                        break;
                    case ImageTypes.Int32:
                        image = Dlib.LoadPng<int>(path.FullName);
                        break;
                    case ImageTypes.HsiPixel:
                        image = Dlib.LoadPng<HsiPixel>(path.FullName);
                        break;
                    case ImageTypes.Float:
                        image = Dlib.LoadPng<float>(path.FullName);
                        break;
                    case ImageTypes.Double:
                        image = Dlib.LoadPng<double>(path.FullName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Assert.AreEqual(image.Columns, cols, $"Failed to load {test.Type}.");
                Assert.AreEqual(image.Rows, rows, $"Failed to load {test.Type}.");

                this.DisposeAndCheckDisposedState(image);
            }
        }

        #endregion

        #region SaveBmp

        [TestMethod]
        public void SaveBmp()
        {
            var path = this.GetDataFile($"{LoadTarget}.jpg");
            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        {
                            var image = Dlib.LoadImage<RgbPixel>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        {
                            var image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt8:
                        {
                            var image = Dlib.LoadImage<byte>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt16:
                        {
                            var image = Dlib.LoadImage<ushort>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt32:
                        {
                            var image = Dlib.LoadImage<uint>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int8:
                        {
                            var image = Dlib.LoadImage<sbyte>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int16:
                        {
                            var image = Dlib.LoadImage<short>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int32:
                        {
                            var image = Dlib.LoadImage<int>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        {
                            var image = Dlib.LoadImage<HsiPixel>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Float:
                        {
                            var image = Dlib.LoadImage<float>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Double:
                        {
                            var image = Dlib.LoadImage<double>(path.FullName);
                            Dlib.SaveBmp(image, $"{Path.Combine(this.GetOutDir(type, "SaveBmp"), $"{LoadTarget}_{test.Type}.bmp")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region SaveDng

        [TestMethod]
        public void SaveDng()
        {
            var path = this.GetDataFile($"{LoadTarget}.jpg");
            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        {
                            var image = Dlib.LoadImage<RgbPixel>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        {
                            var image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt8:
                        {
                            var image = Dlib.LoadImage<byte>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt16:
                        {
                            var image = Dlib.LoadImage<ushort>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt32:
                        {
                            var image = Dlib.LoadImage<uint>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int8:
                        {
                            var image = Dlib.LoadImage<sbyte>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int16:
                        {
                            var image = Dlib.LoadImage<short>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int32:
                        {
                            var image = Dlib.LoadImage<int>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        {
                            var image = Dlib.LoadImage<HsiPixel>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Float:
                        {
                            var image = Dlib.LoadImage<float>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Double:
                        {
                            var image = Dlib.LoadImage<double>(path.FullName);
                            Dlib.SaveDng(image, $"{Path.Combine(this.GetOutDir(type, "SaveDng"), $"{LoadTarget}_{test.Type}.dng")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region SaveJpeg

        [TestMethod]
        public void SaveJpeg()
        {
            var path = this.GetDataFile($"{LoadTarget}.jpg");
            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        {
                            var image = Dlib.LoadImage<RgbPixel>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        {
                            var image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt8:
                        {
                            var image = Dlib.LoadImage<byte>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt16:
                        {
                            var image = Dlib.LoadImage<ushort>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt32:
                        {
                            var image = Dlib.LoadImage<uint>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int8:
                        {
                            var image = Dlib.LoadImage<sbyte>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int16:
                        {
                            var image = Dlib.LoadImage<short>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int32:
                        {
                            var image = Dlib.LoadImage<int>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        {
                            var image = Dlib.LoadImage<HsiPixel>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Float:
                        {
                            var image = Dlib.LoadImage<float>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Double:
                        {
                            var image = Dlib.LoadImage<double>(path.FullName);
                            Dlib.SaveJpeg(image, $"{Path.Combine(this.GetOutDir(type, "SaveJpeg"), $"{LoadTarget}_{test.Type}.jpg")}", 50);
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [TestMethod]
        public void SaveJpegThrowException()
        {
            var path = this.GetDataFile($"{LoadTarget}.jpg");
            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.UInt8,         ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.UInt16,        ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.UInt32,        ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.Int8,          ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.Int16,         ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.Int32,         ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.Float,         ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.Double,        ExpectResult = true, Quality = 0},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.UInt8,         ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.UInt16,        ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.UInt32,        ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.Int8,          ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.Int16,         ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.Int32,         ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.Float,         ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.Double,        ExpectResult = true, Quality = 100},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.UInt8,         ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.UInt16,        ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.UInt32,        ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.Int8,          ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.Int16,         ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.Int32,         ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.Float,         ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.Double,        ExpectResult = false, Quality = -1},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.UInt8,         ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.UInt16,        ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.UInt32,        ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.Int8,          ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.Int16,         ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.Int32,         ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.Float,         ExpectResult = false, Quality = 101},
                new { Type = ImageTypes.Double,        ExpectResult = false, Quality = 101}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                TwoDimentionObjectBase dimentionObject = null;
                var filepath = $"{Path.Combine(this.GetOutDir(type, "SaveJpegThrowException"), $"{LoadTarget}_{test.Type}_{test.Quality}.jpg")}";

                try
                {
                    switch (test.Type)
                    {
                        case ImageTypes.RgbPixel:
                            {
                                var image = Dlib.LoadImage<RgbPixel>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.RgbAlphaPixel:
                            {
                                var image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.UInt8:
                            {
                                var image = Dlib.LoadImage<byte>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.UInt16:
                            {
                                var image = Dlib.LoadImage<ushort>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.UInt32:
                            {
                                var image = Dlib.LoadImage<uint>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Int8:
                            {
                                var image = Dlib.LoadImage<sbyte>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Int16:
                            {
                                var image = Dlib.LoadImage<short>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Int32:
                            {
                                var image = Dlib.LoadImage<int>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.HsiPixel:
                            {
                                var image = Dlib.LoadImage<HsiPixel>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Float:
                            {
                                var image = Dlib.LoadImage<float>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        case ImageTypes.Double:
                            {
                                var image = Dlib.LoadImage<double>(path.FullName);
                                dimentionObject = image;
                                Dlib.SaveJpeg(image, filepath, test.Quality);
                                this.DisposeAndCheckDisposedState(image);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    if (!test.ExpectResult)
                    {
                        Assert.Fail($"Failed to save {test.Type} for quality: {test.Quality}.");
                    }
                }
                catch
                {
                    if (!test.ExpectResult)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    if (dimentionObject != null && !dimentionObject.IsDisposed)
                        this.DisposeAndCheckDisposedState(dimentionObject);
                }
            }
        }

        #endregion

        #region SavePng

        [TestMethod]
        public void SavePng()
        {
            var path = this.GetDataFile($"{LoadTarget}.jpg");
            var tests = new[]
            {
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.UInt32,        ExpectResult = true},
                new { Type = ImageTypes.Int8,          ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var type = this.GetType().Name;
            foreach (var test in tests)
            {
                switch (test.Type)
                {
                    case ImageTypes.RgbPixel:
                        {
                            var image = Dlib.LoadImage<RgbPixel>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.RgbAlphaPixel:
                        {
                            var image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt8:
                        {
                            var image = Dlib.LoadImage<byte>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt16:
                        {
                            var image = Dlib.LoadImage<ushort>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.UInt32:
                        {
                            var image = Dlib.LoadImage<uint>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int8:
                        {
                            var image = Dlib.LoadImage<sbyte>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int16:
                        {
                            var image = Dlib.LoadImage<short>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Int32:
                        {
                            var image = Dlib.LoadImage<int>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.HsiPixel:
                        {
                            var image = Dlib.LoadImage<HsiPixel>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Float:
                        {
                            var image = Dlib.LoadImage<float>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    case ImageTypes.Double:
                        {
                            var image = Dlib.LoadImage<double>(path.FullName);
                            Dlib.SavePng(image, $"{Path.Combine(this.GetOutDir(type, "SavePng"), $"{LoadTarget}_{test.Type}.png")}");
                            this.DisposeAndCheckDisposedState(image);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region LoadImageData

        [TestMethod]
        public void LoadImageData()
        {
            const int cols = 512;
            const int rows = 512;
            var path = this.GetDataFile($"lena_gray.raw");
            var data = File.ReadAllBytes(path.FullName);

            var tests = new[]
            {
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                //new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                //new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                //new { Type = ImageTypes.UInt16,        ExpectResult = true},
                //new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                //new { Type = ImageTypes.Float,         ExpectResult = true},
                //new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            foreach (var test in tests)
            {
                TwoDimentionObjectBase image;
                using (var win = new ImageWindow())
                {
                    switch (test.Type)
                    {
                        case ImageTypes.UInt8:
                            image = Dlib.LoadImageData<byte>(data, 512, 512, 512);

                            if (this.CanGuiDebug)
                            {
                                win.SetImage((Array2D<byte>)image);
                                win.WaitUntilClosed();
                            }
                            break;
                        //case ImageTypes.UInt16:
                        //    image = Dlib.LoadBmp<ushort>(path.FullName);
                        //    break;
                        //case ImageTypes.HsiPixel:
                        //    image = Dlib.LoadBmp<HsiPixel>(path.FullName);
                        //    break;
                        //case ImageTypes.Float:
                        //    image = Dlib.LoadBmp<float>(path.FullName);
                        //    break;
                        //case ImageTypes.Double:
                        //    image = Dlib.LoadBmp<double>(path.FullName);
                        //    break;
                        //case ImageTypes.RgbPixel:
                        //    image = Dlib.LoadBmp<RgbPixel>(path.FullName);
                        //    break;
                        //case ImageTypes.RgbAlphaPixel:
                        //    image = Dlib.LoadBmp<RgbAlphaPixel>(path.FullName);
                        //    break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                Assert.AreEqual(image.Columns, cols, $"Failed to load {test.Type}.");
                Assert.AreEqual(image.Rows, rows, $"Failed to load {test.Type}.");

                this.DisposeAndCheckDisposedState(image);
            }
        }

        [TestMethod]
        public void LoadImageData2()
        {
            const int cols = 512;
            const int rows = 512;
            const int steps = 512;

            var tests = new[]
            {
                new { Type = ImageTypes.UInt8,         ExpectResult = true},
                new { Type = ImageTypes.UInt16,        ExpectResult = true},
                new { Type = ImageTypes.Int16,         ExpectResult = true},
                new { Type = ImageTypes.Int32,         ExpectResult = true},
                new { Type = ImageTypes.HsiPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbPixel,      ExpectResult = true},
                new { Type = ImageTypes.RgbAlphaPixel, ExpectResult = true},
                new { Type = ImageTypes.Float,         ExpectResult = true},
                new { Type = ImageTypes.Double,        ExpectResult = true}
            };

            var random = new Random(0);

            foreach (var test in tests)
            {
                TwoDimentionObjectBase image;
                using (var win = new ImageWindow())
                {
                    switch (test.Type)
                    {
                        case ImageTypes.UInt8:
                            {
                                var data = new byte[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = (byte)random.Next(0, 255);

                                image = Dlib.LoadImageData<byte>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<byte>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.UInt16:
                            {
                                var data = new ushort[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = (ushort)random.Next(0, 255);

                                image = Dlib.LoadImageData<ushort>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<ushort>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.Int16:
                            {
                                var data = new short[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = (short)random.Next(0, 255);

                                image = Dlib.LoadImageData<short>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<short>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.Int32:
                            {
                                var data = new int[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = random.Next(0, 255);

                                image = Dlib.LoadImageData<int>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<int>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.Float:
                            {
                                var data = new float[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = (float)random.NextDouble();

                                image = Dlib.LoadImageData<float>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<float>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.Double:
                            {
                                var data = new double[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = (double)random.NextDouble();

                                image = Dlib.LoadImageData<double>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<double>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.HsiPixel:
                            {
                                var data = new HsiPixel[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = new HsiPixel
                                        {
                                            H = (byte)random.Next(0, 255),
                                            S = (byte)random.Next(0, 255),
                                            I = (byte)random.Next(0, 255)
                                        };

                                image = Dlib.LoadImageData<HsiPixel>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<HsiPixel>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.RgbPixel:
                            {
                                var data = new RgbPixel[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = new RgbPixel
                                        {
                                            Red = (byte)random.Next(0, 255),
                                            Green = (byte)random.Next(0, 255),
                                            Blue = (byte)random.Next(0, 255)
                                        };

                                image = Dlib.LoadImageData<RgbPixel>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<RgbPixel>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        case ImageTypes.RgbAlphaPixel:
                            {
                                var data = new RgbAlphaPixel[rows * cols];
                                for (var r = 0; r < rows; r++)
                                    for (var c = 0; c < cols; c++)
                                        data[steps * r + c] = new RgbAlphaPixel
                                        {
                                            Red = (byte)random.Next(0, 255),
                                            Green = (byte)random.Next(0, 255),
                                            Blue = (byte)random.Next(0, 255),
                                            Alpha = (byte)random.Next(0, 255)
                                        };

                                image = Dlib.LoadImageData<RgbAlphaPixel>(data, rows, cols, steps);

                                if (this.CanGuiDebug)
                                {
                                    win.SetImage((Array2D<RgbAlphaPixel>)image);
                                    win.WaitUntilClosed();
                                }
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                Assert.AreEqual(image.Columns, cols, $"Failed to load {test.Type}.");
                Assert.AreEqual(image.Rows, rows, $"Failed to load {test.Type}.");

                this.DisposeAndCheckDisposedState(image);
            }
        }

        #endregion

        internal static Array2DBase LoadImage(ImageTypes type, FileInfo path)
        {
            Array2DBase image;
            switch (type)
            {
                case ImageTypes.RgbPixel:
                    image = Dlib.LoadImage<RgbPixel>(path.FullName);
                    break;
                case ImageTypes.RgbAlphaPixel:
                    image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName);
                    break;
                case ImageTypes.UInt8:
                    image = Dlib.LoadImage<byte>(path.FullName);
                    break;
                case ImageTypes.UInt16:
                    image = Dlib.LoadImage<ushort>(path.FullName);
                    break;
                case ImageTypes.UInt32:
                    image = Dlib.LoadImage<uint>(path.FullName);
                    break;
                case ImageTypes.Int8:
                    image = Dlib.LoadImage<sbyte>(path.FullName);
                    break;
                case ImageTypes.Int16:
                    image = Dlib.LoadImage<short>(path.FullName);
                    break;
                case ImageTypes.Int32:
                    image = Dlib.LoadImage<int>(path.FullName);
                    break;
                case ImageTypes.HsiPixel:
                    image = Dlib.LoadImage<HsiPixel>(path.FullName);
                    break;
                case ImageTypes.Float:
                    image = Dlib.LoadImage<float>(path.FullName);
                    break;
                case ImageTypes.Double:
                    image = Dlib.LoadImage<double>(path.FullName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return image;
        }

        internal static MatrixBase LoadImageAsMatrix(MatrixElementTypes type, FileInfo path)
        {
            MatrixBase matrix;
            switch (type)
            {
                case MatrixElementTypes.RgbPixel:
                    using (var image = Dlib.LoadImage<RgbPixel>(path.FullName))
                        matrix = new Matrix<RgbPixel>(image);
                    break;
                case MatrixElementTypes.RgbAlphaPixel:
                    using (var image = Dlib.LoadImage<RgbAlphaPixel>(path.FullName))
                        matrix = new Matrix<RgbAlphaPixel>(image);
                    break;
                case MatrixElementTypes.UInt8:
                    using (var image = Dlib.LoadImage<byte>(path.FullName))
                        matrix = new Matrix<byte>(image);
                    break;
                case MatrixElementTypes.UInt16:
                    using (var image = Dlib.LoadImage<ushort>(path.FullName))
                        matrix = new Matrix<ushort>(image);
                    break;
                case MatrixElementTypes.UInt32:
                    using (var image = Dlib.LoadImage<uint>(path.FullName))
                        matrix = new Matrix<uint>(image);
                    break;
                case MatrixElementTypes.Int8:
                    using (var image = Dlib.LoadImage<sbyte>(path.FullName))
                        matrix = new Matrix<sbyte>(image);
                    break;
                case MatrixElementTypes.Int16:
                    using (var image = Dlib.LoadImage<short>(path.FullName))
                        matrix = new Matrix<short>(image);
                    break;
                case MatrixElementTypes.Int32:
                    using (var image = Dlib.LoadImage<int>(path.FullName))
                        matrix = new Matrix<int>(image);
                    break;
                case MatrixElementTypes.HsiPixel:
                    using (var image = Dlib.LoadImage<HsiPixel>(path.FullName))
                        matrix = new Matrix<HsiPixel>(image);
                    break;
                case MatrixElementTypes.Float:
                    using (var image = Dlib.LoadImage<float>(path.FullName))
                        matrix = new Matrix<float>(image);
                    break;
                case MatrixElementTypes.Double:
                    using (var image = Dlib.LoadImage<double>(path.FullName))
                        matrix = new Matrix<double>(image);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return matrix;
        }

    }

}
