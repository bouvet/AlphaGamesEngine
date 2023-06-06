import {createCone} from "./Figures/Cone.ts";

type Handler = {
    [key:string]: (param: any) => THREE.Mesh;
}

export let StaticTypeHandlers: Handler = {};
export let DynamicTypeHandlers: Handler = {};

export function AddTypeHandlers(){

    DynamicTypeHandlers["player"] = (character: any) => {
        return createCone();
    }


    StaticTypeHandlers["wall"] = (staticObject: any) => {
        const objectGeom = new THREE.BoxGeometry(1, 1, 1);
        const objectMat = new THREE.MeshNormalMaterial();

        objectGeom.translate(0.5, 0.5, 0.5);
        return new THREE.Mesh(objectGeom, objectMat);
    }
}