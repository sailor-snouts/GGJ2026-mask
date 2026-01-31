using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float startDelay = 0.5f;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Colors")]
    [SerializeField] private Color startColor = Color.black;
    [SerializeField] private Color endColor = Color.white;

    private void Awake()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = startColor;
        }
    }

    private void Start()
    {
        if (fadeImage == null)
        {
            Debug.LogError("SceneTransition: No fade image assigned!");
            return;
        }

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {

        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        fadeImage.color = endColor;
        fadeImage.gameObject.SetActive(false);
    }
}
