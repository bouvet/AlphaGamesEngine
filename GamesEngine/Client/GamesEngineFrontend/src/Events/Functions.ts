import * as THREE from 'three';
import {Marker} from '../Figures/Marker';
import {Grid} from '../Figures/Grid';
import {AxesHelper} from '../Helpers/AxesHelper';
import {connection} from '../SignalR/Functions';

var camera = new THREE.PerspectiveCamera(
    60,
    window.innerWidth / window.innerHeight,
    1,
    1000
);
camera.position.set(0, 0, 5);
//camera.rotateX(45);
//camera.lookAt(new THREE.Vector3(0, 0, 0));

var scene = new THREE.Scene();
var plane = new THREE.Plane(new THREE.Vector3(0, 0, 1), 0);
var raycaster = new THREE.Raycaster();
var mouse = new THREE.Vector2();
var intersectPoint = new THREE.Vector3();
var beams: THREE.Mesh[] = [];
var cones: THREE.Mesh[] = [];
var staticObjects: THREE.Mesh[] = [];
var marker = Marker(scene);
Grid(scene);
AxesHelper(scene);

setInterval(() => {
    connection.send("SendMessage", JSON.stringify({Type: "FetchDynamicObjects"}));

    if(keys.down || keys.up || keys.left || keys.right){
        sendKeyboardEvent();
    }
}, 100);

var renderer = new THREE.WebGLRenderer({
    antialias: true,
});

renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

export function sendKeyboardEvent() {
    let message = {Type: "MovePlayer", x: direction.x, y: direction.y, z: direction.z}
    connection.send("SendMessage", JSON.stringify(message));
    console.log(`move, ${JSON.stringify(message)}`);

}

export function sendMouseEvent(MousePositionX: number, MousePositionY: number) {
    let message = {Type: "RotateGameObject", MousePositionX: MousePositionX, MousePositionY: MousePositionY}
    connection.send("SendMessage", JSON.stringify(message));
    onMouseMove({clientX: MousePositionX, clientY: MousePositionY});
}

var direction = new THREE.Vector3();

// Create a map of keys to key codes
let keyCodes: { [key: string]: number[] } = {
    left: [37, 65], // left arrow, 'A'
    right: [39, 68], // right arrow, 'D'
    up: [38, 87], // up arrow, 'W'
    down: [40, 83] // down arrow, 'S'
};

let keys: { [key: string]: boolean } = {
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
    for (var key in keys) {
        if (isKeyCodeMappedToKey(event.keyCode, key)) {
            keys[key] = true;
        }
    }
}

// Listen for keyup events
export function onDocumentKeyUp(event: { which: any, key: string, keyCode: number }) {
    for (var key in keys) {
        if (isKeyCodeMappedToKey(event.keyCode, key)) {
            keys[key] = false;
        }
    }
}

// Update the direction vector based on the keys that are pressed
function updateDirection() {
    direction.set(0, 0, 0);
    if (keys.left) direction.x -= 1;
    if (keys.right) direction.x += 1;
    if (keys.up) direction.y += 1;
    if (keys.down) direction.y -= 1;

    // Normalize the direction vector
    if (direction.length() > 0) {
        direction.normalize();
    }
}

let playerId = -1;
function ClientDispatcher(message: any) {
    let content = JSON.parse(message.content);
    let type = message.type;
    if(type === "FetchDynamicObjects"){
        RemoveAllCharacters();
        AddAllCharacters(content);
    }else if(type ==="PlayerId"){
        playerId = content.id;
    }else if(type === "FetchStaticObjects"){
        content.forEach((staticObject: any) => {
            console.log(staticObject);
            var objectGeom = new THREE.BoxGeometry(1, 1, 1);
            var objectMat = new THREE.MeshNormalMaterial();

            objectGeom.translate(0.5, 0.5, 0.5); // pivot point is shifted
            var object = new THREE.Mesh(objectGeom, objectMat);

            object.rotation.x = staticObject.WorldMatrix._matrix.M11;
            object.rotation.y = staticObject.WorldMatrix._matrix.M12;
            object.rotation.z = staticObject.WorldMatrix._matrix.M13;

            object.position.x = staticObject.WorldMatrix._matrix.M41;
            object.position.y = staticObject.WorldMatrix._matrix.M42;
            object.position.z = staticObject.WorldMatrix._matrix.M43;

            object.userData.id = staticObject.Id;

            scene.add(object);
            staticObjects.push(object);
        });
    }
}

connection.on("ClientDispatcherFunctionName", (message: any) => ClientDispatcher(message));

export function createCone() {
    const coneGeom = new THREE.ConeGeometry(0.2, 1, 10);
    coneGeom.translate(0, .5, 0);
    coneGeom.rotateX(Math.PI / 2);
    const coneMat = new THREE.MeshNormalMaterial();
    const cone = new THREE.Mesh(coneGeom, coneMat);
    cone.lookAt(new THREE.Vector3(0, 1, 0));
    return cone;
}

function RemoveAllCharacters() {
    cones.forEach(cone => {
        scene.remove(cone);
    });
    cones = [];
}

function AddAllCharacters(characters: any[]) {
    characters.forEach(character => {
        const cone = createCone();
        cone.position.x = character.WorldMatrix._matrix.M41;
        cone.position.y = character.WorldMatrix._matrix.M42;
        cone.position.z = character.WorldMatrix._matrix.M43;

        cone.rotation.x = character.WorldMatrix._matrix.M11;
        cone.rotation.y = character.WorldMatrix._matrix.M12;
        cone.rotation.z = character.WorldMatrix._matrix.M13;
        cone.userData.id = character.Id;

        scene.add(cone);
        cones.push(cone);

        if(character.Id === playerId){
            camera.position.set(cone.position.x, cone.position.y, 5);
        }
    })
}

// connection.on("AddAllCharacters", (characters: any[]) => AddAllCharacters(characters));

export function shootBeam() {
    /*
    var direction = new THREE.Vector3();
    var beamGeometry = new THREE.CylinderGeometry(0.02, 0.02, 0.5, 6);

    var beamMaterial = new THREE.MeshBasicMaterial({color: 0xff0000});
    var beam = new THREE.Mesh(beamGeometry, beamMaterial);

    beam.userData.startPosition = cone.position.clone();
    beam.userData.endPosition = intersectPoint.clone();

    beam.position.copy(beam.userData.startPosition);

    direction
        .subVectors(beam.userData.endPosition, beam.userData.startPosition)
        .normalize();

    beam.position.addScaledVector(direction, 0.5);

    beam.userData.step = 0.1;

    direction.multiplyScalar(beam.userData.step);
    beam.userData.direction = direction;

    if (direction.y * direction.x < 0) {
        beam.rotation.z = Math.abs(cone.rotation.y);
    } else {
        beam.rotation.z = -Math.abs(cone.rotation.y);
    }
    scene.add(beam);
    beams.push(beam);
     */
}

export function onMouseMove(event: { clientX: number; clientY: number }) {
    mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
    mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
    raycaster.setFromCamera(mouse, camera);
    raycaster.ray.intersectPlane(plane, intersectPoint);
    marker.position.copy(intersectPoint);
}

export function render() {
    updateDirection();
    requestAnimationFrame(render);

    for (var i = 0; i < beams.length; i++) {
        beams[i].position.x += beams[i].userData.direction.x;
        beams[i].position.y += beams[i].userData.direction.y;
    }

    renderer.render(scene, camera);
}


