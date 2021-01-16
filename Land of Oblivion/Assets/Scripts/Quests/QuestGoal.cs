using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
  public GoalType goalType;

  public int requiredAmount;
  public int currentAmount;

  public bool IsReached(){
    return (currentAmount >= requiredAmount);
  }

  public void ItemCollected(){
      if(goalType == GoalType.Gathering){
          currentAmount++;
      }
  }

  public void EnemyKilled(){
      if(goalType == GoalType.Kill){
          currentAmount++;
      }
  }

  public void AdjustQuantity(){
      // A veces, por algun motivo, no cuenta bien algunos enemigos. Ajuste seguro.
      currentAmount = requiredAmount;
  }

  public void SetPlayerPrefs(){
        requiredAmount = PlayerPrefs.GetInt("QuestGoalRequired", 0);
        currentAmount = PlayerPrefs.GetInt("QuestGoalCurrent", 0);

        if(PlayerPrefs.GetInt("QuestGoalType", -1) == 0){
            goalType = GoalType.Kill;
        }else if(PlayerPrefs.GetInt("QuestGoalType", -1) == 1){
            goalType = GoalType.Gathering;
        }
    }

    public void SavePlayerPrefs(){
        PlayerPrefs.SetInt("QuestGoalRequired", requiredAmount);
        PlayerPrefs.SetInt("QuestGoalCurrent", currentAmount);

        if(goalType == GoalType.Kill){
            PlayerPrefs.SetInt("QuestGoalType", 0);
        }else{
            PlayerPrefs.SetInt("QuestGoalType", 1);
        }   
    }

    public void LoadQuestGoal(QuestGoalData data){
        currentAmount = data.currentAmount;
        requiredAmount = data.requiredAmount;

        if(data.isKill == true){
            goalType = GoalType.Kill;
        }else{
            goalType = GoalType.Gathering;

        }
    }

}

public enum GoalType{
    Kill,
    Gathering
}
