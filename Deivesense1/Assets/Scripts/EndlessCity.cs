using UnityEngine;

public class EndlessCity : MonoBehaviour
{
 [SerializeField] Transform playerCarTransform;
 [SerializeField] Transform otherCityTransform;
 [SerializeField] float halfLength;
    void Start()
    {
        
    }
      public void SetTransform(Transform transform)
    {
        playerCarTransform = transform;
    }
    void Update()
    {
        if(playerCarTransform.position.z > transform.position.z + halfLength + 10f)
        {
            transform.position = new Vector3(0,0,otherCityTransform.position.z + halfLength * 2);
        }
    }
}
