import * as THREE from 'three';


export function Marker(scene: THREE.Scene) {
    var marker = new THREE.Mesh(new THREE.SphereGeometry(0.062, 4, 2), new THREE.MeshBasicMaterial({
        color: "red"
      }));
    scene.add(marker);
    return marker;
}
