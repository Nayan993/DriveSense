using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carssPrefabs;
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] UIManager uIManager;
    [SerializeField] EndlessCity[] cityArray;
    [SerializeField] TrafficManager trafficManager;
    [SerializeField] LaneMovement laneMovement;
    [SerializeField] GestureInputReceiver gestureInputReceiver;

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

        carController.SetUiManager(uIManager);
        cameraMovement.SetTransform(carController.transform);
        uIManager.SetCarController(carController);
        cityArray[0].SetTransform(carController.transform);
        cityArray[1].SetTransform(carController.transform);
        trafficManager.SetCarController(carController);
        laneMovement.SetTransform(carController.transform);
        gestureInputReceiver.carController = carController;

    }
}
