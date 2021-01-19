using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    bool started = false;

    void OnTriggerEnter(Collider other){
        if(started == false && other.gameObject.CompareTag("Player")){
            Player havook = GameObject.Find("Havook").GetComponent<Player>();
            havook.quest.mainObjective = "Derrota a Rendskull";
            havook.quest.started = true;
            
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
            started = true;
        }
    }
}
