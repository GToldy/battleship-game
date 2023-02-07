using System;
using System.Threading;

namespace BattleshipGame
{

    public class Game
    {
        public Player player1 = new Player("Player 1");
        public Player player2 = new Player("Player 2");
        public Board board = new Board();
        public BoardFactory boardFactory = new BoardFactory();
        public Display display = new Display();
        public Input Input = new Input();

        public void GameFlow(int option)
        {
            Player currentPlayer = player1;
            Player enemyPlayer = player2;

            bool isGameRunning = true;

            while (isGameRunning)
            {
                bool isplayerVsAI = option == 1 ? true : false;
                bool isPlacementPhase = true;

                while (isPlacementPhase)
                {
                    Board playerBoard = new Board();
                    display.Clear();
                    DisplayCurrentPlayer(currentPlayer);
                    display.PlacementPhaseOcean(currentPlayer, playerBoard.Ocean, playerBoard.Cols, playerBoard.Rows);

                    if (isplayerVsAI && currentPlayer.Name == player2.Name)
                    {
                        foreach (ShipType shipType in Enum.GetValues(typeof(ShipType)))
                        {
                            ComputerShipPlacement(currentPlayer, playerBoard, shipType);
                        }
                    }
                    else
                    {
                        foreach (ShipType shipType in Enum.GetValues(typeof(ShipType)))
                        {
                            ManualShipPlacement(currentPlayer, playerBoard, shipType);
                        }
                    }
                    display.Clear();
                    display.Message("Press enter to continue...");

                    if (currentPlayer.Name == player2.Name) isPlacementPhase = false;
                    currentPlayer = GetCurrentPlayer(currentPlayer);
                    Console.ReadKey();
                }

                bool isShootingPhase = true;

                while (isShootingPhase)
                {
                    string result = "";
                    display.Clear();
                    DisplayCurrentPlayer(currentPlayer);
                    display.ShootingPhaseOcean(enemyPlayer, enemyPlayer.Board.Ocean, board.Cols, board.Rows);

                    if (isplayerVsAI && currentPlayer.Name == player2.Name)
                    {
                        result = currentPlayer.MakeShot(enemyPlayer.Board, enemyPlayer, true);
                    }
                    else
                    {
                        result = currentPlayer.MakeShot(enemyPlayer.Board, enemyPlayer);
                    }
                    

                    display.Clear();
                    DisplayCurrentPlayer(currentPlayer);
                    display.ShootingPhaseOcean(enemyPlayer, enemyPlayer.Board.Ocean, board.Cols, board.Rows);
                    display.Message(result);
                    
                    if (!enemyPlayer.IsPlayerAlive())
                    {
                        display.Message($"{currentPlayer.Name} won the game!");
                        isShootingPhase = false;
                        isGameRunning = false;
                    }

                    currentPlayer = GetCurrentPlayer(currentPlayer);
                    enemyPlayer = GetEnemyPlayer(enemyPlayer);
                    Console.ReadKey(true);
                }
            }
        }

        public void AIvsAI()
        {
            Player currentPlayer = player1;
            Player enemyPlayer = player2;

            bool isGameRunning = true;

            while (isGameRunning)
            {
                bool isPlacementPhase = true;

                while (isPlacementPhase)
                {
                    Board playerBoard = new Board();
                    display.Clear();
                    DisplayCurrentPlayer(currentPlayer);
                    display.PlacementPhaseOcean(currentPlayer, playerBoard.Ocean, playerBoard.Cols, playerBoard.Rows);

                    foreach (ShipType shipType in Enum.GetValues(typeof(ShipType)))
                    {
                        Thread.Sleep(1000);
                        ComputerShipPlacement(currentPlayer, playerBoard, shipType);
                    }

                    display.Clear();

                    if (currentPlayer.Name == player2.Name) isPlacementPhase = false;
                    currentPlayer = GetCurrentPlayer(currentPlayer);
                    display.Message("Press enter to continue...");
                    Console.ReadKey(true);
                }

                bool isShootingPhase = true;

                while (isShootingPhase)
                {
                    string result = "";
                    display.Clear();
                    DisplayCurrentPlayer(currentPlayer);
                    display.ShootingPhaseOcean(enemyPlayer, enemyPlayer.Board.Ocean, board.Cols, board.Rows);

                    Thread.Sleep(1000);
                    result = currentPlayer.MakeShot(enemyPlayer.Board, enemyPlayer, true);

                    display.Clear();
                    DisplayCurrentPlayer(currentPlayer);
                    display.ShootingPhaseOcean(enemyPlayer, enemyPlayer.Board.Ocean, board.Cols, board.Rows);
                    display.Message(result);

                    if (!enemyPlayer.IsPlayerAlive())
                    {
                        display.Message($"{currentPlayer.Name} won the game!");
                        isShootingPhase = false;
                        isGameRunning = false;
                    }

                    
                    currentPlayer = GetCurrentPlayer(currentPlayer);
                    enemyPlayer = GetEnemyPlayer(enemyPlayer);
                    display.Message("Press enter to continue...");
                    Console.ReadKey(true);
                }
            }
        }

        public Player GetCurrentPlayer(Player player)
        {
            switch (player.Name)
            {
                case "Player 1":
                    return player2;
                case "Player 2":
                    return player1;
            }
            return null;
        }

        public Player GetEnemyPlayer(Player player)
        {
            switch (player.Name)
            {
                case "Player 1":
                    return player2;
                case "Player 2":
                    return player1;
            }
            return null;
        }

        public void DisplayCurrentPlayer(Player player)
        {
            switch (player.Name)
            {
                case "Player 1":
                    display.Message(String.Format("{0, 19}", $"{player1.Name}'s turn\n"));
                    break;
                case "Player 2":
                    display.Message(String.Format("{0, 19}", $"{player2.Name}'s turn\n"));
                    break;
            }
        }

        public void ManualShipPlacement(Player currentPlayer, Board playerBoard, ShipType shipType)
        {
            bool isCoordinateOk = false;
            while (!isCoordinateOk)
            {
                display.Message("Choose a coordinate");
                isCoordinateOk = boardFactory.ManualPlacement(currentPlayer, playerBoard, shipType);
            }
            display.Clear();
            DisplayCurrentPlayer(currentPlayer);
            display.PlacementPhaseOcean(currentPlayer, playerBoard.Ocean, playerBoard.Cols, playerBoard.Rows);
        }

        public void ComputerShipPlacement(Player currentPlayer, Board playerBoard, ShipType shipType)
        {
            bool isCoordinateOk = false;
            while (!isCoordinateOk)
            {
                isCoordinateOk = boardFactory.ComputerPlacement(currentPlayer, playerBoard, shipType);
            }
            display.Clear();
            DisplayCurrentPlayer(currentPlayer);
            display.PlacementPhaseOcean(currentPlayer, playerBoard.Ocean, playerBoard.Cols, playerBoard.Rows);
        }

    }


}