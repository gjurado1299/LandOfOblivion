using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Image weapon;
    private int index = 3;

    // Update is called once per frame
    void Update()
    {
        var fist = Resources.Load<Sprite>("Sprites/fist");

        if(EquipmentManager.instance != null){
            Sprite weaponImg = EquipmentManager.instance.returnEquipped(index);
            if(weaponImg != null)
                weapon.sprite = weaponImg;
            else{
                weapon.sprite = fist;
            }
        }
    }
}
