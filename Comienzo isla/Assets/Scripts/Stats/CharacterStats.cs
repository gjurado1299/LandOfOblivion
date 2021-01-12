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

    public event System.Action<int, int> OnHealthChanged;

    void Awake(){
        currentHealth = maxHealth;
        if(barraVida != null){
            barraVida.SetMaxHealth(maxHealth);
        }
    }

    void Update(){
        if(barraVida != null)
            barraVida.SetHealth(currentHealth);

        if(Input.GetMouseButton(1))
            blocking = true;
        else
            blocking = false;
        
    }
    
    public void TakeDamage (int damage){

        // Al integrar armadura hay que corregir esta expresion
        damage -= damage * (armor.GetValue()/100);

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

    public void setCurrentHealth(int health){
        currentHealth = health;
    }

    public void IncreaseHealth(int increment){
        currentHealth += increment;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
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
