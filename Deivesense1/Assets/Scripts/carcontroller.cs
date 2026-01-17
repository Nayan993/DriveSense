using UnityEngine;

public class Carcontroller : MonoBehaviour
{
    [SerializeField]private WheelCollider frontrightcollider;
    [SerializeField]private WheelCollider backrightcollider;
    [SerializeField]private WheelCollider frontleftcollider;
    [SerializeField]private WheelCollider backleftcollider;

    [SerializeField] private Transform frontrightwheeltransform;
    [SerializeField] private Transform backrightwheeltransform;
    [SerializeField] private Transform frontleftwheeltransform;
    [SerializeField] private Transform backleftwheeltransform;
    [SerializeField] private Transform carCentreOfMassTransform;
    [SerializeField]private float motorforce = 100f;
    [SerializeField]private float steeringAngle = 30f;
    [SerializeField]private float brakeforce = 1000f;
    float verticalInput;
    float horizantalInput;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = carCentreOfMassTransform.localPosition;
    }
    void FixedUpdate()
    {
        // Debug.Log("FixedUpdate running");
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
