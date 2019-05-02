using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Geometry
{

    [TestClass]
    public class PointTest : TestBase
    {

        [TestMethod]
        public void Create1()
        {
            var point = new Point();
        }

        [TestMethod]
        public void Create2()
        {
            var x = this.NextRandom(0, 100);
            var y = this.NextRandom(0, 100);
            var point = new Point(x, y);
            Assert.AreEqual(point.X, x);
            Assert.AreEqual(point.Y, y);
        }

        [TestMethod]
        public void Length()
        {
            var x = this.NextRandom(1, 100);
            var y = this.NextRandom(1, 100);
            var point = new Point(x, y);
            var length = Math.Sqrt(x * x + y * y);
            Assert.AreEqual(point.Length, length);
        }

        [TestMethod]
        public void Rotate()
        {
            const int away = 90;
            var x = 10;
            var y = 20;
            var point = new Point(x, y);
            var move = new Point(-away, 0);
            var p = point + move;

            for (var i = 1; i <= 3; i++)
            {
                var rotated = point.Rotate(p, 90 * i);

                switch (i)
                {
                    case 1:
                        Assert.AreEqual(rotated.X, x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(rotated.Y, y - away, $"Check Y Rotate: {90 * i}");
                        break;
                    case 2:
                        Assert.AreEqual(rotated.X, x + away, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(rotated.Y, y, $"Check Y Rotate: {90 * i}");
                        break;
                    case 3:
                        Assert.AreEqual(rotated.X, x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(rotated.Y, y + away, $"Check Y and Rotate: {90 * i}");
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
            var point = new Point(x, y);
            var move = new Point(-away, 0);
            var p = point + move;

            for (var i = 1; i <= 3; i++)
            {
                var rotated = Point.Rotate(point, p, 90 * i);

                switch (i)
                {
                    case 1:
                        Assert.AreEqual(rotated.X, x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(rotated.Y, y - away, $"Check Y Rotate: {90 * i}");
                        break;
                    case 2:
                        Assert.AreEqual(rotated.X, x + away, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(rotated.Y, y, $"Check Y Rotate: {90 * i}");
                        break;
                    case 3:
                        Assert.AreEqual(rotated.X, x, $"Check X and Rotate: {90 * i}");
                        Assert.AreEqual(rotated.Y, y + away, $"Check Y and Rotate: {90 * i}");
                        break;
                }
            }
        }

        [TestMethod]
        public void LengthSquared()
        {
            var x = this.NextRandom(1, 100);
            var y = this.NextRandom(1, 100);
            var point = new Point(x, y);
            var lengthSquared = x * x + y * y;
            Assert.AreEqual(point.LengthSquared, lengthSquared);
        }

        [TestMethod]
        public void OperatorAdd()
        {
            var lx = this.NextRandom(0, 100);
            var ly = this.NextRandom(0, 100);
            var rx = this.NextRandom(0, 100);
            var ry = this.NextRandom(0, 100);

            var r = new Point(lx, ly);
            var l = new Point(rx, ry);
            var rl = r + l;

            Assert.AreEqual(rl.X, lx + rx);
            Assert.AreEqual(rl.Y, ly + ry);
        }

        [TestMethod]
        public void OperatorSub()
        {
            var lx = this.NextRandom(0, 100);
            var ly = this.NextRandom(0, 100);
            var rx = this.NextRandom(0, 100);
            var ry = this.NextRandom(0, 100);

            var r = new Point(lx, ly);
            var l = new Point(rx, ry);
            var rl = r - l;

            Assert.AreEqual(rl.X, lx - rx);
            Assert.AreEqual(rl.Y, ly - ry);
        }

        [TestMethod]
        public void OperatorMul_Point_Int32()
        {
            var lx = this.NextRandom(10, 100);
            var ly = this.NextRandom(10, 100);

            var l = new Point(lx, ly);
            var r = 2;
            var lr = l * r;

            Assert.AreEqual(lr.X, lx * r);
            Assert.AreEqual(lr.Y, ly * r);
        }

        [TestMethod]
        public void OperatorMul_Int32_Point()
        {
            var rx = this.NextRandom(10, 100);
            var ry = this.NextRandom(10, 100);

            var r = new Point(rx, ry);
            var l = 2;
            var lr = l * r;

            Assert.AreEqual(lr.X, l * rx);
            Assert.AreEqual(lr.Y, l * ry);
        }

        [TestMethod]
        public void OperatorMul_Double_Point()
        {
            var rx = this.NextRandom(10, 100);
            var ry = this.NextRandom(10, 100);

            var r = new Point(rx, ry);
            var l = 2.5d;
            var lr = l * r;

            Assert.AreEqual(lr.X, (int)Math.Floor(l * rx + 0.5), $"rx: {rx}");
            Assert.AreEqual(lr.Y, (int)Math.Floor(l * ry + 0.5), $"ry: {ry}");
        }

        [TestMethod]
        public void OperatorDiv()
        {
            var lx = this.NextRandom(10, 100);
            var ly = this.NextRandom(10, 100);

            var r = new Point(lx, ly);
            var l = 2;
            var rl = r / l;

            Assert.AreEqual(rl.X, lx / l);
            Assert.AreEqual(rl.Y, ly / l);
        }

        [TestMethod]
        public void OperatorDivByZero()
        {
            var lx = this.NextRandom(10, 100);
            var ly = this.NextRandom(10, 100);

            try
            {
                var r = new Point(lx, ly);
                var rl = r / 0;
                Assert.Fail("Should throw DivideByZeroException when Point was divided by 0");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("OK");
            }
        }

        [TestMethod]
        public void OperatorEqual()
        {
            var x = this.NextRandom(1, 100);
            var y = this.NextRandom(1, 100);

            var r = new Point(x, y);
            var l = new Point(x, y);
            var l1 = new Point(x * 2, y);
            var l2 = new Point(x, y * 2);

            Assert.IsTrue(r == l, $"1 - RX: {r.X}, RY: {r.Y}\nLX: {l.X}, LY: {l.Y}");
            Assert.IsTrue(r != l1, $"2 - RX: {r.X}, RY: {r.Y}\nLX: {l1.X}, LY: {l1.Y}");
            Assert.IsTrue(r != l2, $"3 - RX: {r.X}, RY: {r.Y}\nLX: {l2.X}, LY: {l2.Y}");
        }

    }
}
