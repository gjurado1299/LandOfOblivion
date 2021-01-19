using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool dead = false;
    public int maxHealth = 100;
    public int currentHealth{get; private set;}

    public Stat damage;
    public Stat armor;
    public Stat attackSpeed;
    public Stat block;

    public bool blocking = false;
    public BarraVida barraVida;
    Animator animator;
    GameManager gm;

    [HideInInspector]
    public bool buffed = false;
    
    float timeBuffed = 0f;
    float lastTimeBuffed = 0f;
    bool bloqueado = false;

    public event System.Action<int, int> OnHealthChanged;

    void Awake(){
        currentHealth = maxHealth;
        if(barraVida != null){
            barraVida.SetMaxHealth(maxHealth);
        }

        animator = gameObject.GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        CheckBar();

        // Checkear tiempo de daño
        if(Time.time - lastTimeBuffed > timeBuffed && buffed == true){
            damage.damageBuff = 0;
            buffed = false;
        }

        if(Input.GetMouseButton(1) && (gm.bloqueado == false || blocking == true)){
            blocking = true;
            gm.bloqueado = true;
            bloqueado = true;
        }
        else{
            blocking = false;
            if(bloqueado == true){
                gm.bloqueado = false;
                bloqueado = false;
            }
        }
        
    }
    
    public void TakeDamage (int damage){
        if(gameObject.name != "Havook" || animator.GetCurrentAnimatorStateInfo(0).IsName("Stand To Roll") == false){

            // Al integrar armadura hay que corregir esta expresion
            // damage -= damage * (armor.GetValue()/100);

            if(blocking){
                damage = (int)((double) damage - damage * block.GetValue()/100);
            }

            currentHealth -= damage;

            if(OnHealthChanged != null){
                OnHealthChanged(maxHealth, currentHealth);
            }
                
            if(currentHealth <= 0){
                dead = true;
                Die();
            }
        }
    }
    
    public void CheckBar(){
        if(barraVida != null){
            barraVida.SetHealth(currentHealth);
        }
    }

    public void setCurrentHealth(int health){
        currentHealth = health;
    }

    public bool GetBuffed(){
        Debug.Log(buffed);
        return buffed;
    }

    public void IncreaseHealth(int increment){
        currentHealth += increment;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void IncreaseDamage(int increment, float time){
        lastTimeBuffed = Time.time;
        timeBuffed = time;
        damage.damageBuff = increment;
        buffed = true;
    }

    public bool isEnraged(float percentage){
        return ((percentage*maxHealth) >= currentHealth);
    }

    public bool isFullHealth(){
        return (currentHealth >= maxHealth);
    }

    public void LoadPlayer(int health){
        currentHealth = health;
    }

    public virtual void Die(){
        // Muere

    }
}
