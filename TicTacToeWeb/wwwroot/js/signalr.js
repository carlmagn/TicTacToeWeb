const connection = new signalR.HubConnectionBuilder()
    .withUrl("/playhub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
        findGame();
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(start);

// Start the connection.
start();

async function findGame() {
    try {
        await connection.invoke("FindGame");
    } catch (err) {
        console.error(err);
    }
}

async function leaveQueue() {
    try {
        await connection.invoke("LeaveQueue");
        window.location.href = '/';
    } catch (err) {
        console.error(err);
    }
}

async function sendMove(move) {
    try {
        await connection.invoke("MakeMove", move);
    } catch (err) {
        console.error(err);
    }
}

connection.on("ReceiveMessage", (user, message) => {
    console.log(`User: ${user}, \n Message: ${message}`);
});

connection.on("WaitingForGame", () => {
    console.log('Waiting for game');
    showLoader();
});

connection.on("FoundGame", (isMyTurn) => {
    console.log('Found game');
    setPlayerTurn(isMyTurn);
    showGame();
});

connection.on("LeftQueue", () => {
    console.log('You have left queue');
});

connection.on("OpponentDisconnected", () => {
    setStatusText('Opponent left')
});

connection.on("ReceiveMove", (player, move) => {
    makeMove(player, move);
});

connection.on("Won", (playerMarker, winningRow) => {
    animateWinningRow(playerMarker, winningRow);
    playerWon();
});

connection.on("Lost", (playerMarker, winningRow) => {
    animateWinningRow(playerMarker, winningRow);
    playerLost();
});

connection.on("Draw", () => {
    playersTied();
});

connection.on("Ongoing", () => {
    switchPlayer();
});

connection.on("ResetGame", () => {
    resetGame();
});