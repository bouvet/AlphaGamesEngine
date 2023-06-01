import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
            }).build();


export async function startSignalR() {
    try {
      connection.start();
      console.log("SignalR connected");
    } catch (error) {
      console.log(error);
      // setTimeout(start, 5000);
    }
  }