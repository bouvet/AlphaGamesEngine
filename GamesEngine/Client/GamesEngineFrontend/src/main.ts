
import {startSignalR} from './SignalR/Functions';
import {onDocumentKeyDown, onDocumentKeyUp, sendMouseEvent, startInputHandler} from "./Events/InputHandler.ts";
import {AddDispatchHandlers} from "./DispatchHandlers.ts";
import {render} from "./Rendering.ts";
import {AddTypeHandlers} from "./ObjectTypeHandler.ts";


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

render();
startSignalR();
AddDispatchHandlers();
AddTypeHandlers();
startInputHandler();