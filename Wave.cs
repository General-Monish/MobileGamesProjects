using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float waveInterval = 5f; // Time between waves
    [SerializeField] int initialEnemyCount = 0;

    int currentWave = 0;
    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (currentWave < 5)
        {
            currentWave++;
            SpawnEnemies(currentWave);
            yield return new WaitForSeconds(waveInterval);
        }

        // All waves completed, player wins
        GameManager.instance.GameWonMethod();
        // Add win condition logic here
    }

    void SpawnEnemies(int waveNumber)
    {
        int enemyCount = initialEnemyCount;
        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
