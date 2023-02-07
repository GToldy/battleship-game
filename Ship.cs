using System;
using System.Collections.Generic;

namespace BattleshipGame
{

    public class Ship
    {
        public List<Square> Location { get; set; }

        public ShipType Type { get; set; }
        public string Name { get; set; }

        public Ship()
        {
            Location = new List<Square>();
        }



        public void AddLocation(Square square)
        {
            Location.Add(square);
        }

    }


}