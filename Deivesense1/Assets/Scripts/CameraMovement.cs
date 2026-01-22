using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerCarTransform; // Reference to the player car
    [SerializeField] float offSet = -5; // Distance behind the car

    void Start()
    {
        // Intentionally left empty
    }

    public void SetTransform(Transform transform)
    {
        // Assign the player car transform at runtime
        playerCarTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Keep camera aligned with car on Z axis
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offSet;
        transform.position = cameraPos;
    }
}
