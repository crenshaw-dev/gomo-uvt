using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using UniqueViewTime;

namespace UniqueViewTimeTest
{
    [TestClass]
    public class ViewTimeFrameTest
    {
        /// <summary>
        /// Make sure the time frame constructor throws an exception if the start time is
        /// after the end time.
        /// </summary>
        [TestMethod]
        public void InstantiateInvalidFrame_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ViewTimeFrame(100, 0)
            );
        }

        /// <summary>
        /// Make sure frames with the same start and end times are considered equal.
        /// </summary>
        [TestMethod]
        public void CompareEqualFrames()
        {
            var x = new ViewTimeFrame(100, 500);
            var y = new ViewTimeFrame(100, 500);

            Assert.AreEqual(x, y);
        }

        /// <summary>
        /// Make sure attempting to get the union of two disjoint frames throws an exception.
        /// </summary>
        [TestMethod]
        public void AddDisjointFrames_ThrowsArgumentException()
        {
            var x = new ViewTimeFrame(0, 100);
            var y = new ViewTimeFrame(200, 300);

            Assert.ThrowsException<ArgumentException>(() =>
                x.Union(y)
            );
        }

        /// <summary>
        /// Make sure the union of adjacent frames is correct.
        /// </summary>
        [TestMethod]
        public void AddAdjacentFrames()
        {
            var x = new ViewTimeFrame(0, 100);
            var y = new ViewTimeFrame(100, 200);

            Assert.AreEqual(x.Union(y), new ViewTimeFrame(0, 200));
        }

        /// <summary>
        /// Make sure the union of overlapping frames is correct.
        /// </summary>
        [TestMethod]
        public void AddOverlappingFrames()
        {
            var x = new ViewTimeFrame(0, 100);
            var y = new ViewTimeFrame(50, 150);

            Assert.AreEqual(x.Union(y), new ViewTimeFrame(0, 150));
        }

        /// <summary>
        /// Make sure taking the union of one frame and a frame completely contained within 
        /// the first frame is equal to the first frame.
        /// </summary>
        public void AddFrameWithinFrame()
        {
            var x = new ViewTimeFrame(0, 300);
            var y = new ViewTimeFrame(100, 200);

            Assert.AreEqual(x.Union(y), new ViewTimeFrame(0, 300));
        }

        /// <summary>
        /// Make sure the union operation is commutative, i.e. A + B = B + A where + is the union
        /// operation.
        /// </summary>
        [TestMethod]
        public void UnionCommutative()
        {
            var x = new ViewTimeFrame(0, 100);
            var y = new ViewTimeFrame(50, 150);

            Assert.AreEqual(x.Union(y), y.Union(x));
        }
    }
}
