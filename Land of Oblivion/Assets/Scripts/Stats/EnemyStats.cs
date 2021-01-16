using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : CharacterStats
{

    bool waitBool = false;
    WaveManager waveManager;

    public GameObject loot;
    public bool boss = false;
    bool enraged = false;
    public float enragedPerc = .25f;
    public float probDrop;
    public float attackRange = 5f;

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

        if(boss == true && enraged == false){
            if(isEnraged(enragedPerc) == true){
                gameObject.GetComponent<Animator>().SetBool("IsEnraged", true);
                attackRange = 8f;
                damage.AddModifier(20);
                enraged = true;
            }
        }

        CheckBar();

        if(waitBool == true){
            Destroy(gameObject);
            waitBool = false;
            
            // Soltar loot
            
            if(Random.Range(0f, 1.0f) < probDrop){
                GameObject instanciado = Instantiate(loot, gameObject.transform.position, Quaternion.identity);
                instanciado.name = loot.name;
            }

            if(waveManager != null){
                waveManager.EnemyDied();
            }
        }
    }

    IEnumerator DeadEnemy(){
        if(boss == true || gameObject.name == "Skeleton")   {
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }else{
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Die");
        }
        yield return new WaitForSeconds(3);
        waitBool = true;
    }
}
