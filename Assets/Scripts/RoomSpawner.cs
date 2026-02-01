using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public static RoomSpawner Instance { get; private set; }

    [Header("Room Settings")]
    [SerializeField] private List<GameObject> roomPrefabs;
    [SerializeField] private float roomWidth = 20f;

    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Spawning")]
    [SerializeField] private float cleanupDistance = 30f;

    private List<GameObject> activeRooms = new List<GameObject>();
    private float nextRoomSpawnX = 0f;
    private bool isSpawning;
    private int roomsSpawned = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Spawn initial rooms immediately - one for player, two ahead
        SpawnNextRoom();
        SpawnNextRoom();
        SpawnNextRoom();
        isSpawning = true;
    }

    private void OnEnable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnGameOver -= StopSpawning;
    }

    private void Update()
    {
        if (!isSpawning) return;
        if (player == null) return;

        // Spawn next room 2 rooms ahead of player
        if (player.position.x + (roomWidth * 2) > nextRoomSpawnX)
        {
            SpawnNextRoom();
        }

        CleanupOldRooms();
    }

    private void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnNextRoom()
    {
        if (roomPrefabs == null || roomPrefabs.Count == 0) return;

        // First 2 rooms always use the first prefab, then random
        GameObject prefab;
        if (roomsSpawned < 2)
            prefab = roomPrefabs[0];
        else
            prefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

        var room = Instantiate(prefab, new Vector3(nextRoomSpawnX, -3.5f, 0f), Quaternion.identity);
        activeRooms.Add(room);
        nextRoomSpawnX += roomWidth;
        roomsSpawned++;
    }

    private void CleanupOldRooms()
    {
        for (int i = activeRooms.Count - 1; i >= 0; i--)
        {
            if (activeRooms[i] == null)
            {
                activeRooms.RemoveAt(i);
                continue;
            }

            // Room is far behind the player
            if (activeRooms[i].transform.position.x + roomWidth < player.position.x - cleanupDistance)
            {
                Destroy(activeRooms[i]);
                activeRooms.RemoveAt(i);
            }
        }
    }

    public void ClearAllRooms()
    {
        foreach (var room in activeRooms)
        {
            if (room != null)
                Destroy(room);
        }
        activeRooms.Clear();
        nextRoomSpawnX = 0f;
    }
}
