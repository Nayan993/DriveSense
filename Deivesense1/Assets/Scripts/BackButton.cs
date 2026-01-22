using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    void Start()
    {
        // Attach click listener to the UI button
        GetComponent<Button>().onClick.AddListener(GoBack);
    }

    void GoBack()
    {
        // Load the main menu scene when button is clicked
        SceneManager.LoadScene("MainMenu");
    }
}
