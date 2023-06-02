import * as THREE from 'three';
import { Cone } from '../Figures/Cone';
var camera = new THREE.PerspectiveCamera(60, window.innerWidth / window.innerHeight, 1, 1000);
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
var cone = Cone();
scene.add(cone);
var marker = new THREE.Mesh(new THREE.SphereGeometry(0.062, 4, 2), new THREE.MeshBasicMaterial({
  color: "red"
}));
var grid = new THREE.GridHelper(12, 12, "white", "white");
grid.rotation.x = Math.PI / 2;
scene.add(grid);
var axesHelper = new THREE.AxesHelper( 5 );
scene.add( axesHelper );

var marker = new THREE.Mesh(new THREE.SphereGeometry(0.062, 4, 2), new THREE.MeshBasicMaterial({
  color: "red"
}));
var renderer = new THREE.WebGLRenderer({
  antialias: true
});


renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

scene.add(marker);
scene.add(marker);

export function onDocumentKeyDown(event: { which: any; }) {
    var keyCode = event.which;
  cone.lookAt(intersectPoint);
  marker.position.copy(intersectPoint);
    if (keyCode == 87) { // w
        moveForward = true;
    } else if (keyCode == 83) { // s
        moveBackward = true;
    } else if (keyCode == 65) { // a
        moveLeft = true;
    } else if (keyCode == 68) { // d
        moveRight = true;
    }
};

export function onDocumentKeyUp(event: { which: any; }) {
    var keyCode = event.which;
    cone.lookAt(intersectPoint);
    marker.position.copy(intersectPoint);
    if (keyCode == 87) {
        moveForward = false;
    } else if (keyCode == 83) {
        moveBackward = false;
    } else if (keyCode == 65) {
        moveLeft = false;
    } else if (keyCode == 68) {
        moveRight = false;
    }
};

export function shootBeam() {
    var direction = new THREE.Vector3();
    var beamGeometry = new THREE.CylinderGeometry(0.02, 0.02, 0.5, 6);

    var beamMaterial = new THREE.MeshBasicMaterial({ color: 0xff0000 });
    var beam = new THREE.Mesh(beamGeometry, beamMaterial);

    beam.userData.startPosition = cone.position.clone();
    beam.userData.endPosition = intersectPoint.clone();

    beam.position.copy(beam.userData.startPosition);

    direction.subVectors(beam.userData.endPosition, beam.userData.startPosition).normalize();

    beam.position.addScaledVector(direction, 0.5);

    beam.userData.step = 0.1;
    beam.userData.elapsedTime = 0;

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

export function onMouseMove(event: { clientX: number; clientY: number; }) {
    mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
    mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
    raycaster.setFromCamera(mouse, camera);
    raycaster.ray.intersectPlane(plane, intersectPoint);
    cone.lookAt(intersectPoint);
    marker.position.copy(intersectPoint);
  }

export function render() {
    requestAnimationFrame(render);
  //cone.rotation.z += 0.01;
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
  // beams[i].position.z += beams[i].userData.direction.z;
  // beams[i].lookAt(beams[i].userData.endPosition);
  beams[i].position.x += beams[i].userData.direction.x;
  beams[i].position.y += beams[i].userData.direction.y;
}

renderer.render(scene, camera);

}