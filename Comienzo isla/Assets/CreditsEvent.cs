using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEvent : MonoBehaviour
{
    void BackToMenuEvent(){
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().GoMainMenu();
    }
}
