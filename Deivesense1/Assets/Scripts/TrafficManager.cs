using UnityEngine;
using System.Collections;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] Transform[] lanes;
    [SerializeField] GameObject[] trafficVehicles;
    [SerializeField] Carcontroller carController;

    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }

    IEnumerator TrafficSpawner()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
           if(carController.CarSpeed() > 20f)
            {
                SpawnTrafficVehicle();
            }
            yield return new WaitForSeconds(2f);
        }
    }
    void SpawnTrafficVehicle()
    {
         int randomlaneindex = Random.Range(0, lanes.Length);
            int randomvehicleindex = Random.Range(0, trafficVehicles.Length);
            Instantiate(
                trafficVehicles[randomvehicleindex],
                lanes[randomlaneindex].position,
                Quaternion.identity
            );
    }
}
