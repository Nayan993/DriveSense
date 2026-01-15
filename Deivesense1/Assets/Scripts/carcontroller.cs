using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public WheelCollider frontrightcollider;
    public WheelCollider backrightcollider;
    public WheelCollider frontleftcollider;
    public WheelCollider backleftcollider;

    public Transform frontrightwheeltransform;
    public Transform backrightwheeltransform;
    public Transform frontleftwheeltransform;
    public Transform backleftwheeltransform;

    void FixedUpdate()
    {
        MotorForce();
        UpdateWheels();
    }

    void MotorForce()
    {
        frontrightcollider.motorTorque = 10f;
        frontleftcollider.motorTorque = 10f;
    }

    void UpdateWheels()
    {
        RotateWheel(frontrightcollider, frontrightwheeltransform);
         RotateWheel(backleftcollider, backleftwheeltransform);
          RotateWheel(frontleftcollider,frontleftwheeltransform);
           RotateWheel(backrightcollider, backrightwheeltransform);
    }

    void RotateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;   

        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
