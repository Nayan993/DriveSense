using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carssPrefabs;
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] UIManager uIManager;
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
        GameObject newCar = Instantiate(carssPrefabs[currentCarIndex], transform.position, transform.rotation);
        Carcontroller carController = newCar.GetComponent<Carcontroller>();
        cameraMovement.SetTransform(carController.transform);
        uIManager.SetCarController(carController);
    }
}
