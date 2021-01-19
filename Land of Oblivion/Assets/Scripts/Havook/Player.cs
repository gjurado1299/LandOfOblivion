using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int experience;
    public int gold;
    public GameManager gameManager;

    // puede ser una lista
    public Quest quest;
    PlayerStats stats;

    public void Start(){
        experience = PlayerPrefs.GetInt("Experience", 0);
        gold = PlayerPrefs.GetInt("Gold", 0);
        quest.SetPlayerPrefs();

        stats = gameObject.GetComponent<PlayerStats>();

        if(quest.isActive && quest.goal.IsReached() && SceneManager.GetActiveScene().buildIndex == 4){
            Vector3 position = new Vector3(42.78f, 21.95f, 43.24f);
            gameObject.SetActive(false);
            gameObject.transform.position = position;
            gameObject.SetActive(true);
        }

        if(LoadedCheck.instance != null){
            if(LoadedCheck.instance.loaded == true){
                LoadPlayer(true);
                int index = SceneManager.GetActiveScene().buildIndex;
                if(index == 3 && quest.started == true)
                    GameObject.Find("WaveManager").GetComponent<WaveManager>().AdjustSceneOnLoad(quest);

                if(index == 1)
                    LoadedCheck.instance.skipDialogue = true;
                    
                LoadedCheck.instance.loaded = false;

            }else if(LoadedCheck.instance.died == true){
                LoadPlayer(false);
                LoadedCheck.instance.died = false;
            }
        }
    }

    public void SavePlayerPrefs(){
        PlayerPrefs.SetInt("Experience", experience);
        PlayerPrefs.SetInt("Gold", gold);
        quest.SavePlayerPrefs();
    }

    public void getMissionObject(){
        if(quest.isActive){
            quest.goal.ItemCollected();
        }
    }

    public void missionCompleted(){
        experience += quest.experienceReward;
        gold += quest.goldReward;

        if(quest.removableObject.Length > 0){
            GameObject removable = EquipmentManager.instance.FindItem(quest.removableObject);
            if(removable != null){

                ItemPickUp i = removable.GetComponent<ItemPickUp>();
                if (i != null){
                    Inventario.instance.Remove(i.item);
                }
                Destroy(removable);
            }
        }

        GameObject itemR = GameObject.Find(quest.itemReward);
        if(itemR != null){
            ItemPickUp i = itemR.GetComponent<ItemPickUp>();
            if (i != null){
                i.PickUp();
            }
        }
        
        quest.Complete();

        if(gameManager){
            gameManager.bloqueado = false;
        }
    }

    public void SavePlayer(){
        GameObject rightH = gameObject.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;
        GameObject leftH = gameObject.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        GameObject bodyItems = gameObject.transform.GetChild(1).GetChild(0).gameObject;

        SaveSystem.SavePlayer(this, stats);
        SaveSystem.SaveQuest(quest);
        SaveSystem.SaveQuestGoal(quest.goal);
        SaveSystem.SaveDialogueTrigger(quest.dialogueTrigger);
        SaveSystem.SaveInventoryObjects(bodyItems, rightH, leftH, false);
    }

    public void LoadPlayer(bool loadAll){
        GameObject rightH = gameObject.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;
        GameObject leftH = gameObject.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        GameObject bodyItems = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        InventoryObjectData inv = null;
        Vector3 position;

        if(loadAll == true){
            PlayerData data = SaveSystem.LoadPlayer();
            QuestData q = SaveSystem.LoadQuest();
            QuestGoalData qg = SaveSystem.LoadQuestGoal();
            DialogueTriggerData dt = SaveSystem.LoadDialogueTrigger();
            inv = SaveSystem.LoadInventoryObjects(false);

            experience = data.experience;
            gold = data.gold;

            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            gameObject.SetActive(false);
            transform.position = position;
            gameObject.SetActive(true);

            stats.LoadPlayer(data.health);
            quest.LoadQuest(q);
            quest.goal.LoadQuestGoal(qg);
            quest.dialogueTrigger.LoadDialogueTrigger(dt);
        }else{
            inv = SaveSystem.LoadInventoryObjects(true);
        }

        foreach(GameObjectSaveData obj in inv.equipmentRightHand){
            GameObject weapon = Instantiate(Resources.Load("Weapons/"+obj.id)) as GameObject;
            weapon.name = obj.id;
            weapon.transform.SetParent(rightH.transform);

            weapon.transform.GetChild(0).gameObject.SetActive(false);
            weapon.GetComponent<Rigidbody>().isKinematic = true;

            Item item = weapon.GetComponent<ItemPickUp>().item;
            
            weapon.SetActive(false);
            weapon.transform.localPosition = item.PickPosition;
            weapon.transform.localEulerAngles = item.PickRotation;

            int index = SceneManager.GetActiveScene().buildIndex;
            if(index == 5 || index == 6){
                weapon.transform.localScale = new Vector3((weapon.transform.localScale.x*2/3), (weapon.transform.localScale.y*2/3), (weapon.transform.localScale.z*2/3));
            }

            Inventario.instance.Add(item);
            
        }

        foreach(GameObjectSaveData obj in inv.equipmentLeftHand){
            GameObject weapon = Instantiate(Resources.Load("Weapons/"+obj.id)) as GameObject;
            weapon.name = obj.id;
            weapon.transform.SetParent(leftH.transform);

            weapon.transform.GetChild(0).gameObject.SetActive(false);
            weapon.GetComponent<Rigidbody>().isKinematic = true;

            Item item = weapon.GetComponent<ItemPickUp>().item;
            
            weapon.SetActive(false);
            weapon.transform.localPosition = item.PickPosition;
            weapon.transform.localEulerAngles = item.PickRotation;

            int index = SceneManager.GetActiveScene().buildIndex;
            if(index == 5 || index == 6){
                weapon.transform.localScale = new Vector3((weapon.transform.localScale.x*2/3), (weapon.transform.localScale.y*2/3), (weapon.transform.localScale.z*2/3));
            }

            Inventario.instance.Add(item);
        }

        foreach(GameObjectSaveData obj in inv.inventoryItems){
            if(obj.id == "Gema" || obj.id == "Llave" || obj.id == "Mithril"){
                GameObject itemD = GameObject.Find(obj.id);
                if(itemD != null){
                    Destroy(itemD);
                }
            }

            GameObject item = Instantiate(Resources.Load("Props/"+obj.id)) as GameObject;
            item.name = obj.id;
            item.transform.SetParent(bodyItems.transform);

            ItemPickUp iP = item.GetComponent<ItemPickUp>();
            Item itemPick = iP.item;

            item.transform.GetChild(0).gameObject.SetActive(false);
            item.GetComponent<Rigidbody>().isKinematic = true;
            
            item.SetActive(false);
            item.transform.localPosition = itemPick.PickPosition;
            item.transform.localEulerAngles = itemPick.PickRotation;

            int index = SceneManager.GetActiveScene().buildIndex;
            if(index == 5 || index == 6){
                item.transform.localScale = new Vector3((item.transform.localScale.x*2/3), (item.transform.localScale.y*2/3), (item.transform.localScale.z*2/3));
            }

            iP.picked = true;
            Inventario.instance.Add(itemPick);
        }

        if(loadAll == true){            
            //Si has loadeado partida, guardamos los nuevos playerprefs que se cargaran si mueres
            SaveSystem.SaveInventoryObjects(bodyItems, rightH, leftH, true);
            SavePlayerPrefs();
            stats.SavePlayerPrefs();
        }
    }
}
