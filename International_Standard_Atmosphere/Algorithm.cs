using System;
namespace ISA
{
	public static class Algorithm
	{
        //Temperature gradienin Kelvin per metre
        static float[] a_val = { -0.0065f, 0, 0.0010f, 0.0028f, 0, -0.0028f, -0.0020f, 0 };

        //Constants
        static float g = 9.80665f;  //Acceleration due to gravity   
        static float R = 287.0f;    //Molar gas constant for air
        static float e = 2.71828f;  //Euler's constant

        //Altitudes Step
        static float[] alt = { 11000, 20000, 32000, 47000, 51000, 71000, 84000, 90000, 0 };

        // a wraper function to hide calculate's extra input and output
        public static (float, float, float) isa(float h, float Pi = 101325, float Di = 1.225f, float Ti = 288.15f)
        {
            var (Pf, Df, Tf, _) = calculate(h, Pi, Di, Ti);
            return (Pf, Df, Tf);
        }

        private static (float, float, float, int) calculate(float h, float Pi, float Di, float Ti, int counter = 0)
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

            if (counter == 0)
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
                return (0, 0, 0, counter);
            }
            else if (h > alt[counter])
            {
                if (counter == 0 || counter == 3 || counter == 6)
                {
                    counter += 1;
                    (Pf, Df, Tf, counter) = pause(h, Pf, Df, Tf, counter);
                }
                else
                {
                    counter += 1;
                    (Pf, Df, Tf, counter) = calculate(h, Pf, Df, Tf, counter);
                }
                return (Pf, Df, Tf, counter);
            }
            else
            {
                return (Pf, Df, Tf, counter);
            }
        }

        private static (float, float, float, int) pause(float h, float Pi, float Di, float Ti, int counter)
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
                return (0, 0, 0, counter);
            }
            else if (h > alt[counter])
            {
                if (counter == 0 || counter == 3 || counter == 6)
                {
                    counter += 1;
                    (Pf, Df, Tf, counter) = pause(h, Pf, Df, Tf, counter);
                }
                else
                {
                    counter += 1;
                    (Pf, Df, Tf, counter) = calculate(h, Pf, Df, Tf, counter);
                }
                return (Pf, Df, Tf, counter);
            }
            else
            {
                return (Pf, Df, Tf, counter);
            }
        }
    }
}

