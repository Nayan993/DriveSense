using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class GestureInputReceiver : MonoBehaviour
{
    public int port = 5055;

    private UdpClient udpClient;
    private Thread receiveThread;
    private bool running = false;

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

                Debug.Log("Gesture received: " + message);
            }
            catch
            {
                // Ignore socket errors on shutdown
            }
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
