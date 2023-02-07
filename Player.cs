using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame
{
    public class Player
    {
        public List<Ship> ships = new List<Ship>();
        public Board Board = new Board();
        public bool IsAlive { get; private set; }
        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
            IsAlive = IsPlayerAlive();
        }

        public bool IsPlayerAlive()
        {
            foreach (Ship ship in ships)
            {
                foreach (Square square in ship.Location)
                {
                    if (square.Status == Square.SquareStatus.Ship)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string MakeShot(Board board, Player enemyPlayer, bool isAI = false)
        {
            Input input = new Input();
            Display display = new Display();
            Square square = null;

            if (isAI)
            {
                Random random = new Random();
                int row = Convert.ToInt32(Math.Round(random.NextDouble() * 9));
                int col = Convert.ToInt32(Math.Round(random.NextDouble() * 9));
                (int row, int col) coordinate = (row, col);
                square = board.Ocean[coordinate.row, coordinate.col];
                display.Message($"{coordinate.row}, {coordinate.col}");
            }
            else
            {
                display.Message("Choose a coordinate");
                (int row, int col) coordinate = input.ValidateCoordinates(board.Cols, board.Rows, display);
                square = board.Ocean[coordinate.row, coordinate.col];
            }

            if (CheckForUnusedSquare(board.Ocean, square))
            {
                if (CheckShot(board.Ocean, enemyPlayer, square))
                {
                    return "You hit a ship! Keep it up!";
                }
                else
                {
                    return "You missed! Good luck next time!";
                }
            }
            return "";
        }

        private bool CheckShot(Square[,] ocean, Player enemyPlayer, Square square)
        {
            foreach (Ship ship in enemyPlayer.ships)
            {
                foreach (Square shipSquare in ship.Location)
                {
                    if (shipSquare.Position == square.Position)
                    {
                        shipSquare.Status = Square.SquareStatus.Hit;
                        return true;
                    }
                }
            }
            square.Status = Square.SquareStatus.Missed;
            return false;
        }

        private bool CheckForUnusedSquare(Square[,] ocean, Square square)
        {
            (int row, int col) position = square.Position;
            if (ocean[position.row, position.col].Status == Square.SquareStatus.Hit || ocean[position.row, position.col].Status == Square.SquareStatus.Missed)
            {
                return false;
            }
            return true;
        }
    }
}