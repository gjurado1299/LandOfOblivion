using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTerceraPersona : MonoBehaviour
{
    public Transform camara;
    Animator animator;
    CharacterController controller;
    AudioManager audioManager;
    public GameManager gameManager;

    Transform focus;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    Vector3 velocity;
    bool isGrounded = true;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    
    bool moving = false;
    bool isColliding = false;

    [HideInInspector]
    public bool playing = false;
    
    string LastCollision;
    float minTimeBetweenPlays = 1f;
    float lastTimePlayed;

    void Start(){
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
        if( !gameManager.bloqueado){
            
            // Focus target
            if(focus != null){
                FaceTarget();
            }

            //jump
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (Input.GetButtonDown("Jump") && isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("JumpRunning") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Run To Stop"))
            {
                animator.SetBool("IsMoving",false);
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

                if(playing){
                    audioManager.StopWalking();
                    playing = false;
                }

            }else if (isGrounded && velocity.y < 0)
            {
                animator.SetBool("IsInAir", false);
                velocity.y = -2f;
            }

            //gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            //walk
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direccion;

            direccion = new Vector3(horizontal, 0f, vertical).normalized;

            if(direccion.magnitude >= 0.1f )
            {
                moving = true;
                float targetAngle = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if(animator.GetInteger("TimeRunning") < 20){
                    controller.Move(moveDir.normalized * speed/6 * Time.deltaTime);
                }else if(animator.GetInteger("TimeRunning") < 40){ 
                    controller.Move(moveDir.normalized * (3*speed)/6 * Time.deltaTime);
                }else{
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }

                if(playing)
                    lastTimePlayed = Time.time;

            }else{
                if(playing){
                    audioManager.StopWalking();
                    playing = false;
                }
                moving = false;
            }

        }else{
            if(playing){
                audioManager.StopWalking();
                playing = false;
            }
            velocity.y = -2f;
        }
    }

    // Walking sound
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        bool changedCollision = false;
        if(isColliding) return;
        isColliding = true;
        
        if(LastCollision != hit.gameObject.tag)
            changedCollision = true;

        if(moving && (!playing || changedCollision) && isGrounded){
            if(playing){
                audioManager.StopWalking();
                playing = false;
            }

            if(Time.time - lastTimePlayed > minTimeBetweenPlays || lastTimePlayed == 0 || changedCollision){
                audioManager.Play(hit.gameObject.tag+"Walking");
                playing = true;
            }
        }

        LastCollision = hit.gameObject.tag;
    }

    public void SetFocus(GameObject target){
        if(target != null){
            focus = target.transform;
        }else{
            focus = null;
        }
    }

    void FaceTarget(){
        
        if(focus.parent.gameObject.GetComponent<EnemyStats>().dead == false){
            Vector3 direction = (focus.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
