using System;
using System.Collections.Generic;
using System.IO;

namespace Base64
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

            int fileLength = 0;
            using (StreamReader r = new StreamReader(@"D:\6 семестр\КС\Lab1OlgaKolesnikova\Repka.txt.gz"))
            {
                string read = r.ReadToEnd();
                fileLength = read.Length;
            }

            Byte[] bytes;
            using (BinaryReader f1 = new BinaryReader(File.Open(@"D:\6 семестр\КС\Lab1OlgaKolesnikova\Repka.txt.gz", FileMode.Open)))
            {
                bytes = f1.ReadBytes(fileLength);
            }
            Console.WriteLine(Convert.ToBase64String(bytes));
            Console.WriteLine("************************************************");

            string output = "";
            int b;
            for (int i = 0; i < bytes.Length; i += 3)
            {
                b = (bytes[i] & 0xFC) >> 2;//умножили на 252, сдвиг на 2 разряда, получение первого 6-битового байта
                output += alphabet[b];//кодировка байта
                b = (bytes[i] & 0x03) << 4;//умножили на 3, оттянули влево на 4 разряда, получили 2 бита второго байта
                if (i + 1 < bytes.Length)
                {
                    b |= (bytes[i + 1] & 0xF0) >> 4;//получили из второго 8-битового байта 4 бита 6-битового байта, затем сложили с 2мя, полученными выше
                    output += alphabet[b];//кодировка байта
                    b = (bytes[i + 1] & 0x0F) << 2;//умножили на 15, оттянули влево на 2 разряда, получили 4 бита третьего байта
                    if (i + 2 < bytes.Length)
                    {
                        b |= (bytes[i + 2] & 0xC0) >> 6;//получили из третьего 8-битового байта 2 бита 6-битового байта, затем сложили с 4мя, полученными выше
                        output += alphabet[b];//кодировка байта
                        b = bytes[i + 2] & 0x3F;//умножаем на 63, тем самым получаем 6 бит четвертого 6-битового байта
                        output += alphabet[b];//кодировка байта
                    }
                    else//если в конце остается 2 байта, а не 3
                    {
                        output += alphabet[b];
                        output += '=';
                    }
                }
                else//если в конце остается 1 байт, а не 3
                {
                    output += alphabet[b];
                    output += "==";
                }
            }
            Console.WriteLine(output);
            using (StreamWriter wr = new StreamWriter(@"D:\6 семестр\КС\Lab1OlgaKolesnikova\Base64Repka.txt.gz", true))
            {
                wr.Write(Convert.ToBase64String(bytes));
            }
            Console.ReadKey();
        }
    }
}

