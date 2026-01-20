using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carssPrefabs;
    void Start()
    {
        int currentCarIndex = PlayerPrefs.GetInt("CarIndexValue", 0);
        SpawnCar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnCar()
    {
        int currentCarIndex = PlayerPrefs.GetInt("CarIndexValue", 0);
        Instantiate(carssPrefabs[currentCarIndex], transform.position, transform.rotation);
    }
}
