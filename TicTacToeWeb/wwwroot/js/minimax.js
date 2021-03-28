// https://medium.com/@alialaa/tic-tac-toe-with-javascript-es2015-ai-player-with-minimax-algorithm-59f069f46efa

var maxDepth = 3;
var nodeMap = new Map();

function minimax(board, depth, maximizing) {
    if (depth === 0) {
        nodeMap.clear();
        console.log(maxDepth);
    }
    let availableMoves = getAvailableMoves(board);
    let gameHasEnded = checkEndOfGame(board, availableMoves)
    if (gameHasEnded) {
        return getScore(depth, board);
    }
    if (depth === maxDepth) {
        return -10;
    }

    if (maximizing) {
        let bestScore = -100;

        availableMoves.forEach(move => {
            let nodeBoard = board.slice();
            nodeBoard[move] = 'O';

            let nodeValue = minimax(nodeBoard, depth + 1, false)
            bestScore = Math.max(bestScore, nodeValue);

            if (depth === 0) {
                nodeMap.has(nodeValue) ? nodeMap.get(nodeValue).push(move) : nodeMap.set(nodeValue, [move]);
            }
        });

        if (depth === 0) {
            highestScoringMoves = nodeMap.get(bestScore);
            if (highestScoringMoves.length > 1) {
                highestScoringMoves = findBlockingMoves(highestScoringMoves, board);
                let randomIndex = Math.floor(Math.random() * highestScoringMoves.length);
                return highestScoringMoves[randomIndex];
            } else {
                return highestScoringMoves[0];
            }
        }

        return bestScore;
    }
    else {
        let bestScore = 100;

        availableMoves.forEach(move => {
            let nodeBoard = board.slice();
            nodeBoard[move] = 'X';

            let nodeValue = minimax(nodeBoard, depth + 1, true)
            bestScore = Math.min(bestScore, nodeValue);

            if (depth === 0) {
                nodeMap.has(nodeValue) ? nodeMap.get(nodeValue).push(move) : nodeMap.set(nodeValue, [move]);
            }
        });

        return bestScore;
    }
}

function getAvailableMoves(board) {
    let availableMoves = [];

    for (i = 0; i < board.length; i++) {
        if (board[i] === '') {
            availableMoves.push(i);
        }
    }

    return availableMoves;
}

function getScore(depth, board) {
    let playerXHasWon = checkPlayerWin('X', board);
    if (playerXHasWon) { return -100 + depth }

    let playerOHasWon = checkPlayerWin('O', board);
    if (playerOHasWon) { return 100 - depth }

    return 0;
}

function checkEndOfGame(board, availableMoves) {
    return checkPlayerWin('X', board) || checkPlayerWin('O', board) || availableMoves.length === 0;
}

function checkPlayerWin(player, board) {
    //Horizontal wins
    if (board[0] === player && board[1] === player && board[2] === player) { return true; }
    if (board[3] === player && board[4] === player && board[5] === player) { return true; }
    if (board[6] === player && board[7] === player && board[8] === player) { return true; }

    //Vertical wins
    if (board[0] === player && board[3] === player && board[6] === player) { return true; }
    if (board[1] === player && board[4] === player && board[7] === player) { return true; }
    if (board[2] === player && board[5] === player && board[8] === player) { return true; }

    //Diagonal wins
    if (board[0] === player && board[4] === player && board[8] === player) { return true; }
    if (board[6] === player && board[4] === player && board[2] === player) { return true; }

    return false;
}

function findBlockingMoves(moves, board) {
    let blockingMoves = [];
    moves.forEach(move => {
        let testBoard = board.slice();
        testBoard[move] = 'X';
        let playerXHasWon = checkPlayerWin('X', testBoard);
        if (playerXHasWon) {
            blockingMoves.push(move);
        }
    });

    return blockingMoves.length > 1 ? blockingMoves : moves;
}