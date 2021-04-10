var board = ['', '', '', '', '', '', '', '', '']
var isMyTurn;
var didStart;

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
}

function setPlayerTurn(isMyTurn) {
    this.isMyTurn = isMyTurn;
    this.didStart = isMyTurn;

    setStatusText(isMyTurn ? 'Your turn' : 'Opponents turn');
}

function playerWon() {
    setStatusText('You won!');

    let score = parseInt($('#player-score').text());
    $('#player-score').text(++score);
}

function playerLost() {
    setStatusText('You lost!');

    let score = parseInt($('#opponent-score').text());
    $('#opponent-score').text(++score);
}

function playersTied() {
    setStatusText('Tied!');

    let score = parseInt($('#tie-score').text());
    $('#tie-score').text(++score);
}

function resetGame() {
    board = ['', '', '', '', '', '', '', '', '']

    $('.marker-img').remove();
    $('td').removeClass('rotate');

    isMyTurn = !didStart;
    didStart = !didStart;
    setStatusText(isMyTurn ? 'Your turn' : 'Opponents turn');
}

function setStatusText(text) {
    $('#status-text').text(text);
}

function switchPlayer() {
    isMyTurn = !isMyTurn;
    setStatusText(isMyTurn ? 'Your turn' : 'Opponents turn');
}

// Fix wincondition
function animateWinningRow(player, winningRow) {
    player === 'X' ?
        $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).addClass('rotate') :
        $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).fadeOut(150).fadeIn(150).fadeOut(150).fadeIn(150);
}