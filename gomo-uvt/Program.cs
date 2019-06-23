using System;

using UniqueViewTime;

namespace gomo_uvt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a UVT digest from the start-end pairs passed as command line arguments.
            var digest = UniqueViewTimeDigest.FromStartEndPairs(args);

            // Write the calculated UVT.
            Console.WriteLine(digest.Uvt);
        }
    }
}
