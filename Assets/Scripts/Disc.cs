using UnityEngine;

/// <summary>
/// A collectible disc that gives the player points when collected.
/// In the room-based system, discs are statically placed in room prefabs.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Disc : MonoBehaviour
{
    [SerializeField] private int value = 1;

    public int Value => value;

    // Collection is handled by PlayerController.OnTriggerEnter2D
}
