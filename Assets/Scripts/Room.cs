using UnityEngine;

/// <summary>
/// Marks a GameObject as a room for the RoomSpawner system.
/// Attach this to room prefabs to identify room boundaries and hold room-specific data.
/// </summary>
public class Room : MonoBehaviour
{
    [Header("Room Info")]
    [SerializeField] private string roomName;
    [SerializeField] private float width = 20f;

    public string RoomName => roomName;
    public float Width => width;
}
