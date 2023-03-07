using System;
using System.Collections.Generic;
using System.IO;

class Lab2
{
    static void Main(string[] args)
    {
        int mouse_pos = 0, cat_pos = 0;
        int size;
        bool flag = false;
        int c_traveled = 0, m_traveled = 0;
        StreamReader streamReader = new StreamReader("3.ChaseData.txt");
        StreamWriter streamWriter = new StreamWriter("Report_L2.txt");
        using (streamReader)
        {
            using (streamWriter)
            {
                streamWriter.WriteLine("Cat and Mouse \n");
                streamWriter.WriteLine("Cat\t\tMouse\tDistance\n=========================");
                size = int.Parse(streamReader.ReadLine());
                while (!streamReader.EndOfStream)
                {
                    string checker = streamReader.ReadLine();
                    if (checker.StartsWith("C"))
                    {
                        if (cat_pos + int.Parse(checker.Substring(1)) < 0)
                            cat_pos = cat_pos + size + int.Parse(checker.Substring(1));
                        else if (cat_pos + int.Parse(checker.Substring(1)) > size)
                            cat_pos = cat_pos + int.Parse(checker.Substring(1)) - size;
                        else
                            cat_pos = cat_pos + int.Parse(checker.Substring(1));
                        c_traveled = c_traveled + Math.Abs(int.Parse(checker.Substring(1)));
                    }
                    else if (checker.StartsWith("M"))
                    {
                        if (mouse_pos + int.Parse(checker.Substring(1)) < 0)
                            mouse_pos = size + mouse_pos + int.Parse(checker.Substring(1));
                        else if (mouse_pos + int.Parse(checker.Substring(1)) > size)
                            mouse_pos = mouse_pos + int.Parse(checker.Substring(1)) - size;
                        else
                            mouse_pos = mouse_pos + int.Parse(checker.Substring(1));
                        m_traveled = m_traveled + Math.Abs(int.Parse(checker.Substring(1)));
                    }
                    else
                    {
                        if (mouse_pos == cat_pos)
                        {
                            streamWriter.WriteLine("=========================\n");
                            streamWriter.WriteLine("\nDistance traveled: Cat\tMouse");
                            streamWriter.WriteLine("\t\t\t\t\t{0, -7}{1}\n", c_traveled, m_traveled);
                            streamWriter.WriteLine("Mouse caught at: {0}", cat_pos);
                            flag = true;
                            break;
                        }
                        if (cat_pos == 0)
                        {
                            streamWriter.WriteLine("??\t\t{0}", mouse_pos);
                        }
                        else if (mouse_pos == 0)
                        {
                            streamWriter.WriteLine("{0}\t\t??", cat_pos);
                        }
                        else
                        {
                            streamWriter.WriteLine("{0}\t\t{1}\t\t{2}", cat_pos, mouse_pos, Math.Abs(cat_pos - mouse_pos)); 
                        }
                    }                    
                }
                if (!flag)
                {
                    streamWriter.WriteLine("=========================\n");
                    streamWriter.WriteLine("\nDistance traveled: Cat\tMouse");
                    streamWriter.WriteLine("\t\t\t\t\t{0, -7}{1}\n", c_traveled, m_traveled);
                   streamWriter.WriteLine("Mouse evaded Cat");
                }
                streamWriter.Close();
            }
            streamReader.Close();
        }
    }
}

