using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveScene(int data){
        Debug.Log("SAVING SCENE");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scene.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayer (Player player, PlayerStats stats){
        Debug.Log("SAVING PLAYER");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, stats);

        formatter.Serialize(stream, data);
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


}

