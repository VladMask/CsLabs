using System;
using System.IO;


namespace Lab_5
{
    enum GameState
    {
        Start,
        End
    }

    internal class Game
    {
        public static int Size { get; set; }
        public static string InputFile {get; set;}
        public static string OutputFile {get; set;}

        public Player cat;
        public Player mouse;

        public static GameState StateOfGame { get; set; }

        public Game() 
        { 
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            StateOfGame = GameState.Start;
        }

        public void Run()
        {
            StreamReader streamReader = new StreamReader(InputFile);
            StreamWriter streamWriter = new StreamWriter(OutputFile);
            streamWriter.WriteLine("Cat and Mouse \n");
            streamWriter.WriteLine("Cat\t\tMouse\tDistance\n=========================");

            while (Game.StateOfGame != GameState.End)
            { 
                using(streamReader) 
                {
                    Size = int.Parse(streamReader.ReadLine());
                    using(streamWriter) 
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            char ch = line[0];
                            switch (ch) 
                            {
                                case 'P': DoPrintCommand(ref streamWriter); break;
                                default: 
                                    int distance = int.Parse(line.Substring(1));
                                    DoMoveCommand(ch, distance);
                                    break;
                            }
                            if (cat.Location == mouse.Location && cat.State == State.Playing && mouse.State == State.Playing ) 
                            { 
                                cat.State = State.Winner;
                                mouse.State = State.Loaser;
                                DoPrintCommand(ref streamWriter);
                                break;
                            }
                        }
                        Game.StateOfGame = GameState.End;
                        cat.State = State.NotInGame;
                        mouse.State = State.NotInGame;
                        DoPrintCommand(ref streamWriter);
                    }
                }
            }
            streamWriter.Close();
            streamReader.Close();
        }

        private void DoMoveCommand(char ch, int distance)
        { 
            switch(ch)
            {
                case 'M': mouse.Move(distance); break;
                case 'C': cat.Move(distance); break;
            }
        }

        private void DoPrintCommand(ref StreamWriter streamWriter)
        {
            if (cat.State == State.Winner)
            {
                streamWriter.WriteLine($"\nMouse caught at: {cat.Location}");
            }
            else if (Game.StateOfGame == GameState.End)
            {
                streamWriter.WriteLine("\nDistance travaled:\tMouse\tCat");
                streamWriter.WriteLine($"\t\t\t\t\t{mouse.DistanceTravaled}\t\t{cat.DistanceTravaled}");
            }
            else
            {
                string catLocation = cat.Location == -1 ? "??" : cat.Location.ToString();
                string mouseLocation = mouse.Location == -1 ? "??" : mouse.Location.ToString();
                string distance = GetDistance() == 0 ? "" : GetDistance().ToString();
                streamWriter.WriteLine($"{catLocation}\t\t{mouseLocation}\t\t{distance}");
            }
        }

        private int GetDistance()
        {
            if (cat.Location == -1 || mouse.Location == -1) return 0;
            return Math.Abs(cat.Location - mouse.Location);
        }
    }
}
