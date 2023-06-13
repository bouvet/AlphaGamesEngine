import { gameCanvas } from '../../main.ts';
import { DynamicGameObject } from './DynamicGameObject.ts';
import { IVector } from './Vector.ts';

export function drawSquare(character: any)
{
  if (!gameCanvas)
  {
    throw new Error('gameCanvas is not initialized.');
  }
  let context = gameCanvas.getContext('2d');
  const size = 10;
  let playerX = character.WorldMatrix._matrix.M41 * 10;
  let playerY = character.WorldMatrix._matrix.M42 * 10;

  if (!context)
  {
    throw new Error('Failed to get gameCanvas 2D context.');
  }

  context.clearRect(0, 0, gameCanvas.width, gameCanvas.height);
  context.beginPath();
  context.rect(playerX, playerY, size, size);
  context.fillStyle = '#FF0000';
  context.fill();
  context.closePath();

  return {
    id: character.Id,
    posX: playerX,
    posY: playerY,
    width: size,
    height: size,
  };
}

export class Rectangle extends DynamicGameObject
{
  width: number;
  height: number;

  constructor(position: IVector, width: number, height: number)
  {
    super(position);
    this.width = width;
    this.height = height;
  }

  render(ctx: CanvasRenderingContext2D): void
  {
    ctx.beginPath();
    ctx.rect(this.position.x, this.position.y, this.width, this.height);
    ctx.fillStyle = '#FF0000';
    ctx.fill();
  }
}
