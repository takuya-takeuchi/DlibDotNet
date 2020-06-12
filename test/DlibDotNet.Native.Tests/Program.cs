using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Native.Tests
{

    [TestClass]
    public class DlibTest
    {

        private const string VersionKey = "DLIBDOTNET_VERSION";

        private const string GuiSupportKey = "DLIBDOTNET_GUI_SUPPORT";

        [TestMethod]
        public void CheckDlibDotNetNativeVersion()
        {
            var values = Environment.GetEnvironmentVariables();
            if (!values.Contains(VersionKey))
                Assert.Fail($"{VersionKey} is not found.");

            Console.WriteLine($"{VersionKey}: {values[VersionKey]}");
            Assert.AreEqual(values[VersionKey], DlibDotNet.Dlib.GetNativeVersion());
        }

        [TestMethod]
        public void CheckDlibDotNetNativeDnnVersion()
        {
            var values = Environment.GetEnvironmentVariables();
            if (!values.Contains(VersionKey))
                Assert.Fail($"{VersionKey} is not found.");

            Console.WriteLine($"{VersionKey}: {values[VersionKey]}");
            Assert.AreEqual(values[VersionKey], DlibDotNet.Dlib.GetNativeDnnVersion());
        }

        [TestMethod]
        public void GetFrontalFaceDetector()
        {
            using(var faceDetector = DlibDotNet.Dlib.GetFrontalFaceDetector())
            using(var image = DlibDotNet.Dlib.LoadImage<RgbPixel>("Lenna.png"))
            {
                var dets = faceDetector.Operator(image);
                Assert.AreEqual(1, dets.Length);
            }
        }

        [TestMethod]
        public void LossMmod()
        {
            using(var loss = DlibDotNet.Dnn.LossMmod.Deserialize("mmod_human_face_detector.dat"))
            using(var matrix = DlibDotNet.Dlib.LoadImageAsMatrix<RgbPixel>("Lenna.png"))
            {
                var dets = loss.Operator(matrix).First().ToArray();
                Assert.AreEqual(1, dets.Length);
            }
        }

        [TestMethod]
        public void CheckIsSupoortGui()
        {
            var values = Environment.GetEnvironmentVariables();
            if (!values.Contains(GuiSupportKey))
                Assert.Fail($"{GuiSupportKey} is not found.");

            Console.WriteLine($"{GuiSupportKey}: {values[GuiSupportKey]}");
            Assert.AreEqual((string)values[GuiSupportKey] != "0", DlibDotNet.Dlib.IsSupportGui);
        }

        [TestMethod]
        public void CheckIsDnnSupoortGui()
        {
            var values = Environment.GetEnvironmentVariables();
            if (!values.Contains(GuiSupportKey))
                Assert.Fail($"{GuiSupportKey} is not found.");

            Console.WriteLine($"{GuiSupportKey}: {values[GuiSupportKey]}");
            Assert.AreEqual((string)values[GuiSupportKey] != "0", DlibDotNet.Dlib.IsDnnSupportGui);
        }

    }

}
