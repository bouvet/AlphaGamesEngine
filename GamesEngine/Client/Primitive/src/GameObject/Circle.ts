import { DynamicGameObject } from "./DynamicGameObject";
import { IVector } from "./Vector";

export class Circle extends DynamicGameObject {
    radius: number;
  
    constructor(position: IVector, radius: number) {
      super(position);
      this.radius = radius;
    }
  
    render(ctx: CanvasRenderingContext2D): void {
      ctx.beginPath();
      ctx.arc(this.position.x, this.position.y, this.radius, 0, Math.PI * 2);
      ctx.fillStyle = "#00FF00";
      ctx.fill();
    }
  }
  