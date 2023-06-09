import "./style.css";

import * as PIXI from "pixi.js";
import {AddDispatchHandlers} from "./Communication/DispatchHandlers.ts";
import {Communication, ICommunication} from "./Communication/Communication.ts";
import {Dispatcher} from "./Communication/Dispatcher.ts";
import {ICommunicationStrategy, SignalRCommunicationStrategy} from "./Communication/CommunicationStrategy.ts";

export const strategy: ICommunicationStrategy = new SignalRCommunicationStrategy();
export const dispatcher = new Dispatcher();
AddDispatchHandlers();

export const communication: ICommunication = new Communication(dispatcher, strategy);
await communication.Init();

communication.SendToServer({Type: "FetchDynamicObjects"});
communication.SendToServer({Type: "FetchStaticObjects"});

setInterval(() => {
    communication.SendToServer({Type: "FetchDynamicObjects"});
}, 100);


const app = new PIXI.Application({
  width: 1200,
  height: 600,
});

document
  .querySelector<HTMLElement>("#app")!
  .appendChild(app.view as HTMLCanvasElement);

let sprites = new Map<string, PIXI.Sprite>();

let mousePosition = new PIXI.Point(); //Todo: remove this since we are using mousemove event and sending mouse position to server

function sendKeyboardEvent(keyboardevent: string) {
 let direction = {x: 0, y: 0, z: 0};
  switch(keyboardevent){
    case "a":
      direction.x -= 3;
      break;
    case "d":
      direction.x += 3;
      break;
    case "s":
      direction.y -= 3;
      break;
    case "w":
      direction.y += 3;
      break;
  }
    communication.SendToServer({Type: "MovePlayer", x: direction.x, y: direction.y, z: direction.z});
}

function sendMouseEvent(mouseX: number, mouseY: number) {
    communication.SendToServer({Type: "RotateGameObject", MousePositionX: mouseX, MousePositionY: mouseY});
}

// function sendShootRequest() {
//   let message = { Type: "Shoot" };
//   connection.send("SendMessage", JSON.stringify(message));
// }

document.addEventListener("keydown", (event) => {
  console.log(event.key);
  if (event.key == " ") {
    // sendShootRequest();
  } else {
    // console.log("move sprites");
    sendKeyboardEvent(event.key);
  }
});

if (app.view) {
  app.view.addEventListener("mousemove", (e) => {
    let mouseEvent = e as MouseEvent;
    mousePosition.set(mouseEvent.offsetX, mouseEvent.offsetY); //Todo: remove this since we are using mousemove event and sending mouse position to server
    sendMouseEvent(mousePosition.x, mousePosition.y); // Todo: Use this instead of local mouse position
  });
}

export let playerId = -1;
export function SetPlayerId(id: number){
    playerId = id;
}

export function RenderCharacters(characters: any[]) {
  // console.log(`All characters ${characters}`);

  characters.forEach((character) => {
    let sprite = PIXI.Sprite.from("sample.png");
    sprite.scale = new PIXI.Point(0.5, 0.5);
    sprite.anchor.set(0.5, 0.5);
    app.stage.addChild(sprite);
    
    let speed = 10;
    sprite.x = character.WorldMatrix._matrix.M41 * speed + app.view.width / 2;
    sprite.y = character.WorldMatrix._matrix.M42 * (-1) * speed + app.view.height / 2;

    // rotate to mouse position
    let currentAngle = Math.atan2(character.WorldMatrix._matrix.M12, character.WorldMatrix._matrix.M11);
    let offsetAngle = Math.PI / 2;
    let deltaAngle =  currentAngle + offsetAngle;
    sprite.rotation = deltaAngle;

    // console.log(`character rotation, x: ${character.WorldMatrix._matrix.M11}, y: ${character.WorldMatrix._matrix.M12}`);

    if(character.Id == playerId){
        let dx = mousePosition.x - sprite.x;
        let dy = mousePosition.y - sprite.y;
        sprite.rotation = Math.atan2(dy, dx) + Math.PI / 2;
        sprite.tint = 0xffff00;
    }

    sprites.set(character.Id, sprite); // Todo: this isn't used as of now, but we can use this to reuse sprites instead of removing and adding new sprites on every frame
  });
}

export function RemoveAllCharacters() {
    sprites.forEach((sprite: PIXI.Sprite, id: string) => {
    app.stage.removeChild(sprite);
  });
  sprites.clear();
}

// Star background
function addSprite(asset: string, spriteName: string, x: number, y: number, size: number = 1) {
  const sprite: PIXI.Sprite = PIXI.Sprite.from(asset);
  sprite.anchor.set(0.5);
  app.stage.addChild(sprite);
//   sprites[spriteName] = sprite;
  sprite.x = x;
  sprite.y = y;
  sprite.scale.set(size);
  return sprite;
}

const stars: PIXI.Sprite[] = [];
function addBackground() {
  // Sprinkle the background with some stars
  for (let i = 0; i < 100; i++) {
    const x = Math.random() * app.screen.width;
    const y = Math.random() * app.screen.height;
    const star = addSprite("star.png", `star${i}`, x, y, 0.5);
    stars.push(star); // so we can animate them later
  }
}

async function startStarBackground(): Promise<void> {
  // Add some test sprites
  addBackground();

  PIXI.Ticker.shared.add(() => {

    stars.forEach((star) => {
      // We can use the FPS to throttle the animation 
      // so it doesn't impact performance on slower devices
      const fps = PIXI.Ticker.shared.FPS;
      if (fps > 60) 
      star.alpha = Math.random() * 0.5 + 0.5;
    });

  });
}

startStarBackground();