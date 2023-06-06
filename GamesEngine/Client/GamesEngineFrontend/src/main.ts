
import {onDocumentKeyDown, onDocumentKeyUp, sendMouseEvent, startInputHandler} from "./Events/InputHandler.ts";
import {AddDispatchHandlers} from "./Communication/DispatchHandlers.ts";
import {render} from "./Rendering.ts";
import {AddTypeHandlers} from "./ObjectTypeHandler.ts";
import {ICommunication, SignalRCommunication} from "./Communication/Communication.ts";
import {Dispatcher} from "./Communication/Dispatcher.ts";

export const dispatcher = new Dispatcher();
AddDispatchHandlers();

export const communication: ICommunication = new SignalRCommunication(dispatcher);
communication.init();

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
AddTypeHandlers();
startInputHandler();