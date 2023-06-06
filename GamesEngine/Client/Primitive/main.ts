import "./node_modules/bootstrap/dist/js/bootstrap.bundle.min.js";
import "./node_modules/bootstrap/dist/css/bootstrap.min.css";
// import { startSignalR, sendMouseEvent, onDocumentKeyDown, onDocumentKeyUp, } from './src/SignalR/SignalR';
import { AddDispatchHandlers } from './src/Communication/DispatchHandlers';
import { Communication, ICommunication } from './src/Communication/Communication';
import { Dispatcher } from './src/Communication/Dispatcher';
import { ICommunicationStrategy, SignalRCommunicationStrategy } from './src/Communication/CommunicationStrategy';
import { startInputHandler, updateDirection, onDocumentKeyDown, onDocumentKeyUp, sendMouseEvent } from './src/Events/InputHandler';


const canvas = document.getElementById('myCanvas') as HTMLCanvasElement | null;

export const strategy: ICommunicationStrategy = new SignalRCommunicationStrategy();
export const dispatcher = new Dispatcher();
AddDispatchHandlers();

export const communication: ICommunication = new Communication(dispatcher, strategy);
await communication.Init();

communication.SendToServer({Type: "FetchDynamicObjects"});
communication.SendToServer({Type: "FetchStaticObjects"});

setInterval(() => {
    communication.SendToServer({Type: "FetchDynamicObjects"});
}, 100);

window.addEventListener("mousemove", (event) => {
    sendMouseEvent(event.clientX, event.clientY);
});
document.addEventListener("keydown", onDocumentKeyDown, false);
document.addEventListener("keyup", onDocumentKeyUp, false);
document.addEventListener('mousedown', function (event) {
    if (event.button === 0) {
        //shootBeam();
    }
});

startInputHandler();

if (canvas) {
  const context = canvas.getContext('2d');
  
  let playerX = 100;
  let playerY = 100;

  const size = 10;
  function drawSquare() {
      
    if (context && canvas) {
      // Clear the previous frame
      context.clearRect(0, 0, canvas.width, canvas.height);

      context.beginPath();
      context.rect(playerX, playerY, size, size);
      context.fillStyle = "#FF0000";
      context.fill();
      context.closePath();
    }
  }

  function animate() {
    drawSquare();
    updateDirection();
    requestAnimationFrame(animate);
  }
  animate();
}

