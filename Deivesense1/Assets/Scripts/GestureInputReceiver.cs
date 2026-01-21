using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class GestureInputReceiver : MonoBehaviour
{
    [Header("UDP Settings")]
    public int port = 5055;

    [Header("Runtime Reference")]
    public Carcontroller carController;

    [Header("Command Hold Settings")]
    [SerializeField] private float commandHoldTime = 0.3f;

    private UdpClient udpClient;
    private Thread receiveThread;
    private bool running = false;

    // Thread-safe communication
    private string pendingCommand = "IDLE";
    private bool commandReceived = false;

    // Main-thread state
    private string lastCommand = "IDLE";
    private float lastCommandTime = 0f;

    void Start()
    {
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
        if (carController == null)
            return;

        // Safely transfer command from background thread
        if (commandReceived)
        {
            lastCommand = pendingCommand;
            lastCommandTime = Time.time;
            commandReceived = false;
        }

        // Expire command if too old
        if (Time.time - lastCommandTime > commandHoldTime)
        {
            carController.gestureVertical = 0f;
            carController.gestureHorizontal = 0f;
            carController.gestureBrake = false;
            return;
        }

        // Reset transient inputs every frame
        carController.gestureHorizontal = 0f;
        carController.gestureBrake = false;

        // Apply command continuously
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
        running = false;

        if (udpClient != null)
            udpClient.Close();

        if (receiveThread != null && receiveThread.IsAlive)
            receiveThread.Abort();
    }
}
