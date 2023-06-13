import { DynamicGameObject } from "./DynamicGameObject";
import { IVector } from "./Vector";

export class Triangle extends DynamicGameObject {
    size: number;
  
    constructor(position: IVector, size: number) {
      super(position);
      this.size = size;
    }
  
    render(ctx: CanvasRenderingContext2D): void {
      ctx.beginPath();
      ctx.moveTo(this.position.x, this.position.y);
      ctx.lineTo(this.position.x + this.size / 2, this.position.y + this.size);
      ctx.lineTo(this.position.x - this.size / 2, this.position.y + this.size);
      ctx.closePath();
      ctx.fillStyle = "#0000FF";
      ctx.fill();
    }
  }