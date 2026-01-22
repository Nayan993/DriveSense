using UnityEngine;

public class FollowCar : MonoBehaviour
{
    private Transform playerCarTransform; // Player car transform
    private Transform cameraPointTransform; // Camera follow point
    private Vector3 velocity = Vector3.zero; // SmoothDamp velocity

    void Start()
    {
        // Find player car using tag
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraPointTransform = playerCarTransform.Find("CameraPoint").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // Always look at the car
        transform.LookAt(playerCarTransform);

        // Smoothly follow camera point
        transform.position = Vector3.SmoothDamp(
            transform.position,
            cameraPointTransform.position,
            ref velocity,
            0.2f
        );
    }
}
