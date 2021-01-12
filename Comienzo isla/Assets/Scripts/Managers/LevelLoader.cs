﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{

    #region Singleton
        public static LevelLoader instance;

        void Awake()
        {
            instance = this;
        }

    #endregion

    public Animator transition;
    Player player;
    PlayerStats stats;

    public float transitionTime = 1f;
    bool loaded = false;
    // Update is called once per frame
    void Update()
    {

    }

    void Start(){
        
    }

    public void Play(){
        PlayerPrefs.DeleteAll();
        if(EquipmentManager.instance != null)
            Destroy(EquipmentManager.instance.gameObject);

        if(Inventario.instance != null)
            Destroy(Inventario.instance.gameObject);
        
        if(InventarioUI.instance != null)  
            Destroy(InventarioUI.instance.gameObject);

       
        LoadNextLevel();
    }

    public void Load(){
        LoadLevelByIndex(SaveSystem.LoadScene());
        LoadedCheck.instance.loaded = true;
    }

    public void Save(){
        SaveSystem.SaveScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel(){
        // Guardar info del jugador
        GameObject havook = GameObject.Find("Havook");
        if(havook){
            player = havook.GetComponent<Player>();
            stats = havook.GetComponent<PlayerStats>();
            player.SavePlayerPrefs();

            if(EquipmentManager.instance != null)
                EquipmentManager.instance.SaveGameObjects();

            stats.SavePlayerPrefs();
        }

        int index = SceneManager.GetActiveScene().buildIndex + 1;

        if(index == 1)
            AudioManager.instance.Stop("MainTheme");
        else if(index == 2)
            AudioManager.instance.Stop("Tutorial");

        StartCoroutine(LoadLevel(index));        
    }

    public void GoMainMenu(){
        Time.timeScale=1;
        AudioManager.instance.Stop("GameOver");
        AudioManager.instance.Stop("Tutorial");
        AudioManager.instance.Stop("Bibury");
        AudioManager.instance.Stop("TierrasPerdidas");
        AudioManager.instance.Stop("InicioOleadas");
        AudioManager.instance.Play("MainTheme");
        StartCoroutine(LoadLevel(0));
    }

    public void LoadLevelByIndex(int index){
        if(index == -1) return;

        GameObject havook = GameObject.Find("Havook");
        if(havook){
            player = havook.GetComponent<Player>();
            stats = havook.GetComponent<PlayerStats>();
            player.SavePlayerPrefs();

            if(EquipmentManager.instance != null)
                EquipmentManager.instance.SaveGameObjects();

            stats.SavePlayerPrefs();
        }

        StartCoroutine(LoadLevel(index));
    }

    public void KillPlayer(){

        // Tener en cuenta checkpoints
        

        AudioManager.instance.Stop("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex){
        // Lanzar la animacion
        if(levelIndex != 0){
            transition.SetTrigger("Start");

            // Esperar a la transicion
            yield return new WaitForSeconds(transitionTime);
        }
        
        // Cargar la escena
        SceneManager.LoadScene(levelIndex);

        yield return new WaitForSeconds(3);
    }
}