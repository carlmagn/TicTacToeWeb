using System;

namespace TicTacToeWeb.Models
{
    public class Game
    {
        public Game(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;

            player1.playerMarker = "X";
            player2.playerMarker = "O";

            GameId = Guid.NewGuid().ToString();

            player1.gameId = GameId;
            player2.gameId = GameId;

            Board = new Board();
        }

        /// <summary>
        /// First player to connect
        /// </summary>
        public Player Player1 { get; set; }

        /// <summary>
        /// Second player to connect
        /// </summary>
        public Player Player2 { get; set; }

        /// <summary>
        /// Id for the game. Used by to create groups in signalR.
        /// </summary>
        public string GameId { get; set; }

        /// <summary>
        /// The Board belonging to the game
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// A boolean indicating whether or not it is player1s turn to make a move.
        /// </summary>
        public bool IsPlayer1sTurn { get; set; } = true;

        /// <summary>
        /// A boolean indicating whether or not player1 started.
        /// </summary>
        public bool didPlayer1Start { get; set; } = true;

        /// <summary>
        /// A boolean indicating wheter the game is over or not
        /// </summary>
        public bool IsGameOver { get; set; } = false;

        public void SwitchPlayer()
        {
            IsPlayer1sTurn = !IsPlayer1sTurn;
        }

        /// <summary>
        /// Checks the gamestate for wins and draws and returns a <value name="GameState"/> enum depending on the result.
        /// </summary>
        /// <returns>
        /// * <value name="GameState.Win"/> with the corresponding WinningRow as an array of integers, if <paramref name="playerMarker"> has won.
        /// * <value name="GameState.Draw"/> if all spaces on the Board is filled and noone won.
        /// * <value name="GameState.Ongoing"/> otherwise.
        /// </returns>
        public (GameState gameState, int[] winningRow) CheckGameState(string playerMarker)
        {
            //Checks vertical wins
            if (Board.BoardState[0] == playerMarker && Board.BoardState[1] == playerMarker && Board.BoardState[2] == playerMarker) { return (GameState.Win, new int[] { 0, 1, 2 }); };
            if (Board.BoardState[3] == playerMarker && Board.BoardState[4] == playerMarker && Board.BoardState[5] == playerMarker) { return (GameState.Win, new int[] { 3, 4, 5 }); };
            if (Board.BoardState[6] == playerMarker && Board.BoardState[7] == playerMarker && Board.BoardState[8] == playerMarker) { return (GameState.Win, new int[] { 6, 7, 8 }); };

            //Checks horizontal wins
            if (Board.BoardState[0] == playerMarker && Board.BoardState[3] == playerMarker && Board.BoardState[6] == playerMarker) { return (GameState.Win, new int[] { 0, 3, 6 }); };
            if (Board.BoardState[1] == playerMarker && Board.BoardState[4] == playerMarker && Board.BoardState[7] == playerMarker) { return (GameState.Win, new int[] { 1, 4, 7 }); };
            if (Board.BoardState[2] == playerMarker && Board.BoardState[5] == playerMarker && Board.BoardState[8] == playerMarker) { return (GameState.Win, new int[] { 2, 5, 8 }); };

            //Checks diagonal wins
            if (Board.BoardState[0] == playerMarker && Board.BoardState[4] == playerMarker && Board.BoardState[8] == playerMarker) { return (GameState.Win, new int[] { 0, 4, 8 }); };
            if (Board.BoardState[6] == playerMarker && Board.BoardState[4] == playerMarker && Board.BoardState[2] == playerMarker) { return (GameState.Win, new int[] { 6, 4, 2 }); };

            //Check for draws
            var isDrawGameState = CheckDrawGameState();
            if (isDrawGameState) { return (GameState.Draw, null); };

            return (GameState.Ongoing, null);
        }

        public void ResetGame()
        {
            IsGameOver = false;
            Board = new Board();

            IsPlayer1sTurn = !didPlayer1Start;
            didPlayer1Start = !didPlayer1Start;
        }

        /// <summary>
        /// Checks if there is a draw. I.e. all Board spaces are covered.
        /// </summary>
        /// <returns>
        /// *True if Board is full. 
        /// *False if its not.
        /// </returns>
        private bool CheckDrawGameState()
        {
            for (var i = 0; i < 9; i++)
            {
                if (Board.BoardState[i] == null)
                {
                    return false;
                }
            }

            return true;
        }

        public enum GameState
        {
            Ongoing = 1,
            Win = 2,
            Draw = 3,
        }

    }
}
