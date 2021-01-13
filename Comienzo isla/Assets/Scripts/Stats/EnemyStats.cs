using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : CharacterStats
{

    bool waitBool = false;
    WaveManager waveManager;

    public GameObject loot;
    public float probDrop;
    

    public override void Die(){
        base.Die();
        // Lanzar animacion / ragdoll effect
        StartCoroutine(DeadEnemy());
    }

    void Start(){
        if(SceneManager.GetActiveScene().buildIndex == 3)
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    void Update(){
        
        if(waitBool == true){
            Destroy(gameObject);
            waitBool = false;
            
            // Soltar loot
            
            if(Random.Range(0f, 1.0f) < probDrop){
                Instantiate(loot, gameObject.transform.position, Quaternion.identity);
            }

            if(waveManager != null){
                waveManager.EnemyDied();
            }
        }
    }

    IEnumerator DeadEnemy(){
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(3);
        waitBool = true;
    }
}
