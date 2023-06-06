import "./node_modules/bootstrap/dist/js/bootstrap.bundle.min.js";
import "./node_modules/bootstrap/dist/css/bootstrap.min.css";

import * as signalR from "@microsoft/signalr";

export const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
            }).build();


export async function startSignalR() {
  connection.start()
            .then(() => {
              console.log("SignalR connected");
              let message = {Type: "FetchDynamicObjects"}
              connection.send("SendMessage", JSON.stringify(message));
            }).catch((error) => console.log(error));
}

const canvas = document.getElementById('myCanvas') as HTMLCanvasElement | null;
if (canvas) {
  const context = canvas.getContext('2d');
  
    let playerStartX = 100;
    let playerStartY = 100;

    function onDocumentKeyDown(event: KeyboardEvent) {};
    function onDocumentKeyUp(event: KeyboardEvent) {};

    function keyDownHandler(event: KeyboardEvent) {
      if (event.key === 'Right' || event.key === 'ArrowRight') {
          // Left arrow
          playerStartX += 1;
      } else if (event.key === 'Left' || event.key === 'ArrowLeft') {
          // Right arrow
          playerStartX -= 1;
      } else if (event.key === 'Up' || event.key === 'ArrowUp') {
          // Up arrow
          playerStartY -= 1;
      } else if (event.key === 'Down' || event.key === 'ArrowDown') {
          // Down arrow
          playerStartY += 1;
      }
  }
    document.addEventListener("keydown", onDocumentKeyDown, false);
    document.addEventListener("keyup", onDocumentKeyUp, false);
    document.addEventListener('keydown', (event) => {
      keyDownHandler(event);
    }, false);


    const size = 10;
    function drawSquare() {
        
        if (context && canvas) {
            // Clear the previous frame
            context.clearRect(0, 0, canvas.width, canvas.height);

            context.beginPath();
            context.rect(playerStartX, playerStartY, size, size);
            context.fillStyle = "#FF0000";
            context.fill();
            context.closePath();
        }
    }

    function animate() {
      drawSquare();
      requestAnimationFrame(animate);
    }

    animate();
    startSignalR();
  }

