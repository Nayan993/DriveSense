using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float speed = 1f;
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeIn());
    }
    private void Awake()
    {
        canvasGroup.alpha = 1f;
    }
    IEnumerator FadeIn()
    {
        while(canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= speed*Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
     IEnumerator FadeOut(string sceneName)
    {
        while(canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += speed*Time.unscaledDeltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
   public void SceneLoader(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
