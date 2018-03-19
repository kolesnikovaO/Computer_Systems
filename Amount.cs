using System;
using System.Collections.Generic;
using System.IO;

namespace InformAmount
{
    class Program
    {
        static void Main(string[] args)
        {
                Dictionary<char, double> freq = new Dictionary<char, double>();
                string allFile = "";
                string helpString = "";
                using (StreamReader f1 = new StreamReader(@"D:\6 семестр\КС\Lab1OlgaKolesnikova\pci.gz"))
                {
                    while ((helpString = f1.ReadLine()) != null)
                    {
                        string[] str = helpString.Split(' ');
                        foreach (string letter in str)
                        {
                            allFile += letter;
                        }
                    }
                }
                freq = FillDictionary(freq, allFile);
                foreach (var letter in freq.Keys)// count freq 4 every letter
                {
                    Console.WriteLine("Frequency for " + letter + " IS " + freq[letter] / allFile.Length);
                }
                double H = 0;//entropy
                double p = 0;//frequency
                foreach (var letter in freq.Keys)
                {
                    p = freq[letter] / allFile.Length;
                    H += p * Math.Log(1 / p, 2);
                }
                Console.WriteLine("Entropy " + H);
                Console.WriteLine("Quontity of Information " + H / 8 * allFile.Length);
                Console.ReadKey();
            }
            static Dictionary<char, double> FillDictionary(Dictionary<char, double> dict, string str)
            {
                foreach (char letter in str)
                {
                    if (dict.ContainsKey(letter))
                    {
                        dict[letter]++;
                    }
                    else
                    {
                        dict.Add(letter, 1);
                    }
                }
                return dict;
            }

        }
    }