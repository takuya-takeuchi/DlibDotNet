using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Queue
{

    [TestClass]
    public class Kernel1AOfUInt32Test : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var queue = new Queue<uint>.Kernel1A();
            this.DisposeAndCheckDisposedState(queue);
        }

        [TestMethod]
        public void Clear()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
                queue.Enqueue((uint)i);

            queue.Clear();
            Assert.AreEqual(0, queue.Count);

            this.DisposeAndCheckDisposedState(queue);
        }

        [TestMethod]
        public void Enqueue()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
            {
                queue.Enqueue((uint)i);
                Assert.AreEqual(queue.Count, i + 1);
            }
            this.DisposeAndCheckDisposedState(queue);
        }

        [TestMethod]
        public void Dequeue()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
                queue.Enqueue((uint)i);

            var cur = 0u;
            while (queue.Count > 0)
            {
                queue.Dequeue(out var item);
                Assert.AreEqual(cur, item);
                cur++;
            }

            this.DisposeAndCheckDisposedState(queue);
        }

        [TestMethod]
        public void ElementAndMoveNext()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
                queue.Enqueue((uint)i);

            var cur = 0u;
            queue.Reset();
            while (queue.MoveNext)
            {
                Assert.AreEqual(cur, queue.Element());
                cur++;
            }

            this.DisposeAndCheckDisposedState(queue);
        }

    }

}