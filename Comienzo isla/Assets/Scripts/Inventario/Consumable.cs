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
        PlayerStats stats = GameObject.Find("Havook").GetComponent<PlayerStats>();
        base.Use();

        if(stats.isFullHealth() != true){
            if(type == BuffType.Health){
                stats.IncreaseHealth(increment);
            }

            RemoveFromInventory();
        }
    }


}

public enum BuffType { Health, Damage }