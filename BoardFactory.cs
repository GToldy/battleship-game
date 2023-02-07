using System;
using System.Collections.Generic;

namespace BattleshipGame
{

    public class BoardFactory
    {
        Input input = new Input();
        Display display = new Display();

        public bool ComputerPlacement(Player player, Board board, ShipType shipType)
        {
            List<Square> location = new List<Square>();

            (int x, int y) position = (display.RandomNumber(), display.RandomNumber());

            char orientation = display.RandomOrientation();

            Ship ship = GenerateShipCoordinates(board.Ocean, position, orientation, shipType);
            if (ship is null)
            {
                return false;
            }
            else
            {
                player.ships.Add(ship);
                return true;
            }
        }

        public bool ManualPlacement(Player player, Board board, ShipType shipType)
        {
            List<Square> Ship = new List<Square>();
            
            (int x, int y) position = input.ValidateCoordinates(board.Cols, board.Rows, display);

            display.Message("Which orientation (H/V)");
            char orientation = Convert.ToChar(Console.ReadLine().ToUpper());

            Ship ship = GenerateShipCoordinates(board.Ocean, position, orientation, shipType);
            if (ship is null)
            {
                display.Message("Ship can't be put there, try again");
                return false;
            }
            else
            {
                player.ships.Add(ship);
                return true;
            }
        }

        private Ship GenerateShipCoordinates(Square[,] ocean, (int, int) position, char orientation, ShipType shipType)
        {
            Ship Ship = new Ship();
            (int row, int col) coordinate = position;
            Square square = null;

            switch (orientation)
            {
                case 'H':
                    if (coordinate.col <= 10 - (int)shipType)
                    {
                        for (int i = 0; i < (int)shipType; i++)
                        {
                            square = ocean[coordinate.row, coordinate.col + i];
                            if (CheckIfPositionEmpty(Ship, square, ocean))
                            {
                                Ship.Location.Add(square);
                            }
                            else
                            {
                                return null;
                            }
                        }

                        foreach (Square shipSquare in Ship.Location)
                        {
                            shipSquare.Status = Square.SquareStatus.Ship;
                        }

                        Ship.Name = shipType.ToString();
                        return Ship;
                    }
                    break;
                case 'V':
                    if (coordinate.row <= 10 - (int)shipType)
                    {
                        for (int i = 0; i < (int)shipType; i++)
                        {
                            square = ocean[coordinate.row + i, coordinate.col];
                            if (CheckIfPositionEmpty(Ship, square, ocean))
                            {
                                Ship.Location.Add(square);
                            }
                            else
                            {
                                return null;
                            }
                        }

                        foreach (Square shipSquare in Ship.Location)
                        {
                            shipSquare.Status = Square.SquareStatus.Ship;
                        }

                        Ship.Name = shipType.ToString();
                        return Ship;
                    }
                    break;
            }

            return null;
        }

        public bool CheckIfPositionEmpty(Ship ship, Square square, Square[,] ocean)
        {
            (int row, int col) pos = square.Position;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    try
                    {
                        if (ocean[pos.row + i, pos.col + j].Status != Square.SquareStatus.Empty) return false;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }
                }
            }
            return true;
        }
    }


}