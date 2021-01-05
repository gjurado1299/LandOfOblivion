using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    
    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI mainObjective;
    public TextMeshProUGUI additionalInfo;
    public Player player;

    Color32 green;
    Color32 yellow;

    void Start(){
        green = new Color32(129, 255, 119, 255);
        yellow = new Color32(255, 220, 119, 255);
    }

    // Update is called once per frame
    void Update()
    {
        goldUI.text = player.gold.ToString();

        if(player.quest == null){
            additionalInfo.gameObject.SetActive(false);
        }else{
            if(player.quest.goal.goalType == GoalType.Kill){
                additionalInfo.text = "Elimina enemigos: " + player.quest.goal.currentAmount.ToString() + "/" + player.quest.goal.requiredAmount.ToString();
            }else{
                additionalInfo.text = "Objetos encontrados: " + player.quest.goal.currentAmount.ToString() + "/" + player.quest.goal.requiredAmount.ToString();
            }

            if(player.quest.goal.IsReached() && player.quest.isActive){
                mainObjective.text = player.quest.completedText;

                if(player.quest.goal.requiredAmount > 0){
                    additionalInfo.color = green;
                }else{
                    additionalInfo.gameObject.SetActive(false);
                }

            }else if(player.quest.goal.IsReached() == false && player.quest.isActive){

                mainObjective.text = player.quest.mainObjective;

                if(player.quest.goal.requiredAmount > 0){
                    additionalInfo.gameObject.SetActive(true);
                    additionalInfo.color = yellow;
                }else{
                    additionalInfo.gameObject.SetActive(false);
                }
            }else{
                if(player.quest.title.Length > 0)
                    mainObjective.text = player.quest.nextObjective;

                additionalInfo.gameObject.SetActive(false);
            }
        }
    }
}
