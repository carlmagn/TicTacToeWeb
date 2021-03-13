using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeWeb.Models;
using static TicTacToeWeb.Models.Board;

namespace TicTacToeWeb
{
    public class PlayHub : Hub
    {
        public static IDictionary<Game, Player[]> currentGames = new Dictionary<Game, Player[]>();
        public static Queue<Player> gameQueue = new Queue<Player>();

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task FindGame()
        {
            var joiningPlayer = new Player(Context.ConnectionId);
            Player waitingPlayer;

            if (gameQueue.TryDequeue(out waitingPlayer))
            {
                await CreateGame(waitingPlayer, joiningPlayer);
                var game = FindGameFromPlayerId(waitingPlayer.playerId);

                await Clients.Client(joiningPlayer.playerId).SendAsync("FoundGame", !game.isPlayer1sTurn);
                await Clients.Client(waitingPlayer.playerId).SendAsync("FoundGame", game.isPlayer1sTurn);
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

            await Groups.AddToGroupAsync(player1.playerId, game.gameId);
            await Groups.AddToGroupAsync(player2.playerId, game.gameId);
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
            await Groups.RemoveFromGroupAsync(leavingPlayer, game.gameId);
            await Clients.Group(game.gameId).SendAsync("OpponentDisconnected");
        }

        private Game FindGameFromPlayerId(string playerId)
        {
            var game = currentGames.Where(x => x.Value[0].playerId == playerId || x.Value[1].playerId == playerId).First();

            return game.Key;
        }

        private Player GetPlayerFromPlayerId(string playerId)
        {
            var game = FindGameFromPlayerId(playerId);

            var player = game.player1.playerId == playerId ? game.player1 : game.player2;

            return player;
        }

        public async Task MakeMove(string move)
        {
            var playerId = Context.ConnectionId;
            var game = FindGameFromPlayerId(playerId);
            var isPlayersTurn = checkPlayersTurn(playerId, game);

            if (!isPlayersTurn || game.isGameOver)
            {
                return;
            }

            if (!Int32.TryParse(move, out var moveInt))
            {
                Console.WriteLine($"Error occured in class {nameof(PlayHub)}, method {nameof(MakeMove)}: could not convert {move} to {nameof(Int32)}");
            }

            var legalMove = game.board.CheckLegalMove(moveInt); 

            if (!legalMove)
            {
                await Clients.Caller.SendAsync("IllegalMove");
                return;
            }

            var player = GetPlayerFromPlayerId(playerId);
            game.board.MakeMove(player.playerMarker, moveInt);
            var gameState = game.board.CheckGameState(player.playerMarker);

            if (gameState == GameState.Win)
            {
                game.isGameOver = true;
                await Clients.Group(game.gameId).SendAsync("Won");
            }
            if (gameState == GameState.Draw)
            {
                game.isGameOver = true;
                await Clients.Group(game.gameId).SendAsync("Draw");
            }

            game.SwitchPlayer();

            await Clients.Group(game.gameId).SendAsync("ReceiveMove", player.playerMarker, move);
        }

        private bool checkPlayersTurn(string playerId, Game game)
        {
            return (game.isPlayer1sTurn && playerId == game.player1.playerId) || 
                (!game.isPlayer1sTurn && playerId == game.player2.playerId);
        }
    }
}
