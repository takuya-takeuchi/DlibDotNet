using System;
using Xunit;

namespace DlibDotNet.Tests
{

    public class CudaTest : TestBase
    {

        [Fact]
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

        [Fact]
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

        [Fact]
        public void GetDnnCudaDriverVersion()
        {
            var ret = DlibDotNet.Dnn.Cuda.TryGetDriverVersion(out var version);
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

        [Fact]
        public void GetDnnRuntimeVersion()
        {
            var ret = DlibDotNet.Dnn.Cuda.TryGetRuntimeVersion(out var version);
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
