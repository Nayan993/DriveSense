using UnityEngine;

public class Carcontroller : MonoBehaviour
{
    public WheelCollider frontrightcollider;
    public WheelCollider backrightcollider;
    public WheelCollider frontleftcollider;
    public WheelCollider backleftcollider;

    public Transform frontrightwheeltransform;
    public Transform backrightwheeltransform;
    public Transform frontleftwheeltransform;
    public Transform backleftwheeltransform;
    float verticalInput;
    float horizantalInput;

    public float motorforce = 100f;
    public float steeringAngle = 30f;
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate running");
        MotorForce();
        UpdateWheels();
        GetInput();
        Steering();
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizantalInput = Input.GetAxis("Horizontal");
    }

    void MotorForce()
    {
        frontrightcollider.motorTorque = motorforce * verticalInput;
        frontleftcollider.motorTorque = motorforce * verticalInput;
    }

    void Steering()
    {
        frontrightcollider.steerAngle = horizantalInput * steeringAngle;
        frontleftcollider.steerAngle = horizantalInput * steeringAngle;
    }

    void UpdateWheels()
    {
        RotateWheel(frontrightcollider, frontrightwheeltransform);
        RotateWheel(backleftcollider, backleftwheeltransform);
        RotateWheel(frontleftcollider, frontleftwheeltransform);
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
