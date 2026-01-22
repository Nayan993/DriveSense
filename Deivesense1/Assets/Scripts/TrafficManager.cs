using UnityEngine;
using System.Collections;

public class TrafficManager : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] Transform[] lanes; // Traffic spawn lanes
    [SerializeField] GameObject[] trafficVehicles; // Traffic vehicle prefabs

    [Header("Player Reference")]
    [SerializeField] Carcontroller carController; // Player car controller
    [SerializeField] float minSpawnTime = 30f; // Minimum spawn delay
    [SerializeField] float maxSpawnTime = 60f; // Maximum spawn delay

    private float dynamicTimer = 2f; // Dynamic spawn timer

    void Start()
    {
        // Start traffic spawning loop
        StartCoroutine(TrafficSpawner());
    }

    public void SetCarController(Carcontroller controller)
    {
        // Assign player car controller
        carController = controller;
    }

    IEnumerator TrafficSpawner()
    {
        // Initial delay so game settles
        yield return new WaitForSeconds(2f);

        while (true)
        {
            // Safety check
            if (carController == null)
            {
                Debug.LogError("CarController not assigned in TrafficManager!");
                yield break;
            }

            // Get safe speed value
            float speed = Mathf.Max(carController.CarSpeed(), 1f);

            // Spawn only when car is moving fast enough
            if (speed > 20f)
            {
                // Dynamic spawn delay (higher speed = more traffic)
                dynamicTimer = Mathf.Clamp(
                    Random.Range(minSpawnTime, maxSpawnTime) / speed,
                    1.5f,
                    6f
                );

                SpawnTrafficVehicle();
            }

            yield return new WaitForSeconds(dynamicTimer);
        }
    }

    void SpawnTrafficVehicle()
    {
        // Validate spawn data
        if (lanes.Length == 0 || trafficVehicles.Length == 0)
        {
            Debug.LogWarning("Lanes or Traffic Vehicles not assigned!");
            return;
        }

        // Pick random lane and vehicle
        int randomLaneIndex = Random.Range(0, lanes.Length);
        int randomVehicleIndex = Random.Range(0, trafficVehicles.Length);

        Instantiate(
            trafficVehicles[randomVehicleIndex],
            lanes[randomLaneIndex].position,
            Quaternion.identity
        );
    }
}
