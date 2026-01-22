using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    void Start()
    {
        // Nothing needed here
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the parent object on trigger contact
        Destroy(other.transform.parent.gameObject);
    }
}
