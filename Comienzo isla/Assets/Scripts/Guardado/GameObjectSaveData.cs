using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectSaveData
{
    public string id;
    public float[] position;
    public float[] rotation;
    public bool activeSelf;

    public GameObjectSaveData(GameObject gobject){
        id = gobject.name;
        position = new float[3];
        rotation = new float[3];

        position[0] = gobject.transform.position.x;
        position[1] = gobject.transform.position.y;
        position[2] = gobject.transform.position.z;

        rotation[0] = gobject.transform.rotation.x;
        rotation[1] = gobject.transform.rotation.y;
        rotation[2] = gobject.transform.rotation.z;

        activeSelf = gobject.activeSelf;
    }
}