import * as THREE from 'three';

export function AxesHelper(scene: THREE.Scene) {
    var axesHelper = new THREE.AxesHelper( 5 );
    scene.add( axesHelper );
}