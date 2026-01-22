using UnityEngine;

public class LaneMovement : MonoBehaviour
{
    [SerializeField] Transform playerCarTransform; // Player car reference
    [SerializeField] float offSet = -5; // Offset from car position

    void Start()
    {
        // Not used
    }

    public void SetTransform(Transform transform)
    {
        // Assign player car transform
        playerCarTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Skip if car not assigned
        if(playerCarTransform == null)
        {
            return;
        }

        // Follow car on Z axis
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offSet;
        transform.position = cameraPos;
    }
}
