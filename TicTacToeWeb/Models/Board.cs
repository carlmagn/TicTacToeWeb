namespace TicTacToeWeb.Models
{
    public class Board
    {
        public string[] _board { get; set; }

        public Board()
        {
            _board = new string[9];
        }

        public bool CheckLegalMove(int move)
        {
            return _board[move] == null ? true : false;
        }

        public void MakeMove(string playerMarker, int move)
        {
            _board[move] = playerMarker;
        }

        /// <summary>
        /// Checks the gamestate for wins and draws and returns a <value name="GameState"/> enum depending on the result.
        /// </summary>
        /// <returns>
        /// * <value name="GameState.Win"/> if <paramref name="_currentPlayer"> has won.
        /// * <value name="GameState.Draw"/> if all spaces on the board is filled and noone won.
        /// * <value name="GameState.Ongoing"/> otherwise.
        /// </returns>
        public GameState CheckGameState(string playerMarker)
        {
            //Checks vertical wins
            if (_board[0] == playerMarker && _board[1] == playerMarker && _board[2] == playerMarker) { return GameState.Win; };
            if (_board[3] == playerMarker && _board[4] == playerMarker && _board[5] == playerMarker) { return GameState.Win; };
            if (_board[6] == playerMarker && _board[7] == playerMarker && _board[8] == playerMarker) { return GameState.Win; };

            //Checks horizontal wins
            if (_board[0] == playerMarker && _board[3] == playerMarker && _board[6] == playerMarker) { return GameState.Win; };
            if (_board[1] == playerMarker && _board[4] == playerMarker && _board[7] == playerMarker) { return GameState.Win; };
            if (_board[2] == playerMarker && _board[5] == playerMarker && _board[8] == playerMarker) { return GameState.Win; };

            //Checks diagonal wins
            if (_board[0] == playerMarker && _board[4] == playerMarker && _board[8] == playerMarker) { return GameState.Win; };
            if (_board[6] == playerMarker && _board[4] == playerMarker && _board[2] == playerMarker) { return GameState.Win; };

            //Check for draws
            var isDrawGameState = CheckDrawGameState();
            if (isDrawGameState) { return GameState.Draw; }

            return GameState.Ongoing;
        }

        /// <summary>
        /// Checks if there is a draw. I.e. all boardspaces are covered.
        /// </summary>
        /// <returns>
        /// *True if board is full. 
        /// *False if its not.
        /// </returns>
        private bool CheckDrawGameState()
        {
            for (var i = 0; i < 9; i++)
            {
                if (_board[i] == null)
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
