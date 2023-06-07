
import {onDocumentKeyDown, onDocumentKeyUp, sendMouseEvent, startInputHandler} from "./Events/InputHandler.ts";
import {AddDispatchHandlers} from "./Communication/DispatchHandlers.ts";
import {render} from "./Rendering.ts";
import {AddTypeHandlers} from "./ObjectTypeHandler.ts";
import {Communication, ICommunication} from "./Communication/Communication.ts";
import {Dispatcher} from "./Communication/Dispatcher.ts";
import {ICommunicationStrategy, SignalRCommunicationStrategy} from "./Communication/CommunicationStrategy.ts";

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

render();
AddTypeHandlers();
startInputHandler();