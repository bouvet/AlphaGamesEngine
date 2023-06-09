// import {
//     AddDynamicObjects,
//     AddStaticObjects,
//     RemoveDynamicObjects,
//     RemoveStaticObjects,
//     SetPlayerId
// } from "../SceneHandler.ts";
import {RemoveAllCharacters, RenderCharacters, dispatcher, SetPlayerId} from "../main.ts";



export function AddDispatchHandlers(){
    dispatcher.AddHandler("PlayerId", (content) => {
        SetPlayerId(content.id);
    });

    dispatcher.AddHandler("FetchDynamicObjects", (content) => {
        // RemoveDynamicObjects();
        // AddDynamicObjects(content);
        RemoveAllCharacters();
        RenderCharacters(content);
    });

    dispatcher.AddHandler("FetchStaticObjects", (content) => {
        // RemoveStaticObjects();
        // AddStaticObjects(content);
    });
}