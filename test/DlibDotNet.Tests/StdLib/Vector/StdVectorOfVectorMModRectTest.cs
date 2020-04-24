using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfVectorMModRectTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<StdVector<MModRect>>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<StdVector<MModRect>>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(j => new StdVector<MModRect>(Enumerable.Range(0, size).Select(i => new MModRect { Ignore = true, DetectionConfidence = i })));
            var vector = new StdVector<StdVector<MModRect>>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var j = 0; j < size; j++)
            {
                var tmp = ret[j].ToArray();
                for (var i = 0; i < size; i++)
                {
                    Assert.Equal(tmp[i].DetectionConfidence, i);
                    Assert.Equal(tmp[i].Ignore, true);
                }
            }
            this.DisposeAndCheckDisposedState(vector);
            foreach (var s in source)
                s.Dispose();
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(j => new StdVector<MModRect>(Enumerable.Range(0, size).Select(i => new MModRect { Ignore = true, DetectionConfidence = i })));
            var vector = new StdVector<StdVector<MModRect>>(source);
            Assert.Equal(vector.Size, size);
            var ret = new StdVector<MModRect>[15];
            vector.CopyTo(ret, 5);

            for (var j = 0; j < size; j++)
            {
                var tmp = ret[j + 5].ToArray();
                for (var i = 0; i < size; i++)
                {
                    Assert.Equal(tmp[i].DetectionConfidence, i);
                    Assert.Equal(tmp[i].Ignore, true);
                }
            }

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
