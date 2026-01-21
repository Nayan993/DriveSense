using UnityEngine;

public class GarageMusicController : MonoBehaviour
{
    private AudioSource musicSource;

    void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void StopGarageMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
    }
}
