using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace UniqueViewTime
{
    /// <summary>
    /// Methods to support command-line input of view time info.
    /// </summary>
    public partial class UniqueViewTimeDigest
    {
        /// <summary>
        /// Construct a UVT digest from start-end time pairs as strings.
        /// </summary>
        public static UniqueViewTimeDigest FromStartEndPairs(string[] pairs)
        {
            // Convert the start-end time pairs to ViewTimeFrames to construct the UVT digest.
            return new UniqueViewTimeDigest(pairs.Select(p => StartEndPairToFrame(p)));
        }

        /// <summary>
        /// Regex matching start-end time pairs. The first matching group is the start time, and
        /// the second matching group is the end time.
        /// </summary>
        private static readonly Regex StartEndPairRegex = new Regex("^([0-9]+)-([0-9]+)$");

        /// <summary>
        /// Convert a start-end pair from a string to a ViewTimeFrame.
        /// </summary>
        private static ViewTimeFrame StartEndPairToFrame(string pair)
        {
            Match match = StartEndPairRegex.Match(pair);

            if (!match.Success)
            {
                throw new ArgumentException("The given start-end pair was incorrectly formatted.");
            }

            // The first matching group is the full match.
            // No need to TryParse, because regex validation ensures the input is valid uints.
            uint start = uint.Parse(match.Groups[1].Value);
            uint end = uint.Parse(match.Groups[2].Value);

            return new ViewTimeFrame(start, end);
        }
    }
}
