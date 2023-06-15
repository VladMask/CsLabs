using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab_4
{
    class Protein
    { 
        public string Name { get; set; }
        public string Entity { get; set; }
        public string Sequence { get; set; }

        public Protein() { }
    }

    internal class Lab_4
    {
        static List<Protein> GetData(string finName)
        {
            List<Protein> data = new List<Protein>();

            StreamReader streamReader= new StreamReader(finName);
            using (streamReader)
            {
                while (!streamReader.EndOfStream)
                { 
                    Protein protein = new Protein();
                    String line = streamReader.ReadLine();
                    protein.Name = line.Substring(0, line.IndexOf('\t'));
                    protein.Entity = line.Substring(line.IndexOf('\t') + 1, line.LastIndexOf('\t') - line.IndexOf('\t') - 1);
                    protein.Sequence = line.Substring(line.LastIndexOf("\t") + 1, line.Length - line.LastIndexOf("\t") - 1);
                    data.Add(protein);
                }
            }
            streamReader.Close();

            return data;
        }

        static bool ContainsDigits(string sequince)
        {
            foreach (char ch in sequince)
            {
                if (int.TryParse(ch.ToString(), out int value)) return true;
            }
            return false;
        }

        static string Decript(string sequince)
        {
            if (ContainsDigits(sequince))
            {
                StringBuilder sb = new StringBuilder();
                int value = 0;
                for (int i = 0; i < sequince.Length; i++)
                {
                    if (!int.TryParse(sequince[i].ToString(), out value))
                    {
                        sb.Append(sequince[i]);
                    }
                    else
                    {
                        for (int j = 1; j < value; j++)
                        {
                            sb.Append(sequince[i + 1]);
                        }
                    }
                }
                return sb.ToString();
            }
            return sequince;
        }

        static void Search(string command, List<Protein> data, ref StreamWriter streamWriter, ref int num)
        {
            bool flag = false;
            
            string sequince = Decript(command.Substring(command.IndexOf('\t') + 1));

            streamWriter.WriteLine("==============================================================================");
            streamWriter.WriteLine($"{num}\tsearch\t{sequince}");
            streamWriter.WriteLine("organism\t\tprotein");

            foreach (Protein protein in data)
            {
                if (protein.Sequence.Contains(sequince))
                {
                    streamWriter.WriteLine(protein.Entity + "\t" + protein.Name);
                    num++;
                    flag = true;
                }
            }
            if (!flag)
                streamWriter.WriteLine("NOT FOUND");

            streamWriter.WriteLine("==============================================================================");
        }

        static void Diff(string command, List<Protein> data, ref StreamWriter streamWriter, ref int num)
        {
            num++;
            string pr1Name = command.Substring(command.IndexOf("\t") + 1, command.LastIndexOf("\t") - command.IndexOf("\t") - 1);
            string pr2Name = command.Substring(command.LastIndexOf("\t") + 1);

            streamWriter.WriteLine("==============================================================================");
            streamWriter.WriteLine($"{num}\tdiff\t{pr1Name}\t\t{pr2Name}");
            streamWriter.WriteLine("amino-acids difference:");

            Protein protein1 = new Protein();
            Protein protein2 = new Protein();

            foreach (Protein protein in data)
            {
                if (protein.Name == pr1Name)
                    protein1= protein;
                else if (protein.Name == pr2Name)
                    protein2= protein;
            }

            int difference = 0;
            if (protein1 != null && protein2 != null)
            {
                int min = Math.Min(protein1.Sequence.Length, protein2.Sequence.Length);
                difference = Math.Max(protein1.Sequence.Length, protein2.Sequence.Length) - min;
                for (int i = 0; i < min; i++)
                {
                    if (protein1.Sequence[i] != protein2.Sequence[i])
                        difference++;
                }
                streamWriter.WriteLine(difference.ToString());
            }
            else
                streamWriter.WriteLine("MISSING");

            streamWriter.WriteLine("==============================================================================");
        }

        static void Mod(string command, List<Protein> data, ref StreamWriter streamWriter, ref int num)
        {
            num++;

            string prName = command.Substring(command.IndexOf('\t') + 1, command.Length - command.IndexOf('\t') - 1);

            streamWriter.WriteLine("==============================================================================");
            streamWriter.WriteLine($"{num}\tmode\t{prName}");
            streamWriter.WriteLine("amino-acid occurs: ");

            Protein protein = new Protein();

            foreach (Protein pr in data)
            {
                if (pr.Name == prName)
                    protein = pr;
            }

            if (protein != null)
            {
                Dictionary<char, int> pairs = new Dictionary<char, int>();

                char[] chars = protein.Sequence.ToCharArray();
                foreach (char ch in chars)
                {
                    if (!pairs.ContainsKey(ch))
                        pairs.Add(ch, 1);
                    else
                        pairs[ch]++;
                }

                int max = 0;
                foreach (char ch in chars)
                {
                    if (pairs[ch] > max)
                        max = pairs[ch];
                }

                List<char> list = new List<char>();
                foreach (char ch in chars)
                {
                    if (pairs[ch] == max)
                        list.Add(ch);
                }

                char minCh = list[0];
                foreach (char ch in list)
                {
                    if (ch < minCh)
                        minCh = ch;
                }

                streamWriter.WriteLine($"{minCh}\t\t{pairs[minCh]}");
            }
            else
                streamWriter.WriteLine("MISSING");

            streamWriter.WriteLine("==============================================================================");
        }

        static void RunCommand(string command, List<Protein> data, ref StreamWriter streamWriter, ref int num) 
        {
            if (command.StartsWith("search"))
            {
                Search(command, data, ref streamWriter, ref num);
            }
            else if (command.StartsWith("diff"))
            {
                Diff(command, data, ref streamWriter, ref num);
            }
            else if (command.StartsWith("mode"))
            {
                Mod(command, data, ref streamWriter, ref num);
            }
            else
                streamWriter.WriteLine("ERROR. Unknown command");
        }

        static void CreateOutput(string foutName, string scriptName, List<Protein> data)
        {
            int num = 1;

            StreamReader streamReader= new StreamReader(scriptName);
            StreamWriter streamWriter= new StreamWriter(foutName);
            using (streamReader)
            {                
                string line;
                while ((line = streamReader.ReadLine()) != null) 
                {
                    RunCommand(line, data, ref streamWriter, ref num);
                }
            }
            streamWriter.Close();
            streamReader.Close();
        }

        static void Main(string[] args)
        {
            string scriptName = "commands.0.txt";
            string foutName = "Out.txt";
            string finName = "sequences.0.txt";
            List<Protein> data = GetData(finName);
            CreateOutput(foutName, scriptName, data);
            
            Console.WriteLine("Ready");
        }
    }
}
