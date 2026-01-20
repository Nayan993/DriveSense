using UnityEngine;

public class LaneMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   [SerializeField] Transform playerCarTransform;
    [SerializeField] float offSet = -5;
    void Start()
    {
        
    }
     public void SetTransform(Transform transform)
    {
        playerCarTransform = transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(playerCarTransform == null)
        {
            return;
        }
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offSet;
        transform.position = cameraPos;
    }
}
