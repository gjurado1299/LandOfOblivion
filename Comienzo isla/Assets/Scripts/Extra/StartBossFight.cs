using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    bool started = false;

    void OnTriggerEnter(Collider other){
        if(started == false && other.gameObject.CompareTag("Player")){
            GameObject.Find("Havook").GetComponent<Player>().quest.mainObjective = "Derrota a Rendskull";
            
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
            started = true;
        }
    }
}
