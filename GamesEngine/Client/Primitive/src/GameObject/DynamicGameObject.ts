import { IVector } from './Vector';

interface IDynamicGameObject {
    position: IVector;
    //velocity: IVector;
    render(context: CanvasRenderingContext2D): void;
    // update(deltaTime: number): void;
}

export class DynamicGameObject implements IDynamicGameObject {
    position: IVector;
    //velocity: IVector;

    constructor(position: IVector) {
        this.position = position;
    }

    render(context: CanvasRenderingContext2D): void {
        context.beginPath();
        context.rect(this.position.x, this.position.y, 10, 10); // draws a 10x10 square
        context.fillStyle = '#FF0000';
        context.fill();
    }

    // update(deltaTime: number): void {
    //   this.position.x += this.velocity.x * deltaTime;
    //   this.position.y += this.velocity.y * deltaTime;
    // }
}
