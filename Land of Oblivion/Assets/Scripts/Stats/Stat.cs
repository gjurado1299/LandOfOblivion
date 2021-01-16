using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue = 0;

    private List<int> modifiers = new List<int>();

    public int GetValue(){
        int finalValue = baseValue;

        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier){
        if( modifier != 0){
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier){
        if( modifier != 0){
            modifiers.Remove(modifier);
        }
    }

    public void SaveModifiers(string name){
        for(int i=0; i<modifiers.Count; i++){
            PlayerPrefs.SetInt(name+i.ToString(), modifiers[i]);
        }
    }

    public void SetModifiers(string name){
        int i=0;
        int mod = PlayerPrefs.GetInt(name+i.ToString(), -1);
        while( mod != -1){
            modifiers.Add(mod);
            i++;
            mod = PlayerPrefs.GetInt(name+i.ToString(), -1);
        }
    }
}
