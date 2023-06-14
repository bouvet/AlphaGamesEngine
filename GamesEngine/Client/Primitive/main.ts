import "./node_modules/bootstrap/dist/js/bootstrap.bundle.min.js";
import "./node_modules/bootstrap/dist/css/bootstrap.min.css";
import { AddDispatchHandlers } from './src/Communication/DispatchHandlers.ts';
import { Communication, ICommunication } from './src/Communication/Communication.ts';
import { ClientDispatcher } from './src/Communication/ClientDispatcher.ts';
import { ICommunicationStrategy, SignalRCommunicationStrategy } from './src/Communication/CommunicationStrategy.ts';
import { startInputHandler, updateDirection, onDocumentKeyDown, onDocumentKeyUp } from './src/Events/InputHandler.ts';
import { TypeHandler } from './src/ObjectTypeHandler.ts';

export const gameCanvas = document.getElementById('myCanvas') as HTMLCanvasElement;

export const strategy: ICommunicationStrategy = new SignalRCommunicationStrategy();
export const clientDispatcher = new ClientDispatcher();
AddDispatchHandlers();

export const communication: ICommunication = new Communication(clientDispatcher, strategy);

await communication.Init();

communication.SendToServer({ Type: "FetchDynamicObjects" });
communication.SendToServer({ Type: "FetchStaticObjects" });

setInterval(() => {
    communication.SendToServer({ Type: "FetchDynamicObjects" });
}, 100);

// window.addEventListener("mousemove", (event) => {
//     sendMouseEvent(event.clientX, event.clientY);
// });
document.addEventListener("keydown", onDocumentKeyDown, false);
document.addEventListener("keyup", onDocumentKeyUp, false);
document.addEventListener('mousedown', function (event) {
    if (event.button === 0) {
        //shootBeam();
    }
});

function animate() {
    requestAnimationFrame(animate);
    updateDirection();
    gameCanvas.getContext('2d')?.clearRect(0, 0, gameCanvas.width, gameCanvas.height);
}
export const typeHandler = new TypeHandler();
typeHandler.AddDynamicTypeHandlers();
startInputHandler();
animate();


