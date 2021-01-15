using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public bool boss = false;

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;

        if(boss == false)
            fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health){
        
        slider.value = health;
        if(boss == false)
            fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
