using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerScript : MonoBehaviour {

    public float timeBetweenEnemies = 1;
    public float timeBetweenWaves = 10;
    public int enemiesInWave = 5;
    public int numWaves = 10;
    public bool waveCurrentlyRunning;

    private IEnumerator waveCoroutine;
    private int currentWave;
    private bool wavesStarted = false;
    private bool paused;
    // Use this for initialization
    void Start() {
        currentWave = 1;
        waveCoroutine = BeginSpawning();
        paused = false;
    }

    // Update is called once per frame
    void Update() {
        if(!wavesStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wavesStarted = true;
                StartSpawningButton();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(paused)
            {
                Debug.Log("Unpaused");
                StartCoroutine(waveCoroutine);
                paused = false;
            }
            else
            {
                Debug.Log("Paused");
                StopCoroutine(waveCoroutine);
                paused = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartNextWaveButton();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
        }
    }
    public void StartSpawningButton()
    {
        StartCoroutine(waveCoroutine);
    }
    public void StartNextWaveButton()
    {
        if (currentWave <= numWaves)
        {
            StartCoroutine(SpawnNextWave(currentWave));
            ++currentWave;
        }
    }
    public IEnumerator BeginSpawning()
    {
        StartCoroutine(SpawnNextWave(currentWave));
        ++currentWave;
        for (; currentWave < numWaves; ++currentWave)
        {
            for(int timePassed = 0; timePassed < timeBetweenWaves; ++timePassed)
            {
                if(waveCurrentlyRunning)
                {
                    yield return new WaitWhile(() => waveCurrentlyRunning);
                    timePassed = 0;
                }
                //Debug.Log("Time Remaining: " + (timeBetweenWaves - timePassed));
                yield return new WaitForSeconds(1.0f);
            }
            if(currentWave < numWaves)
            {
                StartCoroutine(SpawnNextWave(currentWave));
            }
        }
    }
    public IEnumerator SpawnNextWave(int currentWave)
    {
        waveCurrentlyRunning = true;
        for (int i = 0; i < enemiesInWave; ++i)
        {
            SpawnEnemy(currentWave, i);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        if (currentWave == this.currentWave - 1)
        {
            waveCurrentlyRunning = false;
        }
    }
    public void SpawnEnemy(int currentWave, int enemyNum)
    {
        Debug.Log("EnemySpawning: Wave: " + currentWave + " EnemyNum: " + enemyNum);
    }
}
