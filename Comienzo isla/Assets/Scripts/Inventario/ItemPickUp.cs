using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public int bodyPart;
    bool picked = false;
    
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
                GameObject.Find("Havook").GetComponent<Player>().quest.completedText = "Habla con Harald";
                picked = true;
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevelByIndex(2);
            }
        }
    } 
}
