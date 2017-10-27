using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Geometry
{

    [TestClass]
    public class RectangleTest : TestBase
    {

        [TestMethod]
        public void Create1()
        {
            var rect = new Rectangle();
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Create2()
        {
            var width = (uint)this.NextRandom(1, 100);
            var height = (uint)this.NextRandom(1, 100);
            var rect = new Rectangle(width, height);
            Assert.AreEqual((uint)rect.Right, (width - 1));
            Assert.AreEqual((uint)rect.Bottom, (height - 1));
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Create3()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            Assert.AreEqual(rect.Left, left);
            Assert.AreEqual(rect.Top, top);
            Assert.AreEqual(rect.Right, right);
            Assert.AreEqual(rect.Bottom, bottom);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Create4()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var p1 = new Point(left, top);
            var p2 = new Point(right, bottom);
            var rect = new Rectangle(p1, p2);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(p1);
            this.DisposeAndCheckDisposedState(p2);
        }

        [TestMethod]
        public void Create5()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var p = new Point(left, top);
            var rect = new Rectangle(p);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(p);
        }

        [TestMethod]
        public void Left()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Left = value;
            Assert.AreEqual(rect.Left, value);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Top()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Top = value;
            Assert.AreEqual(rect.Top, value);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Right()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Right = value;
            Assert.AreEqual(rect.Right, value);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Bottom()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Bottom = value;
            Assert.AreEqual(rect.Bottom, value);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Width()
        {
            var width = (uint)this.NextRandom(0, 100);
            var height = (uint)this.NextRandom(0, 100);
            var rect = new Rectangle(width, height);
            Assert.AreEqual(rect.Width, width);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Height()
        {
            var width = (uint)this.NextRandom(0, 100);
            var height = (uint)this.NextRandom(0, 100);
            var rect = new Rectangle(width, height);
            Assert.AreEqual(rect.Height, height);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Area()
        {
            var width = (uint)this.NextRandom(0, 100);
            var height = (uint)this.NextRandom(0, 100);
            var rect = new Rectangle(width, height);
            Assert.AreEqual(rect.Area, width * height);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void IsEmpty()
        {
            var right = this.NextRandom(1, 100);
            var bottom = this.NextRandom(1, 100);
            var left = right * 2;
            var top = bottom * 2;
            var rect1 = new Rectangle(left, top, right, bottom);
            Assert.IsTrue(rect1.IsEmpty);
            this.DisposeAndCheckDisposedState(rect1);

            left = this.NextRandom(1, 100);
            top = this.NextRandom(1, 100);
            right = left * 2;
            bottom = top * 2;
            var rect2 = new Rectangle(left, top, right, bottom);
            Assert.IsFalse(rect2.IsEmpty);
            this.DisposeAndCheckDisposedState(rect2);
        }

        [TestMethod]
        public void TopLeft()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var tl = rect.TopLeft;
            Assert.AreEqual(tl.X, left);
            Assert.AreEqual(tl.Y, top);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(tl);
        }

        [TestMethod]
        public void TopRight()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var tr = rect.TopRight;
            Assert.AreEqual(tr.X, right);
            Assert.AreEqual(tr.Y, top);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(tr);
        }

        [TestMethod]
        public void BottomLeft()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var bl = rect.BottomLeft;
            Assert.AreEqual(bl.X, left);
            Assert.AreEqual(bl.Y, bottom);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(bl);
        }

        [TestMethod]
        public void BottomRight()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var br = rect.BottomRight;
            Assert.AreEqual(br.X, right);
            Assert.AreEqual(br.Y, bottom);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(br);
        }

        [TestMethod]
        public void CenteredRect1()
        {
            const int x = 50;
            const int y = 60;
            const int width = 20;
            const int height = 30;

            var result = Rectangle.CenteredRect(x, y, width, height);

            var expected = new Rectangle();
            expected.Left = x - (width / 2);
            expected.Top = y - (height / 2);
            expected.Right = expected.Left + width - 1;
            expected.Bottom = expected.Top + height - 1;

            Assert.AreEqual(result.Left, expected.Left);
            Assert.AreEqual(result.Top, expected.Top);
            Assert.AreEqual(result.Right, expected.Right);
            Assert.AreEqual(result.Bottom, expected.Bottom);

            this.DisposeAndCheckDisposedState(result);
            this.DisposeAndCheckDisposedState(expected);
        }

        [TestMethod]
        public void CenteredRect2()
        {
            const int x = 50;
            const int y = 60;
            var p = new Point(x, y);
            const int width = 20;
            const int height = 30;

            var result = Rectangle.CenteredRect(p, width, height);

            var expected = new Rectangle();
            expected.Left = x - (width / 2);
            expected.Top = y - (height / 2);
            expected.Right = expected.Left + width - 1;
            expected.Bottom = expected.Top + height - 1;

            Assert.AreEqual(result.Left, expected.Left);
            Assert.AreEqual(result.Top, expected.Top);
            Assert.AreEqual(result.Right, expected.Right);
            Assert.AreEqual(result.Bottom, expected.Bottom);

            this.DisposeAndCheckDisposedState(p);
            this.DisposeAndCheckDisposedState(result);
            this.DisposeAndCheckDisposedState(expected);
        }

        [TestMethod]
        public void CenteredRect3()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            var rect = new Rectangle(left, top, right, bottom);

            const int width = 20;
            const int height = 30;

            var result = Rectangle.CenteredRect(rect, width, height);

            var expected = new Rectangle();
            expected.Left = ((left + right) / 2) - (width / 2);
            expected.Top = ((top + bottom) / 2) - (height / 2);
            expected.Right = expected.Left + width - 1;
            expected.Bottom = expected.Top + height - 1;

            Assert.AreEqual(result.Left, expected.Left);
            Assert.AreEqual(result.Top, expected.Top);
            Assert.AreEqual(result.Right, expected.Right);
            Assert.AreEqual(result.Bottom, expected.Bottom);

            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(result);
            this.DisposeAndCheckDisposedState(expected);
        }

        [TestMethod]
        public void Contains1()
        {
            var left = 0;
            var top = 0;
            var right = 100;
            var bottom = 100;
            var x = this.NextRandom(0, 100);
            var y = this.NextRandom(0, 100);
            var p = new Point(x, y);
            var rect = new Rectangle(left, top, right, bottom);
            Assert.IsTrue(rect.Contains(p));
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Contains2()
        {
            var left = 0;
            var top = 0;
            var right = 100;
            var bottom = 100;
            var x = this.NextRandom(0, 100);
            var y = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            Assert.IsTrue(rect.Contains(x, y));
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Intersect()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            var rect = new Rectangle(left, top, right, bottom);

            const int left2 = 10;
            const int top2 = 10;
            const int right2 = 30;
            const int bottom2 = 50;
            var rect2 = new Rectangle(left2, top2, right2, bottom2);

            var result = rect.Intersect(rect2);

            var left3 = Math.Max(rect.Left, rect2.Left);
            var top3 = Math.Max(rect.Top, rect2.Top);
            var right3 = Math.Min(rect.Right, rect2.Right);
            var bottom3 = Math.Min(rect.Bottom, rect2.Bottom);
            var expected = new Rectangle(left3, top3, right3, bottom3);

            Assert.AreEqual(result.Left, expected.Left);
            Assert.AreEqual(result.Top, expected.Top);
            Assert.AreEqual(result.Right, expected.Right);
            Assert.AreEqual(result.Bottom, expected.Bottom);

            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(rect2);
            this.DisposeAndCheckDisposedState(expected);
        }

        [TestMethod]
        public void Center()
        {
            var left = 0;
            var top = 0;
            var right = 59;
            var bottom = 99;
            var x = 30;
            var y = 50;
            var rect = new Rectangle(left, top, right, bottom);
            var center = rect.Center;
            Assert.AreEqual(center.X, x);
            Assert.AreEqual(center.Y, y);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(center);
        }

        [TestMethod]
        public void DCenter()
        {
            const int left = 0;
            const int top = 0;
            const int right = 61;
            const int bottom = 101;
            const double x = 30.5d;
            const double y = 50.5d;
            Rectangle rect = new Rectangle(left, top, right, bottom);
            var center = rect.DCenter;
            Assert.AreEqual(center.X, x);
            Assert.AreEqual(center.Y, y);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(center);
        }

        [TestMethod]
        public void Translate()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = rect.Translate(point);

            Assert.AreEqual(ret.Left, left + x);
            Assert.AreEqual(ret.Top, top + y);
            Assert.AreEqual(ret.Right, right + x);
            Assert.AreEqual(ret.Bottom, bottom + y);

            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(point);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void TranslateStatic()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = Rectangle.Translate(rect, point);

            Assert.AreEqual(ret.Left, left + x);
            Assert.AreEqual(ret.Top, top + y);
            Assert.AreEqual(ret.Right, right + x);
            Assert.AreEqual(ret.Bottom, bottom + y);

            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(point);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void TranslateXY()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);

            var ret = rect.Translate(x, y);

            Assert.AreEqual(ret.Left, left + x);
            Assert.AreEqual(ret.Top, top + y);
            Assert.AreEqual(ret.Right, right + x);
            Assert.AreEqual(ret.Bottom, bottom + y);

            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void TranslateStaticXY()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = Rectangle.Translate(rect, x, y);

            Assert.AreEqual(ret.Left, left + x);
            Assert.AreEqual(ret.Top, top + y);
            Assert.AreEqual(ret.Right, right + x);
            Assert.AreEqual(ret.Bottom, bottom + y);

            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(point);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void OperatorAdd()
        {
            var lleft = this.NextRandom(0, 100);
            var ltop = this.NextRandom(0, 100);
            var lright = lleft * 2;
            var lbottom = ltop * 2;
            var rleft = this.NextRandom(0, 100);
            var rtop = this.NextRandom(0, 100);
            var rright = rleft * 2;
            var rbottom = rtop * 2;

            var r = new Rectangle(lleft, ltop, lright, lbottom);
            var l = new Rectangle(rleft, rtop, rright, rbottom);
            var rl = r + l;

            Assert.AreEqual(rl.Left, Math.Min(l.Left, r.Left));
            Assert.AreEqual(rl.Top, Math.Min(l.Top, r.Top));
            Assert.AreEqual(rl.Right, Math.Max(l.Right, r.Right));
            Assert.AreEqual(rl.Bottom, Math.Max(l.Bottom, r.Bottom));

            this.DisposeAndCheckDisposedState(rl);
            this.DisposeAndCheckDisposedState(l);
            this.DisposeAndCheckDisposedState(r);
        }

        [TestMethod]
        public void OperatorEqual()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = left * 2;
            var bottom = top * 2;

            var r = new Rectangle(left, top, right, bottom);
            var l = new Rectangle(left, top, right, bottom);
            var l1 = new Rectangle(left, top, right * 2, bottom);
            var l2 = new Rectangle(left, top, right, bottom * 2);
            var l3 = new Rectangle(left * 2, top, right, bottom);
            var l4 = new Rectangle(left, top * 2, right, bottom);

            Assert.IsTrue(r == l);
            Assert.IsTrue(r != l1);
            Assert.IsTrue(r != l2);
            Assert.IsTrue(r != l3);
            Assert.IsTrue(r != l4);

            this.DisposeAndCheckDisposedState(l);
            this.DisposeAndCheckDisposedState(r);
            this.DisposeAndCheckDisposedState(l1);
            this.DisposeAndCheckDisposedState(l2);
            this.DisposeAndCheckDisposedState(l3);
            this.DisposeAndCheckDisposedState(l4);
        }

    }
}
