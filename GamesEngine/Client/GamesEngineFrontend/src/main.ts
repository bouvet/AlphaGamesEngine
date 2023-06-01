import * as THREE from 'three';
import './style.css';

var scene = new THREE.Scene();
var camera = new THREE.PerspectiveCamera(60, window.innerWidth / window.innerHeight, 1, 1000);
camera.position.set(0, 0, 5);
var renderer = new THREE.WebGLRenderer({
  antialias: true
});
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

var coneGeom = new THREE.ConeGeometry(0.2, 1, 10);
coneGeom.translate(0, .5, 0);
coneGeom.rotateX(Math.PI / 2);
var coneMat = new THREE.MeshNormalMaterial();
var cone = new THREE.Mesh(coneGeom, coneMat);
cone.lookAt(new THREE.Vector3(0, 1, 0));
scene.add(cone);

var grid = new THREE.GridHelper(12, 12, "white", "white");
grid.rotation.x = Math.PI / 2;
scene.add(grid);

camera.position.z = 5;

let moveForward = false;
let moveBackward = false;
let moveLeft = false;
let moveRight = false;

var beams: { translateZ: (arg0: number) => void; }[] = [];

document.addEventListener("keydown", onDocumentKeyDown, false);
function onDocumentKeyDown(event: { which: any; }) {
    var keyCode = event.which;
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

document.addEventListener("keyup", onDocumentKeyUp, false);
function onDocumentKeyUp(event: { which: any; }) {
    var keyCode = event.which;
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

document.addEventListener('mousedown', function(event) {
    if (event.button === 0) {
        shootBeam();
    }
});

function shootBeam() {
    var beamGeometry = new THREE.CylinderGeometry(0.01, 0.01, 3, 32);
    var beamMaterial = new THREE.MeshBasicMaterial({ color: 0xff0000 });
    var beam = new THREE.Mesh(beamGeometry, beamMaterial);

    beam.position.copy(cone.position);
    // beam.rotation.copy(cone.rotation);

    // We want the beam to be emitted from the tip of the cone, not its center.
    // We move it forward by half its length.
    beam.translateZ(0.8);

    scene.add(beam);

    beams.push(beam);
}

var marker = new THREE.Mesh(new THREE.SphereGeometry(0.062, 4, 2), new THREE.MeshBasicMaterial({
  color: "red"
}));
scene.add(marker);

window.addEventListener("mousemove", onmousemove, false);

var plane = new THREE.Plane(new THREE.Vector3(0, 0, 1), 0);
var raycaster = new THREE.Raycaster();
var mouse = new THREE.Vector2();
var intersectPoint = new THREE.Vector3();

function onmousemove(event: { clientX: number; clientY: number; }) {
  mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
  mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
  raycaster.setFromCamera(mouse, camera);
  raycaster.ray.intersectPlane(plane, intersectPoint);
  cone.lookAt(intersectPoint);
  marker.position.copy(intersectPoint);
}

render();

function render() {
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

  // Move the beams
  for (var i = 0; i < beams.length; i++) {
	beams[i].translateZ(-0.1);
}

  	renderer.render(scene, camera);
}
