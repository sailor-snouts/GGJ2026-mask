using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Disc : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private float destroyX = -15f;

    public int Value => value;

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
