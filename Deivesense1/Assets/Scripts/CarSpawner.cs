using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carssPrefabs; // Available car prefabs
    [SerializeField] CameraMovement cameraMovement; // Camera follow script
    [SerializeField] UIManager uIManager; // UI handler
    [SerializeField] EndlessCity[] cityArray; // City chunks
    [SerializeField] TrafficManager trafficManager; // Traffic controller
    [SerializeField] LaneMovement laneMovement; // Lane movement logic
    [SerializeField] GestureInputReceiver gestureInputReceiver; // Gesture input handler

    void Start()
    {
        // Get selected car index
        int currentCarIndex = PlayerPrefs.GetInt("CarIndexValue", 0);
        SpawnCar();
    }

    // Update is called once per frame
    void Update()
    {
        // Not used
    }

    void SpawnCar()
    {
        // Spawn selected car
        int currentCarIndex = PlayerPrefs.GetInt("CarIndexValue", 0);
        GameObject newCar = Instantiate(carssPrefabs[currentCarIndex], transform.position, transform.rotation);
        Carcontroller carController = newCar.GetComponent<Carcontroller>();

        // Link car with other systems
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
