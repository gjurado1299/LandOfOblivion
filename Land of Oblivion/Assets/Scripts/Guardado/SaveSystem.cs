using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveScene(int data){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scene.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayer (Player player, PlayerStats stats){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, stats);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveQuest(Quest quest){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/quest.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        QuestData data = new QuestData(quest);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveQuestGoal(QuestGoal goal){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/questGoal.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        QuestGoalData data = new QuestGoalData(goal);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveDialogueTrigger(DialogueTrigger dialogue){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/dTrigger.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        DialogueTriggerData data = new DialogueTriggerData(dialogue);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveInventoryObjects(GameObject itemBp, GameObject rightHand, GameObject leftHand, bool byScene){
        InventoryObjectData inventoryObjectData = new InventoryObjectData();
        string path = "";

        
        for (int i = itemBp.transform.childCount-1; i >= 0; i--){
            Transform child = itemBp.transform.GetChild(i);
            ItemPickUp item = child.gameObject.GetComponent<ItemPickUp>();
            if(item != null){
                inventoryObjectData.AddItem(new GameObjectSaveData(child.gameObject));
            }
        }

        for (int i = rightHand.transform.childCount-1; i >= 0; i--){
            Transform child = rightHand.transform.GetChild(i);
            ItemPickUp item = child.gameObject.GetComponent<ItemPickUp>();
            if(item != null){
                inventoryObjectData.AddRightHand(new GameObjectSaveData(child.gameObject));
            }
        }

        for (int i = leftHand.transform.childCount-1; i >= 0; i--){
            Transform child = leftHand.transform.GetChild(i);
            ItemPickUp item = child.gameObject.GetComponent<ItemPickUp>();
            if(item != null){
                inventoryObjectData.AddLeftHand(new GameObjectSaveData(child.gameObject));
            }
        }

        BinaryFormatter formatter = new BinaryFormatter();
        if(byScene == true){
            path = Application.persistentDataPath + "/inventoryObjectsScene.data";
        }else{
            path = Application.persistentDataPath + "/inventoryObjects.data";
        }

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, inventoryObjectData);
        stream.Close();
    }

    public static int LoadScene(){
        string path = Application.persistentDataPath + "/scene.data";

        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int buildIndex = (int)formatter.Deserialize(stream);
            stream.Close();

            return buildIndex;
        }else{
            Debug.LogError("Save not found in "+path);
            return -1;
        }
    }

    public static InventoryObjectData LoadInventoryObjects(bool byScene){
        string path = "";

        if(byScene == true){
            path = Application.persistentDataPath + "/inventoryObjectsScene.data";
        }else{
            path = Application.persistentDataPath + "/inventoryObjects.data";
        }

        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryObjectData data = formatter.Deserialize(stream) as InventoryObjectData;
            stream.Close();

            return data;
        }else{
            Debug.LogError("Save not found in "+path);
            return null;
        }
    }

    public static PlayerData LoadPlayer(){
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }else{
            Debug.LogError("Save not found in "+path);
            return null;
        }
    }

    public static QuestData LoadQuest(){
        string path = Application.persistentDataPath + "/quest.data";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            QuestData data = formatter.Deserialize(stream) as QuestData;
            stream.Close();

            return data;
        }else{
            Debug.LogError("Save not found in "+path);
            return null;
        }
    }

    public static QuestGoalData LoadQuestGoal(){
        string path = Application.persistentDataPath + "/questGoal.data";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            QuestGoalData data = formatter.Deserialize(stream) as QuestGoalData;
            stream.Close();

            return data;
        }else{
            Debug.LogError("Save not found in "+path);
            return null;
        }
    }

    public static DialogueTriggerData LoadDialogueTrigger(){
        string path = Application.persistentDataPath + "/dTrigger.data";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DialogueTriggerData data = formatter.Deserialize(stream) as DialogueTriggerData;
            stream.Close();

            return data;
        }else{
            Debug.LogError("Save not found in "+path);
            return null;
        }
    }
}

