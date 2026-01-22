using UnityEngine;

public class Carcontroller : MonoBehaviour
{
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontrightcollider; // Front right wheel collider
    [SerializeField] private WheelCollider backrightcollider; // Back right wheel collider
    [SerializeField] private WheelCollider frontleftcollider; // Front left wheel collider
    [SerializeField] private WheelCollider backleftcollider; // Back left wheel collider

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontrightwheeltransform; // Front right wheel mesh
    [SerializeField] private Transform backrightwheeltransform; // Back right wheel mesh
    [SerializeField] private Transform frontleftwheeltransform; // Front left wheel mesh
    [SerializeField] private Transform backleftwheeltransform; // Back left wheel mesh

    [Header("Car Physics")]
    [SerializeField] private Transform carCentreOfMassTransform; // Center of mass reference
    [SerializeField] private float motorforce = 100f; // Engine force
    [SerializeField] private float steeringAngle = 30f; // Max steering angle
    [SerializeField] private float brakeforce = 1000f; // Brake strength
    [SerializeField] UIManager uiManager; // UI manager reference

    // ================= GESTURE INPUT =================
    [HideInInspector] public float gestureVertical = 0f; // Forward/back gesture
    [HideInInspector] public float gestureHorizontal = 0f; // Left/right gesture
    [HideInInspector] public bool gestureBrake = false; // Brake gesture

    // ================= INPUT =================
    float verticalInput; // Combined vertical input
    float horizantalInput; // Combined horizontal input

    // ================= STEERING SMOOTHING =================
    [Header("Steering Tuning")]
    [SerializeField] private float steeringSmoothSpeed = 5f; // Steering smoothness
    private float currentSteer = 0f; // Current steering value

    private Rigidbody rb;

    private void Awake()
    {
        // Cache rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Stability settings
        rb.centerOfMass = carCentreOfMassTransform.localPosition;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.angularDamping = 4.5f; // Extra stability
    }

    public void SetUiManager(UIManager manager)
    {
        // Assign UI manager
        uiManager = manager;
    }

    void FixedUpdate()
    {
        GetInput();
        MotorForce();
        Steering();
        ApplyBrakes();
        UpdateWheels();
        AutoStraighten();

        // Extra angular stability
        rb.angularVelocity *= 0.95f;
    }

    // ================= INPUT =================
    void GetInput()
    {
        float keyVertical = Input.GetAxis("Vertical");
        float keyHorizontal = Input.GetAxis("Horizontal");

        // Prefer gesture input if available
        verticalInput =
            Mathf.Abs(gestureVertical) > 0.01f
            ? gestureVertical
            : keyVertical;

        // Slightly reduce gesture steering
        if (Mathf.Abs(gestureHorizontal) > 0.01f)
            horizantalInput = gestureHorizontal * 0.7f;
        else
            horizantalInput = keyHorizontal;
    }

    // ================= MOTOR =================
    void MotorForce()
    {
        // Apply motor torque
        frontrightcollider.motorTorque = motorforce * verticalInput;
        frontleftcollider.motorTorque = motorforce * verticalInput;
    }

    // ================= STEERING =================
    void Steering()
    {
        float speed = CarSpeed();

        // Reduce steering at higher speed
        float speedFactor = Mathf.Lerp(1f, 0.3f, speed / 100f);

        float targetSteer =
            horizantalInput * steeringAngle * speedFactor;

        // Smooth steering transition
        currentSteer = Mathf.Lerp(
            currentSteer,
            targetSteer,
            Time.fixedDeltaTime * steeringSmoothSpeed
        );

        frontrightcollider.steerAngle = currentSteer;
        frontleftcollider.steerAngle = currentSteer;
    }

    // ================= AUTO STRAIGHTEN =================
    void AutoStraighten()
    {
        // Return wheels to center when no input
        if (Mathf.Abs(horizantalInput) < 0.05f)
        {
            currentSteer = Mathf.Lerp(
                currentSteer,
                0f,
                Time.fixedDeltaTime * 5f
            );

            frontrightcollider.steerAngle = currentSteer;
            frontleftcollider.steerAngle = currentSteer;
        }
    }

    // ================= BRAKES =================
    void ApplyBrakes()
    {
        // Apply brake from gesture or space key
        float brake = (gestureBrake || Input.GetKey(KeyCode.Space))
            ? brakeforce
            : 0f;

        frontrightcollider.brakeTorque = brake;
        frontleftcollider.brakeTorque = brake;
        backrightcollider.brakeTorque = brake;
        backleftcollider.brakeTorque = brake;
    }

    // ================= WHEELS =================
    void UpdateWheels()
    {
        // Sync wheel meshes with colliders
        RotateWheel(frontrightcollider, frontrightwheeltransform);
        RotateWheel(frontleftcollider, frontleftwheeltransform);
        RotateWheel(backrightcollider, backrightwheeltransform);
        RotateWheel(backleftcollider, backleftwheeltransform);
    }

    void RotateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        // Update wheel position and rotation
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    // ================= SPEED =================
    public float CarSpeed()
    {
        // Convert m/s to mph
        return rb.linearVelocity.magnitude * 2.23693629f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Trigger game over on traffic hit
        if (collision.gameObject.tag == "TrafficVehicle")
        {
            uiManager.GameOver();
        }
    }
}
