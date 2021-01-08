using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public BuffType type;
    public int increment;

    public override void Use()
    {
        base.Use();

        if(type == BuffType.Health){
            GameObject.Find("Havook").GetComponent<PlayerStats>().IncreaseHealth(increment);
        }

        RemoveFromInventory();
    }


}

public enum BuffType { Health, Damage }