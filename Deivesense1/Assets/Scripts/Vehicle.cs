using UnityEngine;

public class Vehicle : MonoBehaviour
{
   [SerializeField]  float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f,0f,speed * Time.deltaTime);
    }
}
