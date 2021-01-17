
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SingleDialogue: MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(){
        GameObject.FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
