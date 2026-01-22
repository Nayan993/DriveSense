using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] GameObject[] cars; // All selectable cars
    int currentCarIndex = 0; // Currently selected car

    void Start()
    {
        Time.timeScale = 1f; // Ensure game runs normally
        ShowCar(currentCarIndex);
    }

    public void NextCar()
    {
        // Move to next car
        currentCarIndex = (currentCarIndex + 1) % cars.Length;
        ShowCar(currentCarIndex);
    }

    public void PreviousCar()
    {   
        // Move to previous car
        currentCarIndex = (currentCarIndex - 1 + cars.Length) % cars.Length;
        ShowCar(currentCarIndex);
    }

    void ShowCar(int index)
    {
        // Activate only the selected car
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == index);
        }
    }

    public void SelectCar()
    {
        // Save selected car index
        PlayerPrefs.SetInt("CarIndexValue", currentCarIndex);
    }
}
