using System;
using System.Collections.Generic;
using System.Text;
using BattleshipGame;

namespace BattleshipGame

{

    public class Display
    {
        public int GameMenu(Input input)
        {
            GetMenuOptions();
            Message("Choose an option");
            return input.ValidateMenuOption(Console.ReadLine());
        }

        private void GetMenuOptions()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Battleship\n")
                .AppendLine("1. Single player")
                .AppendLine("2. Player vs player")
                .AppendLine("3. AI vs AI\n")
                .AppendLine("0. exit");
            Console.Write(stringBuilder);
        }

        public void Message(string message) => Console.WriteLine(message);

        public void Clear() => Console.Clear();

        public void PlacementPhaseOcean(Player player, Square[,] Ocean, Dictionary<string, int> Cols, Dictionary<string, int> Rows)
        {
            FillOceanWithPlayerShips(player, Ocean);
            StringBuilder stringBuilder = new StringBuilder(String.Format("{0, 2}", ' '));

            foreach (KeyValuePair<string, int> kvp in Cols)
            {
                Console.ForegroundColor = ConsoleColor.White;
                stringBuilder.Append(String.Format("{0, 2}", kvp.Key));
            }
            stringBuilder.AppendLine();
            Console.Write(stringBuilder);
            stringBuilder.Clear();

            foreach (KeyValuePair<string, int> kvp in Rows)
            {
                Console.ForegroundColor = ConsoleColor.White;
                stringBuilder.Append(String.Format("{0, 2}", kvp.Key));
                Console.Write(stringBuilder);
                stringBuilder.Clear();

                int index = kvp.Value;
                for (int j = 0; j < Rows.Count; j++)
                {
                    if (Ocean[index, j].Status == Square.SquareStatus.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        stringBuilder.Append(String.Format("{0, 2}", $"~"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                    else if (Ocean[index, j].Status == Square.SquareStatus.Ship)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        stringBuilder.Append(String.Format("{0, 2}", $"O"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                    else if (Ocean[index, j].Status == Square.SquareStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        stringBuilder.Append(String.Format("{0, 2}", $"H"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                    else if (Ocean[index, j].Status == Square.SquareStatus.Missed)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        stringBuilder.Append(String.Format("{0, 2}", $"X"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                }
                stringBuilder.AppendLine();
                Console.Write(stringBuilder);
                stringBuilder.Clear();
            }
            Console.WriteLine(stringBuilder);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShootingPhaseOcean(Player player, Square[,] Ocean, Dictionary<string, int> Cols, Dictionary<string, int> Rows)
        {
            FillOceanWithEnemyShips(player, Ocean);
            StringBuilder stringBuilder = new StringBuilder(String.Format("{0, 2}", ' '));

            foreach (KeyValuePair<string, int> kvp in Cols)
            {
                Console.ForegroundColor = ConsoleColor.White;
                stringBuilder.Append(String.Format("{0, 2}", kvp.Key));
            }
            stringBuilder.AppendLine();
            Console.Write(stringBuilder);
            stringBuilder.Clear();

            foreach (KeyValuePair<string, int> kvp in Rows)
            {
                Console.ForegroundColor = ConsoleColor.White;
                stringBuilder.Append(String.Format("{0, 2}", kvp.Key));
                Console.Write(stringBuilder);
                stringBuilder.Clear();

                int index = kvp.Value;
                for (int j = 0; j < Rows.Count; j++)
                {
                    if (Ocean[index, j].Status == Square.SquareStatus.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        stringBuilder.Append(String.Format("{0, 2}", $"~"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                    else if (Ocean[index, j].Status == Square.SquareStatus.Ship)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        stringBuilder.Append(String.Format("{0, 2}", $"O"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                    else if (Ocean[index, j].Status == Square.SquareStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        stringBuilder.Append(String.Format("{0, 2}", $"H"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                    else if (Ocean[index, j].Status == Square.SquareStatus.Missed)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        stringBuilder.Append(String.Format("{0, 2}", $"X"));
                        Console.Write(stringBuilder);
                        stringBuilder.Clear();
                    }
                }
                stringBuilder.AppendLine();
                Console.Write(stringBuilder);
                stringBuilder.Clear();
            }
            Console.WriteLine(stringBuilder);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void FillOceanWithPlayerShips(Player player, Square[,] ocean)
        {
            foreach (Ship ship in player.ships)
            {
                foreach (Square square in ship.Location)
                {
                    (int row, int col) position = square.Position;
                    ocean[position.row, position.col].Status = square.Status;
                }
            }
        }

        public void FillOceanWithEnemyShips(Player player, Square[,] ocean)
        {
            foreach (Ship ship in player.ships)
            {
                foreach (Square square in ship.Location)
                {
                    (int row, int col) position = square.Position;
                    if (square.Status == Square.SquareStatus.Hit)
                    {
                        ocean[position.row, position.col].Status = square.Status;
                    }
                    else if (square.Status == Square.SquareStatus.Missed)
                    {
                        ocean[position.row, position.col].Status = square.Status;
                    }
                }
            }
        }

        public int RandomNumber()
        {
            Random random = new Random();
            return Convert.ToInt32(Math.Round(random.NextDouble() * 9));
        }

        public char RandomOrientation()
        {
            List<char> orientation = new List<char> { 'H', 'V' };
            Random random = new Random();
            int index = Convert.ToInt32(Math.Round(random.NextDouble()));
            return orientation[index];
        }
    }
}