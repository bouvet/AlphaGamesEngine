import * as THREE from 'three';


export function Marker(scene: THREE.Scene) {
    var marker = new THREE.Mesh(new THREE.SphereGeometry(0.062, 4, 2), new THREE.MeshBasicMaterial({
        color: "red"
      }));
    scene.add(marker);
}

export function Grid(scene: THREE.Scene) {
      var grid = new THREE.GridHelper(12, 12, "white", "white");
      grid.rotation.x = Math.PI / 2;
    scene.add(grid);
}