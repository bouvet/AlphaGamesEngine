import * as THREE from 'three';


export function Grid(scene: THREE.Scene) {
    var grid = new THREE.GridHelper(12, 12, "white", "white");
    grid.rotation.x = Math.PI / 2;
    scene.add(grid);
}