using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText; // Speed display
    [SerializeField] TextMeshProUGUI distanceText; // Distance display
    [SerializeField] TextMeshProUGUI scoreText; // Score display
    [SerializeField] TextMeshProUGUI totalScoreText; // Final score
    [SerializeField] TextMeshProUGUI totalDistanceText; // Final distance
    [SerializeField] TextMeshProUGUI maximumSpeedText; // Max speed reached

    [SerializeField] Carcontroller carController; // Player car reference

    [SerializeField] GameObject gameOverPanel; // Game over UI
    [SerializeField] GameObject speedIcon; // Speed icon
    [SerializeField] GameObject distanceIcon; // Distance icon
    [SerializeField] GameObject scoreIcon; // Score icon

    [SerializeField] CarEngineSound carEngineSound; // Engine sound handler

    private float distance = 0f; // Travelled distance
    private float speed = 0f; // Current speed
    private float score = 0f; // Player score
    private float maximumSpeed = 0f; // Highest speed achieved

    void Start()
    {
        // Reset game state
        CarEngineSound.isGameOver = false;
        gameOverPanel.SetActive(false);

        speedIcon.SetActive(true);
        distanceIcon.SetActive(true);
        scoreIcon.SetActive(true);

        Time.timeScale = 1f;
    }

    public void SetCarController(Carcontroller controller)
    {
        // Assign car controller
        carController = controller;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceUI();
        SpeedUI();
        ScoreUI();
        MaximumSpeed();
    }

    void DistanceUI()
    {
        // Update distance text
        distance = carController.transform.position.z / 1000;
        distanceText.text = distance.ToString("0.00" + "Km");
    }

    void SpeedUI()
    {
        // Update speed text
        speed = carController.CarSpeed();
        speedText.text = speed.ToString("0" + "Km/h");
    }

    void ScoreUI()
    {
        // Calculate and update score
        score = carController.transform.position.z * 3; // random value
        scoreText.text = score.ToString("0");
    }

    public void GameOver()
    {
        // Handle game over state
        CarEngineSound.isGameOver = true;

        speedIcon.SetActive(false);
        distanceIcon.SetActive(false);
        scoreIcon.SetActive(false);

        Time.timeScale  = 0f;
        gameOverPanel.SetActive(true);

        totalScoreText.text = score.ToString("0");
        totalDistanceText.text = distance.ToString("0.00" + "Km");

        LeaderboardManager.SaveScore(score);
        Debug.Log("Score saved: " + score);
        Debug.Log("Total saved scores: " + PlayerPrefs.GetInt("ScoreCount"));
    }

    void MaximumSpeed()
    {
        // Track maximum speed
        float currentSpeed = carController.CarSpeed();
        if(currentSpeed > maximumSpeed)
        {
            maximumSpeed = currentSpeed;
        }

        maximumSpeedText.text = maximumSpeed.ToString("0" + "km/h");
    }

    public void TryAgian()
    {
        // Restart current scene
        CarEngineSound.isGameOver = false;
        CarEngineSound.isGameOver = false;

        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GarageButton()
    {
        // Go to garage scene
        SceneManager.LoadScene("Garage");
    }
}
