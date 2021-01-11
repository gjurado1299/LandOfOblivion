using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    bool started = false;

    void OnTriggerEnter(Collider other){
        if(started == false && other.gameObject.CompareTag("Player")){
            Debug.Log("Empieza la batalla del boss");
            started = true;
        }
    }
}
