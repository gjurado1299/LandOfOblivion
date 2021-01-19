using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    GameManager gameManager;
    public GameObject GameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        this.SetPlayerPrefs();
    }

    // Update is called once per frame
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {   
        if(newItem != null){
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            attackSpeed.AddModifier(newItem.speedModifier);
            block.AddModifier(newItem.blockModifier);
        }

        if(oldItem != null){
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            attackSpeed.RemoveModifier(oldItem.speedModifier);
            block.RemoveModifier(oldItem.blockModifier);
        }
    }

    public override void Die(){
        base.Die();

        this.gameObject.GetComponent<Animator>().SetTrigger("IsDead");
        gameManager.bloqueado = true;
        LoadedCheck.instance.died = true;
        EquipmentManager.instance.reset = true;

        StartCoroutine(GameOverPanel());
    }

    IEnumerator GameOverPanel(){
        GameOver.SetActive(true);
        yield return new WaitForSeconds(3);
        AudioManager.instance.Play("GameOver");
        GameOver.GetComponent<Animator>().SetTrigger("IsOpen");
        GameOver.GetComponent<CanvasGroup>().interactable = true;
        yield return new WaitForSeconds(1);
    }

    public void SetPlayerPrefs(){
        //armor.SetModifiers("Armor");
        //damage.SetModifiers("Damage");
        //attackSpeed.SetModifiers("SpeedAttack");
        base.setCurrentHealth(PlayerPrefs.GetInt("CurrentHealth", maxHealth));
    }

    public void SavePlayerPrefs(){
        //armor.SaveModifiers("Armor");
        //damage.SaveModifiers("Damage");
        //attackSpeed.SaveModifiers("SpeedAttack");
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
    }

    
}
