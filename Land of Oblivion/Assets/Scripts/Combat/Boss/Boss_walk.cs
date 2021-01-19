using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_walk : StateMachineBehaviour
{
    public float speed = 2.5f;
    Transform player;
    Rigidbody rb;
    EnemyStats stats;
    bool PlayingWalk = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        stats = animator.GetComponent<EnemyStats>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Vector3 target = new Vector3(player.position.x, rb.position.y, player.position.z); 
        Vector3 newP = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        Vector3 direction = (player.position - rb.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.deltaTime * 1f);

        if(Vector3.Distance(player.position, rb.position) <= stats.attackRange){

            if(PlayingWalk == true){
                AudioManager.instance.Stop("BossWalk");
                PlayingWalk = false;
            }

            if(player.GetComponent<PlayerStats>().dead == false){
                animator.SetTrigger("Attack");
            }else
                animator.SetBool("Moving", false);
        }else{
            if(PlayingWalk == false){
                AudioManager.instance.Play("BossWalk");
                PlayingWalk = true;
            }

            rb.MovePosition(newP);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
