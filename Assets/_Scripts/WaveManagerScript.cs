using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerScript : MonoBehaviour {

    public float timeBetweenEnemies = 1;
    public float timeBetweenWaves = 10;
    public int enemiesInWave = 5;
    public int numWaves = 10;
    public bool waveCurrentlyRunning;

    private List<IEnumerator> waveCoroutines;
    private IEnumerator allWaveCoroutine;
    private int currentWave;
    private bool wavesStarted = false;
    private bool paused;
    private bool wasPaused;
    // Use this for initialization
    void Start() {
        currentWave = 0;
        allWaveCoroutine = BeginSpawning();
        waveCoroutines = new List<IEnumerator>();
        for(int i = 1; i <= numWaves; ++i)
        {
            waveCoroutines.Add(SpawnNextWave(i));
        }
        paused = false;
        wasPaused = false;
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
            paused = !paused;
        }
        if (!paused && wasPaused)
        {
            Debug.Log("Unpaused");
            StartCoroutine(allWaveCoroutine);
            for(int runningCoroutines = 0; runningCoroutines < currentWave; ++runningCoroutines)
            {
                StartCoroutine(waveCoroutines[runningCoroutines]);
            }
            wasPaused = false;
        }
        else if (paused && !wasPaused)
        {
            Debug.Log("Paused");
            StopAllCoroutines();
            wasPaused = true;
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
        StartCoroutine(allWaveCoroutine);
    }
    public void StartNextWaveButton()
    {
        if (wavesStarted && currentWave <= numWaves)
        {
            StartCoroutine(waveCoroutines[currentWave]);
            ++currentWave;
        }
    }
    public IEnumerator BeginSpawning()
    {
        StartCoroutine(waveCoroutines[currentWave]);
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
                if (currentWave > numWaves)
                {
                    break;
                }
                Debug.Log("Time Remaining: " + (timeBetweenWaves - timePassed));
                yield return new WaitForSeconds(1.0f);
            }
            if(currentWave <= numWaves)
            {
                StartCoroutine(waveCoroutines[currentWave]);
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
        if (currentWave == this.currentWave)
        {
            waveCurrentlyRunning = false;
        }
    }
    public void SpawnEnemy(int currentWave, int enemyNum)
    {
        Debug.Log("EnemySpawning: Wave: " + currentWave + " EnemyNum: " + enemyNum);
    }
}
