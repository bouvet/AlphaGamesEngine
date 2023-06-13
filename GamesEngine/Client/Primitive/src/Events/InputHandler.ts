import { communication } from "../../main.ts";

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

// let mouseX : number = 0, mouseY : number = 0;

let direction = { x: 0, y: 0 };

// export function onMouseMove(event: { clientX: number; clientY: number }) {
//     mouseX = event.clientX;
//     mouseY = event.clientY;
//     // mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
//     // mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
//     // raycaster.setFromCamera(mouse, camera);
//     // raycaster.ray.intersectPlane(plane, intersectPoint);
//     // marker.position.copy(intersectPoint);
// }

// Update the direction vector based on the keys that are pressed
export function updateDirection() {
    direction = { x: 0, y: 0 };
    if (keys.left) direction.x -= 1;
    if (keys.right) direction.x += 1;
    if (keys.up) direction.y -= 1;
    if (keys.down) direction.y += 1;

    // Normalize the direction vector
    if (vectorLength([direction.x, direction.y]) > 0) {
        vectorNormalize([direction.x, direction.y]);
    }
}

function vectorLength(vector: number[]): number {
    let sum = 0;
    for (let component of vector) {
        sum += component * component;
    }
    return Math.sqrt(sum);
}

function vectorNormalize(vector: number[]): number[] {
    let length = vectorLength(vector);
    return vector.map(component => component / length);
}

export function sendKeyboardEvent() {
    communication.SendToServer({ Type: "MovePlayer", x: direction.x, y: direction.y });
    // onMouseMove({clientX: mouseX, clientY: mouseY});
}

// export function sendMouseEvent(MousePositionX: number, MousePositionY: number) {
//     communication.SendToServer({Type: "RotateGameObject", MousePositionX: MousePositionX, MousePositionY: MousePositionY});
//     onMouseMove({clientX: MousePositionX, clientY: MousePositionY});
// }