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
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI protectionText;

    public bool bloqueado = false;
    public bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Death") == true)
            bloqueado = true;
            
        if(inventarioUI == null){
            inventarioUI = InventarioUI.instance.gameObject;
            inventarioUI.SetActive(false);
        }

        if(InfoPanel == null){
            InfoPanel = inventarioUI.transform.Find("InfoPanel").GetChild(0).gameObject;
            itemName = InfoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            infoText = InfoPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            damageText = InfoPanel.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
            protectionText = InfoPanel.transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();

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
        inventarioUI.SetActive(false);
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

    public void EmptyInfo(){
        CanvasGroup cg = InfoPanel.GetComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    public void ActivateInfo(Slot slot){
        string damage;
        string protection;
        if(slot.item != null){
            itemName.text = slot.item.name;
            infoText.text = slot.item.infoPick;
            if(slot.item.GetType() == typeof(Equipment)){
                damage = ((Equipment)slot.item).damageModifier.ToString();
                protection = ((Equipment)slot.item).blockModifier.ToString();
            }else{
                damage = "-";
                protection = "-";
            }

            damageText.text = damage;
            protectionText.text = protection;
            CanvasGroup cg = InfoPanel.GetComponent<CanvasGroup>();
            cg.alpha = 1;
        }
    }

    public bool ActivePanels(){
        return (pauseUI.activeSelf || inventarioUI.activeSelf);
    }
}
