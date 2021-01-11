using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool started = false;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;
    public string itemReward;

    public string mainObjective;
    public string completedText;
    public string nextObjective;

    public DialogueTrigger dialogueTrigger;

    public QuestGoal goal;
    public string removableObject;

    public void Complete(){
        isActive = false;
    }

    public void SetPlayerPrefs(){
        title = PlayerPrefs.GetString("QuestTitle", "");
        description = PlayerPrefs.GetString("QuestDescription", "");
        itemReward = PlayerPrefs.GetString("QuestItemReward", "");
        mainObjective = PlayerPrefs.GetString("QuestMainObjective", "");
        completedText = PlayerPrefs.GetString("QuestCompletedText", "");
        nextObjective = PlayerPrefs.GetString("QuestNextObjective", "");
        removableObject = PlayerPrefs.GetString("QuestRemovableObject", "");
        experienceReward = PlayerPrefs.GetInt("QuestExperience", experienceReward);
        goldReward = PlayerPrefs.GetInt("QuestGold", goldReward);

        if(PlayerPrefs.GetInt("QuestIsActive", 0) == 1)
            isActive = true;
        else
            isActive = false;

        

        goal.SetPlayerPrefs();
        dialogueTrigger.SetPlayerPrefs();

    }

    public void SavePlayerPrefs(){
        PlayerPrefs.SetString("QuestTitle", title);
        PlayerPrefs.SetString("QuestDescription", description);
        PlayerPrefs.SetString("QuestRemovableObject", removableObject);
        PlayerPrefs.SetString("QuestMainObjective", mainObjective);
        PlayerPrefs.SetString("QuestCompletedText", completedText);
        PlayerPrefs.SetString("QuestNextObjective", nextObjective);
        PlayerPrefs.SetInt("QuestExperience", experienceReward);
        PlayerPrefs.SetInt("QuestGold", goldReward);
        PlayerPrefs.SetString("QuestItemReward", itemReward);

        if(isActive)
            PlayerPrefs.SetInt("QuestIsActive", 1);
        else
            PlayerPrefs.SetInt("QuestIsActive", 0);

        

        goal.SavePlayerPrefs();
        dialogueTrigger.SavePlayerPrefs();
        
    }
}
