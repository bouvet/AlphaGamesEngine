import * as THREE from 'three';

export function Cone() {
    var coneGeom = new THREE.ConeGeometry(0.2, 1, 10);
    coneGeom.translate(0, .5, 0);
    coneGeom.rotateX(Math.PI / 2);
    var coneMat = new THREE.MeshNormalMaterial();
    var cone = new THREE.Mesh(coneGeom, coneMat);
    cone.lookAt(new THREE.Vector3(0, 1, 0));
    return cone;
}