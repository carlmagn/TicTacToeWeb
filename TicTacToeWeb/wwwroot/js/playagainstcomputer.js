let board = ['', '', '', '', '', '', '', '', '']
let gameOver = false;
let computerGoesFirst = false;


$(function () {
    $('td').click(function () { putPlayerMarker(this.id) });
});

function putPlayerMarker(id) {
    isLegitMove = board[id] === '' ? true : false;

    if (!isLegitMove || gameOver) {
        return;
    }
    makeMove('X', id)

    let { player, playerHasWon, winningRow } = checkWinningState('X');
    if (playerHasWon) {
        animateWinningRow(player, winningRow)
        gameOver = true;
    }
    else if (checkDraw()) {
        gameOver = true;
    }
    else {
        putComputerMarker();
    }
}

function putComputerMarker() {
    let move = minimax(board, 0, true);
    makeMove('O', move)

    let { player, playerHasWon, winningRow } = checkWinningState('O');
    if (playerHasWon) {
        animateWinningRow(player, winningRow)
        gameOver = true;
    }
    else if (checkDraw()) {
        gameOver = true;
    }

    return;
}

function makeMove(player, move) {
    $(`#${move}`).html(`<img src="/img/TicTacToe${player}.png" class="marker-img"/>`);
    board[move] = player;
}

function checkWinningState(player) {
    //Horizontal wins
    if (board[0] == player && board[1] == player && board[2] == player) {
        return {player: player,  playerHasWon: true, winningRow: [0, 1, 2]};
    };
    if (board[3] == player && board[4] == player && board[5] == player) {
        return { player: player, playerHasWon: true, winningRow: [3, 4, 5] };
    };
    if (board[6] == player && board[7] == player && board[8] == player) {
        return { player: player, playerHasWon: true, winningRow: [6, 7, 8] };
    };

    //Vertical wins
    if (board[0] == player && board[3] == player && board[6] == player) {
        return { player: player, playerHasWon: true, winningRow: [0, 3, 6] };
    };
    if (board[1] == player && board[4] == player && board[7] == player) {
        return { player: player, playerHasWon: true, winningRow: [1, 4, 7] };
    };
    if (board[2] == player && board[5] == player && board[8] == player) {
        return { player: player, playerHasWon: true, winningRow: [2, 5, 8] };
    };

    //Diagonal wins
    if (board[0] == player && board[4] == player && board[8] == player) {
        return { player: player, playerHasWon: true, winningRow: [0, 4, 8] };
    };
    if (board[6] == player && board[4] == player && board[2] == player) {
        return { player: player, playerHasWon: true, winningRow: [6, 4, 2] };
    };

    return { playerHasWon: false };
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

    computerGoesFirst = !computerGoesFirst;
    gameOver = false;

    if (computerGoesFirst) {
        putComputerMarker();
    }
}

function animateWinningRow(player, winningRow) {
    player === 'X'
        ? $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).addClass('rotate')
        : $(`#${winningRow[0]}, #${winningRow[1]}, #${winningRow[2]}`).fadeOut(150).fadeIn(150).fadeOut(150).fadeIn(150);
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
            maxDepth = 9;
            break;
        default:
            maxDepth = 3;
            console.log(`Didn't recognize difficulty: ${difficulty}. Set medium difficulty.`);
    }
}