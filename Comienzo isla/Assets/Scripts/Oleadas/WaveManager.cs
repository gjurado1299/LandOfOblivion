using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(Timer))]
public class WaveManager : MonoBehaviour
{
    private int currentWave = 0;
    private int enemiesInWave;
    private int enemiestoSpawn;

    private Timer timer;

    private List<Transform> spawners = new List<Transform>();

    public float spawnDelay;
    public float roundDelay;
    public int enemyIncreaseFactor;

    public int numberOfWaves;

    public GameObject enemy;
    public GameObject keyInteractable;
    public GameObject efecto;
    Player havook;

    private void Start()
    {
        timer = GetComponent<Timer>();
        havook = GameObject.Find("Havook").GetComponent<Player>();

        // Actualizar mision info
        havook.quest.mainObjective = "Encuentra la llave de la Fortaleza Oscura";

        foreach(Transform child in transform)
        {
            spawners.Add(child);
        }
    }

    public void StartWave()
    {
        // Si havook ha muerto, parar
        if(currentWave < numberOfWaves){
            currentWave += 1;

            enemiesInWave = enemyIncreaseFactor * currentWave;
            enemiestoSpawn = enemiesInWave;

            StartCoroutine(Spawn());
        }else{
            efecto.SetActive(false);
            keyInteractable.SetActive(true);
            AudioManager.instance.Stop("InicioOleadas");
            AudioManager.instance.Play("TierrasPerdidas");
        }
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < enemiestoSpawn; i++)
        {
            // Si havook ha muerto, parar

            Instantiate(enemy, GetRandomSpawner(), Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void EndWave()
    {
        // Actualizar mision info

        timer.StartTimer(roundDelay);
    }

    public void EnemyDied()
    {
        enemiesInWave -= 1;
        havook.quest.goal.EnemyKilled();
        if (enemiesInWave == 0)
        {
            EndWave();
        }
    }

    public Vector3 GetRandomSpawner()
    {
        return spawners[Random.Range(0, spawners.Count)].position;
    }

    public void StartTimer(){
        timer.StartTimer(roundDelay);
        havook.quest.started = true;
        havook.quest.mainObjective = "Libera la tierra de los escorpiones";

        //Cambiar audio
        AudioManager.instance.Stop("TierrasPerdidas");
        AudioManager.instance.Play("InicioOleadas");
    }

    public void AdjustSceneOnLoad(Quest quest){
        int enemiesKilled = quest.goal.currentAmount;
        int counter = 0;
        int n = 0;
        bool breaked = false;

        int i=0, j=0;

        AudioManager.instance.Stop("TierrasPerdidas");
        AudioManager.instance.Play("InicioOleadas");
        if(quest.goal.currentAmount > 0){
            for(i=0; i<numberOfWaves; i++){
                currentWave+=1;
                enemiestoSpawn = (i+1)*2;
                n = enemiestoSpawn;

                for(j=0; j<n; j++){
                    counter += 1;
                    enemiestoSpawn -= 1;
                    if(counter == enemiesKilled){
                        breaked = true;
                        break;
                    }
                }

                if(breaked)
                    break;
            }

            enemiesInWave = enemiestoSpawn;
            StartCoroutine(Spawn());
        }else{
            StartWave();
        }
    }
}