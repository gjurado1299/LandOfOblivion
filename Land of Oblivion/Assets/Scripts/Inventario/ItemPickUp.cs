﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public int bodyPart;
    [HideInInspector] public bool picked = false;
    
    public void PickUp()
    {
        bool wasPickedUp = Inventario.instance.Add(item);

        if(wasPickedUp){
            // Enparentamos la parte del cuerpo
            if(item.GetType() == typeof(Equipment)){
                gameObject.transform.SetParent(EquipmentManager.instance.bodyParts[bodyPart].transform);
            }else{
                gameObject.transform.SetParent(GameObject.Find("Havook").transform.GetChild(1).GetChild(0));
            }

            // Desactivamos interactableSphere y componente Rigidbody del equipamiento
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Posicion relativa a la parte del cuerpo (util para equipamiento solo)
            gameObject.transform.localPosition = item.PickPosition;
            gameObject.transform.localEulerAngles = item.PickRotation;

            // Ocultamos el objeto, ya que no lo tiene equipado
            gameObject.SetActive(false);

            AudioManager.instance.Play("PickUp");

            if(gameObject.name == "Llave" && picked == false){
                Player player = GameObject.Find("Havook").GetComponent<Player>();
                player.quest.completedText = "Habla con Harald";
                player.quest.AdjustQuantity();
                picked = true;
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevelByIndex(2);

            }else if(gameObject.name == "Mithril" && picked == false){

                Player player = GameObject.Find("Havook").GetComponent<Player>();
                player.quest.completedText = "Vuelve con Harald";

                // Asegurar que el contador esta actualizado
                player.quest.goal.currentAmount = 1;
                picked = true;
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevelByIndex(4);
            }
        }
    } 
}
