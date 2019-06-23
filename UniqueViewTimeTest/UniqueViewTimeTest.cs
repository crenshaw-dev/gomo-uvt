using Microsoft.VisualStudio.TestTools.UnitTesting;

using UniqueViewTime;

namespace UniqueViewTimeTest
{
    [TestClass]
    public class UniqueViewTimeTest
    {
        /// <summary>
        /// Test passing no frames. It should return 0 UVT.
        /// </summary>
        [TestMethod]
        public void NoFrames()
        {
            AssertFramesProduceUvt(0);
        }

        /// <summary>
        /// Test passing just one frame. The UVT should be the same length as the frame.
        /// </summary>
        [TestMethod]
        public void OneFrame()
        {
            AssertFramesProduceUvt(
                1000, 
                new ViewTimeFrame(0, 1000)
            );
        }

        /// <summary>
        /// Test passing overlapping frames.
        /// </summary>
        [TestMethod]
        public void OverlappingFrames()
        {
            AssertFramesProduceUvt(
                400,
                new ViewTimeFrame(0, 200),
                new ViewTimeFrame(100, 300),
                new ViewTimeFrame(200, 400)
            );
        }

        /// <summary>
        /// Test passing frames that don't touch each other.
        /// </summary>
        [TestMethod]
        public void DisjointFrames()
        {
            AssertFramesProduceUvt(
                200,
                new ViewTimeFrame(0, 100),
                new ViewTimeFrame(300, 400)
            );
        }

        /// <summary>
        /// Check the given expected UVT against the UVT calculated from the given frames.
        /// </summary>
        /// <param name="expected">The expected UVT.</param>
        /// <param name="frames">The frames from which to calculate the UVT.</param>
        private void AssertFramesProduceUvt(uint expected, params ViewTimeFrame[] frames)
        {
            Assert.AreEqual(expected, new UniqueViewTimeDigest(frames).Uvt);
        }
    }
}
