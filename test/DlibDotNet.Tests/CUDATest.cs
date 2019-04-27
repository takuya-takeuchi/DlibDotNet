using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests
{

    [TestClass]
    public class CudaTest : TestBase
    {

        [TestMethod]
        public void GetCudaDriverVersion()
        {
            var ret = Cuda.TryGetDriverVersion(out var version);
            if (ret)
            {
                Console.WriteLine($"Version: {version}");
            }
            else
            {
                if (version == -1)
                    Console.WriteLine("DlibDotNet.Native is not built with CUDA");
                else
                    Console.WriteLine("CUDA return invalid value");
            }
        }

        [TestMethod]
        public void GetCudaRuntimeVersion()
        {
            var ret = Cuda.TryGetRuntimeVersion(out var version);
            if (ret)
            {
                Console.WriteLine($"Version: {version}");
            }
            else
            {
                if (version == -1)
                    Console.WriteLine("DlibDotNet.Native is not built with CUDA");
                else
                    Console.WriteLine("CUDA return invalid value");
            }
        }

        [TestMethod]
        public void GetDnnCudaDriverVersion()
        {
            var ret = Dnn.Cuda.TryGetDriverVersion(out var version);
            if (ret)
            {
                Console.WriteLine($"Version: {version}");
            }
            else
            {
                if (version == -1)
                    Console.WriteLine("DlibDotNet.Native.Dnn is not built with CUDA");
                else
                    Console.WriteLine("CUDA return invalid value");
            }
        }

        [TestMethod]
        public void GetDnnRuntimeVersion()
        {
            var ret = Dnn.Cuda.TryGetRuntimeVersion(out var version);
            if (ret)
            {
                Console.WriteLine($"Version: {version}");
            }
            else
            {
                if (version == -1)
                    Console.WriteLine("DlibDotNet.Native.Dnn is not built with CUDA");
                else
                    Console.WriteLine("CUDA return invalid value");
            }
        }

    }

}
