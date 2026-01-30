using UnityEngine;

public class DiscSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject discPrefab;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 4f;

    [Header("Spawn Position")]
    [SerializeField] private float spawnX = 12f;
    [SerializeField] private float minY = -1f;
    [SerializeField] private float maxY = 2f;

    private float nextSpawnTime;
    private bool isSpawning;

    private void OnEnable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnGameStart += StartSpawning;
        GameManager.Instance.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnGameStart -= StartSpawning;
        GameManager.Instance.OnGameOver -= StopSpawning;
    }

    private void Update()
    {
        if (!isSpawning) return;
        if (GameManager.Instance.State != GameState.Playing) return;
        if (Time.time < nextSpawnTime) return;

        SpawnDisc();
        ScheduleNextSpawn();
    }

    private void StartSpawning()
    {
        isSpawning = true;
        ScheduleNextSpawn();
    }

    private void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnDisc()
    {
        if (discPrefab == null) return;

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);
        Instantiate(discPrefab, spawnPos, Quaternion.identity);
    }

    private void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
