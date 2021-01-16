using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public bool inicioMision;
    public bool helper;
    public bool unico;


    [TextArea(3,10)]
    public string[] sentences;
    public string[] names;

    //Alternativa cuando haya varias misiones simultaneas: Que cada dialogo vaya ligado a las misiones que inicia/finaliza, y sus paneles
}
