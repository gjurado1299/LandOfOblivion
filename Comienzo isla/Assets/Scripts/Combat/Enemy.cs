using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    GameObject player;
    CharacterStats myStats;

    void Start(){
        player = GameObject.FindWithTag("Player");
        myStats = GetComponent<CharacterStats>();
    }

    public void recibeDaño(){
        CharacterCombat playerCombat = player.GetComponent<CharacterCombat>();
        if(playerCombat != null){
            playerCombat.Attack(myStats);
        }
    }
}
