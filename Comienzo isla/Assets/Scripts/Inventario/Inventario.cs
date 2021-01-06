using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> items = new List<Item>();

    public animationStateController animacion;
    
    #region Singleton

    public static Inventario instance;

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


    public bool Add(Item item)
    {
        if(animacion == null)
            animacion = GameObject.FindWithTag("Player").GetComponent<animationStateController>();
        

        if(!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("No hay suficiente espacio en el inventario.");
                animacion.sinEspacio = true;
                return false;
            }
            animacion.sinEspacio = false;
            items.Add(item);

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
            
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);  

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
