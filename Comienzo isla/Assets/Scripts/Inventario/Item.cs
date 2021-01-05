using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public string infoPick;
    public Vector3 PickPosition;
    public Vector3 PickRotation;

    public virtual void Use()
    {
        // TODO: Use the item
        // Se va a sobreescribir en cada item
    }

    public void RemoveFromInventory ()
    {
        Inventario.instance.Remove(this);
    }

    public void SetPlayerPrefs(){
        name = PlayerPrefs.GetString("ItemRewardName", "");
        infoPick = PlayerPrefs.GetString("ItemInfoPick", "");

        if(PlayerPrefs.GetInt("QuestIsDefaultItem", 0) == 1)
            isDefaultItem = true;
        else
            isDefaultItem = false;
    }

    public void SavePlayerPrefs(){
        PlayerPrefs.SetString("ItemRewardName", name);
        PlayerPrefs.SetString("ItemInfoPick", infoPick);
        
        if(isDefaultItem)
            PlayerPrefs.SetInt("QuestIsDefaultItem", 1);
        else
            PlayerPrefs.SetInt("QuestIsDefaultItem", 0);
    }

}
