using System;
using System.IO;
using System.Text;

class Player
{
    public bool isFilled = false;
    public int position = 0;
    public int distanceTravaled = 0;

    public Player()
    {

    }

    public Player(int position)
    {
        this.position = position;
    }

}

class Lab2
{
    static string MakeStep(string line, Player cat, Player mouse, int size, out bool flag)
     {
        flag = false;
        StringBuilder strBuilder= new StringBuilder();
        if (line.StartsWith("C"))
        {
            if (cat.position + int.Parse(line.Substring(1)) < 0)
                cat.position = cat.position + size + int.Parse(line.Substring(1));
            else if (cat.position + int.Parse(line.Substring(1)) > size)
                cat.position = cat.position + int.Parse(line.Substring(1)) - size;
            else
                cat.position = cat.position + int.Parse(line.Substring(1));
            if (cat.isFilled)
            cat.distanceTravaled = cat.distanceTravaled + Math.Abs(int.Parse(line.Substring(1)));
            cat.isFilled = true;
        }
        else if (line.StartsWith("M"))
        {
            if (mouse.position + int.Parse(line.Substring(1)) < 0)
                mouse.position = size + mouse.position + int.Parse(line.Substring(1));
            else if (mouse.position + int.Parse(line.Substring(1)) > size)
                mouse.position = mouse.position + int.Parse(line.Substring(1)) - size;
            else
                mouse.position = mouse.position + int.Parse(line.Substring(1));
            if (mouse.isFilled)
                mouse.distanceTravaled = mouse.distanceTravaled + Math.Abs(int.Parse(line.Substring(1)));
            mouse.isFilled = true;
        }
        else
        {
            if (mouse.position == cat.position)
            {
                strBuilder.Append("=========================\n");
                strBuilder.Append("\nDistance traveled: Mouse\tCat\n");
                strBuilder.AppendFormat("\t\t\t\t\t{1,-9}{0}\n", cat.distanceTravaled, mouse.distanceTravaled);
                strBuilder.AppendFormat("\nMouse caught at: {0}\n", cat.position);
                flag = true;
            }
            else if (!cat.isFilled)
            {
                strBuilder.AppendFormat("??\t\t{0}", mouse.position);
            }
            else if (!mouse.isFilled)
            {
                strBuilder.AppendFormat("{0}\t\t??", cat.position);
            }
            else
            {
                strBuilder.AppendFormat("{0}\t\t{1}\t\t{2}", cat.position, mouse.position, Math.Abs(cat.position - mouse.position));
            }
        
        }
        return strBuilder.ToString();
    }

    static void CreateOutputFile(string filename)
    {
        Player mouse = new Player();
        Player cat = new Player();
        int size;
        bool flag = false;
        StreamReader streamReader = new StreamReader(filename);
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
                    string line = streamReader.ReadLine();
                    string outLine = MakeStep(line, cat, mouse, size, out flag);
                    if (outLine.Length > 0)
                        streamWriter.WriteLine(outLine);
                    if (flag)
                        break;
                }
                if (!flag)
                {
                    streamWriter.WriteLine("=========================\n");
                    streamWriter.WriteLine("\nDistance traveled: Mouse\tCat");
                    streamWriter.WriteLine("\t\t\t\t\t{1, -9}{0}\n", cat.distanceTravaled, mouse.distanceTravaled);
                    streamWriter.WriteLine("Mouse evaded Cat");
                }
                streamWriter.Close();
            }
            streamReader.Close();
        }
    }


    static void Main(string[] args)
    {
        string filename = "1.ChaseData.txt";
        CreateOutputFile(filename);
    }
}

