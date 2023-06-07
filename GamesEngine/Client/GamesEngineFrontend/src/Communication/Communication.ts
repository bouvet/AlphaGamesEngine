import {Dispatcher} from "./Dispatcher.ts";
import {ICommunicationStrategy} from "./CommunicationStrategy.ts";

export abstract class ICommunication
{
    dispatcher: Dispatcher;
    strategy: ICommunicationStrategy;

    abstract Init(): void;
    abstract SendToServer(message: object): void;

    constructor(dispatcher: Dispatcher, strategy: ICommunicationStrategy){
        this.dispatcher = dispatcher;
        this.strategy = strategy;
    }

    OnMessage(message: any) {
     this.dispatcher.OnMessage(message);
    }
}

export class Communication extends ICommunication
{
    SendToServer(message: object) {
        this.strategy.SendToServer(message);
    }

    async Init() {
        await this.strategy.Start((mes: any) => this.OnMessage(mes))
    }
}