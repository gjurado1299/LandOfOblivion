using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    GameManager gameManager;
    protected CharacterCombat combat;
    protected CharacterStats stats;
    protected AnimatorOverrideController overrideController;

    [System.NonSerialized]
    public AnimationClip[] currentAttackAnimSet;

    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;

    MovimientoTerceraPersona mtp;    

    public bool conversation = false;
    public bool sinEspacio = false;
    public bool nearObject = false;
    
    void Start(){
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;

        mtp = GetComponent<MovimientoTerceraPersona>();
    }

    // Update is called once per frame
    void Update()
    {
        if( !gameManager.bloqueado){
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized;


            if(Input.GetButtonDown("Jump")){
                animator.SetBool("IsInAir", true);
            }

            if(Input.GetButtonDown("Interact") && !conversation && !sinEspacio && nearObject){
                animator.SetBool("IsPicking", true);
                nearObject = false;
            }
            else
                animator.SetBool("IsPicking", false);

            if(Input.GetButtonDown("Roll") && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand To Roll") == false){
                animator.SetBool("IsRolling", true);
                AudioManager.instance.StopWalking();
                mtp.playing = false;
            }else{
                animator.SetBool("IsRolling", false);
            }

            if(direccion.magnitude >= 0.1f && !animator.GetBool("IsInAir")){
                animator.SetInteger("TimeRunning",(animator.GetInteger("TimeRunning")+1));
                animator.SetBool("IsMoving", true);
            }else{
                animator.SetBool("IsMoving", false);
            } 

            animator.SetBool("HasWeapon", EquipmentManager.instance.hasWeapon);
            animator.SetBool("InCombat", combat.InCombat);
        }else if(combat.InCombat == false){
            animator.SetBool("InCombat", combat.InCombat);
        }
        animator.SetBool("IsBlocking", stats.blocking);
    }

    protected virtual void OnAttack(){
        animator.SetTrigger("Attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }

    public void RestartTime(int value) {
        animator.SetInteger("TimeRunning", value);
    }

    void PlayingBlock(int value){
        if(value == 1){
            gameManager.bloqueado = true;
            AudioManager.instance.StopWalking();
            animator.SetBool("PlayingBlock", true);
        }
        else{
            animator.SetBool("PlayingBlock", false);
            gameManager.bloqueado = false;
        }
    }
}
