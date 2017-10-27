using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Geometry
{

    [TestClass]
    public class DRectangleTest : TestBase
    {

        [TestMethod]
        public void Create1()
        {
            var rect = new DRectangle();
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Create2()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var ret = new DRectangle(rect);
            Assert.AreEqual(rect.Left, left);
            Assert.AreEqual(rect.Top, top);
            Assert.AreEqual(rect.Right, right);
            Assert.AreEqual(rect.Bottom, bottom);
            Assert.AreEqual(ret.Left, left);
            Assert.AreEqual(ret.Top, top);
            Assert.AreEqual(ret.Right, right);
            Assert.AreEqual(ret.Bottom, bottom);
            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Create3()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            Assert.AreEqual(rect.Left, left);
            Assert.AreEqual(rect.Top, top);
            Assert.AreEqual(rect.Right, right);
            Assert.AreEqual(rect.Bottom, bottom);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Create4()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var p1 = new DPoint(left, top);
            var p2 = new DPoint(right, bottom);
            var rect = new DRectangle(p1, p2);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(p1);
            this.DisposeAndCheckDisposedState(p2);
        }

        [TestMethod]
        public void Create5()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var p = new DPoint(left, top);
            var rect = new DRectangle(p);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(p);
        }

        [TestMethod]
        public void IsEmpty()
        {
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var left = right * 2;
            var top = bottom * 2;
            var rect1 = new DRectangle(left, top, right, bottom);
            Assert.IsTrue(rect1.IsEmpty);
            this.DisposeAndCheckDisposedState(rect1);

            left = this.NextRandom(1, 100);
            top = this.NextRandom(1, 100);
            right = left * 2;
            bottom = top * 2;
            var rect2 = new DRectangle(left, top, right, bottom);
            Assert.IsFalse(rect2.IsEmpty);
            this.DisposeAndCheckDisposedState(rect2);
        }

        [TestMethod]
        public void Cast()
        {
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var left = right * 2;
            var top = bottom * 2;
            var drect = new DRectangle(left, top, right, bottom);
            var rect = (Rectangle)drect;
            Assert.AreEqual(rect.Left, Math.Floor(drect.Left + 0.5d));
            Assert.AreEqual(rect.Top, Math.Floor(drect.Top + 0.5d));
            Assert.AreEqual(rect.Right, Math.Floor(drect.Right + 0.5d));
            Assert.AreEqual(rect.Bottom, Math.Floor(drect.Bottom + 0.5d));
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(drect);
        }

        [TestMethod]
        public void TopLeft()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var tl = rect.TopLeft;
            Assert.AreEqual(tl.X, left);
            Assert.AreEqual(tl.Y, top);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(tl);
        }

        [TestMethod]
        public void TopRight()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var tr = rect.TopRight;
            Assert.AreEqual(tr.X, right);
            Assert.AreEqual(tr.Y, top);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(tr);
        }

        [TestMethod]
        public void BottomLeft()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var bl = rect.BottomLeft;
            Assert.AreEqual(bl.X, left);
            Assert.AreEqual(bl.Y, bottom);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(bl);
        }

        [TestMethod]
        public void BottomRight()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var br = rect.BottomRight;
            Assert.AreEqual(br.X, right);
            Assert.AreEqual(br.Y, bottom);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(br);
        }

        [TestMethod]
        public void Width()
        {
            var width = (double)this.NextRandom(1, 100);
            var height = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(0, 0, width, height);
            Assert.AreEqual(rect.Width, width + 1);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Height()
        {
            var width = (double)this.NextRandom(1, 100);
            var height = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(0, 0, width, height);
            Assert.AreEqual(rect.Height, height + 1);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Area()
        {
            var width = (double)this.NextRandom(1, 100);
            var height = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(0, 0, width, height);
            Assert.AreEqual(rect.Area, (width + 1) * (height + 1));
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Intersect()
        {
            const double left = 0;
            const double top = 0;
            const double right = 100;
            const double bottom = 100;
            var rect = new DRectangle(left, top, right, bottom);

            const double left2 = 10;
            const double top2 = 10;
            const double right2 = 30;
            const double bottom2 = 50;
            var rect2 = new DRectangle(left2, top2, right2, bottom2);

            var result = rect.Intersect(rect2);

            var left3 = Math.Max(rect.Left, rect2.Left);
            var top3 = Math.Max(rect.Top, rect2.Top);
            var right3 = Math.Min(rect.Right, rect2.Right);
            var bottom3 = Math.Min(rect.Bottom, rect2.Bottom);
            var expected = new DRectangle(left3, top3, right3, bottom3);

            Assert.AreEqual(result.Left, expected.Left);
            Assert.AreEqual(result.Top, expected.Top);
            Assert.AreEqual(result.Right, expected.Right);
            Assert.AreEqual(result.Bottom, expected.Bottom);

            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(rect2);
            this.DisposeAndCheckDisposedState(expected);
        }

        [TestMethod]
        public void Contains1()
        {
            var left = 0;
            var top = 0;
            var right = 100;
            var bottom = 100;
            var x = (double)this.NextRandom(1, 100);
            var y = (double)this.NextRandom(1, 100);
            var p = new DPoint(x, y);
            var rect = new DRectangle(left, top, right, bottom);
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
            var rect = new DRectangle(left, top, right, bottom);
            var rhs = new DRectangle(10, 10, 30, 30);
            Assert.IsTrue(rect.Contains(rhs));
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void Center()
        {
            var left = 0;
            var top = 0;
            var right = 59;
            var bottom = 99;
            var x = 29.5;
            var y = 49.5;
            var rect = new DRectangle(left, top, right, bottom);
            var center = rect.Center;
            Assert.AreEqual(center.X, x);
            Assert.AreEqual(center.Y, y);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(center);
        }

        [TestMethod]
        public void DCenter()
        {
            const double left = 0;
            const double top = 0;
            const double right = 61;
            const double bottom = 101;
            const double x = 30.5d;
            const double y = 50.5d;
            DRectangle rect = new DRectangle(left, top, right, bottom);
            var center = rect.DCenter;
            Assert.AreEqual(center.X, x);
            Assert.AreEqual(center.Y, y);
            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(center);
        }

        [TestMethod]
        public void CenteredRect()
        {
            const int x = 50;
            const int y = 60;
            var p = new DPoint(x, y);
            double width = 20;
            double height = 30;

            var result = DRectangle.CenteredRect(p, width, height);

            width--;
            height--;

            var eLeft = x - width / 2;
            var eTop = y - height / 2;
            var eRight = eLeft + width - 1;
            var eBottom = eTop + height - 1;

            Assert.AreEqual(result.Left, eLeft);
            Assert.AreEqual(result.Top, eTop);
            Assert.AreEqual(result.Right, eRight + 1);
            Assert.AreEqual(result.Bottom, eBottom + 1);

            this.DisposeAndCheckDisposedState(p);
            this.DisposeAndCheckDisposedState(result);
        }

        [TestMethod]
        public void CenteredRect2()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            var rect = new DRectangle(left, top, right, bottom);

            double width = 20;
            double height = 30;

            var result = DRectangle.CenteredRect(rect, width, height);

            width--;
            height--;

            var expected = new DRectangle();
            var eLeft = ((left + right) / 2) - width / 2;
            var eTop = ((top + bottom) / 2) - height / 2;
            var eRight = eLeft + width - 1;
            var eBottom = eTop + height - 1;

            Assert.AreEqual(result.Left, eLeft);
            Assert.AreEqual(result.Top, eTop);
            Assert.AreEqual(result.Right, eRight + 1);
            Assert.AreEqual(result.Bottom, eBottom + 1);

            this.DisposeAndCheckDisposedState(rect);
            this.DisposeAndCheckDisposedState(result);
            this.DisposeAndCheckDisposedState(expected);
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
            var rect = new DRectangle(left, top, right, bottom);
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
            var rect = new DRectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = DRectangle.Translate(rect, point);

            Assert.AreEqual(ret.Left, left + x);
            Assert.AreEqual(ret.Top, top + y);
            Assert.AreEqual(ret.Right, right + x);
            Assert.AreEqual(ret.Bottom, bottom + y);

            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(point);
            this.DisposeAndCheckDisposedState(rect);
        }

        [TestMethod]
        public void TranslateD()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new DRectangle(left, top, right, bottom);
            var point = new DPoint(x, y);

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
        public void TranslateStaticD()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new DRectangle(left, top, right, bottom);
            var point = new DPoint(x, y);

            var ret = DRectangle.Translate(rect, point);

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
            var lleft = (double)this.NextRandom(0, 100);
            var ltop = (double)this.NextRandom(0, 100);
            var lright = lleft * 2;
            var lbottom = ltop * 2;
            var rleft = (double)this.NextRandom(0, 100);
            var rtop = (double)this.NextRandom(0, 100);
            var rright = rleft * 2;
            var rbottom = rtop * 2;

            var r = new DRectangle(lleft, ltop, lright, lbottom);
            var l = new DRectangle(rleft, rtop, rright, rbottom);
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
            var left = (double)this.NextRandom(0, 100);
            var top = (double)this.NextRandom(0, 100);
            var right = left * 2;
            var bottom = top * 2;

            var r = new DRectangle(left, top, right, bottom);
            var l0 = new DRectangle(left, top, right, bottom);
            var l1 = new DRectangle(left, top, right * 2, bottom);
            var l2 = new DRectangle(left, top, right, bottom * 2);
            var l3 = new DRectangle(left * 2, top, right, bottom);
            var l4 = new DRectangle(left, top * 2, right, bottom);

            Assert.IsTrue(r == l0, $"1 - RWidth: {r.Width}, RHeight: {r.Height}\nLWidth: {l0.Width}, LHeight: {l0.Height}");
            Assert.IsTrue(r != l1, $"2 - RWidth: {r.Width}, RHeight: {r.Height}\nLWidth: {l1.Width}, LHeight: {l1.Height}");
            Assert.IsTrue(r != l2, $"3 - RWidth: {r.Width}, RHeight: {r.Height}\nLWidth: {l2.Width}, LHeight: {l2.Height}");
            Assert.IsTrue(r != l1, $"4 - RWidth: {r.Width}, RHeight: {r.Height}\nLWidth: {l3.Width}, LHeight: {l3.Height}");
            Assert.IsTrue(r != l2, $"5 - RWidth: {r.Width}, RHeight: {r.Height}\nLWidth: {l4.Width}, LHeight: {l4.Height}");

            this.DisposeAndCheckDisposedState(l0);
            this.DisposeAndCheckDisposedState(r);
            this.DisposeAndCheckDisposedState(l1);
            this.DisposeAndCheckDisposedState(l2);
            this.DisposeAndCheckDisposedState(l3);
            this.DisposeAndCheckDisposedState(l4);
        }

    }
}
