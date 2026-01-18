using UnityEngine;

public class Carcontroller : MonoBehaviour
{
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontrightcollider;
    [SerializeField] private WheelCollider backrightcollider;
    [SerializeField] private WheelCollider frontleftcollider;
    [SerializeField] private WheelCollider backleftcollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontrightwheeltransform;
    [SerializeField] private Transform backrightwheeltransform;
    [SerializeField] private Transform frontleftwheeltransform;
    [SerializeField] private Transform backleftwheeltransform;

    [Header("Car Physics")]
    [SerializeField] private Transform carCentreOfMassTransform;
    [SerializeField] private float motorforce = 100f;
    [SerializeField] private float steeringAngle = 30f;
    [SerializeField] private float brakeforce = 1000f;

    float verticalInput;
    float horizantalInput;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Stability settings
        rb.centerOfMass = carCentreOfMassTransform.localPosition;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.angularDamping = 2.5f;
    }

    void FixedUpdate()
    {
        GetInput();
        MotorForce();
        Steering();
        ApplyBrakes();
        UpdateWheels();
        AutoStraighten();
    }

    // ================= INPUT =================
    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizantalInput = Input.GetAxis("Horizontal");
    }

    // ================= MOTOR =================
    void MotorForce()
    {
        frontrightcollider.motorTorque = motorforce * verticalInput;
        frontleftcollider.motorTorque = motorforce * verticalInput;
    }

    // ================= STEERING =================
    void Steering()
    {
        float speed = CarSpeed();

        // Reduce steering at high speed
        float speedFactor = Mathf.Lerp(1f, 0.25f, speed / 120f);
        float steer = horizantalInput * steeringAngle * speedFactor;

        frontrightcollider.steerAngle = steer;
        frontleftcollider.steerAngle = steer;
    }

    // Auto-straighten wheels when no input
    void AutoStraighten()
    {
        if (Mathf.Abs(horizantalInput) < 0.05f)
        {
            frontrightcollider.steerAngle = Mathf.Lerp(
                frontrightcollider.steerAngle, 0f, Time.fixedDeltaTime * 5f);

            frontleftcollider.steerAngle = Mathf.Lerp(
                frontleftcollider.steerAngle, 0f, Time.fixedDeltaTime * 5f);
        }
    }

    // ================= BRAKES =================
    void ApplyBrakes()
    {
        float brake = Input.GetKey(KeyCode.Space) ? brakeforce : 0f;

        frontrightcollider.brakeTorque = brake;
        frontleftcollider.brakeTorque = brake;
        backrightcollider.brakeTorque = brake;
        backleftcollider.brakeTorque = brake;
    }

    // ================= WHEELS =================
    void UpdateWheels()
    {
        RotateWheel(frontrightcollider, frontrightwheeltransform);
        RotateWheel(frontleftcollider, frontleftwheeltransform);
        RotateWheel(backrightcollider, backrightwheeltransform);
        RotateWheel(backleftcollider, backleftwheeltransform);
    }

    void RotateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    // ================= SPEED =================
    public float CarSpeed()
    {
        return rb.linearVelocity.magnitude * 2.23693629f; // m/s → mph
    }
}
