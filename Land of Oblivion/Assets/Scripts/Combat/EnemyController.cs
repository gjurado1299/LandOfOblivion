using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    
    public float lookRadius = 30f;

    Transform target;
    NavMeshAgent agent;
    Animator enemyAnimator;
    CharacterCombat combat;

    CharacterStats stats;
    CharacterStats targetStats;
    bool pegando = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        combat = GetComponent<CharacterCombat>();
        if(gameObject.name == "Skeleton")
            enemyAnimator = gameObject.GetComponent<Animator>();
        else
            enemyAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius && stats.dead == false){
            
            agent.SetDestination(target.position);
            
            if (distance <= agent.stoppingDistance){

                targetStats = target.GetComponent<CharacterStats>();
                if(combat.attackCooldown <= 0f && targetStats != null && targetStats.dead == false && pegando == false){
                    if(gameObject.name == "Skeleton")
                        pegando = true;
                    else   
                        combat.Attack(targetStats);
                        
                    enemyAnimator.SetTrigger("Attack");
                    
                }

                // Mirar al jugador
                FaceTarget();
            }

            if(agent.velocity.magnitude > 0.1){
                enemyAnimator.SetBool("Walk", true);
            }else{
                enemyAnimator.SetBool("Walk", false);
            }

        }else{
            enemyAnimator.SetBool("Walk", false);
        }

    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void AttackTarget(){
        combat.Attack(targetStats);
    }

    public void ResetPegando(){
        pegando = false;
    }
}
