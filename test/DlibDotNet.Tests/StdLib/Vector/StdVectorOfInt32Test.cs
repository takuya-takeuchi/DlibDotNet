using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfInt32Test : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<int>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<int>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).ToArray();
            var vector = new StdVector<int>(source);
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
            var source = Enumerable.Range(0, size).ToArray();
            var vector = new StdVector<int>(source);
            Assert.Equal(vector.Size, size);
            var ret = new int[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
                Assert.Equal(ret[i + 5], i);

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
