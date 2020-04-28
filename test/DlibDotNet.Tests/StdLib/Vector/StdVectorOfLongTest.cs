using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfLongTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<long>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<long>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(s => (long)s).ToArray();
            var vector = new StdVector<long>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
                Assert.Equal(ret[i], i);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => (long)i).ToArray();
            var vector = new StdVector<long>(source);
            Assert.Equal(vector.Size, size);
            var ret = new long[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
                Assert.Equal(ret[i + 5], i);

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
