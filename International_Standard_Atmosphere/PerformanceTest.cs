using System;
using System.Diagnostics;

namespace ISA
{
	public class PerformanceTest
	{
        private float[] GenerateRandomSet(int sampleSize)
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

		public TimeSpan Test(Delegate method, int sampleSize)
		{
            TimeSpan total = TimeSpan.Zero;
            int loop = 1000;

            for (int j = 0; j < loop; j++)
            {
                var timer = new Stopwatch();

                var h = GenerateRandomSet(sampleSize);

                timer.Start();
                for (int i = 0; i < h.Length; i++)
                {
                    method.DynamicInvoke(h[i], 101325, 1.225f, 288.15f);
                }
                timer.Stop();

                total = total + timer.Elapsed;
            }

            total = total / loop;

            return total;
        }
	}
}

