using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEvent : MonoBehaviour
{
    void ClosingEvent(){
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel();
    }
}
