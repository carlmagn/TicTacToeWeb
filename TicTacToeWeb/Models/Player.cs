namespace TicTacToeWeb.Models
{
    public class Player
    {
        public Player(string id)
        {
            playerId = id;
        }

        /// <summary>
        /// Which marker used to represent the player. Possible values: X, O.
        /// </summary>
        public string playerMarker { get; set; }

        /// <summary>
        /// Id used to identify the player.
        /// </summary>
        public string playerId { get; set; }

        /// <summary>
        /// Id used to identify which game the player is in.
        /// </summary>
        public string gameId { get; set; }

    }
}
