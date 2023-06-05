import * as THREE from 'three';
import { Marker } from '../Figures/Marker';
import { Grid } from '../Figures/Grid';
import { AxesHelper } from '../Helpers/AxesHelper';
import { connection } from '../SignalR/Functions';


var camera = new THREE.PerspectiveCamera(
    60,
    window.innerWidth / window.innerHeight,
    1,
    1000
);
camera.position.set(0, 0, 5);
var scene = new THREE.Scene();
var plane = new THREE.Plane(new THREE.Vector3(0, 0, 1), 0);
var raycaster = new THREE.Raycaster();
var mouse = new THREE.Vector2();
var intersectPoint = new THREE.Vector3();
let moveForward = false;
let moveBackward = false;
let moveLeft = false;
let moveRight = false;
var beams: THREE.Mesh[] = [];
var cones: THREE.Mesh[] = [];
var cone = createCone(scene, cones);
var marker = Marker(scene);
Grid(scene);
AxesHelper(scene);

var renderer = new THREE.WebGLRenderer({
    antialias: true,
});

renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

export function sendKeyboardEvent(keyboardevent: string) {
    let message = {Type: "MovePlayer", KeyboardEvent: keyboardevent}
    connection.send("SendMessage", message);
}

export function sendMouseEvent(MousePositionX: number, MousePositionY: number ) {
    let message = {Type: "RotateGameObject", MousePositionX: MousePositionX, MousePositionY:MousePositionY}
    connection.send("SendMessage", message);
}

const movementKeyMap : { [key: string]: number } = {
    forward: 87, //w
    backward: 83, //s
    left: 65, //a
    right: 68, //d
}
export function onDocumentKeyDown(event: { which: any, key: string }) {
  var keyCode = event.which;
//   cone.lookAt(intersectPoint);
//   marker.position.copy(intersectPoint);
  let movement: string = '';

  for(let key in movementKeyMap){
    if(keyCode == movementKeyMap[key]){
      movement = key;
    }
  }
  // Send the movement to SignalR
  sendKeyboardEvent(movement);
}

export function onDocumentKeyUp(event: { which: any }) {
  var keyCode = event.which;
//   cone.lookAt(intersectPoint);
//   marker.position.copy(intersectPoint);
  let movement = '';

  if (keyCode == 87) {
      // w
    //   moveForward = false;
      movement = 'forward';
  } else if (keyCode == 83) {
      // s
    //   moveBackward = false;
      movement = 'backward';
  } else if (keyCode == 65) {
      // a
    //   moveLeft = false;
      movement = 'left';
  } else if (keyCode == 68) {
      // d
    //   moveRight = false;
      movement = 'right';
  }

  // Send the movement to SignalR
  connection.send("StopMove", movement);
}

function ClientDispatcher(message: any){
    let content = JSON.parse(message.Content);
    RemoveAllCharacters();
    AddAllCharacters(content);
}

connection.on("ClientDispatcherFunctionName", (message: any) => ClientDispatcher(message));

export function createCone(scene: THREE.Scene, coneArray: THREE.Mesh[]) {
  var coneGeom = new THREE.ConeGeometry(0.2, 1, 10);
  coneGeom.translate(0, .5, 0);
  coneGeom.rotateX(Math.PI / 2);
  var coneMat = new THREE.MeshNormalMaterial();
  var cone = new THREE.Mesh(coneGeom, coneMat);
  cone.lookAt(new THREE.Vector3(0, 1, 0));
  scene.add(cone);
  coneArray.push(cone);
  return cone;
}

connection.on("RemoveAllCharacters", () => RemoveAllCharacters());

function RemoveAllCharacters(){
    cones.forEach(cone => {
      scene.remove(cone);
});
cones= [];
}

function AddAllCharacters(characters: any[]){
    characters.forEach(character => {
      var cone = createCone(scene, cones);
      cone.position.x = character.WorldMatrix._matrix.M41;
      cone.position.y = character.WorldMatrix._matrix.M42;
      cone.position.z = character.WorldMatrix._matrix.M43;

      cone.rotation.x = character.WorldMatrix._matrix.M11;
      cone.rotation.y = character.WorldMatrix._matrix.M12;
      cone.rotation.z = character.WorldMatrix._matrix.M13;
      cone.userData.id = character.Id;
      scene.add(cone);
      cones.push(cone);
})
}

// connection.on("AddAllCharacters", (characters: any[]) => AddAllCharacters(characters));

export function shootBeam() {
    var direction = new THREE.Vector3();
    var beamGeometry = new THREE.CylinderGeometry(0.02, 0.02, 0.5, 6);

    var beamMaterial = new THREE.MeshBasicMaterial({ color: 0xff0000 });
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
}

export function onMouseMove(event: { clientX: number; clientY: number }) {
    mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
    mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
    raycaster.setFromCamera(mouse, camera);
    raycaster.ray.intersectPlane(plane, intersectPoint);
    cone.lookAt(intersectPoint);
    marker.position.copy(intersectPoint);
}

export function render() {
    requestAnimationFrame(render);
    cone.rotation.z += 0.01;
    if (moveForward) cone.position.y += 0.1;
    if (moveBackward) cone.position.y -= 0.1;
    if (moveLeft) cone.position.x -= 0.1;
    if (moveRight) cone.position.x += 0.1;

    const conePositionInCameraSpace = cone.position.clone().project(camera);
    if (conePositionInCameraSpace.x < -1) {
        cone.position.setX(-cone.position.x);
    } else if (conePositionInCameraSpace.x > 1) {
        cone.position.setX(-cone.position.x);
    }

    if (conePositionInCameraSpace.y < -1) {
        cone.position.setY(-cone.position.y);
    } else if (conePositionInCameraSpace.y > 1) {
        cone.position.setY(-cone.position.y);
    }

    for (var i = 0; i < beams.length; i++) {
        beams[i].position.x += beams[i].userData.direction.x;
        beams[i].position.y += beams[i].userData.direction.y;
    }

    renderer.render(scene, camera);
}


