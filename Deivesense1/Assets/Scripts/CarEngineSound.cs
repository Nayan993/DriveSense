using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    public Rigidbody carRb;
    private AudioSource engineAudio;

    // 🔴 GLOBAL GAME OVER FLAG
    public static bool isGameOver = false;

    void Start()
    {
        engineAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 🔴 HARD STOP
        if (isGameOver)
        {
            if (engineAudio.isPlaying)
                engineAudio.Stop();
            return;
        }

        if (carRb == null) return;

        float speed = carRb.linearVelocity.magnitude;

        if (speed > 0.2f)
        {
            if (!engineAudio.isPlaying)
                engineAudio.Play();

            engineAudio.pitch = Mathf.Clamp(1f + speed * 0.05f, 1f, 2.5f);
        }
        else
        {
            engineAudio.Stop();
        }
    }
}
