using System;
using System.Collections.Generic;

namespace BattleshipGame
{
    public class Input
    {
        public int ValidateMenuOption(string input)
        {
            if (int.TryParse(input, out int option) && option >= 0 && option <= 3)
            {
                return option;
            }
            return -1;
        }

        public (int, int) ValidateCoordinates(Dictionary<string, int> Cols, Dictionary<string, int> Rows, Display display)
        {
            int col = 0, row = 0;
            bool isValid = false;

            while (!isValid)
            {
                string coordinateInput = Console.ReadLine().ToUpper();
                (string row, string col) coordinate;

                if (coordinateInput.Length <= 1)
                {
                    display.Message("Wrong input, try again");
                    continue;
                }
                else if (coordinateInput.Length > 2)
                {
                    coordinate.col = coordinateInput[0].ToString();
                    coordinate.row = coordinateInput.Substring(1);
                }
                else
                {
                    coordinate.col = coordinateInput[0].ToString();
                    coordinate.row = coordinateInput[1].ToString();
                }

                if (Cols.ContainsKey(coordinate.col) && Rows.ContainsKey(coordinate.row))
                {
                    col = Cols[coordinate.col];
                    row = Rows[coordinate.row];

                    isValid = true;
                }
                else
                {
                    display.Message("Wrong input, try again");
                }

            }
            return (row, col);
        }
    }
}