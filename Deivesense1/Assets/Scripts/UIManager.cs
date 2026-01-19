using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI totalDistanceText;
    [SerializeField] TextMeshProUGUI maximumSpeedText;
    [SerializeField] Carcontroller carController;
    [SerializeField] Transform carTransform;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject speedIcon;
    [SerializeField] GameObject distanceIcon;
    [SerializeField] GameObject scoreIcon;
    private float distance = 0f; 
    private float speed = 0f;
    private float score = 0f;
    private float maximumSpeed = 0f;
    void Start()
    {
        gameOverPanel.SetActive(false);
        speedIcon.SetActive(true);
        distanceIcon.SetActive(true);
        scoreIcon.SetActive(true);
        Time.timeScale = 1f;
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
        distance = carTransform.position.z / 1000;
        distanceText.text = distance.ToString("0.00" + "Km");
    }
    void SpeedUI()
    {
        speed = carController.CarSpeed();
        speedText.text = speed.ToString("0" + "Km/h");
    }
    void ScoreUI()
    {
        score = carTransform.position.z * 3; //random value
        scoreText.text = score.ToString("0");
    }
    public void GameOver()
    {
        speedIcon.SetActive(false);
        distanceIcon.SetActive(false);
        scoreIcon.SetActive(false);
        Time.timeScale  = 0f;
        gameOverPanel.SetActive(true);
        totalScoreText.text = score.ToString("0");
        totalDistanceText.text = distance.ToString("0.00" + "Km");
    }
    void MaximumSpeed()
    {
        float currentSpeed = carController.CarSpeed();
        if(currentSpeed > maximumSpeed)
        {
            maximumSpeed = currentSpeed;
        }
        maximumSpeedText.text = maximumSpeed.ToString("0" + "km/h");
    }
    public void TryAgian()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
