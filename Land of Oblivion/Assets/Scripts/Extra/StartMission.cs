using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMission : MonoBehaviour
{
    bool started = false;

    void OnTriggerEnter(Collider other){
        if(started == false && other.gameObject.CompareTag("Player")){
            GameObject.Find("WaveManager").GetComponent<WaveManager>().StartTimer();
            started = true;
        }
    }
}
