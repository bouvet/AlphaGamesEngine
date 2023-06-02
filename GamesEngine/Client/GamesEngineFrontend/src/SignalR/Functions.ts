import * as signalR from "@microsoft/signalr";

export const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
            }).build();


export async function startSignalR() {
  connection.start()
            .then(() => {
              console.log("SignalR connected");
              let message = {Type: "FetchDynamicObjects"}
              connection.send("SendMessage", JSON.stringify(message));
            }).catch((error) => console.log(error));
}