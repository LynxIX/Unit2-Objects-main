using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public Enemy enemyPrefab; // Enemy prefab ref
    public float spawnInterval; // Spawn interval for enemy type
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform[] spawnPointsArray; 
    [SerializeField] private List<Enemy> listOfAllEnemiesAlive = new List<Enemy>(); 

    [Header("Enemy Configuration")]
    [SerializeField] private EnemyType[] enemyTypes; 

    [Header("Spawn Configuration")]
    [SerializeField] private float difficultyIncreaseRate = 10f; 
    [SerializeField] private float minimumSpawnInterval = 0.5f; 

    private float elapsedTime = 0f; 
    private Dictionary<EnemyType, float> spawnTimers = new Dictionary<EnemyType, float>();

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        foreach (var enemyType in enemyTypes)
        {
            spawnTimers[enemyType] = 0f; // Initialize timers for each enemy type
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Increase difficulty over time
        if (elapsedTime > difficultyIncreaseRate)
        {
            elapsedTime = 0f;
            foreach (var enemyType in enemyTypes)
            {
                enemyType.spawnInterval = Mathf.Max(enemyType.spawnInterval - 0.1f, minimumSpawnInterval);
            }
        }

        // Check spawn timers for each enemy type
        foreach (var enemyType in enemyTypes)
        {
            spawnTimers[enemyType] += Time.deltaTime;
            if (spawnTimers[enemyType] >= enemyType.spawnInterval)
            {
                SpawnEnemy(enemyType);
                spawnTimers[enemyType] = 0f; // Reset timer for this enemy type
            }
        }
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        int randomIndex = Random.Range(0, spawnPointsArray.Length);
        Transform spawnPoint = spawnPointsArray[randomIndex];

        // Instantiate the enemy
        Enemy enemyClone = Instantiate(enemyType.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        listOfAllEnemiesAlive.Add(enemyClone);
    }

    public void RemoveEnemyFromList(Enemy enemyToBeRemoved)
    {
        listOfAllEnemiesAlive.Remove(enemyToBeRemoved);
    }
}
