import "./style.css";

import * as PIXI from "pixi.js";
import * as signalR from "@microsoft/signalr";

import { Matrix, Vector } from "ts-matrix";

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

// const connection = new signalR.HubConnectionBuilder()
//   .withUrl("https://localhost:7247/gamehub", {
//     skipNegotiation: true,
//     transport: signalR.HttpTransportType.WebSockets,
//   })
//   .build();

// async function startSignalR() {
//   connection.start()
//     .then(() => {
//       console.log("SignalR connected");
//       connection.send(
//         "SendMessage",
//         JSON.stringify({ Type: "FetchDynamicObjects" })
//         );
//         console.log(`connection id: ${connection.connectionId}`);
//       // console.log(`First fetch objects called`);
//     })
//     .catch((error) => console.log(error));
// }
// startSignalR();

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
      direction.x -= 1;
      break;
    case "d":
      direction.x += 1;
      break;
    case "s":
      direction.y -= 1;
      break;
    case "w":
      direction.y += 1;
      break;
  }
//   let message = { Type: "MovePlayer", KeyboardEvent: keyboardevent, x: direction.x, y: direction.y, z: direction.z};
//   connection.send("SendMessage", JSON.stringify(message));
    communication.SendToServer({Type: "MovePlayer", x: direction.x, y: direction.y, z: direction.z});


  // console.log(`move requested, ${JSON.stringify(message)}`);
}

function sendMouseEvent(mouseX: number, mouseY: number) {
//   let message = {
//     Type: "RotateGameObject",
//     // MousePositionX: 0,
//     // MousePositionY: 300,
//     MousePositionX: mouseX - app.view.width / 2,
//     MousePositionY: (mouseY - app.view.height / 2) * (-1),
//   };
//   console.log(`mousex ${message.MousePositionX}, mouseY ${message.MousePositionY}`);
    let MousePositionX = mouseX - app.view.width / 2;
    let MousePositionY = (mouseY - app.view.height / 2) * (-1);
//   connection.send("SendMessage", JSON.stringify(message));
    communication.SendToServer({Type: "RotateGameObject", MousePositionX: MousePositionX, MousePositionY: MousePositionY});

  // onMouseMove({clientX: mouseX, clientY: mouseY});
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
    // sendMouseEvent(mouseEvent.offsetX, mouseEvent.offsetY); // Todo: Use this instead of local mouse position
    sendMouseEvent(mouseEvent.clientX, mouseEvent.clientY); // Todo: Use this instead of local mouse position
  });
}

// setInterval(() => { 
//   connection.send(
//     "SendMessage",
//     JSON.stringify({ Type: "FetchDynamicObjects" })
//   );
// }, 100);

// function ClientDispatcher(message: any) {
//   let content = JSON.parse(message.content);
//   let type = message.type;
//   // console.log(`Server called ClientDispatcher, type: ${type}`);
//   if (type === "FetchDynamicObjects") {
//     RemoveAllCharacters();
//     RenderCharacters(content);
//   }
//   // if(type === "Shoot") {

//   // }
// }

// connection.on("ClientDispatcherFunctionName", (message: any) =>
//   ClientDispatcher(message)
// );

export function RenderCharacters(characters: any[]) {
  // console.log(`All characters ${characters}`);

  characters.forEach((character) => {
    let sprite = PIXI.Sprite.from("sample.png");
    sprite.anchor.set(0.5, 0.5);

    let speed = 20;

    sprite.x = character.WorldMatrix._matrix.M41 * speed + app.view.width / 2;
    sprite.y = character.WorldMatrix._matrix.M42 * (-1) * speed + app.view.height / 2;

    app.stage.addChild(sprite);

    // rotate to mouse position
    // console.log(`character id: ${character.ConnectionId}, connection id: ${connection.connectionId}`);
    console.log(`character rotation, x: ${character.WorldMatrix._matrix.M11}, y: ${character.WorldMatrix._matrix.M12}`);

    // if(character.Id == connection.connectionId){
        // let dx = mousePosition.x - sprite.x;
        // let dy = mousePosition.y - sprite.y;
        // sprite.rotation = Math.atan2(dy, dx) + Math.PI / 2;
    // }

    let dx = mousePosition.x - sprite.x;
    let dy = mousePosition.y - sprite.y;
    let desiredAngle = Math.atan2(dy, dx);
    // if(character.WorldMatrix._matrix.M11 === 0 && character.WorldMatrix._matrix.M12 === 0){
    //     character.WorldMatrix._matrix.M11 = 0;
    //     character.WorldMatrix._matrix.M12 = 1;
    // }
    let currentAngle = Math.atan2(character.WorldMatrix._matrix.M12, character.WorldMatrix._matrix.M11);
    let offsetAngle = Math.PI / 2;
    let deltaAngle = desiredAngle - currentAngle + offsetAngle;
    sprite.rotation += deltaAngle;
    
    // Todo: get rotation angle from server
    // let characterRotationVector = new Vector([character.WorldMatrix._matrix.M11, character.WorldMatrix._matrix.M12, character.WorldMatrix._matrix.M13]);
    // console.log(characterRotationVector);
    // let mousePositionVector = new Vector([mousePosition.x, mousePosition.y, 0]);
    // let rotationAngle = Vector.get360angle(characterRotationVector, mousePositionVector);
    // sprite.angle = rotationAngle;
    
    // let characterPositionVector = new Vector([character.WorldMatrix._matrix.M41, character.WorldMatrix._matrix.M42, 0]);
    // let mousePositionVector = new Vector([mousePosition.x, mousePosition.y, 0]);
    // let rotationAngle = Vector.get360angle(characterPositionVector, mousePositionVector);
    // sprite.rotation = rotationAngle;

    // sprite.eventMode = 'dynamic';
    // sprite.on('pointermove', (event) => {
    //   let dx = event.global.x - sprite.x;
    //   let dy = event.global.y - sprite.y;
    //   sprite.rotation = Math.atan2(dy, dx);
    // });

    sprites.set(character.Id, sprite); // Todo: this isn't used as of now, but we can use this to reuse sprites instead of removing and adding new sprites on every frame
  });
}

// connection.on("RemoveAllCharacters", () => RemoveAllCharacters());

export function RemoveAllCharacters() {
  
    sprites.forEach((sprite: PIXI.Sprite, id: string) => {
    app.stage.removeChild(sprite);
    console.log("all removed from stage ?");
  });
  sprites.clear;
//   sprites = new Map<string, PIXI.Sprite>();
  console.log(`Remove all characters, ${sprites}`);
}
