using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3Man
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-2.1 * 123 = " + multiplyFP(-2.1f, 123f) + "("+(-2.1 * 123) + ")");
            Console.ReadKey();
        }

        static int getFloatingPointBinary(float f)
        {
            byte[] bytes = BitConverter.GetBytes(f);
            int binary = 0;
            binary |= bytes[0];
            binary |= bytes[1] << 8;
            binary |= bytes[2] << 16;
            binary |= bytes[3] << 24;
            return binary;
        }

        static float multiplyFP(float multiplicandParam, float multiplierParam)
        {
            long multiplicand = getFloatingPointBinary(multiplicandParam);
            long multiplier = getFloatingPointBinary(multiplierParam);

            int sign1 = (int)((multiplicand >> 31) & 1);
            int sign2 = (int)((multiplier >> 31) & 1);

            int exponent1 = (int)((multiplicand >> 23) & 255);
            int exponent2 = (int)((multiplier >> 23) & 255);

            long mantissa1 = multiplicand & 8388607;
            long mantissa2 = multiplier & 8388607;

            var mantissa = ((1 << 23) | mantissa1) * ((1 << 23) | mantissa2);
            Console.WriteLine("mantissa = mantissa1(" + FinishStringWithZeros(Convert.ToString(mantissa1, 2), 23) + ") * mantissa2(" + FinishStringWithZeros(Convert.ToString(mantissa2, 2), 23) + ")");
            mantissa >>= 23;
            var normalizationBit = ((1 << 24) & mantissa) > 0 ? 1 : 0;
            if (normalizationBit > 0)
            {
                Console.WriteLine("Normalization");
                mantissa >>= 1;
                mantissa &= ~(1 << 23);
            }
            else
            {
                Console.WriteLine("No normalization");
                mantissa &= ~(3 << 23);
            }
            Console.WriteLine("mantissa = " + FinishStringWithZeros(Convert.ToString(mantissa, 2), 23));
            var sign = (sign1 + sign2) & 1;
            Console.WriteLine("sign(" + sign + ") = sign1(" + sign1 + ") xor sign2(" + sign2 + ")");
            var exponent = exponent1 + exponent2 - 127 + normalizationBit;
            Console.WriteLine("exponent(" + FinishStringWithZeros(Convert.ToString(exponent, 2), 8) + ") = exponent1(" + FinishStringWithZeros(Convert.ToString(exponent1, 2), 8) + ") + exponent2(" + Convert.ToString(exponent2, 2) + ") - 127 + normalizationBit(" + Convert.ToString(normalizationBit, 2) + ")");

            int result = (int)mantissa;
            result |= exponent << 23;
            result |= sign << 31;

            Console.WriteLine(Convert.ToString(result, 2));

            byte[] bytes = new byte[4];
            bytes[0] = (byte)(result & 255);
            bytes[1] = (byte)((result >> 8) & 255);
            bytes[2] = (byte)((result >> 16) & 255);
            bytes[3] = (byte)((result >> 24) & 255);

            return BitConverter.ToSingle(bytes, 0);
        }

        static string FinishStringWithZeros(string val, int bits)
        {
            int count = bits - val.Length;
            string head = "";
            for (int i = 0; i < count; ++i)
                head += "0";
            return head + val;
        }
    }
}
