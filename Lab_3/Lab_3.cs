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
                StringBuilder line = new StringBuilder();
                line.Append(text.ToString(0,20));
                text.Remove(0, line.Length);
                unscrambled.Append(Unscramble(line.ToString(), permutations));
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
                List<int> perm = new List<int> { 0 };
                for (int i = 0; i < permutations.Length; i++)
                {
                    if (permutations[i] <= input.Length)
                        perm.Add(permutations[i]);
                }
                int[] smallPermutation = perm.ToArray();
                for (int i = 0; i < input.Length; i++)
                {
                    
                    chars[i] = input[smallPermutation[i]];
                }
            }
            StringBuilder result = new StringBuilder();
            foreach (char ch in chars)
            {
                result.Append(ch);
            }

            return result.ToString();
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
                line = line.Trim();
            }
            streamReader.Close();
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
