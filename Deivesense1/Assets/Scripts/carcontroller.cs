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
    public Transform carCentreOfMassTransform;
    public Rigidbody rigidbody;
    float verticalInput;
    float horizantalInput;

    public float motorforce = 100f;
    public float steeringAngle = 30f;
    public float brakeforce = 1000f;
    void Start()
    {
        rigidbody.centerOfMass = carCentreOfMassTransform.localPosition;
    }
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate running");
        MotorForce();
        UpdateWheels();
        GetInput();
        Steering();
        ApplyBrakes();

    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizantalInput = Input.GetAxis("Horizontal");
    }
    void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontrightcollider.brakeTorque = brakeforce;
            frontleftcollider.brakeTorque = brakeforce;
            backrightcollider.brakeTorque = brakeforce;
            backleftcollider.brakeTorque = brakeforce;
        }
        else
        {
            frontrightcollider.brakeTorque = 0f;
            frontleftcollider.brakeTorque = 0f;
            backrightcollider.brakeTorque = 0f;
            backleftcollider.brakeTorque = 0f;
        }
        
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
