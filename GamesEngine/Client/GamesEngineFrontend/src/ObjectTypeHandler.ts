import * as THREE from 'three';
import {createCone} from "./Figures/Cone.ts";

type Handler = {
    [key:string]: (param: any) => THREE.Mesh;
}

export let StaticTypeHandlers: Handler = {};
export let DynamicTypeHandlers: Handler = {};

export function AddTypeHandlers(){

    //Dynamic Objects
    // @ts-ignore
    DynamicTypeHandlers["player"] = (character: any) => {
        const coneGeom = new THREE.ConeGeometry(0.2, 1, 10);
        coneGeom.translate(0, .5, 0);
        coneGeom.rotateX(Math.PI / 2);
        coneGeom.translate(0, -0.5, 0);
        const coneMat = new THREE.MeshNormalMaterial();
        const cone = new THREE.Mesh(coneGeom, coneMat);
        cone.lookAt(new THREE.Vector3(0, 1, 0));
        return cone;
    }
    }


    //Static Objects
    // @ts-ignore
    StaticTypeHandlers["wall"] = (staticObject: any) => {
        const objectGeom = new THREE.BoxGeometry(1, 1, 1);
        const objectMat = new THREE.MeshNormalMaterial();

        objectGeom.translate(0.5, 0.5, 0.5); // pivot point is shifted
        return new THREE.Mesh(objectGeom, objectMat);
    }
}