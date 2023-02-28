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
        string allowed = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
        foreach (char s in str)
        {
            int index = allowed.IndexOf(s);
            if (index == -1)
                return false;
        }
        return true;
    }

    static List<string> NextValue(List<string> plates)
    {//Z = 90; A = 65;
        List<string> next_plates = new List<string>();
        foreach (string plate in plates)
        {
            if (IsValid(plate))
            {
                plate.Replace(" ", "");
                int first_letter = (int)plate[0];
                int second_letter = (int)plate[1];
                int third_letter = (int)plate[2];
                int number = int.Parse(plate.Substring(3));
                if (number < 9999)
                {
                    number++;
                    string num;
                    if (number < 10)
                        num = "000" + number.ToString();
                    else if (number < 100)
                        num = "00" + number.ToString();
                    else
                        num = "0" + number.ToString();
                    string next_plate = first_letter.ToString() + second_letter.ToString()
                        + third_letter.ToString() + " " + num;
                    next_plates.Add(next_plate);
                }
                else if (third_letter < 90)
                {
                    third_letter++;
                    string next_plate = first_letter.ToString() + second_letter.ToString()
                        + third_letter.ToString() + " 0000";
                    next_plates.Add(next_plate);
                }
                else if (second_letter < 90)
                {
                    third_letter = 65;
                    second_letter++;
                    string next_plate = first_letter.ToString() + second_letter.ToString()
                        + third_letter.ToString() + " 0000";
                    next_plates.Add(next_plate);
                }
                else if (first_letter < 90)
                {
                    third_letter = 65;
                    second_letter = 65;
                    first_letter++;
                    string next_plate = first_letter.ToString() + second_letter.ToString()
                        + third_letter.ToString() + " 0000";
                    next_plates.Add(next_plate);
                }
                else
                {
                    string next_plate = "The last one";
                    next_plates.Add(next_plate);
                }
            }
            else
            {
                string next_plate = "Invalid value";
                next_plates.Add(next_plate);
            }
        }

        return next_plates;
    }

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
                //Console.Write(line);
                //Console.WriteLine(IsValid(line));
            }
        }
        return plates;
    }

    static void Main()
    {
        string fileName = "input_1.txt";
        List<string> data = GetData(fileName);
        List<string> next_plates = NextValue(data);
        for (int i = 0; i < next_plates.Count;i++)  
        {
            Console.WriteLine("{0} ===> {1}", data[i], next_plates[i]);
        }
    }
}