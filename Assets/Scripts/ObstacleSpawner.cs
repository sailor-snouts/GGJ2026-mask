using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float minSpawnInterval = 1.5f;
    [SerializeField] private float maxSpawnInterval = 3f;

    [Header("Spawn Position")]
    [SerializeField] private float spawnX = 12f;
    [SerializeField] private float spawnY = -2f;

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

        SpawnObstacle();
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

    private void SpawnObstacle()
    {
        if (obstaclePrefab == null) return;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }

    private void ScheduleNextSpawn()
    {
        float speedFactor = GameManager.Instance.GameSpeed / 5f;
        float adjustedMin = minSpawnInterval / speedFactor;
        float adjustedMax = maxSpawnInterval / speedFactor;

        nextSpawnTime = Time.time + Random.Range(
            Mathf.Max(0.5f, adjustedMin),
            Mathf.Max(1f, adjustedMax)
        );
    }
}
