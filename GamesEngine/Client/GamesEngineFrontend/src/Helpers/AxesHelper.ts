import * as THREE from 'three';

export function AxesHelper(scene: THREE.Scene) {
    var axesHelper = new THREE.AxesHelper(10);
    axesHelper.position.z = 0.01;
    scene.add(axesHelper);
}