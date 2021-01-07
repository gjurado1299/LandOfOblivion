using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public GameObject enemy;


    private void Start()
    {
        timer = GetComponent<Timer>();

        // Actualizar mision info

        foreach(Transform child in transform)
        {
            spawners.Add(child);
        }
    }

    public void StartWave()
    {
        // Si havook ha muerto, parar
        Debug.Log("EMPEZANDO");
        currentWave += 1;
        Debug.Log("RONDA "+ currentWave.ToString());
        enemiesInWave = enemyIncreaseFactor * currentWave;
        enemiestoSpawn = enemiesInWave;

        Debug.Log("ENEMIGOS "+ enemiesInWave.ToString());
        Debug.Log("ENEMIGOS TOTALES "+ enemiestoSpawn.ToString());
        // Actualizar mision info

        StartCoroutine(Spawn());
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