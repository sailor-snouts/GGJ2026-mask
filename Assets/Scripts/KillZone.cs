using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KillZone : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider2D>();
        if (collider == null) return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position + (Vector3)collider.offset, collider.size);
    }
}
