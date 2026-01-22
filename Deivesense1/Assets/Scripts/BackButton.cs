using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoBack);
    }

    void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
