using System;
using System.Diagnostics;

namespace ISA
{
	public static class PerformanceTest
	{
        private static float[] GenerateRandomSet(int sampleSize)
        {
            float[] heights = new float[sampleSize];
            var rand = new Random();

            for (int i = 0; i < heights.Length; i++)
            {
                //90000 to get a value between 0 and 90k
                //90k is the program max input value
                heights[i] = (float)rand.NextDouble() * 90000;
            }

            return heights;
        }

		public static (TimeSpan, TimeSpan) Test(int sampleSize)
		{
            TimeSpan total = TimeSpan.Zero;

            for (int j = 0; j < sampleSize; j++)
            {
                var timer = new Stopwatch();

                var h = GenerateRandomSet(sampleSize);

                timer.Start();
                for (int i = 0; i < h.Length; i++)
                {
                    Algorithm.isa(h[i]);
                }
                timer.Stop();

                total = total + timer.Elapsed;
            }

            return (total, total/sampleSize);
        }
	}
}

