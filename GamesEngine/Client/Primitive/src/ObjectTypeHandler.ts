import { Rectangle } from './GameObject/Rectangle.ts';
import { Circle } from './GameObject/Circle.ts';
import { Triangle } from './GameObject/Triangle.ts';

// export function AddTypeHandlers(){
//     let StaticTypeHandlers: Handler = {};
//     let DynamicTypeHandlers: Handler = {};
//     type Handler =
//     {
//         [key:string]: (param: any) => object;
//     }
//     DynamicTypeHandlers["player"] = (character: any) => {
//         return drawSquare(character);
//     };

//     // StaticTypeHandlers["wall"] = (staticObject: any) => {
//     //     const objectGeom = new THREE.BoxGeometry(1, 1, 1);
//     //     const objectMat = new THREE.MeshNormalMaterial();

//     //     objectGeom.translate(0.5, 0.5, 0.5);
//     //     return new THREE.Mesh(objectGeom, objectMat);
//     // }
// }
type Handler = {
    [key: string]: (param: any) => object;
};

export class TypeHandler {
    StaticTypeHandlers: Handler = {};
    DynamicTypeHandlers: Handler = {};

    AddDynamicTypeHandlers() {
        this.DynamicTypeHandlers['player'] = (character: any) => {
            switch (character.Type) {
                case 'Rectangle':
                    return new Rectangle(
                        character.position,
                        character.width,
                        character.height
                    );
                case 'Circle':
                    return new Circle(character.position, character.radius);
                case 'Triangle':
                    return new Triangle(character.position, character.size);
                default:
                    throw new Error(
                        `Unsupported player type: ${character.Type}`
                    );
            }
        };
    }
}
