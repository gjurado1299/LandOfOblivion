using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public BuffType type;
    public int increment;
    public float time;
    bool consumed = false;

    public override void Use()
    {
        GameObject havook = GameObject.Find("Havook");
        PlayerStats stats = havook.GetComponent<PlayerStats>();
        GameObject bodyItems = GameObject.Find("Havook").transform.GetChild(1).GetChild(0).gameObject;
        base.Use();

        if(stats.isFullHealth() != true && type == BuffType.Health){
            stats.IncreaseHealth(increment);
            consumed = true;
        }

        if(type == BuffType.Damage && stats.GetBuffed() == false){
            stats.IncreaseDamage(increment, time);
            consumed = true;
        }

        if(consumed == true){
            AudioManager.instance.Play("Potion");
            RemoveFromInventory();
            EquipmentManager.instance.DropItem(this, true);
        }
    }
}

public enum BuffType { Health, Damage }