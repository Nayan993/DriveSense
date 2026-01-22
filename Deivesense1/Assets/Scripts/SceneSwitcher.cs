using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup; // Fade UI group
    [SerializeField] float speed = 1f; // Fade speed

    void Start()
    {
        Time.timeScale = 1f; // Reset time scale
        StartCoroutine(FadeIn());
    }

    private void Awake()
    {
        // Start with full black screen
        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeIn()
    {
        // Fade screen in
        while(canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= speed * Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }

    IEnumerator FadeOut(string sceneName)
    {
        // Fade screen out
        while(canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += speed * Time.unscaledDeltaTime;
            yield return null;
        }

        // Load next scene
        SceneManager.LoadScene(sceneName);
    }

    public void SceneLoader(string sceneName)
    {
        // Start fade-out and scene change
        StartCoroutine(FadeOut(sceneName));
    }

    public void QuitGame()
    {
        // Exit the game
        Application.Quit();
    }
}
