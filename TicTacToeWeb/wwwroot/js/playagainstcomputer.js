// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var board = ['', '', '', '', '', '', '', '', '']
var gameOver = false;

$(function () {
    $('td').click(function () { putPlayerMarker(this.id) });
});

function putPlayerMarker(id) {
    isLegitMove = board[id] === '' ? true : false;

    if (!isLegitMove || gameOver) {
        return;
    }
    makeMove('X', id)

    if (checkPlayerWinWithAnimation('X')) {
        gameOver = true;
        console.log('X won');
    }
    else if (checkDraw()) {
        gameOver = true;
        console.log('Draw');
    }
    else {
        putComputerMarker();
    }
}

function putComputerMarker() {
    //var move = findMove();
    var move = minimax(board, 0, true);
    console.log(nodeMap);
    makeMove('O', move)

    if (checkPlayerWinWithAnimation('O')) {
        gameOver = true;
        console.log('O won');
    }
    else if (checkDraw()) {
        gameOver = true;
        console.log('Draw');
    }

    return;
}

function findMove() {
    if (board[i] === '') {
        return i;
    }
}

function makeMove(player, move) {
    $(`#${move}`).html(`<img src="/img/TicTacToe${player}.png" class="marker-img"/>`);
    board[move] = player;
}

function checkPlayerWinWithAnimation(player) {
    //Horizontal wins
    if (board[0] == player && board[1] == player && board[2] == player) {
        animateWinningRow(player, [0, 1, 2]);
        return true;
    };
    if (board[3] == player && board[4] == player && board[5] == player) {
        animateWinningRow(player, [3, 4, 5]);
        return true;
    };
    if (board[6] == player && board[7] == player && board[8] == player) {
        animateWinningRow(player, [6, 7, 8]);
        return true;
    };

    //Vertical wins
    if (board[0] == player && board[3] == player && board[6] == player) {
        animateWinningRow(player, [0, 3, 6]);
        return true;
    };
    if (board[1] == player && board[4] == player && board[7] == player) {
        animateWinningRow(player, [1, 4, 7]);
        return true;
    };
    if (board[2] == player && board[5] == player && board[8] == player) {
        animateWinningRow(player, [2, 5, 8]);
        return true;
    };

    //Diagonal wins
    if (board[0] == player && board[4] == player && board[8] == player) {
        animateWinningRow(player, [0, 4, 8]);
        return true;
    };
    if (board[6] == player && board[4] == player && board[2] == player) {
        animateWinningRow(player, [6, 4, 2]);
        return true;
    };

    return false;
}

function checkDraw() {
    for (i = 0; i < board.length; i++) {
        if (board[i] == '') {
            return false;
        }
    }

    return true;
}

function resetGame() {
    board = ['', '', '', '', '', '', '', '', '']

    $('.marker-img').remove();
    $('td').removeClass('rotate');

    gameOver = false;
}

function animateWinningRow(player, winningRow) {
    player === 'X' ?
        $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).addClass('rotate') :
        $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).fadeOut(150).fadeIn(150).fadeOut(150).fadeIn(150);
}

function selectDifficulty(difficulty) {
    $('.selected').removeClass('selected');
    $(`#${difficulty}`).addClass('selected');

    switch (difficulty) {
        case 'easy':
            maxDepth = 1;
            break;
        case 'medium':
            maxDepth = 3;
            break;
        case 'hard':
            maxDepth = 5;
            break;
        case 'carl':
            maxDepth = 7;
            break;
        default:
            maxDepth = 3;
            console.log(`Didn't recognize difficulty: ${difficulty}. Set medium difficulty.`);
    }
}