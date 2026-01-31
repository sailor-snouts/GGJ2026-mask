using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneTransition))]
public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    public SceneTransition Transition { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Transition = GetComponent<SceneTransition>();
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        Transition.FadeOut(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        });
    }

    public void LoadScene(int buildIndex)
    {
        Transition.FadeOut(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(buildIndex);
        });
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void QuitGame()
    {
        Transition.FadeOut(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
