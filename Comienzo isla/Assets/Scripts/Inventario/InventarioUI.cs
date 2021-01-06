using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventario inventario;
    GameManager gm;
    Slot[] slots;
    CanvasGroup c;

    #region Singleton

    public static InventarioUI instance;
    void Awake()
    {
        if(instance != null)
        {

            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        c = gameObject.GetComponentInParent(typeof(CanvasGroup)) as CanvasGroup;
        inventario = Inventario.instance;
        inventario.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<Slot>();
    }

    public void UpdateUI()
    {

        if(gm == null)
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(slots != null){
            for (int i = 0; i < slots.Length; i++)
            {
                if(i < inventario.items.Count)
                {
                    slots[i].AddItem(inventario.items[i]);
                } else
                {
                    slots[i].ClearSlot();
                }
            }
        }

        if(inventario.items.Count == 0)
            gm.EmptyInfo();
    }
}
