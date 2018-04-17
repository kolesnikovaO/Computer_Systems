using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2div
{
    class Program
    {
        static void Main(string[] args)
        {
            Int64 divisor, remainderAndQuotient;
            try
            {
                Console.WriteLine("DIVIDENT");
                remainderAndQuotient = Int32.Parse(Console.ReadLine());
                Console.WriteLine("DIVISOR");
                divisor = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("");
                return;
            }
            divisor <<= 32;

            bool setRemLSBToOne = false;
            for (int i = 0; i <= 32; ++i)
            {
                Console.WriteLine( (i + 1) + ") \n");

                Console.Write("DIVISOR");
                if (divisor <= remainderAndQuotient)
                {
                    remainderAndQuotient -= divisor;
                    setRemLSBToOne = true;
                    Console.Write(" < ");
                }
                else
                    Console.Write(" > ");

                Console.WriteLine("REMAINDER");
                Console.WriteLine("Shift remainder left one bit.");

                remainderAndQuotient <<= 1;

                if (setRemLSBToOne)
                {
                    setRemLSBToOne = false;
                    remainderAndQuotient |= 1; //lsb - 1
                    Console.WriteLine("Set remainder lsb to 1");
                }
                Console.WriteLine();

                Console.WriteLine("DIVISOR:\n" + FinishStringWithZeros(Convert.ToString(divisor, 2)) +
                    "\nREMAINDER and QUOTIENT:\n" + FinishStringWithZeros(Convert.ToString(remainderAndQuotient, 2)) + "\n");

                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }
            long quo = remainderAndQuotient & ((long)Math.Pow(2, 33) - 1);
            long rem = remainderAndQuotient >>
                33;
            Console.WriteLine("QUOTIENT:\n" + FinishStringWithZeros(Convert.ToString(quo, 2)) +
                " ( " + quo + " )\n");

            Console.WriteLine("REMAINDER:\n" + FinishStringWithZeros(Convert.ToString(rem, 2)) +
               " ( " + rem + " )");
        }
        static string FinishStringWithZeros(string val)
        {
            int count = 64 - val.Length;
            string head = "";
            for (int i = 0; i < count; ++i)
                head += "0";
            return head + val;
        }
    }
}
