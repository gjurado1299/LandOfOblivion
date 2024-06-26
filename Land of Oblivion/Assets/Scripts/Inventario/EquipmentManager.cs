﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if(instance != null)
        {
            instance.reset = true;
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion


    Inventario inventario;
    Equipment[] currentEquipment;
    public GameObject[] bodyParts = new GameObject[System.Enum.GetNames(typeof(EquipmentSlot)).Length];

    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;
    animationStateController animatorController;

    public delegate void OnEquipmentChanged (Equipment newItem, Equipment oldItemn);
    public OnEquipmentChanged onEquipmentChanged;

    public bool hasWeapon = false;
    public bool reset = false;

    void Start ()
    {
        inventario = Inventario.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        animatorController = GameObject.FindWithTag("Player").GetComponent<animationStateController>();

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach(WeaponAnimations a in weaponAnimations){
            weaponAnimationsDict.Add(a.weapon, a.clips);
        }

    }

    public void Equip (Equipment newItem)
    {

        if(bodyParts[0] == null){
            findBodyparts();
        }

        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventario.Add(oldItem);

            // Buscar en la parte del cuerpo correspondiente el hijo con un item de nombre oldItem, y desactivarlo
            foreach(Transform child in bodyParts[slotIndex].transform)
            {
                ItemPickUp i = child.gameObject.GetComponent<ItemPickUp>();
                if(i != null){
                    if(i.item.name == oldItem.name)
                        child.gameObject.SetActive(false);
                }
            }
        }

        // Play audio
        AudioManager.instance.Play("Equip");

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        // Buscar en la parte del cuerpo correspondiente el hijo con un item de nombre newItem, y activarlo
        foreach(Transform child in bodyParts[slotIndex].transform)
        {
            ItemPickUp i = child.gameObject.GetComponent<ItemPickUp>();
            if(i != null){
                if(i.item.name == newItem.name)
                    child.gameObject.SetActive(true);
            }
        }

        // Override de animaciones
        if(weaponAnimationsDict.ContainsKey(newItem)){
            Debug.Log("");
            animatorController.currentAttackAnimSet = weaponAnimationsDict[newItem];
        }
    }

    public void UnEquip (int slotIndex) 
    {

        if(bodyParts[0] == null){
            findBodyparts();
        }
        

        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventario.Add(oldItem);
            
            foreach(Transform child in bodyParts[slotIndex].transform)
            {
                ItemPickUp i = child.gameObject.GetComponent<ItemPickUp>();
                if(i != null){
                    if(i.item.name == oldItem.name)
                        child.gameObject.SetActive(false);
                }
            }

            currentEquipment[slotIndex] = null;

            if(onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            // Override de animaciones
            animatorController.currentAttackAnimSet = animatorController.defaultAttackAnimSet;
            
        }
    }

    public void UnEquipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
    }

    public Sprite returnEquipped(int index){
        if(currentEquipment != null){
            if(currentEquipment[index] != null){
                return currentEquipment[index].icon;
            }
        }

        return null;
    }

    public GameObject FindItem(string itemName){
        GameObject bp = GameObject.Find("Havook").transform.GetChild(1).GetChild(0).gameObject;
        GameObject child;
        for (int i = bp.transform.childCount-1; i >= 0; i--){
            child = bp.transform.GetChild(i).gameObject;
            if(child.name == itemName)
                return child;
        }

        return null;
    }

    void Update()
    {
        if(animatorController == null){
            GameObject player = GameObject.FindWithTag("Player");
            
            if(player != null)
                animatorController = player.GetComponent<animationStateController>();
        }

        if(Input.GetKeyDown(KeyCode.U))
            UnEquipAll();

        if(SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 8 && bodyParts[0] == null){
            findBodyparts();
            SetGameObjects();
        }

        if(currentEquipment[3] != null || currentEquipment[4] != null){
            hasWeapon = true;
        }else{
            hasWeapon = false;
        }

        if(reset == true && instance == this){
            UnEquipAll();
            reset = false;
        }
    }

    public void DropItem(Item item, bool destroy){
        GameObject havook = GameObject.Find("Havook");
        GameObject objectItem = null;

        if(item.GetType() == typeof(Equipment)){
            // Encontrar item entre los hijos de la parte del cuerpo correspondiente
            foreach(GameObject bp in EquipmentManager.instance.bodyParts){
                for (int i = bp.transform.childCount-1; i >= 0; i--){
                    ItemPickUp itemP = bp.transform.GetChild(i).gameObject.GetComponent<ItemPickUp>();
                    if(itemP != null && itemP.item == item){
                        objectItem = itemP.gameObject;
                    }
                }
            }

        }else{
            // Encontrar item entre los hijos de la parte del cuerpo correspondiente
            GameObject bp = havook.transform.GetChild(1).GetChild(0).gameObject;
            for (int i = bp.transform.childCount-1; i >= 0; i--){
                ItemPickUp itemP = bp.transform.GetChild(i).gameObject.GetComponent<ItemPickUp>();
                if(itemP != null && itemP.item == item){
                    objectItem = itemP.gameObject;
                }
            }
        }

        if(destroy == true){
            Destroy(objectItem);
        }else{
            // Darle parent = null
            objectItem.transform.SetParent(null);

            // Darle posicion = havook
            objectItem.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1, 1, 1);

            // Activar rigidbody e interactable
            objectItem.transform.GetChild(0).gameObject.SetActive(true);
            objectItem.GetComponent<Rigidbody>().isKinematic = false;

            objectItem.SetActive(true);

            // Deshacer progreso si el objeto es de una misión.
            if(objectItem.transform.GetChild(0).gameObject.GetComponent<Interactable>().type == "missionObj"){
                QuestGoal goal = havook.GetComponent<Player>().quest.goal;
                
                if(goal.goalType == GoalType.Gathering)
                    goal.currentAmount--;
            }
        }
    }

    void findBodyparts(){
        GameObject havook = GameObject.Find("Havook");
        GameObject head = null;
        GameObject chest = null;
        GameObject legs = null;
        GameObject weapon = null;
        GameObject shield = null;
        GameObject feet = null;

        if(havook){
            head = havook.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(0).gameObject;
            chest = havook.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).gameObject;
            
            // Ver este en detalle cuando haya armadura (todavia no asignado)
            legs = havook.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

            weapon = havook.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;
            shield = havook.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;

            // Ver este en detalle cuando haya armadura (todavia no asignado)
            feet = havook.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        }
        bodyParts[0] = head;
        bodyParts[1] = chest;
        bodyParts[2] = legs;
        bodyParts[3] = weapon;
        bodyParts[4] = shield;
        bodyParts[5] = feet;
    }

    public void SaveGameObjects(){
        GameObject weapons = new GameObject("Weapons");
        GameObject items = new GameObject("ItemsPlayer");
        Vector3 zero = new Vector3(0,0,0);

        weapons.transform.position = zero;
        items.transform.position = zero;

        foreach(GameObject bp in bodyParts){
            for (int i = bp.transform.childCount-1; i >= 0; i--){
                Transform child = bp.transform.GetChild(i);
                ItemPickUp item = child.gameObject.GetComponent<ItemPickUp>();
                if(item != null){
                    int index = SceneManager.GetActiveScene().buildIndex;
                    
                    if(index == 5 || index == 6){
                        child.localScale = new Vector3((child.localScale.x*3/2), (child.localScale.y*3/2), (child.localScale.z*3/2));
                    }

                    item.gameObject.transform.parent = weapons.transform; 
                    item.gameObject.transform.localPosition = zero;
                    item.gameObject.transform.localEulerAngles = zero;
                }
            }
        }

        GameObject bodyItems = GameObject.Find("Havook").transform.GetChild(1).GetChild(0).gameObject;

        for (int i = bodyItems.transform.childCount-1; i >= 0; i--){
            Transform child = bodyItems.transform.GetChild(i);
            ItemPickUp item = child.gameObject.GetComponent<ItemPickUp>();
            if(item != null){
                int index = SceneManager.GetActiveScene().buildIndex;
                    
                if(index == 5 || index == 6){
                    child.localScale = new Vector3((child.localScale.x*3/2), (child.localScale.y*3/2), (child.localScale.z*3/2));
                }

                item.gameObject.transform.parent = items.transform; 
                item.gameObject.transform.localPosition = zero;
                item.gameObject.transform.localEulerAngles = zero;
            }
        }

        DontDestroyOnLoad(weapons);
        DontDestroyOnLoad(items);
    }

    public void SetGameObjects(){
        
        GameObject weapons = GameObject.Find("Weapons");
        GameObject items = GameObject.Find("ItemsPlayer");
        GameObject bodyItems = GameObject.Find("Havook").transform.GetChild(1).GetChild(0).gameObject;
        bool wasActive = false;

        if(weapons){
            for (int i = weapons.transform.childCount-1; i >= 0; i--){
                wasActive = false;
                Transform child = weapons.transform.GetChild(i);
                ItemPickUp w = child.gameObject.GetComponent<ItemPickUp>();
                GameObject interactable = child.GetChild(0).gameObject;

                if(child.gameObject.activeSelf)
                    wasActive = true;

                child.gameObject.SetActive(true);
                interactable.SetActive(true);
                
                int index = SceneManager.GetActiveScene().buildIndex;
                if(index == 5 || index == 6){
                    child.localScale = new Vector3((child.localScale.x*2/3), (child.localScale.y*2/3), (child.localScale.z*2/3));
                }

                child.parent = bodyParts[w.bodyPart].transform;

                //Posicion relativa a la parte del cuerpo
                child.localPosition = w.item.PickPosition;
                child.localEulerAngles = w.item.PickRotation;

                interactable.SetActive(false);
                if(wasActive == false)
                    child.gameObject.SetActive(false);
            }
            Destroy(weapons);
        }

        if(items){
            for (int i = items.transform.childCount-1; i >= 0; i--){
                Transform child = items.transform.GetChild(i);
                int index = SceneManager.GetActiveScene().buildIndex;
                if(index == 5 || index == 6){
                    child.localScale = new Vector3((child.localScale.x*2/3), (child.localScale.y*2/3), (child.localScale.z*2/3));
                }
                child.parent = bodyItems.transform;
            }
            Destroy(items);
        }

    }

    [System.Serializable]
    public struct WeaponAnimations{
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
