
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueTrigger
{
    public Dialogue firstDialogue;
    public Dialogue helper;
    public Dialogue endingDialogue;

    public void TriggerFirstDialogue(){
        GameObject.FindObjectOfType<DialogueManager>().StartDialogue(firstDialogue);
    }

    public void TriggerHelperDialogue(){
        GameObject.FindObjectOfType<DialogueManager>().StartDialogue(helper);
    }

    public void TriggerEndDialogue(){
        GameObject.FindObjectOfType<DialogueManager>().StartDialogue(endingDialogue);
    }

    public void SetPlayerPrefs(){
        int sF = PlayerPrefs.GetInt("sizeFirst", 0);
        int sH = PlayerPrefs.GetInt("sizeHelper", 0);
        int sE = PlayerPrefs.GetInt("sizeEnding", 0);

        firstDialogue.sentences = new string[sF];
        firstDialogue.names = new string[sF];

        for(int i = 0; i<sF; i++){
            firstDialogue.sentences[i] = PlayerPrefs.GetString("firstSentences"+i, "");
            firstDialogue.names[i] = PlayerPrefs.GetString("firstNames"+i, "");
        }
        firstDialogue.inicioMision = true;

        helper.sentences = new string[sH];
        helper.names = new string[sH];

        for(int i = 0; i<sH; i++){
            helper.sentences[i] = PlayerPrefs.GetString("helperSentences"+i, "");
            helper.names[i] = PlayerPrefs.GetString("helperNames"+i, "");
        }
        helper.helper = true;

        endingDialogue.sentences = new string[sE];
        endingDialogue.names = new string[sE];

        for(int i = 0; i<sE; i++){
            endingDialogue.sentences[i] = PlayerPrefs.GetString("endingSentences"+i, "");
            endingDialogue.names[i] = PlayerPrefs.GetString("endingNames"+i, "");
        }
    }

    public void SavePlayerPrefs(){
        
       PlayerPrefs.SetInt("sizeFirst", firstDialogue.sentences.Length);
        PlayerPrefs.SetInt("sizeHelper", helper.sentences.Length);
        PlayerPrefs.SetInt("sizeEnding", endingDialogue.sentences.Length);

        int i = 0;
        foreach(string s in firstDialogue.sentences){
            PlayerPrefs.SetString("firstSentences"+i++,s);
        }

        i = 0;
        foreach(string n in firstDialogue.names){
            PlayerPrefs.SetString("firstNames"+i++,n);
        }

        i = 0;
        foreach(string s in helper.sentences){
            PlayerPrefs.SetString("helperSentences"+i++,s);
        }

        i = 0;
        foreach(string n in helper.names){
            PlayerPrefs.SetString("helperNames"+i++,n);
        }

        i = 0;
        foreach(string s in endingDialogue.sentences){
            PlayerPrefs.SetString("endingSentences"+i++,s);
        }

        i = 0;
        foreach(string n in endingDialogue.names){
            PlayerPrefs.SetString("endingNames"+i++,n);
        }
    }
}
