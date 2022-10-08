using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;

namespace ISA
{
    public class InternationalStandardAtmosphere
    {
        //Temperature gradienin Kelvin per metre
        float[] a_val = { -0.0065f, 0, 0.0010f, 0.0028f, 0, -0.0028f, -0.0020f, 0 };

        //ISA at sea level
        float Pd = 101325;  //Pascals
        float Dd = 1.225f;   //Kg per cubic metre    
        float Td = 288.15f;  //Kelvin

        //Constants
        float g = 9.80665f;  //Acceleration due to gravity   
        float R = 287.0f;    //Molar gas constant for air
        float e = 2.71828f;  //Euler's constant

        //Altitudes
        float[] alt = { 11000, 20000, 32000, 47000, 51000, 71000, 84000, 90000, 0 };

        //variables
        float[] final = { 0, 0, 0 };   //Pressure final, Temperature final, Density final

        int counter = 0;

        static private void Main()
        {
            InternationalStandardAtmosphere isa = new InternationalStandardAtmosphere();
            while (true)
            {
                isa.ConsoleRead();
                Console.WriteLine("\n");
                Console.WriteLine("-------------------------");
                Console.WriteLine("\n");
            }

            //Speed Test
            //Console.WriteLine(isa.SpeedTest().ToString(@"ss\.fffffff"););
        }

        private void sphere(float h, float Pi, float Di, float Ti)
        {
            float x;
            if (h > alt[counter])
            {
                x = alt[counter];
            }
            else
            {
                x = h;
            }
            float a = a_val[counter];
            float exp = -g / (R * (a));
            float Tf;
            if(counter == 0)
            {
                Tf = Ti + a * (x - alt[alt.Length - 1]);
            }
            else
            {
                Tf = Ti + a * (x - alt[counter - 1]);
            }
            
            float Pf = Pi * MathF.Pow(Tf / Ti, exp);
            float Df = Pf / (R * Tf);

            if (h > alt[alt.Length - 2])
            {
                Console.WriteLine("Altitude out of range");
            }
            else if (h > alt[counter])
            {
                if (counter == 0 || counter == 3 || counter == 6)
                {
                    counter += 1;
                    pause(h, Pf, Df, Tf);
                }
                else
                {
                    counter += 1;
                    sphere(h, Pf, Df, Tf);
                }
            }
            else
            {
                final[0] = Pf;
                final[1] = Df;
                final[2] = Tf;
            }
        }

        private void pause(float h, float Pi, float Di, float Ti)
        {
            float x;
            if (h > alt[counter])
            {
                x = alt[counter];
            }
            else
            {
                x = h;
            }
            float Tf = Ti;

            float exp;
            if (counter == 0)
            {
                exp = (-(g * (x - alt[alt.Length - 1])) / (R * Tf));
            }
            else
            {
                exp = (-(g * (x - alt[counter - 1])) / (R * Tf));
            }

            float Pf = Pi * MathF.Pow(e, exp);
            float Df = Pf / (R * Tf);
            if (h > alt[alt.Length - 2])
            {
                Console.WriteLine("Altitude out of range");
            }
            else if (h > alt[counter])
            {
                if (counter == 0 || counter == 3 || counter == 6)
                {
                    counter += 1;
                    pause(h, Pf, Df, Tf);
                }
                else
                {
                    counter += 1;
                    sphere(h, Pf, Df, Tf);
                }
            }
            else
            {
                final[0] = Pf;
                final[1] = Df;
                final[2] = Tf;
            }
        }

        private void ConsoleRead()
        {
            Console.WriteLine("enter height in meters");
            var stringHeight = Console.ReadLine();
            
            if(stringHeight != null && stringHeight.Length > 0)
            {
                float h;
                bool success = float.TryParse(stringHeight, out h);
                if (success)
                {
                    counter = 0;
                    sphere(h, Pd, Dd, Td);
                    Console.WriteLine($"height =>      {h}m");
                    Console.WriteLine($"temperature => {final[2]}K");
                    Console.WriteLine($"density =>     {final[1]}kg/m3");
                    Console.WriteLine($"pressure =>    {final[0]}Pa");
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
        }

        private TimeSpan SpeedTest()
        {
            TimeSpan total = TimeSpan.Zero;
            int loop = 1000;

            for (int j = 0; j < loop; j++)
            {
                var timer = new Stopwatch();

                float[] h = new float[1000000];
                var rand = new Random();

                for (int i = 0; i < h.Length; i++)
                {
                    h[i] = (float)rand.NextDouble() * 90000;
                }

                timer.Start();
                for (int i = 0; i < h.Length; i++)
                {
                    sphere(h[i], Pd, Dd, Td);
                }
                timer.Stop();

                total = total + timer.Elapsed;
            }

            total = total / loop;

            return total;
        }
    }

}

