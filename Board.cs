using System.Collections.Generic;

namespace BattleshipGame
{

    public class Board
    {
        private int Size = 10;

        public Dictionary<string, int> Cols { get; set; }
        public Dictionary<string, int> Rows { get; set; }
        
        public Square[,] Ocean { get; set; }

        public Board()
        {
            Cols = new Dictionary<string, int>();
            Rows = new Dictionary<string, int>();
            Ocean = new Square[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                Cols.Add($"{(char)(i + 65)}", i);
                Rows.Add($"{i + 1}", i);
                
                for (int j = 0; j < Size; j++)
                {
                    Ocean[i, j] = new Square((i, j));
                }

            }


        }


        /*public bool IsPlacementOk((int x, int y) position)
        {
            
            
            if (Ocean[position.x, position.y].Status == Square.SquareStatus.Empty)
            {
                return true;
            }

            return false;

        }*/

    }

}