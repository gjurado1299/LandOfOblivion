using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    bool waitBool = false;
    public override void Die(){
        base.Die();

        // Lanzar animacion / ragdoll effect
        StartCoroutine(DeadEnemy());

    }

    void Update(){
        if(waitBool == true){
            Destroy(gameObject);
            waitBool = false;
            
            // Soltar loot

        }
    }

    IEnumerator DeadEnemy(){
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(3);
        waitBool = true;
    }
}
