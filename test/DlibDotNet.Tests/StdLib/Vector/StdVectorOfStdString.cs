using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfStdString : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<StdString>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<StdString>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new StdString(i.ToString()));
            var vector = new StdVector<StdString>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i].ToString(), i.ToString());
            }
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new StdString(i.ToString()));
            var vector = new StdVector<StdString>(source);
            Assert.Equal(vector.Size, size);
            var ret = new StdString[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i + 5].ToString(), i.ToString());
            }

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
