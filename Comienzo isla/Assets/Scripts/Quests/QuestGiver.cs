using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Player player;

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI experienceText;

    public GameObject rewardWindow;
    public TextMeshProUGUI goldReward;
    public TextMeshProUGUI expReward;
    public TextMeshProUGUI titleTextReward;
    public TextMeshProUGUI itemName;
    public Image itemImage;

    public GameManager gameManager;


    public void OpenQuestWindow(){
        // Mostrar panel con info actualizada
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = quest.goldReward.ToString();
        experienceText.text = quest.experienceReward.ToString();

    }

    public void OpenRewardWindow(){
        // Mostrar panel con info actualizada
        rewardWindow.SetActive(true);
        titleTextReward.text = quest.title;
        goldReward.text = quest.goldReward.ToString();
        expReward.text = quest.experienceReward.ToString();

        GameObject itemR = GameObject.Find(quest.itemReward);
        if(itemR != null){
            ItemPickUp i = itemR.GetComponent<ItemPickUp>();
            if (i != null){
                itemName.text = i.item.name;
                itemImage.sprite = i.item.icon;
            }
        }
        
    }

    public void InvokeDialogue(){
        //COMPROBAR QUE PASA SI GUARDAMOS Y CARGAMOS PARTIDA, PORQUE LA REFERENCIA DEBERIA SER QUIZÁ AL JUGADOR (Y A SU MISION)
        Debug.Log(player.quest.isActive);
        if(player.quest.isActive && player.quest.goal.IsReached()){
            quest.dialogueTrigger.TriggerEndDialogue();
        }else if(player.quest.isActive && !player.quest.goal.IsReached()){
            quest.dialogueTrigger.TriggerHelperDialogue();
        }else{
            quest.dialogueTrigger.TriggerFirstDialogue();
        }
    }

    public void AcceptQuest(){
        // Ocultar panel y poner la mision activa
        questWindow.SetActive(false);
        quest.isActive = true;

        // Desbloqueamos movimiento
        if(gameManager){
            gameManager.bloqueado = false;
        }
        
        // Asignar misión al jugador
        player.quest = quest;
    }
}
