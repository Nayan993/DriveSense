using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource musicSource;

    void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Stop music immediately on Game Over
        if (CarEngineSound.isGameOver)
        {
            if (musicSource.isPlaying)
                musicSource.Stop();
        }
    }
}
