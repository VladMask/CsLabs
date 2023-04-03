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
            streamWriter.Write(unscrambled.ToString());
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
                List<int> small = new List<int> { 0 };
                for (int i = 0; i < permutations.Length; i++)
                {
                    if (permutations[i] < input.Length)
                        small.Add(permutations[i]);
                }
                int[] smallPermutation = small.ToArray();
                for (int i = 0; i < input.Length; i++)
                {
                    
                    chars[i] = input[smallPermutation[i]];
                }
            }

            return string.Join(string.Empty, chars);
        }

        static int[] GetPermutations(string fileName)
        {//function is OK
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
