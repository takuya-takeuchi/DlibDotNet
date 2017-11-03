using System.Collections.Generic;
using System.Linq;
using DlibDotNet.ImageProcessing;
using DlibDotNet.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [TestClass]
    public class VectorOfVectorMModRectTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new VectorOfVectorMModRect();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new VectorOfVectorMModRect(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(j => new List<MModRect>(Enumerable.Range(0, size).Select(i => new MModRect { Ignore = true, DetectionConfidence = i })));
            var vector = new VectorOfVectorMModRect(source);
            Assert.AreEqual(vector.Size, size);
            var ret = vector.ToArray();
            for (var j = 0; j < size; j++)
            {
                var tmp = ret[j].ToArray();
                for (var i = 0; i < size; i++)
                {
                    Assert.AreEqual(tmp[i].DetectionConfidence, i);
                    Assert.AreEqual(tmp[i].Ignore, true);
                }
            }
            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
