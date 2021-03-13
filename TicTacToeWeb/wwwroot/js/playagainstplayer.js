var board = ['', '', '', '', '', '', '', '', '']
var isMyTurn;

$(function () {
    $('td').click(function () { sendMove(this.id) });
});

function showGame() {
    $('.loader-container').hide();
    $('.game-container').show();
}

function showLoader() {
    $('.game-container').hide();
    $('.loader-container').show();
}

function makeMove(player, move) {
    $(`#${move}`).html(`<img src="/img/TicTacToe${player}.png" class="marker-img"/>`);
    board[move] = player;
    switchPlayer();
}

function setPlayerTurn(isMyTurn) {
    this.isMyTurn = isMyTurn;

    showPlayerTurn();
}

function showPlayerTurn() {
    if (isMyTurn) {
        $('#your-turn').show();
        $('#opponents-turn').hide();
    } else {
        $('#your-turn').hide();
        $('#opponents-turn').show();
    }
}

function switchPlayer() {
    isMyTurn = !isMyTurn;
    showPlayerTurn();
}

// Fix wincondition
function animateWinningRow(player, winningRow) {
    player === 'X' ?
        $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).addClass('rotate') :
        $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).fadeOut(150).fadeIn(150).fadeOut(150).fadeIn(150);
}