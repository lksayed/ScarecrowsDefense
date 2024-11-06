using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private List<int> fastEnemiesEachWave;
    [SerializeField] private List<int> strongEnemiesEachWave;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private bool bossSpawned = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    //private void Start()
    //{
    //    StartCoroutine(StartWave());
    //}

    public void StartWaveButton()
    {
        StartCoroutine(StartWave());
    }
    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            if (currentWave == 10 && bossSpawned == false)
            {
                bossSpawned = true;
                SpawnBoss();
            }

            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawn Enemy");


        // Spawn a Fast Enemy
        if (fastEnemiesEachWave[currentWave - 1] > 0 && enemiesLeftToSpawn < 4)
        {
            GameObject prefabToSpawn = enemyPrefabs[1];
            Instantiate(prefabToSpawn, LevelManager.main.startingPoint.position, Quaternion.identity);
            fastEnemiesEachWave[currentWave - 1]--;
        }
        // Spawn a Strong Enemy
        else if (strongEnemiesEachWave[currentWave - 1] > 0)
        {
            GameObject prefabToSpawn = enemyPrefabs[2];
            Instantiate(prefabToSpawn, LevelManager.main.startingPoint.position, Quaternion.identity);
            strongEnemiesEachWave[currentWave - 1]--;
        }
        // Spawn a Normal Enemy
        else
        {
            GameObject prefabToSpawn = enemyPrefabs[0];
            Instantiate(prefabToSpawn, LevelManager.main.startingPoint.position, Quaternion.identity);
        }
    }

    private void SpawnBoss()
    {
        GameObject boss = enemyPrefabs[3];
        Instantiate(boss, LevelManager.main.startingPoint.position, Quaternion.identity);
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
