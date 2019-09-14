using System;
using System.Reflection;
using DlibDotNet.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Dnn.Tests
{

    [TestClass]
    public class CudaTest : TestBase
    {

        [TestMethod]
        public void ThrowCudaException()
        {
            if (Dlib.IsSupportCuda)
            {
                var type = typeof(Cuda);
                var method = type.GetMethod(nameof(ThrowCudaException), BindingFlags.NonPublic | BindingFlags.Static);
                if (method == null)
                    Assert.Fail($"Failed to get method {nameof(ThrowCudaException)}");

                const int cudaError = 0x77000000;

                const int targetCudaErrorCode = 2;
                const int cudaErrorMemoryAllocation = -(cudaError | targetCudaErrorCode);
                const string targetCudaErrorName = "cudaErrorMemoryAllocation";

                try
                {
                    method.Invoke(null, new object[]
                    {
                        cudaErrorMemoryAllocation
                    });

                    Assert.Fail($"{nameof(ThrowCudaException)} does not throw any {nameof(Exception)}.");
                }
                catch (TargetInvocationException tie)
                {
                    if (tie.InnerException is CudaException ce)
                    {
                        if (!(ce.ErrorName == targetCudaErrorName && ce.ErrorCode == targetCudaErrorCode))
                            Assert.Fail($"{nameof(CudaException)} does not specify {targetCudaErrorName}.");
                    }
                    else
                    {
                        var e = tie.InnerException;
                        Assert.Fail($"{nameof(ThrowCudaException)} does not throw {nameof(CudaException)}. Thrown {e}");
                    }
                }
                const int targetCudaErrorCode2 = 10000;
                const int cudaErrorApiFailureBase = -(cudaError | targetCudaErrorCode2);
                const string targetCudaErrorName2 = "cudaErrorApiFailureBase";

                try
                {
                    method.Invoke(null, new object[]
                    {
                        cudaErrorApiFailureBase
                    });

                    Assert.Fail($"{nameof(ThrowCudaException)} does not throw any {nameof(Exception)}.");
                }
                catch (TargetInvocationException tie)
                {
                    if (tie.InnerException is CudaException ce)
                    {
                        if (!(ce.ErrorName == targetCudaErrorName2 && ce.ErrorCode == targetCudaErrorCode2))
                            Assert.Fail($"{nameof(CudaException)} does not specify {targetCudaErrorName2}.");
                    }
                    else
                    {
                        var e = tie.InnerException;
                        Assert.Fail($"{nameof(ThrowCudaException)} does not throw {nameof(CudaException)}. Thrown {e}");
                    }
                }

                try
                {
                    const int dnnError = 0x7F000000;
                    const int dnnNotSupportNetworkType = -(dnnError | 0x00000001);
                    method.Invoke(null, new object[]
                    {
                        dnnNotSupportNetworkType
                    });
                }
                catch (TargetInvocationException tie)
                {
                    if (tie.InnerException is CudaException)
                    {
                        Assert.Fail($"{nameof(ThrowCudaException)} throw {nameof(CudaException)}.");
                    }
                    else
                    {
                        Assert.Fail($"{nameof(ThrowCudaException)} throw {nameof(Exception)}.");
                    }
                }
            }
        }

    }

}
