using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    // Panel que muestra el mensaje de info ("Pulsa E para...")
    GameObject UIPanel;

    public Camera camaraDialogo;
    public Camera camaraPrincipal;
    public Vector3 posicionDialogo;
    public Quaternion rotacionDialogo;
    GameObject jugador;
    public Animator animatorDialogo;
    public string type;
    bool recolocado = false;
    public bool singleDialogue = false;

    Player jugadorScript;
    animationStateController animadorJugador;
    MovimientoTerceraPersona movimientoJugador;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Havook");
        if(jugador != null){
            jugadorScript = jugador.GetComponent<Player>();
            animadorJugador = jugador.GetComponent<animationStateController>();
            movimientoJugador = jugador.GetComponent<MovimientoTerceraPersona>();
        }

        if(type == "dialogo")
            UIPanel = GameObject.Find("TalkUI");
        else
            UIPanel = GameObject.Find("PickUpUI");

    }

    // Update is called once per frame
    void Update()
    {
        if(UIPanel == null){
            if(type == "dialogo")
                UIPanel = GameObject.Find("TalkUI");
            else
                UIPanel = GameObject.Find("PickUpUI");
        }

        if(jugador == null){
            jugador = GameObject.Find("Havook");
            if(jugador != null){
                jugadorScript = jugador.GetComponent<Player>();
                animadorJugador = jugador.GetComponent<animationStateController>();
            }
        }

        if(recolocado){
            // Al recolocar al usuario, para poder ocular el panel de "pulsa E para hablar"
            // hay que hacerlo fuera del if (sino, al recolocarlo, se llama a OnTriggerEnter,
            // y se queda el panel abierto durante la conversacion
            if(type != "enemy")
                UIPanel.GetComponent<CanvasGroup>().alpha = 0;
        }

        //ver si estamos en rango y pulsando la tecla
        if(isInRange){

            if (type == "dialogo")
            {
                animadorJugador.conversation = true;
                if (Input.GetKeyDown(interactKey) && !animatorDialogo.GetBool("IsOpen"))
                {

                    // Posicionamos al jugador enfrente del NPC
                    if(singleDialogue == false){
                        jugador.SetActive(false);
                        jugador.transform.position = posicionDialogo;
                        jugador.transform.rotation = rotacionDialogo;
                        jugador.SetActive(true);
                    }
                   
                    interactAction.Invoke(); 
                    recolocado = true;
                    
                    if(singleDialogue == false){
                        // Activamos cámara de diálogo
                        camaraDialogo.gameObject.SetActive(true);
                        camaraPrincipal.gameObject.SetActive(false);
                    }
                }
            }else if(type == "enemy"){
                if(Input.GetMouseButtonDown(0)){
                    interactAction.Invoke(); 
                }
            }
            else
            {
                if (Input.GetKeyDown(interactKey))
                {
                    UIPanel.GetComponent<CanvasGroup>().alpha = 0;

                    if(gameObject.transform.parent.gameObject.name == "castle_door_1"){
                        jugadorScript.quest.mainObjective = "Explora el castillo y encuentra el material";
                    }

                    interactAction.Invoke();
                    if(type == "missionObj"){
                        jugadorScript.getMissionObject();
                    }
                }
                
            }
        }else{
            recolocado = false;
            animadorJugador.conversation = false;
        }
    }

    private void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("Player")){

            if(type != "missionObj" || jugadorScript.quest.isActive ){
                isInRange = true;
                if(type != "enemy"){
                    if(UIPanel == null){
                        if(type == "dialogo")
                            UIPanel = GameObject.Find("TalkUI");
                        else{
                            UIPanel = GameObject.Find("PickUpUI");
                        }
                            
                    }

                    if(type == "door"){
                        UIPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pulsa E para abrir";
                    }

                    UIPanel.GetComponent<CanvasGroup>().alpha = 1;
                }else{
                    movimientoJugador.SetFocus(this.gameObject);
                }

                if(type != "dialogo"){
                    animadorJugador.nearObject = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision){
        if(collision.gameObject.CompareTag("Player")){
            isInRange = false;

            if(type != "enemy"){
                UIPanel.GetComponent<CanvasGroup>().alpha = 0;
            }else{
                movimientoJugador.SetFocus(null);
            }

            if(type == "dialogo")
            {
                camaraDialogo.gameObject.SetActive(false);
                camaraPrincipal.gameObject.SetActive(true);
            }

        }
    }
}
