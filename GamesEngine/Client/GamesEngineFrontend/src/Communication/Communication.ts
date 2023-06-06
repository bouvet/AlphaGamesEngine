import * as signalR from "@microsoft/signalr";
import {Dispatcher} from "./Dispatcher.ts";

export abstract class ICommunication
{
    dispatcher: Dispatcher;

    abstract init(): void;
    abstract SendToServer(message: object): void;

    constructor(dispatcher: Dispatcher){
        this.dispatcher = dispatcher;
    }

    OnMessage(message: any) {
     this.dispatcher.OnMessage(message);
    }
}

export class SignalRCommunication extends ICommunication
{
     connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        }).build();

    SendToServer(message: object) {
        this.connection.send("SendMessage", JSON.stringify(message));
    }

    init() {
        this.connection.start()
            .then(() => {
                console.log("SignalR connected");

                this.connection.on("ClientDispatcherFunctionName", (message: any) => this.OnMessage(message));

                this.SendToServer({Type: "FetchDynamicObjects"});
                this.SendToServer({Type: "FetchStaticObjects"});

                setInterval(() => {
                    this.SendToServer({Type: "FetchDynamicObjects"});
                }, 100);
            }).catch((error: any) => console.log(error));
    }

}