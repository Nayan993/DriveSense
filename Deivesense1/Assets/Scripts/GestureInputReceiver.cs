using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class GestureInputReceiver : MonoBehaviour
{
    [Header("UDP Settings")]
    public int port = 5055; // Port to receive gesture data

    [Header("Runtime Reference")]
    public Carcontroller carController; // Reference to car controller

    [Header("Command Hold Settings")]
    [SerializeField] private float commandHoldTime = 0.3f; // Command validity time

    // ================= NEW : GESTURE INDICATOR =================
    [Header("Gesture Indicator")]
    [SerializeField] private Image gestureIndicator; // UI indicator image
    [SerializeField] private Color inactiveColor = Color.red; // No gesture color
    [SerializeField] private Color activeColor = Color.green; // Gesture detected color
    [SerializeField] private float indicatorTimeout = 0.5f; // Indicator reset time

    private float lastGestureTime = -10f; // Last gesture timestamp
    // ===========================================================

    private UdpClient udpClient;
    private Thread receiveThread;
    private bool running = false; // Receiver state

    // Thread-safe communication
    private string pendingCommand = "IDLE"; // Incoming command
    private bool commandReceived = false; // New command flag

    // Main-thread state
    private string lastCommand = "IDLE"; // Last applied command
    private float lastCommandTime = 0f; // Last command time

    void Start()
    {
        // Initialize UDP listener
        udpClient = new UdpClient(port);
        running = true;

        receiveThread = new Thread(ReceiveLoop);
        receiveThread.IsBackground = true;
        receiveThread.Start();

        Debug.Log("GestureInputReceiver started on port " + port);
    }

    void ReceiveLoop()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        while (running)
        {
            try
            {
                // Receive gesture data
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);

                // DO NOT use Unity API here
                pendingCommand = message;
                commandReceived = true;
            }
            catch
            {
                // Ignore errors during shutdown
            }
        }
    }

    void Update()
    {
        // ================= NEW : INDICATOR UPDATE =================
        if (gestureIndicator != null)
        {
            // Update indicator based on recent gesture
            if (Time.time - lastGestureTime < indicatorTimeout)
                gestureIndicator.color = activeColor;
            else
                gestureIndicator.color = inactiveColor;
        }
        // ==========================================================

        if (carController == null)
            return;

        // Safely apply command from background thread
        if (commandReceived)
        {
            lastCommand = pendingCommand;
            lastCommandTime = Time.time;
            lastGestureTime = Time.time; // Mark gesture activity
            commandReceived = false;
        }

        // Reset inputs if command expired
        if (Time.time - lastCommandTime > commandHoldTime)
        {
            carController.gestureVertical = 0f;
            carController.gestureHorizontal = 0f;
            carController.gestureBrake = false;
            return;
        }

        // Clear frame-based inputs
        carController.gestureHorizontal = 0f;
        carController.gestureBrake = false;

        // Apply gesture command
        switch (lastCommand)
        {
            case "FULL_SPEED":
                carController.gestureVertical = 1f;
                break;

            case "SLOW_SPEED":
                carController.gestureVertical = 0.4f;
                break;

            case "REVERSE":
                carController.gestureVertical = -0.5f;
                break;

            case "LEFT":
                carController.gestureHorizontal = -1f;
                break;

            case "RIGHT":
                carController.gestureHorizontal = 1f;
                break;

            case "BRAKE":
                carController.gestureBrake = true;
                carController.gestureVertical = 0f;
                break;
        }
    }

    void OnApplicationQuit()
    {
        // Stop receiver safely
        running = false;

        if (udpClient != null)
            udpClient.Close();

        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();
    }
}
