using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab_3
{
    internal class Lab_3
    {
        static void CreateOutput(string finName, string foutName)
        {
            int[] permutations = GetPermutations(finName);
            StreamReader streamReader = new StreamReader(finName);
            StringBuilder text = new StringBuilder();
            StringBuilder unscrambled = new StringBuilder();
            using (streamReader)
            {
                for (int i = 0; i < 5; i++)
                {
                    streamReader.ReadLine();
                }
                text.Append(streamReader.ReadToEnd());
            }
            streamReader.Close();
            text.Replace("\r", "");
            text.Remove(text.Length - 1, 1);
            while (text.Length > 0)
            {
                if (text.Length >= permutations.Length)
                {
                    StringBuilder line = new StringBuilder();
                    line.Append(text.ToString(0, permutations.Length));
                    text.Remove(0, permutations.Length);
                    unscrambled.Append(Unscramble(line.ToString(), permutations));
                }
                else
                {
                    StringBuilder line = new StringBuilder();
                    line.Append(text.ToString(0, text.Length));
                    text.Remove(0, text.Length);
                    unscrambled.Append(Unscramble(line.ToString(), permutations));
                }
            }

            StreamWriter streamWriter = new StreamWriter(foutName);
            streamWriter.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            streamWriter.WriteLine($"Decripting {unscrambled.Length} characters");
            StringBuilder indexes = new StringBuilder();
            StringBuilder permut = new StringBuilder();
            for (int i = 0; i < permutations.Length; i++)
            {
                indexes.Append(i.ToString() + "\t");
                permut.Append(permutations[i].ToString() + "\t");
            }
            streamWriter.WriteLine($"Using:\t{indexes}");
            streamWriter.WriteLine($"\t\t{permut}");
            streamWriter.WriteLine(unscrambled.ToString());
            streamWriter.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            streamWriter.Close();            
        }

        static string Unscramble(string input, int[] permutations)
        {
            char[] chars = new char[input.Length];
            if (input.Length == permutations.Length)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    chars[i] = input[permutations[i]];
                }
            }
            else
            {
                
                for (int i = 0; i < input.Length; i++)
                {
                    int j = i;
                    do
                    {
                        j = permutations[j];
                    }
                    while (j >= input.Length);
                    chars[i] = input[j];
                }                
            }

            return string.Join(string.Empty, chars);
        }

        static int[] GetPermutations(string fileName)
        {
            List<int> result = new List<int>();
            StreamReader streamReader = new StreamReader(fileName);
            string line;
            using (streamReader)
            {
                streamReader.ReadLine();
                line = streamReader.ReadLine();
            }
            streamReader.Close();
            line = line.Trim();
            string[] permutationsStr = line.Split();
            foreach (string ln in permutationsStr)
            {
                if (ln.Length != 0)
                    result.Add(int.Parse(ln));
            }            
            return result.ToArray();
        }

        static void Main(string[] args)
        {
            string finName = "1.Scrambled.txt";
            string foutName = "Out.txt";
            CreateOutput(finName, foutName);
            Console.WriteLine("Done");
        }
    }
}
