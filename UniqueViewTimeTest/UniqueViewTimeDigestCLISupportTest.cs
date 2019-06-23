using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using UniqueViewTime;

namespace UniqueViewTimeTest
{
    [TestClass]
    public class UniqueViewTimeDigestCLISupportTest
    {
        /// <summary>
        /// Test a trivial example of valid input.
        /// </summary>
        [TestMethod]
        public void OverlappingFrames()
        {
            Assert.AreEqual(
                (uint)400,
                UniqueViewTimeDigest.FromStartEndPairs(new string[]
                {
                    "0-100",
                    "50-150",
                    "300-550",
                }).Uvt
            );
        }

        /// <summary>
        /// Test passing invalid input, i.e. a frame where the end time is after the start
        /// time.
        /// </summary>
        [TestMethod]
        public void InvalidFrames_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                UniqueViewTimeDigest.FromStartEndPairs(new string[]
                {
                    "0-150",
                    "150-0",
                })
            );
        }
    }
}
