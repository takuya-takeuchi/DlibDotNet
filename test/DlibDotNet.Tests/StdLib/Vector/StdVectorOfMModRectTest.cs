using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfMModRectTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<MModRect>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<MModRect>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new MModRect{Ignore = true, DetectionConfidence = i});
            var vector = new StdVector<MModRect>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i].DetectionConfidence, i);
                Assert.Equal(ret[i].Ignore, true);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new MModRect { Ignore = true, DetectionConfidence = i });
            var vector = new StdVector<MModRect>(source);
            Assert.Equal(vector.Size, size);
            var ret = new MModRect[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i + 5].DetectionConfidence, i);
                Assert.Equal(ret[i + 5].Ignore, true);
            }

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
