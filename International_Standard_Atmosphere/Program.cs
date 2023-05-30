using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;

namespace ISA
{
    public static class InternationalStandardAtmosphere
    {
        static private void Main()
        {
            int testSize = 1000;
            (TimeSpan totalextime, TimeSpan extime) = PerformanceTest.Test(testSize);
            Console.WriteLine($"{testSize} result was calculated in {totalextime.TotalSeconds}s");
            Console.WriteLine($"it took {extime} per process");

            while (true)
            {
                ConsoleRead();
            }
        }

        private static void ConsoleRead()
        {
            Console.WriteLine("enter height in meters");
            var stringHeight = Console.ReadLine();
            
            if(stringHeight != null && stringHeight.Length > 0)
            {
                float h;
                bool success = float.TryParse(stringHeight, out h);
                if (success)
                {
                    var (Pf, Df, Tf) = Algorithm.isa(h);
                    Console.WriteLine($"height =>      {h}m");
                    Console.WriteLine($"temperature => {Tf}K");
                    Console.WriteLine($"density =>     {Df}kg/m3");
                    Console.WriteLine($"pressure =>    {Pf}Pa");
                }
                else
                {
                    Console.WriteLine("can't contain leters or spaces");
                }
            }
            else
            {
                Console.WriteLine("can't be null");
            }
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------");
            Console.WriteLine("\n");
        }
    }
}

