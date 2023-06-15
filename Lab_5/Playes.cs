using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    enum State
    {
        Winner,
        Loaser,
        Playing,
        NotInGame
    }

    internal class Player
    {
        public string Name { get; set; }
        public int Location { get; set; }
        public State State { get; set; }
        //public int DistanceTravaled { get; set; }

        private int distanceTravaled = 0;
        public int DistanceTravaled
        {
            get { return distanceTravaled; }
        }

        public Player(string name)
        { 
            Name = name;
            State = State.NotInGame;
            //DistanceTravaled = 0;
            Location = -1;
        }

        public void Move(int distance)
        {
            if (State == State.NotInGame)
            {
                State = State.Playing;
                Location = distance;
            }
            else
            {
                if (Location + distance < 0)
                    Location = Location + Game.Size + distance;
                else if (Location + distance > Game.Size)
                    Location = Location + distance - Game.Size;
                else
                    Location = Location + distance;
                distanceTravaled += Math.Abs(distance);
            }
        }
    }

}
