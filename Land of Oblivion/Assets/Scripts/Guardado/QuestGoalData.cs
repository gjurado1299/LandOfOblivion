using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoalData
{
    public bool isKill;
    
    public int currentAmount;
    public int requiredAmount;

    public QuestGoalData(QuestGoal questGoal){
        currentAmount = questGoal.currentAmount;
        requiredAmount = questGoal.requiredAmount;

        if(questGoal.goalType == GoalType.Kill){
            isKill = true;
        }else{
            isKill = false;
        }
        
    }
}