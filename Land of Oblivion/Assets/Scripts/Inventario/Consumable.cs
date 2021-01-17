using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public BuffType type;
    public int increment;
    public float time;
    public GameObject destroyable;
    bool consumed = false;

    public override void Use()
    {
        PlayerStats stats = GameObject.Find("Havook").GetComponent<PlayerStats>();
        base.Use();

        if(stats.isFullHealth() != true && type == BuffType.Health){
            stats.IncreaseHealth(increment);
            consumed = true;
        }

        if(type == BuffType.Damage && stats.buffed == false){
            stats.IncreaseDamage(increment, time);
            consumed = true;
        }

        if(consumed == true){
            if(destroyable != null){
                Destroy(destroyable);
            }else{
                Debug.Log("DESTROYABLE ES NULL");
                Debug.Log(destroyable);
            }
                
            RemoveFromInventory();
        }
    }
}

public enum BuffType { Health, Damage }