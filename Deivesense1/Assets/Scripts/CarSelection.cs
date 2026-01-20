using UnityEngine;
using UnityEngine.SceneManagement;
public class CarSelection : MonoBehaviour
{
    [SerializeField] GameObject[] cars;
    int currentCarIndex = 0;

    void Start()
    {
        Time.timeScale = 1f;
        ShowCar(currentCarIndex);
    }
    public void NextCar()
    {
    currentCarIndex = (currentCarIndex + 1) % cars.Length;
    ShowCar(currentCarIndex);
    }
    public void PreviousCar()
    {   
    currentCarIndex = (currentCarIndex - 1 + cars.Length) % cars.Length;
    ShowCar(currentCarIndex);
    }

    void ShowCar(int index)
    {
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == index);
        }
    }
    public void SelectCar()
    {
        PlayerPrefs.SetInt("CarIndexValue", currentCarIndex);
    }
}
