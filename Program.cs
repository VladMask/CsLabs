using System;
using System.Collections.Generic;
using System.IO;

class Lab1
{
    static bool IsValid(string str)
    {
        str = str.Replace(" ", "");
        int len = str.Length;
        if (len != 7)
            return false;
        string comp_str = str.ToUpper();
        if (comp_str != str)
            return false;
        string allowed = " QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
        foreach (char s in str)
        {
            int index = allowed.IndexOf(s);
            if (index == -1)
                return false;
        }
        return true;
    }

    //static string NextValue(string line)
    //{//Z = 90; A = 65;
        
    //    return "qwe";
    //}
    static List<string> GetData(string fileName)
    {
        List<string> plates = new List<string>();
        StreamReader streamReader = new StreamReader(fileName);
        using (streamReader)
        {
            
            streamReader.ReadLine();
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                plates.Add(line);
                Console.Write(line);
                Console.WriteLine(IsValid(line));
            }
        }
        return plates;
    }

    static void Main()
    {
        string fileName = "input_1.txt";
        List<string> data = GetData(fileName);       
        
    }
}