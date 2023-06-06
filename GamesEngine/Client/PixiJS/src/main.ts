import "./style.css";

import * as PIXI from "pixi.js";
import * as signalR from "@microsoft/signalr";

import { Matrix, Vector } from "ts-matrix";

const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7247/gamehub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets,
  })
  .build();

async function startSignalR() {
  connection
    .start()
    .then(() => {
      console.log("SignalR connected");
      connection.send(
        "SendMessage",
        JSON.stringify({ Type: "FetchDynamicObjects" })
      );
      // console.log(`First fetch objects called`);
    })
    .catch((error) => console.log(error));
}
startSignalR();

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
  let message = { Type: "MovePlayer", KeyboardEvent: keyboardevent };
  connection.send("SendMessage", JSON.stringify(message));
  // console.log(`move requested, ${JSON.stringify(message)}`);
}

function sendMouseEvent(mouseX: number, mouseY: number) {
  let message = {
    Type: "RotateGameObject",
    MousePositionX: mouseX,
    MousePositionY: mouseY,
  };
  connection.send("SendMessage", JSON.stringify(message));
  // onMouseMove({clientX: mouseX, clientY: mouseY});
}

function sendShootRequest() {
  let message = { Type: "Shoot" };
  connection.send("SendMessage", JSON.stringify(message));
}

document.addEventListener("keydown", (event) => {
  console.log(event.key);
  if (event.key == " ") {
    sendShootRequest();
  } else {
    console.log("move sprites");
    sendKeyboardEvent(event.key);
  }
});

if (app.view) {
  app.view.addEventListener("mousemove", (e) => {
    let mouseEvent = e as MouseEvent;
    mousePosition.set(mouseEvent.offsetX, mouseEvent.offsetY); //Todo: remove this since we are using mousemove event and sending mouse position to server
    sendMouseEvent(mouseEvent.offsetX, mouseEvent.offsetY); // Todo: Use this instead of local mouse position
  });
}

setInterval(() => { 
  connection.send(
    "SendMessage",
    JSON.stringify({ Type: "FetchDynamicObjects" })
  );
}, 100);

function ClientDispatcher(message: any) {
  let content = JSON.parse(message.content);
  let type = message.type;
  // console.log(`Server called ClientDispatcher, type: ${type}`);
  if (type === "FetchDynamicObjects") {
    RemoveAllCharacters();
    RenderCharacters(content);
  }
  // if(type === "Shoot") {

  // }
}

connection.on("ClientDispatcherFunctionName", (message: any) =>
  ClientDispatcher(message)
);

function RenderCharacters(characters: any[]) {
  console.log(`All characters ${characters}`);

  characters.forEach((character) => {
    let sprite = PIXI.Sprite.from("sample.png");
    sprite.anchor.set(0.5, 0.5);

    sprite.x = character.WorldMatrix._matrix.M41;
    sprite.y = character.WorldMatrix._matrix.M42;

    app.stage.addChild(sprite);
    // Todo: get rotation angle from server
    // let characterRotationVector = new Vector([character.WorldMatrix._matrix.M11, character.WorldMatrix._matrix.M12, character.WorldMatrix._matrix.M13]);
    // let characterPositionVector = new Vector([character.WorldMatrix._matrix.M41, character.WorldMatrix._matrix.M42, 0]);
    // let mousePositionVector = new Vector([mousePosition.x, mousePosition.y, 0]);
    // let rotationAngle = Vector.get360angle(characterRotationVector, mousePositionVector);
    // sprite.rotation = rotationAngle;

    // sprite.eventMode = 'dynamic';
    // sprite.on('pointermove', (event) => {
    //   let dx = event.global.x - sprite.x;
    //   let dy = event.global.y - sprite.y;
    //   sprite.rotation = Math.atan2(dy, dx);
    // });

    // rotate to mouse position
    let dx = mousePosition.x - sprite.x;
    let dy = mousePosition.y - sprite.y;
    sprite.rotation = Math.atan2(dy, dx) + Math.PI / 2;

    // let characterPositionVector = new Vector([character.WorldMatrix._matrix.M41, character.WorldMatrix._matrix.M42, 0]);
    // let mousePositionVector = new Vector([mousePosition.x, mousePosition.y, 0]);
    // let rotationAngle = Vector.get360angle(characterPositionVector, mousePositionVector);
    // sprite.rotation = rotationAngle;

    sprites.set(character.Id, sprite); // Todo: this isn't used as of now, but we can use this to reuse sprites instead of removing and adding new sprites on every frame
  });
}

connection.on("RemoveAllCharacters", () => RemoveAllCharacters());

function RemoveAllCharacters() {
  sprites.forEach((sprite) => {
    app.stage.removeChild(sprite);
  });
  sprites = new Map<string, PIXI.Sprite>();
  // console.log(`Remove all characters, ${sprites}`);
}
