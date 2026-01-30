using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float destroyX = -15f;

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Playing) return;

        transform.position += Vector3.left * GameManager.Instance.GameSpeed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
