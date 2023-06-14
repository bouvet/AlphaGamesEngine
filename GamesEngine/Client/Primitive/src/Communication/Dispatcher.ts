export class Dispatcher {
    DispatcherHandlers: Handler = {};

    OnMessage(message: any) {
        let content = JSON.parse(message.content);
        let type = message.type;

        if (this.DispatcherHandlers[type]) {
            this.DispatcherHandlers[type](content);
        }
    }

    AddHandler(type: string, handler: (param: any) => void) {
        this.DispatcherHandlers[type] = handler;
    }
}

type Handler = {
    [key: string]: (param: any) => void;
}