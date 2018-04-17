using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothPart1d
{
    class Program
    {
        static Int64 Algorithm(int multiplicand, int multiplier)
        {
                int bit = 0, s = 0;
                Int64 A, S, P;
                s = -multiplicand;
                A = ((Int64)multiplicand) << 32;
                S = ((Int64)s) << 32;
                //11111111111111111111111111111111 = 4294967295
                P = (Int64)multiplier & 4294967295;
                string lastbit;
                Console.WriteLine("A   " + (A > 0 ? (new string('0', 64 - Convert.ToString(A, 2).Length) + Convert.ToString(A, 2))
                    : Convert.ToString(A, 2)) + " " + bit.ToString() + Environment.NewLine);
                Console.WriteLine("S   " + (S > 0 ? (new string('0', 64 - Convert.ToString(S, 2).Length) + Convert.ToString(S, 2))
                    : Convert.ToString(S, 2)) + " " + bit.ToString() + Environment.NewLine);
                Console.WriteLine("P   " + (P > 0 ? (new string('0', 64 - Convert.ToString(P, 2).Length) + Convert.ToString(P, 2))
                    : Convert.ToString(P, 2)) + " " + bit.ToString() + Environment.NewLine);
                for (int i = 0; i < 32; i++)
                {
                    lastbit = Convert.ToString(P, 2).Last().ToString();
                    P += lastbit + bit.ToString() == "10" ? S : (lastbit + bit == "01") ? A : 0;
                    bit = int.Parse(lastbit);
                    P = P >> 1;
                    Console.WriteLine("P " + (i + 1) + " \n" + (P >= 0 ?
                      (new string('0', 64 - Convert.ToString(P, 2).Length) + Convert.ToString(P, 2))
                      : Convert.ToString(P, 2)) + " " + bit.ToString() + Environment.NewLine);
                }
                return P;
            }
            static void Main(string[] args)
            {
                Console.WriteLine("Enter multiplicand");
                int a = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter multiplier");
                int b = int.Parse(Console.ReadLine());

                Console.WriteLine("PRODUCT: \n" + Algorithm(a, b));
                Console.ReadLine();
            }
        }
    }
