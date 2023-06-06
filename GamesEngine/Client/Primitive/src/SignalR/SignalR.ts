// import * as signalR from "@microsoft/signalr";

// export const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
//     skipNegotiation: true,
//     transport: signalR.HttpTransportType.WebSockets
// }).build();

// export async function startSignalR() {
//     connection.start()
//         .then(() => {
//             console.log("SignalR connected");
//             connection.send("SendMessage", JSON.stringify({Type: "FetchDynamicObjects"}));
//         }).catch((error) => console.log(error));
// }

// setInterval(() => {
//     connection.send("SendMessage", JSON.stringify({Type: "FetchDynamicObjects"}));
//     console.log("fetch");
// }, 100);

// export function sendKeyboardEvent(keyboardevent: string) {
//     let message = {Type: "MovePlayer", KeyboardEvent: keyboardevent}
//     connection.send("SendMessage", JSON.stringify(message));
//     console.log(`move, ${JSON.stringify(message)}`);
// }

// export function sendMouseEvent(MousePositionX: number, MousePositionY: number) {
//     let message = {Type: "RotateGameObject", MousePositionX: MousePositionX, MousePositionY: MousePositionY}
//     connection.send("SendMessage", JSON.stringify(message));
//     onMouseMove({clientX: MousePositionX, clientY: MousePositionY});
// }

// const movementKeyMap: { [key: string]: number } = {
//     up: 87, //w
//     down: 83, //s
//     left: 65, //a
//     right: 68, //d
// }

// export function onDocumentKeyDown(event: { which: any, key: string }) {
//     const keyCode = event.which;
//     let movement: string = '';

//     for (let key in movementKeyMap) {
//         if (keyCode == movementKeyMap[key]) {
//             movement = key;
//         }
//     }
//     sendKeyboardEvent(movement);
// }

// export function onDocumentKeyUp(event: { which: any }) {
//     const keyCode = event.which;
//     let movement: string = '';

//     for (let key in movementKeyMap) {
//         if (keyCode == movementKeyMap[key]) {
//             movement = key;
//         }
//     }
//     connection.send("StopMove", movement);
// }

// function ClientDispatcher(message: any) {
//     let content = JSON.parse(message.content);
//     let type = message.type;
//     if(type === "FetchDynamicObjects"){
//         RemoveAllCharacters();
//         AddAllCharacters(content);
//     }
// }

// connection.on("ClientDispatcherFunctionName", (message: any) => ClientDispatcher(message));

// connection.on("RemoveAllCharacters", () => RemoveAllCharacters());

// function RemoveAllCharacters() {
//     // cones.forEach(cone => {
//     //     scene.remove(cone);
//     // });
//     // cones = [];
// }

// function AddAllCharacters(characters: any[]) {
//     // characters.forEach(character => {
//     //     console.log(character)
//     //     var cone = createCone(scene, cones);
//     //     cone.position.x = character.WorldMatrix._matrix.M41;
//     //     cone.position.y = character.WorldMatrix._matrix.M42;
//     //     cone.position.z = character.WorldMatrix._matrix.M43;

//     //     cone.rotation.x = character.WorldMatrix._matrix.M11;
//     //     cone.rotation.y = character.WorldMatrix._matrix.M12;
//     //     cone.rotation.z = character.WorldMatrix._matrix.M13;
//     //     cone.userData.id = character.Id;
//     //     scene.add(cone);
//     //     cones.push(cone);
//     // })
// }

// export function onMouseMove(event: { clientX: number; clientY: number }) {
//     // mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
//     // mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
//     // raycaster.setFromCamera(mouse, camera);
//     // raycaster.ray.intersectPlane(plane, intersectPoint);
//     // marker.position.copy(intersectPoint);
// }
