using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTriggerData
{
    public string[] dialogueStartSentences;
    public string[] dialogueStartNames;

    public string[] dialogueHelperSentences;
    public string[] dialogueHelperNames;

    public string[] dialogueEndSentences;
    public string[] dialogueEndNames;

    public DialogueTriggerData(DialogueTrigger dt){
        dialogueStartSentences = dt.firstDialogue.sentences;
        dialogueStartNames = dt.firstDialogue.names;
        dialogueHelperSentences = dt.helper.sentences;
        dialogueHelperNames = dt.helper.names;
        dialogueEndSentences = dt.endingDialogue.sentences;
        dialogueEndNames = dt.endingDialogue.names;
    }
}