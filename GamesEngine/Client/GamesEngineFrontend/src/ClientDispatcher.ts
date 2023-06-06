type Handler = {
    [key:string]: (param: any) => void;
}
export let DispatcherHandlers: Handler = {};

export function OnMessage(message: any) {
    let content = JSON.parse(message.content);
    let type = message.type;

    if(DispatcherHandlers[type]){
        DispatcherHandlers[type](content);
    }
}
