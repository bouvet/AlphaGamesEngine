import * as THREE from 'three';

type Handler = {
    [key:string]: (param: any) => THREE.Mesh;
}

export let StaticTypeHandlers: Handler = {};
export let DynamicTypeHandlers: Handler = {};

export function AddTypeHandlers(){

    //Dynamic Objects
    // @ts-ignore
    DynamicTypeHandlers["player"] = (character: any) => {
        /*
        const coneGeom = new THREE.ConeGeometry(0.2, 1, 10);
        coneGeom.translate(0, .5, -0.5);
        coneGeom.rotateX(Math.PI / 2);
        const coneMat = new THREE.MeshNormalMaterial();
        const cone = new THREE.Mesh(coneGeom, coneMat);
        cone.lookAt(new THREE.Vector3(0, 1, 0));
      return cone;
         */

        const objectGeom = new THREE.BoxGeometry(1, 1, 1);
        const objectMat = new THREE.MeshNormalMaterial();

        objectGeom.translate(0.5, 0.5, 0.5); // pivot point is shifted
        return new THREE.Mesh(objectGeom, objectMat);
    }

    // @ts-ignore
    DynamicTypeHandlers["obstacle"] = (staticObject: any) => {
        const objectGeom = new THREE.BoxGeometry(1, 1, 1);
        const objectMat = new THREE.MeshPhongMaterial({color: staticObject.Colliding ? 0xff0000 : 0xffffff});

        objectGeom.translate(0.5, 0.5, 0.5); // pivot point is shifted
        const mesh = new THREE.Mesh(objectGeom, objectMat);
        //mesh.receiveShadow = true;
        //mesh.castShadow = true;
        return mesh;
    }


    //Static Objects
    // @ts-ignore
    StaticTypeHandlers["wall"] = (staticObject: any) => {
        const objectGeom = new THREE.BoxGeometry(1, 1, 1);
        const objectMat = new THREE.MeshPhongMaterial({color: 0x888888});

        objectGeom.translate(0.5, 0.5, 0.5); // pivot point is shifted
        return new THREE.Mesh(objectGeom, objectMat);
    }

    // @ts-ignore
    StaticTypeHandlers["floor"] = (staticObject: any) => {
        const objectGeom = new THREE.BoxGeometry(1, 1, 1);
        const objectMat = new THREE.MeshPhongMaterial({color: staticObject.Id % 3 == 0 ? 0x888888 : 0x828282});

        objectGeom.translate(0.5, 0.5, 0.5); // pivot point is shifted
        return new THREE.Mesh(objectGeom, objectMat);
    }
}