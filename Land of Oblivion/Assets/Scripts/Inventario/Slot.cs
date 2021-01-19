using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot: MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    [HideInInspector]
    public Item item;

    EventTrigger trigger;
    GameManager gm;

    void Update(){
        if(gm == null){
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if(trigger == null){
            GameObject itemButton = gameObject.transform.GetChild(0).gameObject;
            trigger = itemButton.GetComponent<EventTrigger>();

            if(trigger == null){
                trigger = gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener( (eventData) => { gm.ActivateInfo(this); } );
            trigger.triggers.Add(entry);
        }

    }

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
        EquipmentManager.instance.DropItem(item, false);
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
