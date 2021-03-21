using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeWeb.Models;
using static TicTacToeWeb.Models.Game;

namespace TicTacToeWeb
{
    /// <summary>
    /// Handles matchmaking and communication between the two players. 
    /// Based on https://github.com/splttingatms/TicTacToe.
    /// </summary>
    public class PlayHub : Hub
    {
        public static IDictionary<Game, Player[]> currentGames = new Dictionary<Game, Player[]>();
        public static Queue<Player> gameQueue = new Queue<Player>();

        public async Task FindGame()
        {
            var joiningPlayer = new Player(Context.ConnectionId);

            if (gameQueue.TryDequeue(out Player waitingPlayer))
            {
                await CreateGame(waitingPlayer, joiningPlayer);
                var game = FindGameFromPlayerId(waitingPlayer.playerId);

                await Clients.Client(joiningPlayer.playerId).SendAsync("FoundGame", !game.IsPlayer1sTurn);
                await Clients.Client(waitingPlayer.playerId).SendAsync("FoundGame", game.IsPlayer1sTurn);
            }
            else
            {
                gameQueue.Enqueue(joiningPlayer);
                await Clients.Caller.SendAsync("WaitingForGame");
            }
        }

        private async Task CreateGame(Player player1, Player player2)
        {
            var game = new Game(player1, player2);

            currentGames.Add(game, new Player[] { player1, player2 });

            await Groups.AddToGroupAsync(player1.playerId, game.GameId);
            await Groups.AddToGroupAsync(player2.playerId, game.GameId);
        }

        public async Task LeaveQueue()
        {
            var leavingPlayer = Context.ConnectionId;
            gameQueue = new Queue<Player>(gameQueue.Where(x => x.playerId != leavingPlayer));
            await Clients.Caller.SendAsync("LeftQueue");
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            var leavingPlayer = Context.ConnectionId;
            var game = FindGameFromPlayerId(leavingPlayer);
            currentGames.Remove(game);
            await Groups.RemoveFromGroupAsync(leavingPlayer, game.GameId);
            await Clients.Group(game.GameId).SendAsync("OpponentDisconnected");
        }

        private Game FindGameFromPlayerId(string playerId)
        {
            var game = currentGames.Where(x => x.Value[0].playerId == playerId || x.Value[1].playerId == playerId).First();

            return game.Key;
        }

        private Player GetPlayerFromPlayerId(string playerId)
        {
            var game = FindGameFromPlayerId(playerId);

            var player = game.Player1.playerId == playerId ? game.Player1 : game.Player2;

            return player;
        }

        public async Task MakeMove(string move)
        {
            var playerId = Context.ConnectionId;
            var game = FindGameFromPlayerId(playerId);
            var isPlayersTurn = CheckPlayersTurn(playerId, game);

            if (!isPlayersTurn || game.IsGameOver)
            {
                return;
            }

            if (!Int32.TryParse(move, out var moveInt))
            {
                Console.WriteLine($"Error occured in class {nameof(PlayHub)}, method {nameof(MakeMove)}: could not convert {move} to {nameof(Int32)}");
            }

            var isLegalMove = game.Board.CheckLegalMove(moveInt); 

            if (!isLegalMove)
            {
                return;
            }

            var player = GetPlayerFromPlayerId(playerId);
            game.Board.MakeMove(player.playerMarker, moveInt);
            await Clients.Group(game.GameId).SendAsync("ReceiveMove", player.playerMarker, move);

            await UpdateGameState(game, player);
        }

        private async Task UpdateGameState(Game game, Player player)
        {
            var gameState = game.CheckGameState(player.playerMarker);

            switch (gameState)
            {
                case GameState.Win:
                    var opponentId = game.Player1.playerId == player.playerId ? game.Player2.playerId : game.Player1.playerId;
                    game.IsGameOver = true;
                    await Clients.Client(player.playerId).SendAsync("Won", player.playerMarker, game.WinningRow);
                    await Clients.Client(opponentId).SendAsync("Lost", player.playerMarker, game.WinningRow);
                    break;
                case GameState.Draw:
                    game.IsGameOver = true;
                    await Clients.Group(game.GameId).SendAsync("Draw");
                    break;
                case GameState.Ongoing:
                    game.SwitchPlayer();
                    await Clients.Group(game.GameId).SendAsync("Ongoing");
                    break;
            }
        }

        private bool CheckPlayersTurn(string playerId, Game game)
        {
            return (game.IsPlayer1sTurn && playerId == game.Player1.playerId) || 
                (!game.IsPlayer1sTurn && playerId == game.Player2.playerId);
        }
    }
}
