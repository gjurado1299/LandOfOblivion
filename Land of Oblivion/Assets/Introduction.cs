using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{

    public SingleDialogue dialogoInicio;

    public void FinishIntro(){
        AudioManager.instance.Stop("Thunder");
        AudioManager.instance.Play("Tutorial");
        dialogoInicio.TriggerDialogue();
    }

}
