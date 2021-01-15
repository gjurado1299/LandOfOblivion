using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryObjectData
{
    public List<GameObjectSaveData> equipmentRightHand = new List<GameObjectSaveData>();
    public List<GameObjectSaveData> equipmentLeftHand = new List<GameObjectSaveData>();
    public List<GameObjectSaveData> inventoryItems = new List<GameObjectSaveData>();

    public void AddRightHand(GameObjectSaveData data){
        equipmentRightHand.Add(data);
    }

    public void AddLeftHand(GameObjectSaveData data){
        equipmentLeftHand.Add(data);
    }

    public void AddItem(GameObjectSaveData data){
        inventoryItems.Add(data);
    }
}