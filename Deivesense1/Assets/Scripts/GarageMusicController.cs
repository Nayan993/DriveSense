using UnityEngine;

public class GarageMusicController : MonoBehaviour
{
    private AudioSource musicSource; // Garage music source

    void Awake()
    {
        // Get attached AudioSource
        musicSource = GetComponent<AudioSource>();
    }

    public void StopGarageMusic()
    {
        // Stop music if it's playing
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
    }
}
