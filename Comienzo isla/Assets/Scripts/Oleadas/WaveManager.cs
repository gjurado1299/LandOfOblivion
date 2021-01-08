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
    public TextMeshProUGUI mainObjective;
    public TextMeshProUGUI info;

    private void Start()
    {
        timer = GetComponent<Timer>();

        // Actualizar mision info

        foreach(Transform child in transform)
        {
            spawners.Add(child);
        }

        timer.StartTimer(roundDelay);
    }

    public void StartWave()
    {
        // Si havook ha muerto, parar
        if(currentWave < numberOfWaves){
            currentWave += 1;
            
            if(currentWave == 1){
                mainObjective.text = "Libera la tierra de los escorpiones";
            }

            enemiesInWave = enemyIncreaseFactor * currentWave;
            enemiestoSpawn = enemiesInWave;

            Debug.Log("ENEMIGOS TOTALES "+ enemiestoSpawn.ToString());
            // Actualizar mision info

            StartCoroutine(Spawn());
        }else{
            Debug.Log("MISION COMPLETADA");
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

        // Actualizar mision info
        // ui.UpdateUI(currentWave, enemiesInWave);

        if (enemiesInWave == 0)
        {
            EndWave();
        }
    }

    public Vector3 GetRandomSpawner()
    {
        return spawners[Random.Range(0, spawners.Count)].position;
    }
}