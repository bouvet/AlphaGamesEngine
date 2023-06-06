import {camera, direction, intersectPoint, marker, mouse, plane, raycaster} from "./../Rendering.ts";
import {connection} from "../SignalR/Functions.ts";

export function startInputHandler() {
    setInterval(() => {
        if (keys.down || keys.up || keys.left || keys.right) {
            sendKeyboardEvent();
        }
    }, 100);
}

// Create a map of keys to key codes
export let keyCodes: { [key: string]: number[] } = {
    left: [37, 65], // left arrow, 'A'
    right: [39, 68], // right arrow, 'D'
    up: [38, 87], // up arrow, 'W'
    down: [40, 83] // down arrow, 'S'
};

export let keys: { [key: string]: boolean } = {
    left: false,
    right: false,
    up: false,
    down: false
};

// Function to check if a key code is mapped to a key
function isKeyCodeMappedToKey(keyCode: number, key: string): boolean {
    return keyCodes[key].includes(keyCode);
}

// Listen for keydown events
export function onDocumentKeyDown(event: { which: any, key: string, keyCode: number }) {
    for (let key in keys) {
        if (isKeyCodeMappedToKey(event.keyCode, key)) {
            keys[key] = true;
        }
    }
}

// Listen for keyup events
export function onDocumentKeyUp(event: { which: any, key: string, keyCode: number }) {
    for (let key in keys) {
        if (isKeyCodeMappedToKey(event.keyCode, key)) {
            keys[key] = false;
        }
    }
}

let mouseX : number = 0, mouseY : number = 0;

export function onMouseMove(event: { clientX: number; clientY: number }) {
    mouseX = event.clientX;
    mouseY = event.clientY;
    mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
    mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
    raycaster.setFromCamera(mouse, camera);
    raycaster.ray.intersectPlane(plane, intersectPoint);
    marker.position.copy(intersectPoint);
}

export function sendKeyboardEvent() {
    connection.send("SendMessage", JSON.stringify({Type: "MovePlayer", x: direction.x, y: direction.y, z: direction.z}));
    onMouseMove({clientX: mouseX, clientY: mouseY});
}

export function sendMouseEvent(MousePositionX: number, MousePositionY: number) {
    connection.send("SendMessage", JSON.stringify({Type: "RotateGameObject", MousePositionX: MousePositionX, MousePositionY: MousePositionY}));
    onMouseMove({clientX: MousePositionX, clientY: MousePositionY});
}