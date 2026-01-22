using UnityEngine;

public class EndlessCity : MonoBehaviour
{
    [SerializeField] Transform playerCarTransform; // Player car reference
    [SerializeField] Transform otherCityTransform; // Adjacent city piece
    [SerializeField] float halfLength; // Half length of city block

    void Start()
    {
        // Not used
    }

    public void SetTransform(Transform transform)
    {
        // Assign player car transform
        playerCarTransform = transform;
    }

    void Update()
    {
        // Move city forward when car passes it
        if(playerCarTransform.position.z > transform.position.z + halfLength + 10f)
        {
            transform.position = new Vector3(0, 0, otherCityTransform.position.z + halfLength * 2);
        }
    }
}
