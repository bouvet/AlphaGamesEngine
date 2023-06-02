import * as THREE from 'three';
import './style.css';
import { onDocumentKeyDown, onDocumentKeyUp, onMouseMove, shootBeam, render } from './Events/Functions';
import { startSignalR } from './SignalR/Functions';
import { Cone } from './Figures/Cone';


window.addEventListener("mousemove", onMouseMove, false);
document.addEventListener("keydown", onDocumentKeyDown, false);
document.addEventListener("keyup", onDocumentKeyUp, false);
document.addEventListener('mousedown', function(event) {
  if (event.button === 0) {
      shootBeam();
  }
});

render();
startSignalR();
