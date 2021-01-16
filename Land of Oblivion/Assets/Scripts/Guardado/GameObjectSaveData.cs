using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectSaveData
{
    public string id;
    public bool activeSelf;

    public GameObjectSaveData(GameObject gobject){
        id = gobject.name;
        activeSelf = gobject.activeSelf;
    }
}