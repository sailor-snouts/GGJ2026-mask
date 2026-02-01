using UnityEngine;

/// <summary>
/// Marks a GameObject as an obstacle that damages the player on contact.
/// In the room-based system, obstacles are statically placed in room prefabs.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    // Collision is handled by PlayerController.OnTriggerEnter2D
    // This component simply identifies the object as an obstacle via its tag
}
