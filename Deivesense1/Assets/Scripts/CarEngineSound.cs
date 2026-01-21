using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    public Rigidbody carRb;
    private AudioSource engineAudio;

    public static bool isGameOver = false;

    [Header("Engine Sound Settings")]
    public float minPitch = 0.8f;
    public float maxPitch = 2.2f;
    public float minVolume = 0.2f;
    public float maxVolume = 1.0f;

    void Awake()
    {
        isGameOver = false;
        engineAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isGameOver)
        {
            if (engineAudio.isPlaying)
                engineAudio.Stop();
            return;
        }

        if (carRb == null) return;

        float speed = carRb.linearVelocity.magnitude;

        if (speed > 0.1f)
        {
            if (!engineAudio.isPlaying)
                engineAudio.Play();

            // VOLUME CONTROL (runtime)
            engineAudio.volume = Mathf.Lerp(
                minVolume,
                maxVolume,
                speed / 30f
            );

            // PITCH CONTROL (runtime)
            engineAudio.pitch = Mathf.Lerp(
                minPitch,
                maxPitch,
                speed / 30f
            );
        }
        else
        {
            // Idle sound (very low)
            engineAudio.volume = minVolume;
            engineAudio.pitch = minPitch;
        }
    }
}
