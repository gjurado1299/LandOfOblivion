using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<string> names;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animatorDialogue;
    public Animator animatorPlayer;

    public UnityEvent panelMision;
    public UnityEvent panelRecompensa;

    public UnityEvent dialogoUnico;

    public GameManager gameManager;

    bool inicioMision = false;
    bool helper = false;
    bool unico = false;
    public bool escenaFinal = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        animatorDialogue.SetBool("IsOpen", true);
        animatorPlayer.SetBool("IsMoving", false);
        gameManager.bloqueado = true;
        sentences.Clear();
        names.Clear();
        inicioMision = dialogue.inicioMision;
        helper = dialogue.helper;
        unico = dialogue.unico;

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        foreach(string name in dialogue.names){
            names.Enqueue(name);
        }
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue(){
        if(sentences.Count == 0 || names.Count == 0){
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        nameText.text = name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));   
    }

    IEnumerator TypeSentence (string sentence){
        
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            AudioManager.instance.Play("Typing");
            dialogueText.text += letter;
            yield return null;
            AudioManager.instance.Stop("Typing");
        }
        
    }

    void EndDialogue(){
        animatorDialogue.SetBool("IsOpen", false);
        if(!helper){
            if(inicioMision == true){
                panelMision.Invoke();
            }else if(unico == true){
                // hacer algo
                dialogoUnico.Invoke();
                if(escenaFinal == false){
                    gameManager.bloqueado = false;
                }

            }else{
                panelRecompensa.Invoke();
            }
        }else{
            gameManager.bloqueado = false;
        }
    }
}
