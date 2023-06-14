import { ClientDispatcher } from "./ClientDispatcher";
import { ICommunicationStrategy } from "./CommunicationStrategy";

export abstract class ICommunication {
    clientDispatcher: ClientDispatcher;
    strategy: ICommunicationStrategy;

    abstract Init(): void;
    abstract SendToServer(message: object): void;

    constructor(dispatcher: ClientDispatcher, strategy: ICommunicationStrategy) {
        this.clientDispatcher = dispatcher;
        this.strategy = strategy;
    }

    OnMessage(message: any) {
        this.clientDispatcher.OnMessage(message);
    }
}

export class Communication extends ICommunication {
    SendToServer(message: object) {
        this.strategy.SendToServer(message);
    }

    async Init() {
        await this.strategy.Start((mes: any) => this.OnMessage(mes))
    }
}