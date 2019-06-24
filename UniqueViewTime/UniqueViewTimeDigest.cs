using System.Collections.Generic;
using System.Linq;

namespace UniqueViewTime
{
    /// <summary>
    /// Takes information about segments of continuous playback (ViewTimeFrames) and calculates 
    /// the amount of time, in milliseconds, that was watched at least once.
    /// </summary>
    public partial class UniqueViewTimeDigest
    {
        /// <summary>
        /// Build a digest with the given enumerable of frames.
        /// </summary>
        /// <param name="frames">
        /// Frames representing periods of continuous playback in a single view.
        /// </param>
        public UniqueViewTimeDigest(IEnumerable<ViewTimeFrame> frames)
        {
            foreach (ViewTimeFrame frame in frames)
            {
                Add(frame);
            }
        }

        /// <summary>
        /// Set of segments of video that were watched at least once.
        /// </summary>
        private readonly HashSet<ViewTimeFrame> Frames = new HashSet<ViewTimeFrame>();

        /// <summary>
        /// Add a timeframe to the digest.
        /// </summary>
        public void Add(ViewTimeFrame newFrame)
        {
            if (!Frames.Any(f => f.Touches(newFrame)))
            {
                // If the new frame doesn't touch a frame already in the digest, simply add it to
                // the set. No need to combine it with any others.
                Frames.Add(newFrame);
            }
            else
            {
                // Since the new frame touches an existing one, find the first existing frame it
                // touches...
                var touchingFrame = Frames.First(f => f.Touches(newFrame));

                // Then replace the touching frame with the union of the new and old frame.
                Replace(touchingFrame, touchingFrame.Union(newFrame));
            }
        }

        /// <summary>
        /// Replace the given frame in the digest with a new frame.
        /// </summary>
        /// <param name="oldFrame">Frame to replace.</param>
        /// <param name="newFrame">Replacement frame.</param>
        public void Replace(ViewTimeFrame oldFrame, ViewTimeFrame newFrame)
        {
            Frames.Remove(oldFrame);

            // Use Add instead of Frames.Add, because it may be necessary to combine touching 
            // frames.
            Add(newFrame);
        }

        /// <summary>
        /// Unique View Tim,  the number of milliseconds of a video viewed at least once.
        /// </summary>
        public uint Uvt
        {
            get
            {
                uint length = 0;

                foreach (ViewTimeFrame frame in Frames)
                {
                    length += frame.Length;
                }

                return length;
            }
        }
    }
}
