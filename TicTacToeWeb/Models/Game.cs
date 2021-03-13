using System;

namespace TicTacToeWeb.Models
{
    public class Game
    {
        public Game(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            player1.playerMarker = "X";
            player2.playerMarker = "O";

            gameId = Guid.NewGuid().ToString();

            player1.gameId = gameId;
            player2.gameId = gameId;

            board = new Board();
        }

        /// <summary>
        /// First player to connect
        /// </summary>
        public Player player1 { get; set; }

        /// <summary>
        /// Second player to connect
        /// </summary>
        public Player player2 { get; set; }

        /// <summary>
        /// Id for the game. Used by to create groups in signalR.
        /// </summary>
        public string gameId { get; set; }

        /// <summary>
        /// The board belonging to the game
        /// </summary>
        public Board board { get; set; }

        /// <summary>
        /// A boolean indicating whether or not it is player1s turn to make a move or not.
        /// </summary>
        public bool isPlayer1sTurn { get; set; } = true;

        /// <summary>
        /// A boolean indicating wheter the game is over or not
        /// </summary>
        public bool isGameOver { get; set; } = false;

        public void SwitchPlayer()
        {
            isPlayer1sTurn = !isPlayer1sTurn;
        }

    }
}
