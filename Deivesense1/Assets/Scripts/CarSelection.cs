using UnityEngine;

public class CarSelection : MonoBehaviour
{
    [SerializeField] GameObject[] cars;
    int currentCarIndex = 0;

    void Start()
    {
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == currentCarIndex);
        }
    }
    void Update()
    {
        
    }
}
