using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class Lab_5
    {
        static void Main(string[] args)
        {
            Game.InputFile = "2.ChaseData.txt";
            Game.OutputFile = "Out.txt";

            Game game = new Game();
            game.Run();
        }
    }
}
