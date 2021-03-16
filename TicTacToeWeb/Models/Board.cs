namespace TicTacToeWeb.Models
{
    public class Board
    {
        public string[] BoardState { get; set; }

        public Board()
        {
            BoardState = new string[9];
        }

        public bool CheckLegalMove(int move)
        {
            return BoardState[move] == null ? true : false;
        }

        public void MakeMove(string playerMarker, int move)
        {
            BoardState[move] = playerMarker;
        }
    }
}
