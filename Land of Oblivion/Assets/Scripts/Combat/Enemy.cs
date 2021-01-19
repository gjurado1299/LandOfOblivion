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

    void Update(){
        if(player == null){
            player = GameObject.Find("Havook");
        }
    }

    public void recibeDaño(){
        
        CharacterCombat playerCombat = player.GetComponent<CharacterCombat>();
        if(playerCombat != null){
            playerCombat.Attack(myStats);
        }
    }

    public void BossKilled(){
        player.GetComponent<Player>().quest.goal.EnemyKilled();
    }

    public void Roar(){
        AudioManager.instance.Play("Roar");
    }

    public void BossSwing(){
        AudioManager.instance.Play("BossSwing");
    }

    public void BossSmash(){
        AudioManager.instance.Play("BossSmash");
    }

    public void BossDeath(){
        AudioManager.instance.Play("BossDeath");
    }
}
