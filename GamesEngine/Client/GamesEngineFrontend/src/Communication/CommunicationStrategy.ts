import * as signalR from "@microsoft/signalr";

export abstract class ICommunicationStrategy{
    abstract Start(callback: any): void;
    abstract Stop(): void;
    abstract SendToServer(message: object): void;
}

export class SignalRCommunicationStrategy extends ICommunicationStrategy{
    connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7247/gamehub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    }).build();

    SendToServer(message: object): void {
        this.connection.send("FromClient", JSON.stringify(message));
    }

    async Start(callback: any): Promise<void> {
        try{
            await this.connection.start();
            console.log("SignalR connected");
            this.connection.on("FromServer", (message: any) => callback(message));
        }catch (e) {
            console.log(e);
        }
    }

    Stop(): void {
        this.connection.stop();
    }
}