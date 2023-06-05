import './style.css';
import {
    onDocumentKeyDown,
    onDocumentKeyUp,
    render,
    sendKeyboardEvent,
    sendMouseEvent,
    shootBeam
} from './Events/Functions';
import {startSignalR} from './SignalR/Functions';


window.addEventListener("mousemove", (event) => {
    sendMouseEvent(event.clientX, event.clientY);
});
document.addEventListener("keydown", (event) => {
    //sendKeyboardEvent(event.key)
});
document.addEventListener("keydown", onDocumentKeyDown, false);
document.addEventListener("keyup", onDocumentKeyUp, false);
document.addEventListener('mousedown', function (event) {
    if (event.button === 0) {
        shootBeam();
    }
});

render();
startSignalR();
