import * as signalR from "@microsoft/signalr";
import {OnMessage} from "../ClientDispatcher.ts";

export const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
}).build();



export async function startSignalR() {
    connection.start()
        .then(() => {
            console.log("SignalR connected");

            connection.on("ClientDispatcherFunctionName", (message: any) => OnMessage(message));

            connection.send("SendMessage", JSON.stringify({Type: "FetchDynamicObjects"}));
            connection.send("SendMessage", JSON.stringify({Type: "FetchStaticObjects"}));

            setInterval(() => {
                connection.send("SendMessage", JSON.stringify({Type: "FetchDynamicObjects"}));
            }, 100);
        }).catch((error) => console.log(error));
}