using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public bool isActive;
    public bool started;
    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;
    public string itemReward;
    public string mainObjective;
    public string completedText;
    public string nextObjective;
    public string removableObject;


    public QuestData(Quest quest){
        isActive = quest.isActive;
        started = quest.started;
        title = quest.title;
        description = quest.description;
        experienceReward = quest.experienceReward;
        goldReward = quest.goldReward;
        itemReward = quest.itemReward;
        mainObjective = quest.mainObjective;
        completedText = quest.completedText;
        nextObjective = quest.nextObjective;
        removableObject = quest.removableObject;
    }
}