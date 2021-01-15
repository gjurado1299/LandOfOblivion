using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    private float attackCooldown = 0f;
    const float combatCooldown = 5;
    float lastAttackTime;

    public float attackDelay = .6f;

    public bool InCombat {get; private set; }
    public event System.Action OnAttack;
    GameManager gameManager;

    void Start(){
        myStats = GetComponent<CharacterStats>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        attackCooldown -= Time.deltaTime;

        if(Time.time - lastAttackTime > combatCooldown || myStats.dead == true)
            InCombat = false;

        if(myStats.blocking){
            gameManager.bloqueado = true;
        }
    }

    public void Attack(CharacterStats targetStats){

        if(targetStats == null){
            targetStats = GameObject.Find("Havook").GetComponent<PlayerStats>();
        }

        if( attackCooldown <= 0f && targetStats.dead == false && myStats.dead == false && gameManager.ActivePanels() == false){
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if(OnAttack != null){
                OnAttack();
            }
            
            // Ajustar en función del modifier, que sera de tipo int
            attackCooldown = myStats.attackSpeed.GetValue();
            InCombat = true;
            lastAttackTime = Time.time;
        }else if(targetStats.dead == true || myStats.dead == true){
            InCombat = false;
        }
    }

    IEnumerator DoDamage( CharacterStats stats, float delay){
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());

        if(stats.currentHealth <= 0){
            InCombat = false;
        }
    }
}
