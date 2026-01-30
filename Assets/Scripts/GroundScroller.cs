using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [Header("Ground Segments")]
    [SerializeField] private Transform[] segments;
    [SerializeField] private float segmentWidth = 20f;

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Playing) return;

        float speed = GameManager.Instance.GameSpeed;

        foreach (var segment in segments)
        {
            if (segment == null) continue;

            segment.position += Vector3.left * speed * Time.deltaTime;

            if (segment.position.x >= -segmentWidth) continue;

            float rightmostX = float.MinValue;
            foreach (var s in segments)
            {
                if (s != null && s.position.x > rightmostX)
                    rightmostX = s.position.x;
            }

            segment.position = new Vector3(rightmostX + segmentWidth, segment.position.y, segment.position.z);
        }
    }
}
