using UnityEngine;
using System.Collections;

public class TrafficManager : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] Transform[] lanes;
    [SerializeField] GameObject[] trafficVehicles;

    [Header("Player Reference")]
    [SerializeField] Carcontroller carController;
    [SerializeField] float minSpawnTime = 30f;
    [SerializeField] float maxSpawnTime = 60f;
    private float dynamicTimer = 2f;
    void Start()
    {
        StartCoroutine(TrafficSpawner());
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
                 // Dynamic spawn delay (faster speed = more traffic)
            
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
        if (lanes.Length == 0 || trafficVehicles.Length == 0)
        {
            Debug.LogWarning("Lanes or Traffic Vehicles not assigned!");
            return;
        }

        int randomLaneIndex = Random.Range(0, lanes.Length);
        int randomVehicleIndex = Random.Range(0, trafficVehicles.Length);

        Instantiate(
            trafficVehicles[randomVehicleIndex],
            lanes[randomLaneIndex].position,
            Quaternion.identity
        );
    }
}
