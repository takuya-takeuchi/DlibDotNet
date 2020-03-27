using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfMatrixInt32Test : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<Matrix<int>>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<Matrix<int>>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Matrix<int>(i, i));
            var vector = new StdVector<Matrix<int>>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i].Rows, i);
                Assert.Equal(ret[i].Columns, i);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Matrix<int>(i, i));
            var vector = new StdVector<Matrix<int>>(source);
            Assert.Equal(vector.Size, size);
            var ret = new Matrix<int>[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i + 5].Rows, i);
                Assert.Equal(ret[i + 5].Columns, i);
            }

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}