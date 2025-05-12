using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] patternPrefabs;  // Prefabs for patterns to spawn
    public GameObject coinPrefab;        // Prefab for the coin to spawn
    public Transform[] spawnPoints;      // Array of spawn points
    public float spawnInterval = 2f;     // Interval between spawns

    public Player1CarController player1Controller;  // Reference to Player 1's car controller
    public Player2CarController player2Controller;  // Reference to Player 2's car controller

    private float timer = 0f;
    private int currentPatternIndex = 0;

    private List<GameObject> spawnedPatterns = new List<GameObject>();
    private List<GameObject> spawnedCoins = new List<GameObject>();  // To track spawned coins

    void Update()
    {
        float speed = 0f;
        if (player1Controller != null)
            speed = player1Controller.moveSpeed;
        else if (player2Controller != null)
            speed = player2Controller.moveSpeed;
        else
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnPatternAndCoin();

        }

        MoveAndCleanPatterns(speed);
        MoveAndCleanCoins(speed);  // Move and clean coins
    }

    void SpawnPatternAndCoin()
    {
        // Choose two different spawn indexes to prevent overlap
        int patternSpawnIndex = Random.Range(0, spawnPoints.Length);
        int coinSpawnIndex;

        // Ensure coin doesn't spawn at the same spot as the pattern
        do
        {
            coinSpawnIndex = Random.Range(0, spawnPoints.Length);
        }
        while (coinSpawnIndex == patternSpawnIndex && spawnPoints.Length > 1);

        // Spawn pattern
        Transform patternSpawnPoint = spawnPoints[patternSpawnIndex];
        GameObject pattern = Instantiate(patternPrefabs[currentPatternIndex], patternSpawnPoint.position, Quaternion.identity);
        spawnedPatterns.Add(pattern);
        currentPatternIndex = (currentPatternIndex + 1) % patternPrefabs.Length;

        // Spawn coin
        Transform coinSpawnPoint = spawnPoints[coinSpawnIndex];
        GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
        spawnedCoins.Add(coin);
    }


    void MoveAndCleanPatterns(float speed)
    {
        for (int i = spawnedPatterns.Count - 1; i >= 0; i--)
        {
            GameObject pattern = spawnedPatterns[i];
            if (pattern == null) continue;

            pattern.transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

            // If pattern goes offscreen, destroy it
            if (pattern.transform.position.z < -30f)
            {
                Destroy(pattern);
                spawnedPatterns.RemoveAt(i);
            }
        }
    }

    void MoveAndCleanCoins(float speed)
    {
        for (int i = spawnedCoins.Count - 1; i >= 0; i--)
        {
            GameObject coin = spawnedCoins[i];
            if (coin == null) continue;

            coin.transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

            // If coin goes offscreen, destroy it
            if (coin.transform.position.z < -30f)
            {
                Destroy(coin);
                spawnedCoins.RemoveAt(i);
            }
        }
    }
}
