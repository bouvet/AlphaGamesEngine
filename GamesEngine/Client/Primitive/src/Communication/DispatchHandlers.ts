import {
    AddDynamicObjects,
    AddStaticObjects,
    RemoveDynamicObjects,
    RemoveStaticObjects,
    SetPlayerId
} from "../SceneHandler.ts";
import {dispatcher} from "../../main.ts";

export function AddDispatchHandlers(){
    dispatcher.AddHandler("PlayerId", (content) => {
        SetPlayerId(content.id);
    });

    dispatcher.AddHandler("FetchDynamicObjects", (content) => {
        RemoveDynamicObjects();
        AddDynamicObjects(content);
    });

    dispatcher.AddHandler("FetchStaticObjects", (content) => {
        RemoveStaticObjects();
        AddStaticObjects(content);
    });
}