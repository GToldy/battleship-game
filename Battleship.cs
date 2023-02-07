using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class Battleship
    {
        public static void Main(string[] args)
        {
            Display display = new Display();
            Input input = new Input();
            Game game = new Game();

            int option = display.GameMenu(input);
            if (option == 3)
            {
                game.AIvsAI();
            }
            else if (option != 0)
            {
                game.GameFlow(option);
            }
        }
    }
}