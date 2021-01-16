using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : InventoryItemBase
{
    public int HealthPoints = 20;

    public override void OnUse()
    {
        GameManagerJuego.Instance.Player.Rehab(HealthPoints);

        GameManagerJuego.Instance.Player.Inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
