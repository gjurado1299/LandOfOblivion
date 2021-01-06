using UnityEngine;
using UnityEngine.UI;

public class Slot: MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    [HideInInspector]
    public Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.gameObject.SetActive(false);
    }

    public void OnRemoveButton()
    {
        EquipmentManager.instance.DropItem(item);
        Inventario.instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
