using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InventoryItemBase
{
    public int FoodPoints = 20;

    public override void OnUse()
    {
        GameManagerJuego.Instance.Player.Eat(FoodPoints);

        GameManagerJuego.Instance.Player.Inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
