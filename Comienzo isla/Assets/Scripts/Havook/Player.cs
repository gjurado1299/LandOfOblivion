using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(LoadedCheck.instance.loaded == true){
            LoadPlayer();
            LoadedCheck.instance.loaded = false;
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
        SaveSystem.SavePlayer(this, stats);
    }

    public void LoadPlayer(){
        Debug.Log("LOADING PLAYER");
        PlayerData data = SaveSystem.LoadPlayer();

        experience = data.experience;
        gold = data.gold;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        gameObject.SetActive(false);
        transform.position = position;
        gameObject.SetActive(true);


        stats.LoadPlayer(data.health);
    }
}
