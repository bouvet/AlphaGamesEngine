import './style.css'

import * as PIXI from 'pixi.js';
import * as signalR from "@microsoft/signalr";
import { Vector, Matrix } from 'ts-matrix';

const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
            }).build();

async function startSignalR() {
    connection.start()
              .then(() => {
                console.log("SignalR connected");
                connection.send("SendMessage", JSON.stringify({Type: "FetchDynamicObjects"}));
                // console.log(`First fetch objects called`);
              }).catch((error) => console.log(error));
}
startSignalR();

const app: PIXI.Application = new PIXI.Application({ width: 1200, height: 600 });
document.querySelector<HTMLElement>('#app')!.appendChild(app.view as HTMLCanvasElement);

// let sprites = new Map<string, PIXI.Sprite>();
let sprites = new Map<string, PIXI.Sprite>();

// let mousePosition = {
//   x: 0,
//   y: 0,
// };

let mousePosition = new PIXI.Point();

function sendKeyboardEvent(keyboardevent: string){
    let message = {Type: "MovePlayer", KeyboardEvent: keyboardevent}
    connection.send("SendMessage", JSON.stringify(message));
    // console.log(`move requested, ${JSON.stringify(message)}`);
}

function sendMouseEvent(mouseX: number, mouseY: number) {
    let message = {Type: "RotateGameObject", MousePositionX: mouseX, MousePositionY: mouseY}
    connection.send("SendMessage", JSON.stringify(message));
    // onMouseMove({clientX: mouseX, clientY: mouseY});
}

function sendShootRequest(){
    let message = {Type: "Shoot"}
    connection.send("SendMessage", JSON.stringify(message));
}

document.addEventListener("keydown", (event) => {
  console.log(event.key);
  if (event.key == " ") {
    sendShootRequest()
  } else {
    console.log("move sprites");
    sendKeyboardEvent(event.key);
  }
});


if (app.view) {
  app.view.addEventListener("mousemove", (e) => {
      let mouseEvent = e as MouseEvent;
    // mousePosition = {
    //   x: mouseEvent.offsetX,
    //   y: mouseEvent.offsetY,
    // };
    mousePosition.set(mouseEvent.clientX, mouseEvent.clientY);
    sendMouseEvent(mouseEvent.clientX, mouseEvent.clientX);
  });
}

setInterval(() => {
    connection.send("SendMessage", JSON.stringify({Type: "FetchDynamicObjects"}));
}, 100);

function ClientDispatcher(message: any) {
    let content = JSON.parse(message.content);
    let type = message.type;
    // console.log(`Server called ClientDispatcher, type: ${type}`);
    if(type === "FetchDynamicObjects"){
        RemoveAllCharacters();
        AddAllCharacters(content);
    }
    // if(type === "Shoot") {

    // }
}

connection.on("ClientDispatcherFunctionName", (message: any) => ClientDispatcher(message));

connection.on("AddAllCharacters", (characters) => { AddAllCharacters(characters)});

function AddAllCharacters(characters: any[]){
  console.log(`All characters ${characters}`);

  characters.forEach((character) => {
    let sprite = PIXI.Sprite.from('sample.png');
      sprite.anchor.set(0.5, 0.5); 
      // sprite.interactive = true;
      
      sprite.x = character.WorldMatrix._matrix.M41;
      sprite.y = character.WorldMatrix._matrix.M42;
      
      
      // sprite.eventMode = 'dynamic';
      // sprite.on('pointermove', (event) => {
      //   let dx = event.global.x - sprite.x;
      //   let dy = event.global.y - sprite.y;
      //   sprite.rotation = Math.atan2(dy, dx);
      // });

      app.ticker.add(() => {
        let dx = mousePosition.x - sprite.x;
        let dy = mousePosition.y - sprite.y;
        sprite.rotation = Math.atan2(dy, dx);
      })

      
      app.stage.addChild(sprite);
      
      // let characterPositionVector = new Vector([character.WorldMatrix._matrix.M41, character.WorldMatrix._matrix.M42, 0]);
      // // console.log(`characterPositionVector ${character.WorldMatrix._matrix.M41}, ${character.WorldMatrix._matrix.M42}`);

      // let mousePositionVector = new Vector([mousePosition.x, mousePosition.y, 0]);
      // // console.log(`mousePositionVector ${mousePosition.x}, ${mousePosition.y}`);

      // let rotationAngle = Vector.get360angle(characterPositionVector, mousePositionVector, );
      // // console.log(`rotationAngle ${rotationAngle}`);
      // sprite.rotation = rotationAngle + Math.PI;

      sprites.set(character.Id, sprite);
  });
}

connection.on("RemoveAllCharacters", () => RemoveAllCharacters());

function RemoveAllCharacters(){
  sprites.forEach(sprite => {
    app.stage.removeChild(sprite);
  })
  sprites = new Map<string, PIXI.Sprite>();
  // console.log(`Remove all characters, ${sprites}`);
}

// connection.on("MoveCharacter", (character) => {
//   let sprite = sprites.get(character.id);

//   if (sprite) {
//     sprite.x = character.WorldMatrix._matrix.M41;
//     sprite.y = character.WorldMatrix._matrix.M42;
//   }

// });

// connection.on("RotateCharacter", (character, rotation) => {
//   let sprite = sprites.get(character.id);

//   if (sprite){
//     sprite.rotation = rotation;
//   } 
// });

// connection.on("AddShot", (character) => {
//   let sprite = sprites.get(character.id);

//   if (sprite){
//     let bullet = PIXI.Sprite.from('sample.png');
//     bullet.x = sprite.x;
//     bullet.y = sprite.y;

//     app.stage.addChild(bullet);
//   }
// });

// connection.on("RemoveCharacter", (characterID) => {
//   let sprite = sprites.get(characterID);
  
//   if (sprite) {
//     app.stage.removeChild(sprite);
//     sprites.delete(characterID);
//   }
// });

