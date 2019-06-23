using System;

namespace UniqueViewTime
{
    /// <summary>
    /// Representation of a segment of time in which a certain video was watched.
    /// </summary>
    public struct ViewTimeFrame
    {
        /// <summary>
        /// Create a new time frame from a start and end time.
        /// </summary>
        /// <param name="start">
        /// Beginning of frame, in milliseconds from the beginning of the video.
        /// </param>
        /// <param name="end">
        /// End of frame, in milliseconds from the beginning of the video. Must be greater than
        /// or equal to the start.
        /// </param>
        public ViewTimeFrame(uint start, uint end)
        {
            if (end < start)
            {
                throw new ArgumentException("Frame end time cannot be greater than start time");
            }

            Start = start;
            End = end;
        }

        /// <summary>
        /// Beginning of frame, in milliseconds from the beginning of the video.
        /// </summary>
        public uint Start;

        /// <summary>
        /// End of frame, in milliseconds from the beginning of the video.
        /// </summary>
        public uint End;

        /// <summary>
        /// Combine this frame with the given frame, returning a frame representing the 
        /// timespan covered by at least one of the two.
        /// </summary>
        /// <param name="other">The frame to combine with this one.</param>
        public ViewTimeFrame Union(ViewTimeFrame other)
        {
            // If this frame doesn't touch the other one, they can't be combined.
            if (!Touches(other))
            {
                throw new ArgumentException("Cannot combine frames that do not touch.");
            }
            
            // The lesser of the two start times and the greater of the two end times are the 
            // start and end of the combined frame.
            return new ViewTimeFrame(
                Math.Min(Start, other.Start),
                Math.Max(End, other.End)
            );
        }

        /// <summary>
        /// Get whether the given moment of time is in this timeframe. Being the same as the start 
        /// or the same as the end is considered being "contained" in this video.
        /// </summary>
        /// <param name="time">Milliseconds from the beginning of the video.</param>
        public bool Contains(uint time)
        {
            return Start <= time && End >= time;
        }

        /// <summary>
        /// Get whether this time frame touches the other. Touching includes overlapping or being
        /// adjacent (sharing a start or end time).
        /// </summary>
        public bool Touches(ViewTimeFrame other)
        {
            return Contains(other.Start) || other.Contains(Start);
        }

        /// <summary>
        /// The length of this time frame, in milliseconds.
        /// </summary>
        public uint Length => End - Start;
    }
}
