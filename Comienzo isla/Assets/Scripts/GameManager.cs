using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    GameObject inventarioUI;
    public GameObject pauseUI;
    public Animator animatorPlayer;
    public GameObject InfoPanel;
    public bool bloqueado = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(inventarioUI == null){
            inventarioUI = InventarioUI.instance.gameObject;
            inventarioUI.SetActive(false);
        }

        if(Input.GetButtonDown("Inventory") && (inventarioUI.activeSelf || bloqueado == false))
        {
            inventarioUI.SetActive(!inventarioUI.activeSelf);

            if(inventarioUI.activeSelf)
            {
                bloqueado = true;
                animatorPlayer.SetBool("IsMoving", false);
                animatorPlayer.SetBool("IsInAir", false);

                inventarioUI.GetComponent<InventarioUI>().UpdateUI();
            }     
            else
            {
                bloqueado = false;
            }
        }

        if(Input.GetButtonDown("Pause"))
        {
            PauseMenu();
        }
    }

    public void PauseMenu(){
        pauseUI.SetActive(!pauseUI.activeSelf);

        if(pauseUI.activeSelf)
        {
            bloqueado = true;
            Time.timeScale=0;
            animatorPlayer.SetBool("IsMoving", false);
        }     
        else
        {                
            bloqueado = false;
            Time.timeScale=1;
        }
    }

    public void ActivateInfo(string text){
        InfoPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
        CanvasGroup cg = InfoPanel.GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        InfoPanel.GetComponent<Animator>().SetTrigger("Open");
    }

    public bool ActivePanels(){
        return (pauseUI.activeSelf || inventarioUI.activeSelf);
    }
}
