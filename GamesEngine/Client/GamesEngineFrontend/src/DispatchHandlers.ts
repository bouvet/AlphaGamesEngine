import {DispatcherHandlers} from "./ClientDispatcher.ts";
import {
    AddDynamicObjects,
    AddStaticObjects,
    RemoveDynamicObjects,
    RemoveStaticObjects, SetPlayerId
} from "./SceneHandler.ts";

export function AddDispatchHandlers(){
    DispatcherHandlers["PlayerId"] = (content) => {
        SetPlayerId(content.id);
    }

    DispatcherHandlers["FetchDynamicObjects"] = (content) => {
        RemoveDynamicObjects();
        AddDynamicObjects(content);
    }

    DispatcherHandlers["FetchStaticObjects"] = (content) => {
        RemoveStaticObjects();
        AddStaticObjects(content);
    }
}