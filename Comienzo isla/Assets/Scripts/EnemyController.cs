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

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        combat = GetComponent<CharacterCombat>();
        enemyAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius && stats.dead == false){
            
            agent.SetDestination(target.position);
            
            if (distance <= agent.stoppingDistance){

                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null && targetStats.dead == false){
                    enemyAnimator.SetTrigger("Attack");
                    combat.Attack(targetStats);
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
}
