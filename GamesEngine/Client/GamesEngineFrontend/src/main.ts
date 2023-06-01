import * as THREE from 'three';
import './style.css';
import { onDocumentKeyDown, onDocumentKeyUp, onMouseMove, shootBeam, render } from './Events/Functions';
import { startSignalR } from './SignalR/Functions';
import { Cone } from './Figures/Cone';


var scene = new THREE.Scene();
var camera = new THREE.PerspectiveCamera(60, window.innerWidth / window.innerHeight, 1, 1000);
camera.position.set(0, 0, 5);
var renderer = new THREE.WebGLRenderer({
  antialias: true
});


renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

var grid = new THREE.GridHelper(12, 12, "white", "white");
grid.rotation.x = Math.PI / 2;
scene.add(grid);
var axesHelper = new THREE.AxesHelper( 5 );
scene.add( axesHelper );

var marker = new THREE.Mesh(new THREE.SphereGeometry(0.062, 4, 2), new THREE.MeshBasicMaterial({
  color: "red"
}));

scene.add(marker);
// camera.position.z = 5;

window.addEventListener("mousemove", onMouseMove, false);
document.addEventListener("keydown", onDocumentKeyDown, false);
document.addEventListener("keyup", onDocumentKeyUp, false);
document.addEventListener('mousedown', function(event) {
  if (event.button === 0) {
      shootBeam();
  }
});

render();
