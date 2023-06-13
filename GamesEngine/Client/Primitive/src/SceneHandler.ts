import { gameCanvas, typeHandler } from './../main.ts';

let dynamicObjects: [] = [];
let staticObjects: [] = [];

export let playerId = -1;

export function SetPlayerId(id: number) {
    playerId = id;
}

export function RemoveStaticObjects() {
    staticObjects.forEach((obj) => {
        //scene.remove(obj);
    });
    staticObjects = [];
}

export function AddStaticObjects(objects: any[]) {
    objects.forEach((staticObject: any) => {
        let obj = null;

        // if(StaticTypeHandlers[staticObject.Type.toLowerCase()] !== undefined) {
        //     obj = StaticTypeHandlers[staticObject.Type.toLowerCase()](staticObject);
        // }

        if (obj) {
            SetMatrix(obj, staticObject);

            //scene.add(obj);
            //staticObjects.push(obj);
        }
    });
}

export function RemoveDynamicObjects() {
    dynamicObjects = [];
}

let lastDynamicObjects: any[] = [];

export function AddDynamicObjects(objects: any[]) {
    objects.forEach((dynamicObject) => {
        let obj = null;

        if (
            typeHandler.DynamicTypeHandlers[
            dynamicObject.Type.toLowerCase()
            ] !== undefined
        ) {
            obj =
                typeHandler.DynamicTypeHandlers[
                    dynamicObject.Type.toLowerCase()
                ](dynamicObject);
        }

        if (obj) {
            SetMatrix(obj, dynamicObject);

            // scene.add(obj);
            // dynamicObjects.push(obj);
        }
    });

    lastDynamicObjects = dynamicObjects;
}

function SetMatrix(obj: any, gameObject: any) {
    obj.position.x = gameObject.WorldMatrix._matrix.M41;
    obj.position.y = gameObject.WorldMatrix._matrix.M42;
    // obj.position.z = gameObject.WorldMatrix._matrix.M43;

    // obj.rotation.x = gameObject.WorldMatrix._matrix.M11;
    // obj.rotation.y = gameObject.WorldMatrix._matrix.M12;
    // obj.rotation.z = gameObject.WorldMatrix._matrix.M13;
}
