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
            char[] charText = text.ToString().ToCharArray();
            int index = 0;
            while (index < charText.Length + permutations.Length)
            {
                if (charText.Length - index >= permutations.Length)
                {
                    char[] chars = new char[permutations.Length];
                    Array.Copy(charText, index, chars, 0, chars.Length);
                    unscrambled.Append(Unscramble(chars, permutations));
                    index += permutations.Length;
                }
                else
                {
                    char[] chars = new char[charText.Length - index];
                    Array.Copy(charText, index, chars, 0, chars.Length);
                    unscrambled.Append(Unscramble(chars, permutations));
                    index += permutations.Length;
                }
            }

            StreamWriter streamWriter = new StreamWriter(foutName); 
            streamWriter.Write(unscrambled.ToString());
            streamWriter.Close();            
        }

        static string Unscramble(char[] charsIn, int[] permutations)
        {
            char[] chars = new char[charsIn.Length];
            if (charsIn.Length == permutations.Length)
            {
                for (int i = 0; i < charsIn.Length; i++)
                {
                    chars[i] = charsIn[permutations[i]];
                }
            }
            else
            {
                List<int> small = new List<int> { 0 };
                for (int i = 0; i < permutations.Length; i++)
                {
                    if (permutations[i] < charsIn.Length)
                        small.Add(permutations[i]);
                }
                int[] smallPermutation = small.ToArray();
                for (int i = 0; i < charsIn.Length; i++)
                {
                    
                    chars[i] = charsIn[smallPermutation[i]];
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
