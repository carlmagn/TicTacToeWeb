# TicTacToeWeb

## Description
A website where you can play Tic-Tac-Toe against the computer or against another player. The computer can be played on different difficulties using the minimax algorithm. 
Player versus player is orchestrated with signalR.

## Setup
The website can be run in iisexpress directly in visual studio or you can run it as a docker container.

**Run as docker container:**
* Clone the git repository to preferred path, `yourpath`.
* Navigate to `yourpath/TicTacToeWeb` in powershell.
* Build the docker image `docker build -t tictactoe:latest .`
* Start a container based on recently built image `docker run -d -p 8080:80 --name tictactoe tictactoe:latest`. This command will start a docker container called tictactoe in the background which can be reached on `localhost:8080`. *Note: You can stop and start the docker container with `docker stop tictactoe` and `docker start tictactoe`.*
* Go to `localhost:8080` and try it out.

## Cleanup

**Cleanup docker container**
* If you want to remove the container and image you first have to stop the container using `docker stop tictactoe`.
* then remove the container using `docker rm tictactoe`.
* lastly remove the image using `docker rmi tictactoe:latest`