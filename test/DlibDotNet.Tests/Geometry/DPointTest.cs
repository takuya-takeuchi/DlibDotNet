using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Geometry
{

    [TestClass]
    public class DPointTest : TestBase
    {

        [TestMethod]
        public void Create1()
        {
            var point = new DPoint();
        }

        [TestMethod]
        public void Create2()
        {
            var x = this.NextDoubleRandom();
            var y = this.NextDoubleRandom();
            var point = new DPoint(x, y);
            Assert.AreEqual(point.X, x);
            Assert.AreEqual(point.Y, y);
        }

        [TestMethod]
        public void Length()
        {
            var x = this.NextDoubleRandom();
            var y = this.NextDoubleRandom();
            var point = new DPoint(x, y);
            var length = Math.Sqrt(x * x + y * y);
            Assert.AreEqual(point.Length, length);
        }

        [TestMethod]
        public void LengthSquared()
        {
            var x = this.NextDoubleRandom();
            var y = this.NextDoubleRandom();
            var point = new DPoint(x, y);
            var lengthSquared = x * x + y * y;
            Assert.AreEqual(point.LengthSquared, lengthSquared);
        }

        [TestMethod]
        public void Rotate()
        {
            const int away = 90;
            var x = 10;
            var y = 20;
            var point = new DPoint(x, y);
            var move = new DPoint(-away, 0);
            var p = point + move;

            for (var i = 1; i <= 3; i++)
            {
                var rotated = point.Rotate(p, 90 * i);

                switch (i)
                {
                    case 1:
                        Assert.AreEqual(Math.Round(rotated.X), x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(Math.Round(rotated.Y), y - away, $"Check Y Rotate: {90 * i}");
                        break;
                    case 2:
                        Assert.AreEqual(Math.Round(rotated.X), x + away, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(Math.Round(rotated.Y), y, $"Check Y Rotate: {90 * i}");
                        break;
                    case 3:
                        Assert.AreEqual(Math.Round(rotated.X), x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(Math.Round(rotated.Y), y + away, $"Check Y and Rotate: {90 * i}");
                        break;
                }
            }
        }

        [TestMethod]
        public void Rotate2()
        {
            const int away = 90;
            var x = 10;
            var y = 20;
            var point = new DPoint(x, y);
            var move = new DPoint(-away, 0);
            var p = point + move;

            for (var i = 1; i <= 3; i++)
            {
                var rotated = DPoint.Rotate(point, p, 90 * i);

                switch (i)
                {
                    case 1:
                        Assert.AreEqual(Math.Round(rotated.X), x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(Math.Round(rotated.Y), y - away, $"Check Y Rotate: {90 * i}");
                        break;
                    case 2:
                        Assert.AreEqual(Math.Round(rotated.X), x + away, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(Math.Round(rotated.Y), y, $"Check Y Rotate: {90 * i}");
                        break;
                    case 3:
                        Assert.AreEqual(Math.Round(rotated.X), x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(Math.Round(rotated.Y), y + away, $"Check Y and Rotate: {90 * i}");
                        break;
                }
            }
        }

        [TestMethod]
        public void OperatorAdd()
        {
            var lx = (double)this.NextRandom(0, 100);
            var ly = (double)this.NextRandom(0, 100);
            var rx = (double)this.NextRandom(0, 100);
            var ry = (double)this.NextRandom(0, 100);

            var r = new DPoint(lx, ly);
            var l = new DPoint(rx, ry);
            var rl = r + l;

            Assert.AreEqual(rl.X, lx + rx);
            Assert.AreEqual(rl.Y, ly + ry);
        }

        [TestMethod]
        public void OperatorSub()
        {
            var lx = (double)this.NextRandom(0, 100);
            var ly = (double)this.NextRandom(0, 100);
            var rx = (double)this.NextRandom(0, 100);
            var ry = (double)this.NextRandom(0, 100);

            var r = new DPoint(lx, ly);
            var l = new DPoint(rx, ry);
            var rl = r - l;

            Assert.AreEqual(rl.X, lx - rx);
            Assert.AreEqual(rl.Y, ly - ry);
        }

        [TestMethod]
        public void OperatorMul_DPoint_Int32()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            var l = new DPoint(lx, ly);
            var r = 2;
            var lr = l * r;

            Assert.AreEqual(lr.X, lx * r);
            Assert.AreEqual(lr.Y, ly * r);
        }

        [TestMethod]
        public void OperatorMul_DPoint_Double()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            var l = new DPoint(lx, ly);
            var r = 2.5d;
            var lr = l * r;

            Assert.AreEqual(lr.X, lx * r);
            Assert.AreEqual(lr.Y, ly * r);
        }

        [TestMethod]
        public void OperatorMul_Int32_DPoint()
        {
            var rx = (double)this.NextByteRandom();
            var ry = (double)this.NextByteRandom();

            var r = new DPoint(rx, ry);
            var l = 2;
            var lr = l * r;

            Assert.AreEqual(lr.X, l * rx);
            Assert.AreEqual(lr.Y, l * ry);
        }

        [TestMethod]
        public void OperatorMul_Double_DPoint()
        {
            var rx = (double)this.NextByteRandom();
            var ry = (double)this.NextByteRandom();

            var r = new DPoint(rx, ry);
            var l = 2.5d;
            var lr = l * r;

            Assert.AreEqual(lr.X, l * rx);
            Assert.AreEqual(lr.Y, l * ry);
        }

        [TestMethod]
        public void OperatorDiv()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            var r = new DPoint(lx, ly);
            var l = 2;
            var rl = r / l;

            Assert.AreEqual(rl.X, lx / l);
            Assert.AreEqual(rl.Y, ly / l);
        }

        [TestMethod]
        public void OperatorDivByZero()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            try
            {
                var r = new DPoint(lx, ly);
                var rl = r / 0;
                Assert.Fail("Should throw DivideByZeroException when DPoint was divided by 0");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("OK");
            }
        }

        [TestMethod]
        public void OperatorEqual()
        {
            var x = (double)this.NextRandom(1, 100);
            var y = (double)this.NextRandom(1, 100);

            var r = new DPoint(x, y);
            var l = new DPoint(x, y);
            var l1 = new DPoint(x * 2, y);
            var l2 = new DPoint(x, y * 2);

            Assert.IsTrue(r == l, $"1 - RX: {r.X}, RY: {r.Y}\nLX: {l.X}, LY: {l.Y}");
            Assert.IsTrue(r != l1, $"2 - RX: {r.X}, RY: {r.Y}\nLX: {l1.X}, LY: {l1.Y}");
            Assert.IsTrue(r != l2, $"3 - RX: {r.X}, RY: {r.Y}\nLX: {l2.X}, LY: {l2.Y}");
        }

    }
}
