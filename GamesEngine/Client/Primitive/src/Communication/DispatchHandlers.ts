import {
    AddDynamicObjects,
    AddStaticObjects,
    RemoveDynamicObjects,
    RemoveStaticObjects,
    SetPlayerId
} from "../SceneHandler.ts";
import { clientDispatcher } from "../../main.ts";

export function AddDispatchHandlers() {
    clientDispatcher.AddHandler("PlayerId", (content) => {
        SetPlayerId(content.id);
    });

    clientDispatcher.AddHandler("FetchDynamicObjects", (content) => {
        RemoveDynamicObjects();
        AddDynamicObjects(content);
    });

    clientDispatcher.AddHandler("FetchStaticObjects", (content) => {
        RemoveStaticObjects();
        AddStaticObjects(content);
    });
}